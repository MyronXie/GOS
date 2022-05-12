using GTA.Math;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GOSColl
{
    public enum ScriptMode
    {
        HOME,
        Capture,
        Record,
        World,
        END,
    }

    public enum SceneType
    {
        Open = 0,
        Mule = 1,
        Boxville = 2,
        Pounder = 3,
        Sandking = 4,
        Blista = 5,
        Blade = 6,
        Oracle = 7,
        Serrano = 8,
    }

    public enum StyleType
    {
        White = 0,
        Black = 1,
        Red = 2,
        Brown = 3,
        Blue = 4,
        Pink = 5,
    }

    public enum WeatherType
    {
        Sunny = 0,
        Clear = 1,
        Cloudy = 2,
        Smogy = 3,
        Foggy = 4,
        Overcast = 5,
        Rainy = 6,
        Stormy = 7,
        Clearing = 8,
        Neutral = 9,
        Snowy = 10,
    }

    public enum TimeType
    {
        Midnight = 0,
        Predawn = 1,
        Dawn = 2,
        Morning = 3,
        Midday = 4,
        Afternoon = 5,
        Sunset = 6,
        Dusk = 7
    }

    public struct Place
    {
        private string name;
        private float x, y, z, h;

        public string Name
        {
            get => name;
            set => name = value;
        }
        public float X
        {
            get => x;
            set => x = value;
        }
        public float Y
        {
            get => y;
            set => y = value;
        }
        public float Z
        {
            get => z;
            set => z = value;
        }
        public float H
        {
            get => h;
            set => h = value;
        }

        public Vector3 Pos
        {
            get => new(x, y, z);
            set { x = value.X; y = value.Y; z = value.Z; }
        }

        public Place(string _n, float _x, float _y, float _z, float _h)
        {
            name = _n;
            x = _x;
            y = _y;
            z = _z;
            h = _h;
        }
        public Place(string _n, Vector3 _p, float _h)
        {
            name = _n;
            x = _p.X;
            y = _p.Y;
            z = _p.Z;
            h = _h;
        }

        public override string ToString()
        {
            return string.Format("{0},{1,8:F2},{2,8:F2},{3,6:F2},{4,7:F2}", name, x, y, z, h);  //"{0},{1:F2},{2:F2},{3:F2},{4:F2}"
        }
    }

    public struct Street
    {
        public string name;
        public int speed;
        public int invl;    // not used
        public int sub;
        public List<Place> pt;
    }

    public struct CaptureConf
    {
        public int[] view;
        public int[] dist;
        public SceneType[] scene;
        public TimeType[] time;
        public WeatherType[] weat;
    }

    public struct Label
    {
        public string name;
        public float[] loc; // place
        public int dist;
        public int view;
        public Object obj;  // scene
        public string time;
        public string weat;
    }

    public struct CaptureInfo
    {
        public int idx, total;
        public List<Place> pList;
        CaptureConf conf;
        public Place place;
        public int dist;
        public int view;
        public SceneType scene;
        public TimeType time;
        public WeatherType weat;

        public CaptureInfo(List<Place> pl, CaptureConf c)
        {
            idx = 0;
            pList = pl;
            conf = c;
            place = pl[0];
            view = conf.view[0];
            dist = conf.dist[0];
            scene = conf.scene[0];
            time = conf.time[0];
            weat = conf.weat[0];
            total = conf.weat.Count() * conf.time.Count() * conf.scene.Count() * conf.dist.Count() * conf.view.Count() * pList.Count();
        }

        public bool Update()
        {
            int p, v, d, s, t, w, r;

            // *** Sequence Matter! ***
            idx++;
            v = idx % conf.view.Count();
            r = idx / conf.view.Count();
            d = r % conf.dist.Count();
            r = r / conf.dist.Count();
            w = r % conf.weat.Count();
            r = r / conf.weat.Count();
            t = r % conf.time.Count();
            r = r / conf.time.Count();
            s = r % conf.scene.Count();
            r = r / conf.scene.Count();
            p = r % pList.Count();
            r = r / pList.Count();
            if (r > 0) return true;

            place = pList[p];
            view = conf.view[v];
            dist = conf.dist[d];
            scene = conf.scene[s];
            time = conf.time[t];
            weat = conf.weat[w];

            return false;
        }

        public override string ToString()
        {
            return place.Name
                    + "V" + (view / 15).ToString("D2")        // viewpoint
                    + "D" + dist.ToString("D2")             // distance
                    + "S" + ((int)scene).ToString("D2")     // scenetype
                    + "L" + ((int)time).ToString("D1")
                          + ((int)weat).ToString("D1");
        }
    }

    [Flags]
    public enum BBoxType
    {
        None = 0,
        Name = 1,
        B3D = 2,
        B2D = 4,
        END = 8,
    }

    public struct BBox2D
    {
        private float x, y, w, h;
        public float X
        {
            get => x;
            set => x = value;
        }
        public float Y
        {
            get => y;
            set => y = value;
        }
        public float Width
        {
            get => w;
            set => w = value;
        }
        public float Height
        {
            get => h;
            set => h = value;
        }
        public PointF Loc
        {
            get => new(x, y);
            set { x = value.X; y = value.Y; }
        }
        public SizeF Size
        {
            get => new(w, h);
            set { w = value.Width; h = value.Height; }
        }
        public PointF Center
        {
            get => new(x + w / 2, y + h / 2);
        }

        public BBox2D(PointF _p, SizeF _s)
        {
            x = _p.X;
            y = _p.Y;
            w = _s.Width;
            h = _s.Height;
        }
        public BBox2D(PointF _p1, PointF _p2)
        {
            x = _p1.X;
            y = _p1.Y;
            w = _p2.X - _p1.X;
            h = _p2.Y - _p1.Y;
        }

        public override string ToString()
        {
            return string.Format("X:{0:F2}, Y:{1:F2}, W:{2:F2}, H:{3:F2}", x, y, w, h);
        }
    }

    public struct Object
    {
        public string model;
        public string color;
        public bool occluded;
        public float[] bbox;
    }

    public enum TaskStatus
    {
        Idle = 0,
        Ready = 1,
        Running = 2,
        Finish = 3,
        Teleport = 5,
    }

    public enum CaptureMode
    {
        Idle = 0,
        Init = 1,
        Update = 2,
        Setting = 3,
        Capture = 4,    // w/ waiting
        Pause = 5,
        Finish = 6,
    }

    [Flags]
    public enum ImageAttr
    {
        Normal = 0,
        Square = 1,
        Resize = 2,
    }

    [Flags]
    public enum ImageType
    {
        None = 0,
        Color_quick = 1,
        Color = 2,
        Depth = 4,
        Stencil = 8,
    }

}

