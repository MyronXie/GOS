using GTA;                      // from SHVDN (v3.4.0)
using GTA.Math;
using GTA.Native;
using GTA.UI;
using Newtonsoft.Json;          // Export Json (v13.0.1)
using System;
using System.Collections.Generic;
using System.Drawing;           // Export image
using System.IO;                // Save log
using System.Linq;
using System.Text;
using System.Windows.Forms;     // Capture button

namespace GOSColl
{
    public class GOSColl : Script
    {
        // Global params
        ScriptMode modeScript = ScriptMode.HOME;
        public bool flagMenu = true;
        public Ped player;
        public Camera camNow = null;

        // Resource Pool
        public List<Entity> objectPool = new();
        public List<Blip> blipPool = new();

        // readonly Random random = new();

        public bool flagShowCoord = false;
        public bool flagContRecord = false;
        public bool flagAutoBlip = true;
        public BBoxType modeBBox = BBoxType.None;

        public List<Place> placeList = new();
        public int plIdxCur = 0;

        // Record
        public TaskStatus modeTask = TaskStatus.Idle;
        Vehicle taskVehicle = null;
        public List<Street> streetList = new();
        public List<Blip> streetBlip = new();
        int stIdx = 0; int tkIdx = 0; int ptIdx = 0;

        // Capture
        public string saveBase = @"E:\\GOS-Dataset\\";
        CaptureMode modeCapPause, modeCapture = CaptureMode.Idle;
        CaptureConf capConf = new();
        CaptureInfo capInfo;
        double timeStamp, timeBegin;
        int frameCount = 0;
        int capCntNow = 0, capCntTotal = 0;
        ImageAttr imgAttr = ImageAttr.Normal;           // ImageConfig.Square | ImageConfig.Resize;
        ImageType imgType = ImageType.Color_quick;

        // World param
        public TimeType worldTime = TimeType.Morning;
        public WeatherType worldWea = WeatherType.Sunny;
        public int modeCam = 0;     // default

        public GOSColl()
        {
            // Function register
            Tick += OnTick;
            KeyDown += OnKeyDown;
            Aborted += OnAborted;
            Interval = 0;

            // Initial setting
            Logger.FullLog("Script Start!");
            Notification.Show("GOS-Collector Start!");
            World.CurrentTimeOfDay = new TimeSpan(9, 0, 0);
            World.Weather = Weather.ExtraSunny;
            Game.TimeScale = 1.0f;

            // Read settings
            var srtSet = ScriptSettings.Load("./scripts/GOS-Collector.ini");
            saveBase = srtSet.GetValue("Capture", "SaveBase", @"E:\\GOS-Dataset\\VXXSXXLXX\\");
            imgAttr = srtSet.GetValue("Image", "Attribute", ImageAttr.Square | ImageAttr.Resize);
            imgType = srtSet.GetValue("Image", "Type", ImageType.Color_quick);
            //srtSet.SetValue("Capture", "SaveBase", saveBase);
            //srtSet.Save();
        }

        ~GOSColl()
        {
            Logger.FullLog("Script Stop!");
        }

        public void OnTick(object sender, EventArgs args)
        {
            try
            {
                UpdateUI(flagMenu);
                UpdatePlayer(flagShowCoord);
                UpdateCamera(modeCapture);
                UpdateBBox(modeBBox);
                UpdateTraverse();   // modeTask
                UpdateRecord(flagContRecord);
                UpdateCapture();    // modeCapture
                UpdateBlips();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message + ex.StackTrace);
                if (modeCapture != CaptureMode.Capture) // avoid mess up capture
                {
                    GTA.UI.Screen.ShowSubtitle(ex.StackTrace, 1000);
                }
            }
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F7)   // Trigger Menu
                {
                    flagMenu = !flagMenu;
                }

                if (e.KeyCode == Keys.F10)  // Show coordinate lively
                {
                    flagShowCoord = !flagShowCoord;
                    Notification.Show("Show Coord: " + flagShowCoord.ToString());
                }

                if (e.KeyCode == Keys.F11)  // Capture pause
                {
                    if (modeScript == ScriptMode.Capture)
                    {
                        if (modeCapture != CaptureMode.Idle && modeCapture != CaptureMode.Pause)
                        {
                            modeCapPause = modeCapture;
                            modeCapture = CaptureMode.Pause;
                        }
                        else if (modeCapture == CaptureMode.Pause)
                        {
                            modeCapture = modeCapPause;
                        }
                    }
                }

                if (e.KeyCode == Keys.F12)  // Capture stop
                {
                    if (modeCapture != CaptureMode.Idle)
                        modeCapture = CaptureMode.Finish;
                }

                // === empty key bind: T/Y/G/H/J ===

                // Showing Bbox of ped/vehicle/props
                if (e.KeyCode == Keys.N)
                {
                    if (++modeBBox == BBoxType.END) modeBBox = BBoxType.None;
                    Notification.Show("ShowBBox: " + modeBBox.ToString());
                }

                // remove props
                if (e.KeyCode == Keys.J)
                {
                    var nearbyProps = World.GetNearbyProps(player.Position, 10, Misc.MODEL_NEED_REMOVED);
                    foreach (var prop in nearbyProps)
                        prop.Delete();

                    var nearbyVehis = World.GetNearbyVehicles(player.Position, 10);
                    foreach (var vehi in nearbyVehis)
                        vehi.Delete();
                }

                if (!flagMenu) return;

                // === Functions below need menu triggered on ===

                if (e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.D0)          // menu back
                {
                    modeScript = ScriptMode.HOME;
                }

                if (modeScript == ScriptMode.HOME)   // menu select
                {
                    int sel = (int)e.KeyCode - (int)Keys.NumPad0;        // numpad key
                    if (sel <= 0 || sel >= (int)ScriptMode.END)
                    {
                        sel = (int)e.KeyCode - (int)Keys.D0;             // full key
                        if (sel <= 0 || sel >= (int)ScriptMode.END)
                            return;
                    }
                    modeScript = (ScriptMode)sel;
                }
                else if (modeScript == ScriptMode.Record)
                {
                    if (e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.D1)
                    {
                        AddPlace();
                    }
                    if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2)
                    {
                        flagContRecord = !flagContRecord;
                        Notification.Show("Auto Record: " + flagContRecord.ToString());
                    }
                    if (e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.D3)
                    {
                        if (modeTask == TaskStatus.Idle)
                        {
                            var status = LoadStreet(streetList);
                            if (status)
                            {
                                Notification.Show("Read street: " + streetList.Count);
                                modeTask = TaskStatus.Ready;
                            }
                        }
                        else
                        {
                            modeTask = TaskStatus.Finish;
                        }
                    }

                    if (e.KeyCode == Keys.NumPad7 || e.KeyCode == Keys.D7)
                    {
                        if (placeList.Count() <= 0)
                        {
                            Notification.Show("No place in cache!");
                            return;
                        }
                        Teleport(placeList[plIdxCur]);
                        GTA.UI.Screen.ShowSubtitle("Teleport: " + placeList[plIdxCur].ToString());

                        if (++plIdxCur >= placeList.Count()) plIdxCur = 0;
                    }
                    if (e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.D8)
                    {
                        var status = LoadPlace(placeList, false);
                        if (status)
                        {
                            var noti = Notification.Show("Read place: " + placeList.Count().ToString());
                            plIdxCur = 0;
                            AddCoordBlipAll(placeList);

                            Logger.Log("Blip num:" + blipPool.Count);
                            for (int i = 0; i < blipPool.Count; i++)
                            {
                                Logger.Log("Blip #" + i.ToString() + ": " + blipPool[i].NumberLabel.ToString() + ", " + blipPool[i].Color.ToString());
                            }
                        }
                    }
                    if (e.KeyCode == Keys.NumPad9 || e.KeyCode == Keys.D9)
                    {
                        placeList.Clear();

                        foreach (var item in blipPool)
                            item.Delete();
                        blipPool.Clear();

                        Notification.Show("Place List cleared!");
                    }
                }
                else if (modeScript == ScriptMode.Capture)
                {
                    if (e.KeyCode == Keys.NumPad9 || e.KeyCode == Keys.D9)  // [9] Read Place
                    {
                        var status = LoadPlace(placeList, true);
                        GTA.UI.Screen.ShowSubtitle("Read place: " + placeList.Count().ToString(), 1000);
                    }
                    if (e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.D8)  // [8] Inspect
                    {
                        if (placeList.Count() <= 0)
                        {
                            Notification.Show("No place in cache!");
                            return;
                        }
                        Teleport(placeList[plIdxCur]);
                        GTA.UI.Screen.ShowSubtitle("Teleport: " + placeList[plIdxCur].ToString());
                        if (++plIdxCur >= placeList.Count()) plIdxCur = 0;
                    }
                    if (e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.D1)  // [1] Start / End
                    {
                        if (modeCapture == CaptureMode.Idle)
                            modeCapture = CaptureMode.Init;
                        else
                            modeCapture = CaptureMode.Finish;
                    }
                    if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2)  // [2] Pause / Resume
                    {
                        if (modeCapture != CaptureMode.Idle && modeCapture != CaptureMode.Pause)
                        {
                            modeCapPause = modeCapture;
                            modeCapture = CaptureMode.Pause;
                            Notification.Show("Capture Pause!");
                        }
                        else if (modeCapture == CaptureMode.Pause)
                        {
                            modeCapture = modeCapPause;
                        }
                    }
                }
                else if (modeScript == ScriptMode.World)
                {
                    if (e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.D1)
                    {
                        if ((int)++worldTime >= Enum.GetNames(typeof(TimeType)).Length) worldTime = 0;
                        ChangeTime(worldTime);
                        GTA.UI.Screen.ShowSubtitle(worldTime.ToString());
                    }
                    if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2)
                    {
                        if ((int)++worldWea >= Enum.GetNames(typeof(WeatherType)).Length) worldWea = 0;
                        ChangeWeather(worldWea);
                        GTA.UI.Screen.ShowSubtitle(worldWea.ToString());
                    }
                    if (e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.D3)
                    {
                        if (player.IsVisible == true) player.IsVisible = false;
                        else player.IsVisible = true;
                    }
                    if (e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.D4)  // changed in ontick
                    {
                        if (++modeCam >= (Misc.CAMERA_LIST.Length / 3)) modeCam = 0;
                        var camParam = new Vector3(Misc.CAMERA_LIST[modeCam, 0], Misc.CAMERA_LIST[modeCam, 1], Misc.CAMERA_LIST[modeCam, 2]);
                        GTA.UI.Screen.ShowSubtitle("Camera: " + Vec3toStr(camParam));
                    }
                    if (e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.D5)  //quick teleport
                    {
                        if (QuickTeleport()) GTA.UI.Screen.ShowSubtitle("Quick Transport!");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message + ex.StackTrace);
                if (modeCapture != CaptureMode.Capture) // avoid mess up capture
                {
                    GTA.UI.Screen.ShowSubtitle(ex.StackTrace, 1000);
                }
            }
        }

        public void OnAborted(object sender, EventArgs args)
        {
            try
            {
                player.Task.ClearAllImmediately();
                player.IsVisible = true;

                if (objectPool.Count > 0)
                {
                    foreach (var item in objectPool)
                        item.Delete();
                }
                if (blipPool.Count > 0)
                {
                    foreach (var item in blipPool)
                        item.Delete();
                }
                if (streetBlip.Count > 0)
                {
                    foreach (var item in streetBlip)
                        item.Delete();
                }

                if (taskVehicle != null) taskVehicle.Delete();

                World.DestroyAllCameras();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message + ex.StackTrace);
                GTA.UI.Screen.ShowSubtitle(ex.StackTrace);
            }
        }

        void UpdateBlips()      // avoid too many blips crash the map
        {
            int freq = 1;
            if (blipPool.Count >= 5000) freq = 20;
            else if (blipPool.Count >= 2000) freq = 10;
            else if (blipPool.Count >= 1000) freq = 5;
            else if (blipPool.Count >= 500) freq = 2;

            for (int i = 0; i < blipPool.Count - 5; i++)    // fade out some blips
            {
                if (i % freq == 0)
                    blipPool[i].DisplayType = BlipDisplayType.MainMapSelectable;
                else
                    blipPool[i].DisplayType = BlipDisplayType.NoDisplay;
            }

            if (blipPool.Count >= 10)   // nearest 10 blips
            {
                for (int i = blipPool.Count - 10; i < blipPool.Count - 5; i++)
                {
                    blipPool[i].DisplayType = BlipDisplayType.MainMapSelectable;
                }
            }
        }

        void UpdateRecord(bool enable)
        {
            if (!enable) return;
            if (placeList.Count <= 0 || Vector3.Distance(player.Position, placeList.Last().Pos) >= 15f) AddPlace();
        }

        void UpdateTraverse()
        {
            if (modeTask == TaskStatus.Idle) return;

            if (modeTask == TaskStatus.Ready)
            {
                if (streetList.Count <= 0)
                {
                    Notification.Show("Street list is empty!");
                    modeTask = TaskStatus.Idle;
                    return;
                }

                flagShowCoord = true;

                if (player.IsInVehicle())
                {
                    taskVehicle = player.CurrentVehicle;
                }
                else
                {
                    taskVehicle = World.CreateVehicle(VehicleHash.Baller, player.Position + player.RightVector * 1, player.Heading);
                    objectPool.Add(taskVehicle);
                    player.SetIntoVehicle(taskVehicle, VehicleSeat.Driver);
                    Wait(1000);
                }

                stIdx = 0;
                tkIdx = 0;

                modeTask = TaskStatus.Teleport;
            }

            if (modeTask == TaskStatus.Teleport)              // Street start
            {
                if (streetList[stIdx].sub == 0)
                    ptIdx = 1;
                else
                    ptIdx = (ptIdx / 100 + 1) * 100 + 1;    // Index start from X01

                if (streetBlip.Count > 0)
                {
                    foreach (var item in streetBlip)
                        item.Delete();
                    streetBlip.Clear();
                }

                foreach (var item in streetList[stIdx].pt)
                {
                    var bl = World.CreateBlip(item.Pos);
                    bl.Alpha = 200;
                    bl.CategoryType = BlipCategoryType.DistanceShown;
                    bl.DisplayType = BlipDisplayType.BothMapNoSelectable;
                    bl.Sprite = BlipSprite.CaptureHouse;
                    bl.Color = (BlipColor)34;       //Pink;
                    bl.NumberLabel = streetBlip.Count % 100;
                    streetBlip.Add(bl);
                }

                if (player.IsInVehicle())
                {
                    taskVehicle.Heading = streetList[stIdx].pt[0].H;
                    taskVehicle.PositionNoOffset = streetList[stIdx].pt[0].Pos;
                }
                else
                {
                    player.Position = streetList[stIdx].pt[0].Pos;
                    taskVehicle.Heading = streetList[stIdx].pt[0].H;
                    taskVehicle.PositionNoOffset = streetList[stIdx].pt[0].Pos;
                }

                tkIdx++;

                player.Task.DriveTo(taskVehicle, streetList[stIdx].pt[tkIdx].Pos, 2f, streetList[stIdx].speed, DrivingStyle.AvoidTrafficExtremely);//AvoidTrafficExtremely //IgnoreLights
                Logger.Log("Street #" + stIdx.ToString() + "/" + streetList.Count.ToString() + ": " + streetList[stIdx].name);

                streetBlip[tkIdx - 1].Alpha = 50;           // fade out previous blip
                streetBlip[tkIdx - 1].ShowRoute = false;
                streetBlip[tkIdx].ShowRoute = true;

                Wait(100);

                modeTask = TaskStatus.Running;
            }

            if (modeTask == TaskStatus.Running)
            {
                var stNow = streetList[stIdx];

                StringBuilder sbTask = new();
                sbTask.Append("Point: " + tkIdx.ToString() + "/" + (stNow.pt.Count - 1).ToString());
                sbTask.Append(", Next: " + Vector3.Distance(player.Position, stNow.pt[tkIdx].Pos).ToString("F0") + " / ");
                sbTask.Append(Vector3.Distance(player.Position, stNow.pt.Last().Pos).ToString("F0") + "\n");
                sbTask.Append("Street: " + (stIdx + 1).ToString() + "/" + streetList.Count.ToString() + ", ");
                sbTask.Append(stNow.name);

                var strNum = Array.IndexOf(Misc.STREET_LIST_GAME, stNow.name);
                var strGame = strNum >= 0 && strNum < Misc.STREET_LIST_GAME.Count() ? Misc.STREET_LIST_GAME[strNum] : stNow.name;
                var strAbbr = strNum >= 0 && strNum < Misc.STREET_LIST_ABBR.Count() ? Misc.STREET_LIST_ABBR[strNum] : "XXX";
                var ptName = strAbbr + ptIdx.ToString("D3");

                if (placeList.Count <= 0 || (Vector3.Distance(player.Position, placeList[placeList.Count - 1].Pos) >= 15))  // stNow.invl
                {
                    AddPlace(ptName);

                    if (strGame != World.GetStreetName(player.Position, out string xname) && strGame != xname)
                        Logger.Log("Record " + ptName + ": " + strGame + ", " + World.GetStreetName(player.Position));
                    else
                        Logger.Log("Record " + ptName);

                    ptIdx++;
                }

                var teTask = new TextElement(sbTask.ToString(), new PointF(1270, 680), 0.4f, Color.FromArgb(255, Color.White))
                {
                    Alignment = Alignment.Right,
                    Shadow = true,
                    Font = GTA.UI.Font.ChaletComprimeCologne,
                };
                teTask.Draw();

                // Consider speed = 0, task stop automaticly

                if (Vector3.Distance(player.Position, stNow.pt[tkIdx].Pos) <= 3)
                {
                    if (++tkIdx >= stNow.pt.Count)
                    {
                        tkIdx = 0;
                        if (++stIdx >= streetList.Count)
                        {
                            modeTask = TaskStatus.Finish;
                            return;
                        }
                        modeTask = TaskStatus.Teleport;
                        flagContRecord = false;
                        return;
                    }

                    streetBlip[tkIdx - 1].Alpha = 100;
                    streetBlip[tkIdx - 1].ShowRoute = false;
                    if (tkIdx - 1 != 0) streetBlip[tkIdx - 1].DisplayType = BlipDisplayType.MainMapSelectable;
                    streetBlip[tkIdx].ShowRoute = true;

                    player.Task.DriveTo(taskVehicle, streetList[stIdx].pt[tkIdx].Pos, 5f, streetList[stIdx].speed, DrivingStyle.IgnoreLights);//player.CurrentVehicle

                    Logger.Log("Point #" + tkIdx.ToString() + "/" + streetList[stIdx].pt.Count.ToString() + ": " + streetList[stIdx].pt[tkIdx].ToString());
                }
            }

            if (modeTask == TaskStatus.Finish)
            {
                foreach (var item in streetBlip)
                    item.Delete();
                streetBlip.Clear();

                taskVehicle.Speed = 0f;

                player.Task.ClearAll();  //ClearAllImmediately
                Notification.Show("Task Finish!");

                modeTask = TaskStatus.Idle;

                flagContRecord = false;
                return;
            }
        }

        void AddPlace()
        {
            var idx = placeList.Count + 1;  // baseIdx

            var strName = World.GetStreetName(player.Position, out string crossingName);
            var strNum = Array.IndexOf(Misc.STREET_LIST_GAME, strName);
            if (strNum != -1)
                strName = Misc.STREET_LIST_ABBR[strNum];
            else strName = "XXX";

            var placeRec = new Place(strName + "_" + (idx % 100).ToString("D2"), player.Position, player.Rotation.Z);
            placeList.Add(placeRec);
            if (flagAutoBlip) AddCoordBlip(placeRec, false);
            Logger.RecordPlace(placeRec.ToString()
                + "," + World.GetStreetName(player.Position)
                + "," + crossingName
                + "," + World.GetZoneDisplayName(player.Position));   // + "," + World.GetZoneLocalizedName(player.Position));

            Logger.Log("Record #" + placeList.Count);
            GTA.UI.Screen.ShowSubtitle(placeRec.ToString(), 1000);
        }

        void AddPlace(string name)
        {
            var placeRec = new Place(name, player.Position, player.Rotation.Z);
            placeList.Add(placeRec);
            if (flagAutoBlip) AddCoordBlip(placeRec, false);

            var strName = World.GetStreetName(player.Position);
            var strNum = Array.IndexOf(Misc.STREET_LIST_GAME, strName);
            if (strNum != -1) strName = Misc.STREET_LIST_ABBR[strNum];

            Logger.RecordPlace(placeRec.ToString()
                + "," + string.Format("{0,5:F1},{1,5:F1}", player.Rotation.X, player.Rotation.Y)
                + "," + strName
                + "," + World.GetZoneDisplayName(player.Position));
            //Logger.Log("Record " + placeRec.Name);
            GTA.UI.Screen.ShowSubtitle(placeRec.ToString(), 1000);
        }


        public void AddCoordBlip(Place place, bool task)
        {
            var bl = World.CreateBlip(place.Pos);
            bl.Alpha = 200;
            bl.CategoryType = BlipCategoryType.DistanceShown;
            bl.DisplayType = BlipDisplayType.BothMapNoSelectable;
            if (task)
            {
                bl.Color = (BlipColor)50; //BlipColor.Pink;
                bl.NumberLabel = blipPool.Count % 100;
                bl.Sprite = BlipSprite.CaptureHouse;
            }
            else
            {
                bl.Color = (BlipColor)((placeList.Count) / 100 + 2); //BlipColor.Green;
                bl.NumberLabel = placeList.Count % 100;
            }
            blipPool.Add(bl);

            GTA.UI.Screen.ShowSubtitle("Add Blip #" + blipPool.Count + ": " + Vec3toStr(place.Pos), 1000);

        }

        public void AddCoordBlipAll(List<Place> pList)
        {
            var times = pList.Count / 500 + 1;

            for (int i = 0; i < pList.Count; i++)
            {
                if (i % times != 0) continue;

                var bl = World.CreateBlip(pList[i].Pos);
                bl.Alpha = 200;
                bl.CategoryType = BlipCategoryType.DistanceShown;
                bl.DisplayType = BlipDisplayType.BothMapNoSelectable;
                bl.Color = (BlipColor)((i / 100 + 2) % 85); // BlipColor.Green;
                bl.DisplayType = BlipDisplayType.MainMapSelectable;
                bl.NumberLabel = i % 100;
                blipPool.Add(bl);
            }
        }

        void UpdateUI(bool menuen)
        {
            // Rending Menu
            if (menuen)
            {
                StringBuilder menuText = new();
                if (modeScript == ScriptMode.HOME)
                {
                    menuText.Append("=== GOS-Collector ===\n");
                    foreach (int item in Enum.GetValues(typeof(ScriptMode)))
                    {
                        if (item == 0 || (ScriptMode)item == ScriptMode.END) continue;
                        menuText.Append("[" + item.ToString() + "] " + ((ScriptMode)item).ToString() + "\n");
                    }
                }
                else
                {
                    menuText.Append("[Mode] " + modeScript.ToString() + "\n");
                    menuText.Append(Misc.MENU_MSG[(int)modeScript]);
                    menuText.Append("[0] Back\n");
                }
                GTA.UI.Screen.ShowHelpTextThisFrame(menuText.ToString());
            }

            StringBuilder msgCorner = new();
            msgCorner.Append(Game.FPS.ToString("F1"));
            //msgCorner.Append(GTA.UI.Screen.Resolution.ToString());
            //msgCorner.Append(Game.FrameCount.ToString());

            var teName = new TextElement(msgCorner.ToString(), new PointF(1265, 10), 0.4f, Color.FromArgb(200, Color.White))
            {
                Alignment = Alignment.Right,
                Shadow = true,
            };
            //teName.Draw();
        }

        void UpdatePlayer(bool showCoord)
        {
            player = Game.Player.Character;
            if (!player.Exists())
            {
                Notification.Show("Player doesn't exist!", false);
                return;
            }
            if (player.IsDead)
            {
                if (player.Model.Hash != (int)PedHash.Michael)
                    Game.Player.ChangeModel(PedHash.Michael);
            }
            if (player.Health != player.MaxHealth)
                player.Health = player.MaxHealth;
            player.ClearVisibleDamage();

            if (showCoord)
            {
                var ceCoord = new ContainerElement(new PointF(640, 18), new SizeF(512, 20), Color.FromArgb(128, Color.Gray))
                {
                    Centered = true,
                };
                ceCoord.Draw();

                StringBuilder sbCoord = new();
                sbCoord.Append("Pos: " + Vec3toStr(player.Position, "F2") + " / ");
                sbCoord.Append("Rot: " + Vec3toStr(player.Rotation, "F2") + " / ");
                sbCoord.Append("Speed: " + player.Speed.ToString("F2") + " / ");
                sbCoord.Append(World.GetStreetName(player.Position, out string crossingName) + ", ");
                sbCoord.Append(crossingName + " / ");
                sbCoord.Append(World.GetZoneLocalizedName(player.Position));

                var teCoord = new TextElement(sbCoord.ToString(), new PointF(640, 8), 0.4f, Color.FromArgb(255, Color.White))
                {
                    Alignment = Alignment.Center,
                    Shadow = true,
                    Font = GTA.UI.Font.ChaletComprimeCologne,
                };
                teCoord.Draw();
            }
        }

        string Vec3toStr(Vector3 v, string fmt = "F0")
        {
            return v.X.ToString(fmt) + ", " + v.Y.ToString(fmt) + ", " + v.Z.ToString(fmt);
        }

        bool LoadPlace(List<Place> pList, bool select)
        {
            pList.Clear();
            try
            {
                StreamReader sr = new("./GOS-Coll/place_load.txt", Encoding.Default);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "" || line.StartsWith("//") || line.StartsWith("#")) continue;  // comment or blank
                    Place place = new("", new Vector3(0, 0, 0), 0f);
                    string[] item = line.Split(',');
                    place.Name = item[0];
                    place.X = float.Parse(item[1]);
                    place.Y = float.Parse(item[2]);
                    place.Z = float.Parse(item[3]);
                    place.H = float.Parse(item[4]);
                    if (select)
                    {
                        var rotx = float.Parse(item[5]);
                        var roty = float.Parse(item[6]);
                        if (Math.Abs(rotx) >= 4.5f || Math.Abs(roty) >= 4.5f) continue; //badlist
                    }
                    pList.Add(place);
                    //if (flagAutoBlip) AddCoordBlip(place, task, false);
                }
                sr.Close();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message + ex.StackTrace);
                Notification.Show("Read \"./GOS-Coll/place_load.txt\" error!");
                return false;
            }
        }

        bool LoadStreet(List<Street> sList)
        {
            sList.Clear();
            try
            {
                StreamReader sr = new("./GOS-Coll/street_load.txt", Encoding.Default);
                Street st = new();
                st.pt = new List<Place>();
                string line, remark = "";
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "" || line.StartsWith("//") || line.StartsWith("#")) continue;  // comment or blank

                    if (line.StartsWith("[") && line.EndsWith("]")) //  street name
                    {
                        if (st.pt.Count > 0)    // save previous street
                        {
                            sList.Add(st);
                        }

                        st = new Street
                        {
                            pt = new List<Place>(),
                            speed = 15,
                            invl = 15,
                            sub = 0,
                        };
                        var str = line.Substring(1, line.IndexOf(']') - 1).Trim();
                        if (!str.Contains(","))
                        {
                            st.name = str;
                            remark = "";
                        }
                        else            // contains remarks
                        {
                            string[] item = str.Split(',');
                            st.name = item[0];
                            remark = item[1];
                        }

                        if (st.name.Contains("Hwy") || st.name.Contains("Fwy") || st.name.Contains("Freeway") || st.name.Contains("Pkwy") || st.name.Contains("Route"))
                        {
                            st.speed = 30;
                        }
                        else if (remark.Contains("Slow"))
                        {
                            st.speed = 10;
                        }
                        //else if (remark == "Short")
                        //{
                        //    st.invl = 15;
                        //}
                        if (remark.Contains("Sub"))
                        {
                            st.sub = 1;
                        }
                    }
                    else        // street message
                    {
                        string[] item = line.Split(',');
                        Place point = new(item[0], float.Parse(item[1]), float.Parse(item[2]), float.Parse(item[3]), float.Parse(item[4]));
                        st.pt.Add(point);
                    }
                }

                if (st.pt.Count > 0)    // save final street
                {
                    sList.Add(st);
                }

                sr.Close();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message + ex.StackTrace);
                Notification.Show("Read \"./GOS-Coll/street_load.txt\" error!");
                return false;
            }
        }

        void UpdateCamera(CaptureMode mode)
        {
            if (mode != CaptureMode.Idle) return;   // Avoid camera error in capture mode

            if (modeCam == 0)
            {
                if (World.RenderingCamera != null)
                {
                    var cam = World.RenderingCamera;
                    World.RenderingCamera = null;
                    cam.IsActive = false;
                    cam.Delete();
                }
            }
            else
            {
                var view = Misc.CAMERA_LIST[modeCam, 0];
                var dist = Misc.CAMERA_LIST[modeCam, 1];
                var high = Misc.CAMERA_LIST[modeCam, 2];
                var cam_pos = CalcCamPos(player.Position, player.Heading, view, dist, high);
                var cam_rot = CalcCamRot(player.Position, player.Heading, view, dist, high);

                if ((camNow == null) || (!camNow.Exists()))
                {
                    camNow = World.CreateCamera(cam_pos, cam_rot, GameplayCamera.FieldOfView);
                }
                else
                {
                    camNow.Position = cam_pos;
                    camNow.Rotation = cam_rot;
                }
                camNow.IsActive = true;
                World.RenderingCamera = camNow;
            }
        }

        Vector3 CalcForwardVec(float h, float multi)
        {
            return new((float)-Math.Sin(h / 180 * Math.PI) * multi, (float)Math.Cos(h / 180 * Math.PI) * multi, 0f);
        }

        public Vector3 CalcCamPos(Vector3 base_pos, float base_head, float view, float dist, float hgt)
        {
            var yaw = base_head - view;    // counter-clockwise
            var for_vec = CalcForwardVec(yaw, dist);
            var up_vec = new Vector3(0f, 0f, hgt);
            var offset = new Vector3(0f, 0f, 0.5f);   // offset on height (pos.z)

            return base_pos - for_vec + up_vec + offset;
        }

        public Vector3 CalcCamRot(Vector3 base_pos, float base_head, float view, float dist, float hgt)
        {
            var yaw = base_head - view;        // counter-clockwise
            var pitch = (float)(Math.Atan(hgt / dist) / Math.PI * 180f);
            return new Vector3(-pitch, 0, yaw);
        }

        void Teleport(Place pl, bool check = false)
        {
            if (player.IsInVehicle())
            {
                Vehicle vehi = player.CurrentVehicle;
                vehi.Rotation = new Vector3(0, 0, pl.H);
                vehi.Position = new Vector3(pl.X, pl.Y, pl.Z);
            }
            else
            {
                player.Rotation = new Vector3(0, 0, pl.H);
                player.Position = new Vector3(pl.X, pl.Y, pl.Z);
            }
        }

        bool QuickTeleport()
        {
            try
            {
                StreamReader sr = new("./GOS-Coll/qt.txt", Encoding.Default);
                Place qt = new();
                string line = sr.ReadLine();
                if (line != null)
                {
                    string[] item = line.Split(',');
                    // name = item[0];
                    qt.X = float.Parse(item[1]);
                    qt.Y = float.Parse(item[2]);
                    qt.Z = float.Parse(item[3]);
                    qt.H = float.Parse(item[4]);
                }
                sr.Close();

                Teleport(qt);
            }
            catch (Exception ex)
            {
                Notification.Show("Read file " + "./GOS-Coll/qt.txt" + " error!");
                Logger.Log(ex.Message + ex.StackTrace);
                return false;
            }

            return true;

        }

        void UpdateBBox(BBoxType mode)
        {
            if (mode == BBoxType.None) return;

            Entity[] ents;
            if (modeCam == 0)
            {
                ents = World.GetNearbyEntities(player.Position, 10f);
            }
            else
            {
                ents = World.GetNearbyEntities(camNow.Position, 10f);
            }

            foreach (var ent in ents)
            {
                //if (ent == player) continue;

                if (player.Weapons.CurrentWeaponObject != null)     // Ignore weapon component
                {
                    if (ent.IsAttachedTo(player.Weapons.CurrentWeaponObject))
                    {
                        if (ent != player && ent != player.Weapons.CurrentWeaponObject)
                        {
                            continue;
                        }
                    }
                }

                DrawBBox(ent, mode);
            }
        }

        BBox2D GetBBox2D(Vector3[] b3d, bool label)
        {
            // judge b3d.len ?= 8
            PointF[] b3dScr = new PointF[8];
            for (int i = 0; i < b3d.Length; i++)
            {
                b3dScr[i] = GTA.UI.Screen.WorldToScreen(b3d[i]);
            }

            PointF b2dMin = b3dScr[0], b2dMax = b3dScr[0];
            for (int i = 1; i < b3dScr.Length; i++)
            {
                b2dMin.X = Math.Min(b2dMin.X, b3dScr[i].X);
                b2dMin.Y = Math.Min(b2dMin.Y, b3dScr[i].Y);
                b2dMax.X = Math.Max(b2dMax.X, b3dScr[i].X);
                b2dMax.Y = Math.Max(b2dMax.Y, b3dScr[i].Y);
            }

            b2dMin.X = (float)Math.Round(b2dMin.X, 2);
            b2dMin.Y = (float)Math.Round(b2dMin.Y, 2);
            b2dMax.X = (float)Math.Round(b2dMax.X, 2);
            b2dMax.Y = (float)Math.Round(b2dMax.Y, 2);

            BBox2D b2d = new(b2dMin, b2dMax);

            var zoomScale = GTA.UI.Screen.Resolution.Width / GTA.UI.Screen.Width;
            BBox2D b2dZoom = new(
                new PointF(b2d.X * zoomScale, b2d.Y * zoomScale),
                new SizeF(b2d.Width * zoomScale, b2d.Height * zoomScale));

            if (!label) return b2d;
            else return b2dZoom;
        }

        BBox2D GetBBox2D(Entity ent, bool label)
        {
            var b3d = GetBBox3D(ent);
            var b2d = GetBBox2D(b3d, label);
            return b2d;
        }

        Vector3[] GetBBox3D(Entity ent)
        {
            Vector3 dimPlus, dimMinus;

            if (ent.EntityType != EntityType.Ped)
            {
                dimPlus = ent.Model.Dimensions.frontTopRight;
                dimMinus = ent.Model.Dimensions.rearBottomLeft;
            }
            else
            {
                Vector3 b3dMin = Function.Call<Vector3>(Hash.GET_PED_BONE_COORDS, ent, Bone.SkelSpineRoot, 0, 0, 0);
                Vector3 b3dMax = Function.Call<Vector3>(Hash.GET_PED_BONE_COORDS, ent, Bone.SkelSpineRoot, 0, 0, 0);

                var entQInv = Quaternion.Invert(ent.Quaternion);

                foreach (Bone item in Enum.GetValues(typeof(Bone))) // need to improved
                {
                    if (Enum.GetName(typeof(Bone), item).Contains("Facial")) continue;
                    if (Enum.GetName(typeof(Bone), item).Contains("Finger")) continue;

                    var coord = Function.Call<Vector3>(Hash.GET_PED_BONE_COORDS, ent, item, 0, 0, 0);
                    var coordInv = entQInv.RotateTransform(coord, ent.Position);

                    b3dMin.X = Math.Min(b3dMin.X, coordInv.X);
                    b3dMin.Y = Math.Min(b3dMin.Y, coordInv.Y);
                    b3dMin.Z = Math.Min(b3dMin.Z, coordInv.Z);
                    b3dMax.X = Math.Max(b3dMax.X, coordInv.X);
                    b3dMax.Y = Math.Max(b3dMax.Y, coordInv.Y);
                    b3dMax.Z = Math.Max(b3dMax.Z, coordInv.Z);
                }

                // with other adjustment
                b3dMax.Z += 0.1f;       // Head

                dimPlus = b3dMax - ent.Position; //ent.Model.Dimensions.frontTopRight;
                dimMinus = b3dMin - ent.Position;
            }

            Vector3[] deltaBase = new Vector3[8] {
                new Vector3(dimPlus.X, dimPlus.Y, dimPlus.Z),    //front-top-right
                new Vector3(dimPlus.X, dimPlus.Y, dimMinus.Z),
                new Vector3(dimPlus.X, dimMinus.Y, dimPlus.Z),
                new Vector3(dimPlus.X, dimMinus.Y, dimMinus.Z),
                new Vector3(dimMinus.X, dimPlus.Y, dimPlus.Z),
                new Vector3(dimMinus.X, dimPlus.Y, dimMinus.Z),
                new Vector3(dimMinus.X, dimMinus.Y, dimPlus.Z),
                new Vector3(dimMinus.X, dimMinus.Y, dimMinus.Z) //rear-bottom-left
            };

            Vector3[] deltaRot = new Vector3[8];
            for (int i = 0; i < deltaRot.Length; i++)
                deltaRot[i] = ent.Quaternion.RotateTransform(deltaBase[i]);

            Vector3[] b3d = new Vector3[8];
            for (int i = 0; i < b3d.Length; i++)
            {
                b3d[i] = ent.Position + deltaRot[i];
            }

            return b3d;
        }

        void DrawBBox(Entity ent, BBoxType status)
        {
            var entPosScr = GTA.UI.Screen.WorldToScreen(ent.Position);

            Color entColor = Color.FromArgb(255, Color.White);
            if (ent.EntityType == EntityType.Ped) entColor = Color.FromArgb(255, Color.LightBlue);
            else if (ent.EntityType == EntityType.Vehicle) entColor = Color.FromArgb(255, Color.OrangeRed);
            else if (ent.EntityType == EntityType.Prop) entColor = Color.FromArgb(255, Color.LightGreen);

            StringBuilder strAttr = new("");
            if (status.HasFlag(BBoxType.Name) && entPosScr != Point.Empty)
            {
                var teName = new TextElement(SearchName(ent), entPosScr + new SizeF(0, -12.5f), 0.25f, entColor)
                {
                    Outline = true,
                };
                teName.Draw();
            }

            var b3d = GetBBox3D(ent);
            var b2d = GetBBox2D(b3d, false);
            var b2dZoom = GetBBox2D(b3d, true);

            if (ent.IsOnScreen) // && ent.IsRendered
            {
                if (status.HasFlag(BBoxType.B3D))
                {
                    DrawCube(b3d, entColor);
                    strAttr.Append("Pos: " + Vec3toStr(ent.Position) + "\n");
                    strAttr.Append("Rot: " + Vec3toStr(ent.Rotation) + "\n");
                }
                if (status.HasFlag(BBoxType.B2D) && b2d.Loc != PointF.Empty)
                {
                    DrawRect(b2d, entColor);
                    strAttr.Append(b2dZoom.ToString() + "\n");
                }
            }

            var teAttr = new TextElement(strAttr.ToString(), entPosScr + new SizeF(0, 0f), 0.2f, entColor);
            teAttr.Draw();
        }

        void DrawRect(BBox2D bbox, Color c)
        {
            var hori = new SizeF(bbox.Width, 1.5f);//0.75
            var veti = new SizeF(1.5f, bbox.Height);

            var ce = new ContainerElement[4];
            ce[0] = new ContainerElement(bbox.Loc, hori, c);
            ce[1] = new ContainerElement(bbox.Loc, veti, c);
            ce[2] = new ContainerElement(new PointF(bbox.X, bbox.Y + bbox.Height), hori, c);
            ce[3] = new ContainerElement(new PointF(bbox.X + bbox.Width, bbox.Y), veti, c);

            foreach (var item in ce)
                item.Draw();
        }

        void DrawCube(Vector3[] cube, Color c)
        {
            World.DrawLine(cube[0], cube[1], c);
            World.DrawLine(cube[0], cube[2], c);
            World.DrawLine(cube[1], cube[3], c);
            World.DrawLine(cube[2], cube[3], c);
            World.DrawLine(cube[0], cube[4], c);
            World.DrawLine(cube[1], cube[5], c);
            World.DrawLine(cube[2], cube[6], c);
            World.DrawLine(cube[3], cube[7], c);
            World.DrawLine(cube[4], cube[5], c);
            World.DrawLine(cube[4], cube[6], c);
            World.DrawLine(cube[5], cube[7], c);
            World.DrawLine(cube[6], cube[7], c);
        }

        string SearchName(Entity ent)
        {
            string hash_str = ent.Model.ToString();
            uint hash_id = Convert.ToUInt32(ent.Model.ToString(), 16);

            if (Enum.IsDefined(typeof(VehicleHash), hash_id))
            {
                var prop = (VehicleHash)Enum.ToObject(typeof(VehicleHash), hash_id);
                return prop.ToString();
            }
            if (Enum.IsDefined(typeof(PedHash), hash_id))
            {
                var prop = (PedHash)Enum.ToObject(typeof(PedHash), hash_id);
                return prop.ToString();
            }
            if (Enum.IsDefined(typeof(PropHash), hash_id))
            {
                var prop = (PropHash)Enum.ToObject(typeof(PropHash), hash_id);
                return prop.ToString();
            }
            if (ent == player.Weapons.CurrentWeaponObject)
            {
                return player.Weapons.Current.LocalizedName;
            }

            if (Enum.IsDefined(typeof(EntityHash), (int)hash_id))
            {
                var prop = (EntityHash)Enum.ToObject(typeof(EntityHash), (int)hash_id);
                return prop.ToString();
            }
            return hash_str;
        }

        #region Capture Function
        void UpdateCapture()
        {

            if (modeCapture != CaptureMode.Idle)
            {
                string paused = "";
                if (modeCapture == CaptureMode.Pause) paused = "[PAUSED] ";

                var teCap = new TextElement(paused + capCntNow.ToString() + " " + capCntTotal.ToString() + "\n" + capInfo.ToString(),
                    new PointF(1270, 677), 0.4f, Color.FromArgb(200, Color.White))
                {
                    Alignment = Alignment.Right,
                    Shadow = true,
                    Font = GTA.UI.Font.Monospace,
                };
                //teCap.Draw();
            }

            if (modeCapture == CaptureMode.Idle || modeCapture == CaptureMode.Pause)
            {
                return;
            }

            if (modeCapture == CaptureMode.Init)
            {
                if (placeList.Count() <= 0)
                {
                    Notification.Show("Place list is empty!");
                    modeCapture = CaptureMode.Idle;
                    return;
                }

                Directory.CreateDirectory(saveBase);
                Directory.CreateDirectory(Path.Combine(saveBase, "color"));
                Directory.CreateDirectory(Path.Combine(saveBase, "depth"));
                Directory.CreateDirectory(Path.Combine(saveBase, "stencil"));
                Directory.CreateDirectory(Path.Combine(saveBase, "label"));

                var notiConf = LoadConfig(ref capConf);
                capInfo = new CaptureInfo(placeList, capConf);
                capCntTotal = capInfo.total;

                World.IsClockPaused = true;
                Game.TimeScale = 0.1f;
                player.IsVisible = false;

                flagMenu = false;
                taskVehicle = null;

                Wait(1750);
                Notification.Hide(notiConf);
                Wait(250);

                Logger.Log("Capture Start!");
                timeBegin = GetTimeStamp();

                //// avoid_list
                //if (CheckBadList(capInfo))
                //{
                //    modeCapture = CaptureMode.Update;
                //    return;
                //}

                modeCapture = CaptureMode.Setting;
            }

            if (modeCapture == CaptureMode.Update)
            {
                do
                {
                    var overflow = capInfo.Update();
                    if (overflow)
                    {
                        modeCapture = CaptureMode.Finish;
                        return;
                    }
                    if (CheckBadList(capInfo)) capCntTotal--;
                }
                while (CheckBadList(capInfo));

                modeCapture = CaptureMode.Setting;
            }

            if (modeCapture == CaptureMode.Setting)
            {
                if (Vector3.Distance(player.Position, capInfo.place.Pos) >= 15f)
                    Logger.Log(capInfo.place.Name.Substring(0, 6));

                int delay = 15;   // 15: fast speed 1 frame, 70: at least 2 frame to render name
                // need time to render world scene
                delay += Math.Min((int)Vector3.Distance(player.Position, capInfo.place.Pos) / 100 * 250, 4000);
                timeStamp = GetTimeStamp() + delay;

                ChangePlace(capInfo.place);
                ChangeTime(capInfo.time);
                ChangeWeather(capInfo.weat);
                ChangeCamera(capInfo.place, capInfo.view, capInfo.dist);
                ChangeObject(capInfo.place, capInfo.scene);

                modeCapture = CaptureMode.Capture;
            }

            if (modeCapture == CaptureMode.Capture)
            {
                if (GetTimeStamp() < timeStamp) return;

                if (Game.FrameCount - frameCount < 3) return;   //3: minimum, 4: keep timestamp available
                frameCount = Game.FrameCount;

                if (imgType.HasFlag(ImageType.Color_quick))
                    SaveImage(Path.Combine(saveBase, "color", capInfo.ToString() + ".jpg"), imgAttr);

                if (imgType.HasFlag(ImageType.Color))
                    VisionExport.ColorImage(saveBase, capInfo.ToString());
                if (imgType.HasFlag(ImageType.Depth))
                    VisionExport.DepthImage(saveBase, capInfo.ToString());
                if (imgType.HasFlag(ImageType.Stencil))
                    VisionExport.StencilImage(saveBase, capInfo.ToString());

                var imgLbl = CapInfotoLabel(capInfo, imgAttr);
                SaveLabel(Path.Combine(saveBase, "label", capInfo.ToString() + ".json"), imgLbl);

                capCntNow++;

                modeCapture = CaptureMode.Update;
            }

            if (modeCapture == CaptureMode.Finish)
            {
                var finMsg = new StringBuilder("Capture Finish!" + "\n");
                finMsg.Append("Number: " + capCntNow.ToString() + "/" + capCntTotal.ToString() + "\n");
                finMsg.Append("Cost Time: " + ((GetTimeStamp() - timeBegin) / 1000).ToString("F1") + "s");
                Notification.Show(finMsg.ToString());
                Logger.Log(finMsg.ToString());
                //Pusher.SendMessage(finMsg.ToString());

                player.IsVisible = true;
                Game.TimeScale = 1.0f;

                if (camNow != null) camNow.Delete();
                if (taskVehicle != null) taskVehicle.Delete();

                placeList.Clear();
                foreach (var item in blipPool)
                    item.Delete();

                capCntNow = 0;
                capCntTotal = 0;

                modeCapture = CaptureMode.Idle;
            }
        }

        void SaveImage(string filename, ImageAttr conf)
        {
            try
            {
                Bitmap image;
                if (conf.HasFlag(ImageAttr.Square)) image = Screen.CaptureSquare();
                else image = Screen.CaptureAll();

                if (conf.HasFlag(ImageAttr.Resize)) image = Screen.ResizeImage(image, 256, 256);

                image.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message + ex.StackTrace);
                //Notification.Show("Save image error!");
                GTA.UI.Screen.ShowSubtitle(ex.Message, 1000);
            }
        }

        Label CapInfotoLabel(CaptureInfo cap, ImageAttr conf)
        {

            var lbl = new Label
            {
                name = cap.ToString(),
                loc = new float[4] { cap.place.X, cap.place.Y, cap.place.Z, cap.place.H },  // place
                dist = cap.dist,
                view = cap.view,
                time = cap.time.ToString(),
                weat = cap.weat.ToString(),
            };

            if (cap.scene != SceneType.Open)
            {
                var bbox = GetBBox2D(taskVehicle, true);
                lbl.obj.model = SearchName(taskVehicle);
                lbl.obj.color = taskVehicle.Mods.PrimaryColor.ToString();
                lbl.obj.occluded = taskVehicle.IsOccluded;
                lbl.obj.bbox = new float[4] { bbox.X, bbox.Y, bbox.Width, bbox.Height };

                // modify bbox size in different resolutions
                if (conf.HasFlag(ImageAttr.Square))
                {
                    var scrWidth = GTA.UI.Screen.Resolution.Width;
                    var scrHeight = GTA.UI.Screen.Resolution.Height;
                    var diff = (scrWidth - scrHeight) / 2;
                    lbl.obj.bbox[0] = Math.Max(bbox.X - diff, 0);
                    lbl.obj.bbox[2] = Math.Min(bbox.X + bbox.Width - diff, scrHeight) - lbl.obj.bbox[0];  // w=x1-x0
                }
                if (conf.HasFlag(ImageAttr.Resize))
                {
                    float scaleX, scaleY;
                    if (conf.HasFlag(ImageAttr.Square)) scaleX = GTA.UI.Screen.Resolution.Height * 1.0f / 256;
                    else scaleX = GTA.UI.Screen.Resolution.Width * 1.0f / 256;
                    scaleY = GTA.UI.Screen.Resolution.Height * 1.0f / 256;
                    lbl.obj.bbox[0] = lbl.obj.bbox[0] / scaleX;
                    lbl.obj.bbox[1] = lbl.obj.bbox[1] / scaleY;
                    lbl.obj.bbox[2] = lbl.obj.bbox[2] / scaleX;
                    lbl.obj.bbox[3] = lbl.obj.bbox[3] / scaleY;
                }
                lbl.obj.bbox[0] = (float)Math.Floor(lbl.obj.bbox[0] * 100) / 100;
                lbl.obj.bbox[1] = (float)Math.Floor(lbl.obj.bbox[1] * 100) / 100;
                lbl.obj.bbox[2] = (float)Math.Floor(lbl.obj.bbox[2] * 100) / 100;
                lbl.obj.bbox[3] = (float)Math.Floor(lbl.obj.bbox[3] * 100) / 100;
            }

            return lbl;
        }

        void SaveLabel(string filename, Label lbl)
        {
            try
            {
                var label = JsonConvert.SerializeObject(lbl);
                File.WriteAllText(filename, label);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message + ex.StackTrace);
                Notification.Show("Save label error!");
            }
        }

        bool CheckBadList(CaptureInfo cap)
        {
            if (cap.dist == 0 && cap.scene != SceneType.Open)
            {
                return true;
            }
            //string name = cap.ToString();   // AUP015V04D50S10L30
            //if (badlist.Count > 0)
            //{
            //    var name_sub = name.Substring(0, 12);
            //    foreach (var item in badlist)
            //    {
            //        var item_sub = item.Substring(0, 12);
            //        if (string.Equals(name_sub, item_sub)) return true;
            //        //if (name_sub.Contains(item)) return true;
            //    }
            //}
            return false;
        }

        int LoadConfig(ref CaptureConf config)
        {
            string configFile = "./GOS-Coll/config_capture.json";
            try
            {
                var json_input = File.ReadAllText(configFile);
                config = JsonConvert.DeserializeObject<CaptureConf>(json_input);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.StackTrace);
                Notification.Show("Load label error! Using default config");
                GTA.UI.Screen.ShowSubtitle(ex.StackTrace);

                // Default config
                config.weat = new WeatherType[] { WeatherType.Sunny };
                config.time = new TimeType[] { TimeType.Midday };
                config.dist = new int[] { 0, 5, 10, 20, 30, 50 };
                config.view = new int[] { 30, 60, 90, 120, 150 };
                config.scene = new SceneType[] { SceneType.Open };
            }

            for (int i = 0; i < config.view.Count(); i++)
            {
                if (config.view[i] < 0) config.view[i] += 360;
                if (config.view[i] >= 360) config.view[i] -= 360;
            }

            StringBuilder configMsg = new("[Capture config]\n");
            configMsg.Append("Place:" + placeList.Count().ToString() + "\n");
            configMsg.Append("Weat:" + string.Join(",", capConf.weat) + "\n");
            configMsg.Append("Time:" + string.Join(",", capConf.time) + "\n");
            configMsg.Append("Dist:" + string.Join(",", capConf.dist) + "\n");
            configMsg.Append("View:" + string.Join(",", capConf.view) + "\n");
            configMsg.Append("Scene:" + string.Join(",", capConf.scene));
            Logger.Log(configMsg);
            var noti = Notification.Show(configMsg.ToString());

            return noti;
        }

        void ChangeTime(TimeType time)
        {
            switch (time)
            {
                case TimeType.Midnight: World.CurrentTimeOfDay = new TimeSpan(0, 0, 0); break;
                case TimeType.Predawn: World.CurrentTimeOfDay = new TimeSpan(5, 0, 0); break;
                case TimeType.Dawn: World.CurrentTimeOfDay = new TimeSpan(6, 0, 0); break;
                case TimeType.Morning: World.CurrentTimeOfDay = new TimeSpan(8, 0, 0); break;
                case TimeType.Midday: World.CurrentTimeOfDay = new TimeSpan(12, 0, 0); break;
                case TimeType.Afternoon: World.CurrentTimeOfDay = new TimeSpan(16, 0, 0); break;
                case TimeType.Sunset: World.CurrentTimeOfDay = new TimeSpan(18, 30, 0); break;
                case TimeType.Dusk: World.CurrentTimeOfDay = new TimeSpan(21, 0, 0); break;
                default: break;
            }
        }

        void ChangeWeather(WeatherType weat)
        {
            switch (weat)
            {
                case WeatherType.Sunny: World.Weather = Weather.ExtraSunny; break;
                case WeatherType.Clear: World.Weather = Weather.Clear; break;
                case WeatherType.Cloudy: World.Weather = Weather.Clouds; break;
                case WeatherType.Smogy: World.Weather = Weather.Smog; break;
                case WeatherType.Foggy: World.Weather = Weather.Foggy; break;
                case WeatherType.Overcast: World.Weather = Weather.Overcast; break;
                case WeatherType.Rainy: World.Weather = Weather.Raining; break;
                case WeatherType.Stormy: World.Weather = Weather.ThunderStorm; break;
                case WeatherType.Clearing: World.Weather = Weather.Clearing; break;
                case WeatherType.Neutral: World.Weather = Weather.Neutral; break;
                case WeatherType.Snowy: World.Weather = Weather.Snowing; break;
                default: break;
            }
        }

        void ChangeCamera(Place place, float view, float dist)
        {
            Vector3 camPos, camRot;

            var height = 0;
            camPos = CalcCamPos(place.Pos, place.H, view, dist, height);
            camRot = CalcCamRot(place.Pos, place.H, view, dist, height);

            if ((camNow == null) || (!camNow.Exists()))
            {
                camNow = World.CreateCamera(camPos, camRot, GameplayCamera.FieldOfView);
            }
            else
            {
                camNow.Rotation = camRot;
                camNow.Position = camPos;
            }

            camNow.IsActive = true;
            World.RenderingCamera = camNow;

            var range = 10;
            if (dist == 0) return;

            var nearbyProps = World.GetNearbyProps(camPos, range, Misc.MODEL_NEED_REMOVED);//, params GTA.Model[] models
            foreach (var prop in nearbyProps)
            {
                prop.Delete();
            }

            var nearbyVehis = World.GetNearbyVehicles(camPos, range);
            foreach (var vehi in nearbyVehis)
            {
                vehi.Delete();
            }
        }

        int ChangePlace(Place cur)
        {
            int delay = 0;

            var ents = World.GetNearbyEntities(cur.Pos, 5f);
            foreach (var ent in ents)
            {
                if (ent == player) continue;
                ent.IsVisible = false;
            }

            player.Rotation = new Vector3(0, 0, cur.H);
            player.Position = cur.Pos + CalcForwardVec(cur.H, -3);       // place player to other places

            return delay;
        }

        void ChangeObject(Place place, SceneType cur, StyleType sty = StyleType.White)
        {
            if (taskVehicle != null)
            {
                taskVehicle.Delete();
                taskVehicle = null;
            }

            if (cur != SceneType.Open)
            {
                VehicleHash vehiHash;
                VehicleColor vehiColor;
                var vehiPlace = place;

                switch (cur)
                {
                    case SceneType.Mule: vehiHash = VehicleHash.Mule3; vehiColor = VehicleColor.MetallicWhite; break;
                    case SceneType.Boxville: vehiHash = VehicleHash.Boxville2; vehiColor = VehicleColor.MetallicWhite; break;
                    case SceneType.Pounder: vehiHash = VehicleHash.Pounder2; vehiColor = VehicleColor.MetallicWhite; break;
                    case SceneType.Blade: vehiHash = VehicleHash.Blade; vehiColor = VehicleColor.MetallicBistonBrown; break;
                    case SceneType.Blista: vehiHash = VehicleHash.Blista; vehiColor = VehicleColor.MatteWhite; break;
                    case SceneType.Sandking: vehiHash = VehicleHash.Sandking; vehiColor = VehicleColor.MatteDarkRed; break;
                    case SceneType.Oracle: vehiHash = VehicleHash.Oracle2; vehiColor = VehicleColor.MatteWhite; break;
                    case SceneType.Serrano: vehiHash = VehicleHash.Serrano; vehiColor = VehicleColor.MatteWhite; break;
                    default: vehiHash = VehicleHash.Mule3; vehiColor = VehicleColor.MetallicWhite; break;
                }

                if (cur == SceneType.Boxville)
                {
                    vehiPlace.Pos += CalcForwardVec(vehiPlace.H + 90, 0.55f);
                    vehiPlace.Z -= 0.5f;
                }

                switch (sty)
                {
                    case StyleType.White: vehiColor = VehicleColor.MetallicWhite; break;
                    case StyleType.Black: vehiColor = VehicleColor.MatteBlack; break;
                    case StyleType.Red: vehiColor = VehicleColor.MatteDarkRed; break;
                    case StyleType.Brown: vehiColor = VehicleColor.MetallicBistonBrown; break;
                    case StyleType.Blue: vehiColor = VehicleColor.Blue; break;
                    case StyleType.Pink: vehiColor = VehicleColor.HotPink; break;
                }

                vehiPlace.Z -= 0.5f;
                taskVehicle = World.CreateVehicle(vehiHash, vehiPlace.Pos, vehiPlace.H);
                taskVehicle.DirtLevel = 0f;                     // clean car
                taskVehicle.Mods.PrimaryColor = vehiColor;
                taskVehicle.Mods.SecondaryColor = vehiColor;
                taskVehicle.PlaceOnGround();
                Wait(1000);
            }

            player.IsVisible = false;

            Vehicle[] nearbyvehi = World.GetNearbyVehicles(player, 200f);
            foreach (Vehicle car in nearbyvehi)
            {
                if (car != taskVehicle)
                {
                    car.Delete();
                }
            }

            //if (cur != SceneType.open)  camCurr.PointAt(place.pos);
        }

        public static double GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return ts.TotalMilliseconds;
        }
        #endregion
    }
}
