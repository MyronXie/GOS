using System;
using System.Drawing;               //用于生成图像
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace GOSColl
{

    /// <summary>
    /// Static logger class that allows direct logging of anything to a text file
    /// </summary>
    public static class Logger
    {
        public static void FullLog(object message)
        {
            File.AppendAllText("./GOS-Coll/GOS-Coll.log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ": " + message + Environment.NewLine);
        }
        public static void Log(object message)
        {
            File.AppendAllText("./GOS-Coll/GOS-Coll.log", DateTime.Now.ToString("HH:mm:ss.ff") + ": " + message + Environment.NewLine);
        }
        public static void RecordPlace(object message)
        {
            File.AppendAllText("./GOS-Coll/place_save.txt", message + Environment.NewLine);
        }
    }

    /// <summary>
    /// Private token, DO NOT PUBLISH ONLINE!
    /// <summary>
    public class Pusher
    {
        readonly static string base_url = "http://wxpusher.zjiecode.com/api/send/message/";
        readonly static string appToken = "";
        readonly static string uid = "";

        public static void SendMessage(string str)
        {
            //string url = "http://wxpusher.zjiecode.com/api/send/message/?appToken=AT_xxxxx&uid=UID_xxxxx&content="+ HttpUtility.UrlEncode("测试WxPusher Get");
            var url = base_url + "?appToken=" + appToken + "&uid=" + uid + "&content=" + str;

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            // request.UserAgent = DefaultUserAgent;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            Logger.Log(retString);
            myStreamReader.Close();
            myResponseStream.Close();
        }
    }

    /// <summary>
    /// Refer: https://stackoverflow.com/questions/1163761/capture-screenshot-of-active-window
    /// <summary>
    public class Screen
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        public static Bitmap CaptureAll()
        {
            IntPtr hwnd = FindWindow(null, "Grand Theft Auto V");
            return CaptureWindow(hwnd, false);
            //return CaptureWindow(GetForegroundWindow(), false);
        }

        public static Bitmap CaptureSquare()
        {
            //IntPtr hwnd = FindWindow(null, "Grand Theft Auto V");
            //return CaptureWindow(hwnd, true);
            return CaptureWindow(GetForegroundWindow(), true);
        }

        public static Bitmap CaptureWindow(IntPtr handle, bool square)
        {
            var rect = new Rect();
            GetWindowRect(handle, ref rect);
            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            var result = new Bitmap(bounds.Width, bounds.Height);

            if (square)
            {
                if (bounds.Height == Math.Min(bounds.Width, bounds.Height))
                    bounds = new Rectangle(rect.Left + (bounds.Width - bounds.Height) / 2, rect.Top, bounds.Height, bounds.Height);
                else
                    bounds = new Rectangle(rect.Left, rect.Top + (bounds.Height - bounds.Width) / 2, bounds.Width, bounds.Width);
                result = new Bitmap(bounds.Width, bounds.Height);
            }

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return result;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }

            return destImage;
        }
    }
}
