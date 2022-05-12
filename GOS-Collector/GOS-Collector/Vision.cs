using GTA.Math;
using OpenCvSharp;
using System;
using System.Runtime.InteropServices;


[StructLayout(LayoutKind.Sequential)]
public struct rage_matrices
{
    public Matrix world;
    public Matrix worldView;
    public Matrix worldViewProjection;
    public Matrix invView;
}
public class VisionNative
{
    [DllImport("../GTAVisionNative.asi", EntryPoint = "export_get_depth_buffer", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetDepthBuffer(out IntPtr buf);

    [DllImport("../GTAVisionNative.asi", EntryPoint = "export_get_color_buffer", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetColorBuffer(out IntPtr buf);

    [DllImport("../GTAVisionNative.asi", EntryPoint = "export_get_stencil_buffer", CharSet = CharSet.Unicode,
        CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetStencilBuffer(out IntPtr buf);

    [DllImport("../GTAVisionNative.asi", EntryPoint = "export_get_constant_buffer", CharSet = CharSet.Unicode,
         CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetConstants(out rage_matrices buf);

    [DllImport("../GTAVisionNative.asi", EntryPoint = "export_get_last_depth_time",
         CallingConvention = CallingConvention.Cdecl)]
    public static extern long GetLastDepthTime();

    [DllImport("../GTAVisionNative.asi", EntryPoint = "export_get_last_color_time",
         CallingConvention = CallingConvention.Cdecl)]
    public static extern long GetLastColorTime();

    [DllImport("../GTAVisionNative.asi", EntryPoint = "export_get_last_constant_time",
         CallingConvention = CallingConvention.Cdecl)]
    public static extern long GetLastConstantTime();

    [DllImport("../GTAVisionNative.asi", EntryPoint = "export_get_current_time",
         CallingConvention = CallingConvention.Cdecl)]
    public static extern long GetCurrentTime();

    public static byte[] GetDepthBuffer()
    {
        IntPtr buf;
        var sz = GetDepthBuffer(out buf);
        if (sz == -1) return null;

        var result = new byte[sz];
        Marshal.Copy(buf, result, 0, sz);

        return result;
    }

    public static byte[] GetColorBuffer()
    {
        IntPtr buf;
        var sz = GetColorBuffer(out buf);
        if (sz == -1) return null;

        var result = new byte[sz];
        Marshal.Copy(buf, result, 0, sz);

        return result;
    }

    public static byte[] GetStencilBuffer()
    {
        IntPtr buf;
        var sz = GetStencilBuffer(out buf);
        if (sz == -1) return null;

        var result = new byte[sz];
        Marshal.Copy(buf, result, 0, sz);

        return result;
    }

    public static rage_matrices? GetConstants()
    {
        rage_matrices rv;
        rv.invView = Matrix.Identity;
        rv.world = Matrix.Identity;
        rv.worldView = Matrix.Identity;
        rv.worldViewProjection = Matrix.Identity;
        int res = GetConstants(out rv);
        if (res == -1) return null;

        return rv;
    }
}

public class VisionExport
{
    // Need to check resolution of game screen!
    private static int resWidth = 1920;
    private static int resHeight = 1080;

    public static float B2F(byte[] depth, int index)
    {
        byte[] tmp = new byte[4];
        tmp[0] = depth[index + 0];
        tmp[1] = depth[index + 1];
        tmp[2] = depth[index + 2];
        tmp[3] = depth[index + 3];
        return BitConverter.ToSingle(tmp, 0);
    }

    public static void ColorImage(string saveBase, string filename)
    {
        byte[] color = VisionNative.GetColorBuffer();
        //File.WriteAllBytes("csharp_color.raw", color);

        Mat img = Mat.Zeros(new OpenCvSharp.Size(resWidth, resHeight), MatType.CV_8UC3);
        for (int y = 0; y < resHeight; ++y)
        {
            for (int x = 0; x < resWidth; ++x)
            {
                byte r = color[(y * resWidth + x) * 4 + 0];
                byte g = color[(y * resWidth + x) * 4 + 1];
                byte b = color[(y * resWidth + x) * 4 + 2];

                img.At<Vec3b>(y, x)[0] = b;
                img.At<Vec3b>(y, x)[1] = g;
                img.At<Vec3b>(y, x)[2] = r;
            }
        }
        Cv2.ImWrite(saveBase + "color/" + filename + ".jpg", img, new int[] { (int)ImwriteFlags.JpegQuality, 80 });
    }

    public static void DepthImage(string saveBase, string filename)
    {
        byte[] depth = VisionNative.GetDepthBuffer();
        //File.WriteAllBytes("csharp_depth.raw", depth);

        float ratio = 255.0f / 0.03f;
        Mat img_depth = Mat.Zeros(new OpenCvSharp.Size(resWidth, resHeight), MatType.CV_8UC1);
        for (int y = 0; y < resHeight; ++y)
        {
            for (int x = 0; x < resWidth; ++x)
            {
                float value = B2F(depth, (y * resWidth + x) * 4) * ratio;
                byte value2 = value < 255.0f ? (byte)value : (byte)255;
                img_depth.At<byte>(y, x) = value2;
            }
        }
        Cv2.ImWrite(saveBase + "depth/" + filename + ".jpg", img_depth, new int[] { (int)ImwriteFlags.JpegQuality, 80 });
    }

    public static void StencilImage(string saveBase, string filename)
    {
        byte[] stencil = VisionNative.GetStencilBuffer();
        //File.WriteAllBytes("csharp_stencil.raw", stencil);

        Mat img_stencil = Mat.Zeros(new OpenCvSharp.Size(resWidth, resHeight), MatType.CV_8UC3);
        for (int y = 0; y < resHeight; ++y)
        {
            for (int x = 0; x < resWidth; ++x)
            {
                byte pixel = stencil[y * resWidth + x];
                byte r, g, b;
                switch (pixel)
                {
                    case 1:         // Person
                        r = 255;
                        g = 0;
                        b = 0;
                        break;
                    case 2:         // Vehicles
                        r = 142;
                        g = 0;
                        b = 142;
                        break;
                    case 3:         // Vegetations
                        r = 107;
                        g = 142;
                        b = 35;
                        break;
                    case 4:         // Sand/Grass/...
                        r = 152;
                        g = 251;
                        b = 152;
                        break;
                    case 7:         // Sky
                        r = 70;
                        g = 130;
                        b = 180;
                        break;
                    case 8:         // Indoor
                        r = 128;
                        g = 64;
                        b = 64;
                        break;
                    case 16:        // Water
                        r = 112;
                        g = 146;
                        b = 190;
                        break;
                    default:
                        r = 0;
                        g = 0;
                        b = 0;
                        break;
                }

                img_stencil.At<Vec3b>(y, x)[0] = b;
                img_stencil.At<Vec3b>(y, x)[1] = g;
                img_stencil.At<Vec3b>(y, x)[2] = r;
            }
        }
        Cv2.ImWrite(saveBase + "stencil/" + filename + ".jpg", img_stencil, new int[] { (int)ImwriteFlags.JpegQuality, 80 });
    }
}

