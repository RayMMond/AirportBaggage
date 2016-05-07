using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLib;
using System.Threading;
using AirportBaggage.Model;
using CommonLib.ColorHelper;
using CommonLib.DrawHelper;
using CommonLib.XMLHelper;
using System.Xml;
using System.IO;

namespace AirportBaggage
{
    public partial class MainForm : Form
    {
        #region 私有字段
        private System.Windows.Forms.Timer timer;

        private const int FPS = 25;
        private const int Speed = 25;

        private Algorithm algo;


        private List<INext> objects;
        private Shelf shelf1;
        //private Shelf shelf2;
        private StorageStation station;
        private Stacker inputStackerLeft;
        //private Stacker inputStackerRight;
        private Stacker outputStackerLeft;
        //private Stacker outputStackerRight;
        private Stacker middleStacker;

        private Conveyor leftConveyor;

        private BaggageAdder baggageAdder;
        private XmlHelper config;

        #endregion

        #region 构造
        public MainForm()
        {
            objects = new List<INext>();

            AllowTransparency = true;
            InitializeComponent();
            Initialize();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000 / FPS;
            timer.Tick += Timer_Tick;
        }

        #endregion

        #region 事件
        private void MainForm_Shown(object sender, EventArgs e)
        {
#if !DEBUG
            try
            {
#endif
            //ShowOpaqueLayer(125);
            //timer.Start();
#if !DEBUG
            }
            catch (Exception ex)
            {
                MessageBox.Show("窗口初始化失败：" + ex.Message);
            }
#endif
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (algo.FlightTime.Count <= 0)
            {
                MessageBox.Show("当前没有航班信息，请先在配置中配置航班信息!");
            }
            else
            {
                if (objects.Contains(baggageAdder))
                {
                    objects.Remove(baggageAdder);
                }
                else
                {
                    objects.Add(baggageAdder);
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
#if !DEBUG
            try
            {
#endif
            SuspendLayout();
            Next();
            ResumeLayout();
#if !DEBUG
            }
            catch (Exception ex)
            {
                MessageBox.Show("刷新界面失败：" +  ex.Message);
            }
#endif
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            algo.FlightTime.Add("CA0001", DateTime.Now.AddDays(10));
            algo.FlightBaggageCount.Add("CA0001", 6400);
            algo.CurrCount.Add("CA0001", 0);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        Baggage b = new Baggage("CA0001", 21, RandomColor.GetRadomColor());
                        shelf1.PutBaggage(b, i, j);
                        algo.Push("CA0001", 0);
                    }
                }
            }
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Baggage b = new Baggage("CA0001", 21, RandomColor.GetRadomColor());
                    shelf1.PutBaggage(b, i, 7);
                    algo.Push("CA0001", 0);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Configuration c = new Configuration(algo.FlightTime, algo.FlightBaggageCount);
            if (c.ShowDialog() == DialogResult.OK)
            {
                algo.FlightTime = c.Flight;
                algo.FlightBaggageCount = c.FlightBaggageCount;
                if (!string.IsNullOrEmpty(c.DepartureFlight))
                {
                    algo.ChangeTime(c.DepartureFlight, DateTime.Now);
                    algo.CheckForDepartNow = true;
                }
                SaveConfig();
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLocationX.Text = e.X.ToString();
            mouseLocationY.Text = e.Y.ToString();
        }
        #endregion

        #region 方法
        private void Next()
        {
            foreach (var item in objects)
            {
                item.Next();
            }
        }

        private void Initialize()
        {
            algo = new Algorithm(2, new Size(8, 8), 9);
            algo.Rearrange += Algo_Rearrange;
            algo.DepartureTimeReached += Algo_DepartureTimeReached;
            objects.Add(algo);

            station = new StorageStation(1.0, Color.Transparent, 480, 10);
            station.BorderStyle = BorderStyle.FixedSingle;
            station.Name = "station";
            station.ZIndex = 0;
            station.CanCollect += Station_CanCollect;
            Controls.Add(station);
            objects.Add(station);

            shelf1 = new Shelf(150, 90, 1, Color.Black);
            shelf1.ZIndex = 0;
            shelf1.Name = "shelf1";
            shelf1.BaggageDequeued += Shelf1_BaggageDequeued;
            Controls.Add(shelf1);
            objects.Add(shelf1);

            inputStackerLeft = new Stacker(1.0, Color.Ivory);
            inputStackerLeft.OriginalLocation = inputStackerLeft.Location = new Point(shelf1.Location.X + shelf1.Size.Width - inputStackerLeft.Size.Width, shelf1.Top - inputStackerLeft.Size.Height - 10);

            inputStackerLeft.BorderStyle = BorderStyle.FixedSingle;
            inputStackerLeft.ZIndex = 2;
            inputStackerLeft.Speed = Speed;
            inputStackerLeft.Name = "inputStackerLeft";
            inputStackerLeft.TargetPointReached += InputStackerLeft_TargetPointReached;
            Controls.Add(inputStackerLeft);
            objects.Add(inputStackerLeft);

            outputStackerLeft = new Stacker(1.0, Color.HotPink);
            outputStackerLeft.OriginalLocation = outputStackerLeft.Location = new Point(shelf1.Location.X + shelf1.Size.Width - outputStackerLeft.Size.Width, shelf1.Top + shelf1.Size.Height + 10);
            outputStackerLeft.BorderStyle = BorderStyle.FixedSingle;
            outputStackerLeft.ZIndex = 2;
            outputStackerLeft.Speed = Speed;
            outputStackerLeft.Name = "outputStackerLeft";
            outputStackerLeft.TargetPointReached += OutputStackerLeft_TargetPointReached;
            outputStackerLeft.OriginalPointReached += OutputStackerLeft_OriginalPointReached;
            Controls.Add(outputStackerLeft);
            objects.Add(outputStackerLeft);

            middleStacker = new Stacker(1.0, Color.Green);
            middleStacker.OriginalLocation = middleStacker.Location = new Point(outputStackerLeft.Location.X + outputStackerLeft.Size.Width + 10, outputStackerLeft.Location.Y);
            middleStacker.BorderStyle = BorderStyle.None;
            middleStacker.ZIndex = 2;
            middleStacker.Speed = Speed;
            middleStacker.Name = "middleStacker";
            middleStacker.TargetPointReached += MiddleStacker_TargetPointReached;
            Controls.Add(middleStacker);
            objects.Add(middleStacker);

            leftConveyor = new Conveyor(1.0, Color.Ivory, outputStackerLeft.Location.X + 3, outputStackerLeft.Location.Y + outputStackerLeft.Size.Height + 10);
            leftConveyor.ZIndex = 2;
            leftConveyor.Name = "leftConveyor";
            leftConveyor.FlightChanged += LeftConveyor_FlightChanged;
            leftConveyor.CollectBaggage += LeftConveyor_CollectBaggage;
            Controls.Add(leftConveyor);
            objects.Add(leftConveyor);

            config = new XmlHelper("config.xml");
            LoadConfig();

            baggageAdder = new BaggageAdder(algo.FlightBaggageCount, station);

            SetDisplayOrder();
        }

        private void SaveConfig()
        {
            try
            {
                if (config.SelectSingleNode("Root/Flight") == null)
                {
                    config.CreateXmlNode("Root", "Flight", "");
                }
                else
                {
                    config.DeleteChild("Root/Flight");
                }

                if (config.SelectSingleNode("Root/Time") == null)
                {
                    config.CreateXmlNode("Root", "Time", "");
                }
                else
                {
                    config.DeleteChild("Root/Time");
                }

                if (config.SelectSingleNode("Root/Count") == null)
                {
                    config.CreateXmlNode("Root", "Count", "");
                }
                else
                {
                    config.DeleteChild("Root/Count");
                }


                foreach (var item in algo.FlightTime)
                {
                    config.CreateXmlNode("Root/Flight", "Value", item.Key);
                    config.CreateXmlNode("Root/Time", "Value", item.Value.Ticks.ToString());
                }
                foreach (var item in algo.FlightBaggageCount)
                {
                    config.CreateXmlNode("Root/Count", "Value", item.Value.ToString());
                }
                config.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }

        }

        private void LoadConfig()
        {
            try
            {
                if (config.CanLoad)
                {
                    XmlNodeList flightNodes = config.SelectNodes("Root/Flight/Value");
                    XmlNodeList timeNodes = config.SelectNodes("Root/Time/Value");
                    XmlNodeList countNodes = config.SelectNodes("Root/Count/Value");
                    if (flightNodes.Count > 0 && timeNodes.Count > 0 && countNodes.Count > 0 && flightNodes.Count == timeNodes.Count && timeNodes.Count == countNodes.Count)
                    {
                        algo.FlightTime.Clear();
                        algo.FlightBaggageCount.Clear();
                        algo.CurrCount.Clear();
                        for (int i = 0; i < flightNodes.Count; i++)
                        {
                            algo.FlightTime.Add(flightNodes[i].InnerText, new DateTime(long.Parse(timeNodes[i].InnerText)));
                            algo.FlightBaggageCount.Add(flightNodes[i].InnerText, int.Parse(countNodes[i].InnerText));
                            algo.CurrCount.Add(flightNodes[i].InnerText, 0);
                        }
                    }
                }
                else
                {
                    algo.FlightTime = new Dictionary<string, DateTime>();
                    algo.FlightBaggageCount = new Dictionary<string, int>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private void SetDisplayOrder()
        {
            foreach (var item in objects.Where(x => x is ModelBase).OrderBy(x => (x as ModelBase).ZIndex))
            {
                (item as ModelBase).BringToFront();
            }
        }

        private void LeftConveyor_FlightChanged(object sender, string e)
        {
            tbDepartureFlight1.Text = e;
        }

        private void Algo_DepartureTimeReached(object sender, string e)
        {
            leftConveyor.Flight = e;
            //rightConveyor.Flight = e;
        }

        private void Algo_Rearrange(object sender, LogicLocationEventArgs e)
        {
            if (e.Location.Shelf == 0)
            {
                if (outputStackerLeft.ReadyForCollecting)
                {
                    int x, y;
                    x = shelf1.Location.X;
                    y = shelf1.Location.Y;
                    algo.Pop(e.Location);
                    outputStackerLeft.TargetPoint = new Point(x + (e.Location.Row) * 43 + 20, y + (e.Location.Column) * 43 + 20);
                    outputStackerLeft.TargetLocation = e.Location;
                    outputStackerLeft.ReadyForCollecting = false;
                    outputStackerLeft.Rearranging = true;
                    shelf1.SetFrameHighlight(e.Location.Row, e.Location.Column, Color.Red);
                }
            }
            else if (e.Location.Shelf == 1)
            {

            }

        }

        private void MiddleStacker_TargetPointReached(object sender, LogicLocationEventArgs e)
        {
            station.InsertBag(middleStacker.ReleaseBag());
            middleStacker.TargetPoint = Point.Empty;
        }

        private void LeftConveyor_CollectBaggage(object sender, EventArgs e)
        {
            string flight = (sender as Conveyor).Flight;
            if (algo.NeedRearrangeLocation.Count == 0)
            {
                if (outputStackerLeft.ReadyForCollecting)
                {
                    if (algo.CanPop(flight, 0))
                    {
                        LogicLocation l = algo.Peek(flight, 0);
                        if (shelf1.OwnBaggage(l.Column, l.Row, flight))
                        {
                            int x, y;
                            x = shelf1.Location.X;
                            y = shelf1.Location.Y;
                            LogicLocation logic = algo.Pop(flight, 0);
                            outputStackerLeft.TargetPoint = new Point(x + (logic.Column) * 43 + 20, y + (logic.Row) * 43 + 20);
                            outputStackerLeft.TargetLocation = logic;
                            outputStackerLeft.ReadyForCollecting = false;
                            outputStackerLeft.Rearranging = false;
                            shelf1.SetFrameHighlight(logic.Column, logic.Row, Color.Red);
                        }
                    }
                }
            }
        }

        private void OutputStackerLeft_OriginalPointReached(object sender, EventArgs e)
        {
            if (!outputStackerLeft.IsEmpty)
            {
                if (outputStackerLeft.Rearranging && middleStacker.ReadyForCollecting)
                {
                    middleStacker.GrabBag(outputStackerLeft.ReleaseBag());
                    middleStacker.TargetPoint = new Point(station.Location.X, station.Location.Y + station.Size.Height + 10);
                    middleStacker.ReadyForCollecting = false;
                    outputStackerLeft.Rearranging = false;
                }
                else
                {
                    leftConveyor.Push(outputStackerLeft.ReleaseBag());
                }
            }
        }

        private void OutputStackerLeft_TargetPointReached(object sender, LogicLocationEventArgs e)
        {
            shelf1.GetBaggage(e.Location.Column, e.Location.Row);
            shelf1.ClearFrameHighlight(e.Location.Column, e.Location.Row);
        }

        private void InputStackerLeft_TargetPointReached(object sender, LogicLocationEventArgs e)
        {
            shelf1.PutBaggage((sender as Stacker).ReleaseBag(), e.Location.Column, e.Location.Row);
            shelf1.ClearFrameHighlight(e.Location.Column, e.Location.Row);
            inputStackerLeft.TargetPoint = Point.Empty;
        }

        private void Shelf1_BaggageDequeued(object sender, BaggageEventArgs e)
        {
            outputStackerLeft.GrabBag(e.Baggage);
            outputStackerLeft.TargetPoint = Point.Empty;
        }

        private void Station_CanCollect(object sender, BaggageEventArgs e)
        {
            if (inputStackerLeft.ReadyForCollecting)
            {
                int x, y;
                x = shelf1.Location.X;
                y = shelf1.Location.Y;
                LogicLocation logic = algo.Push(e.Baggage.Flight, 0);
                inputStackerLeft.GrabBag(station.ReleaseBag());
                inputStackerLeft.ReadyForCollecting = false;
                inputStackerLeft.TargetPoint = new Point(x + (logic.Column) * 43 + 20, y + (logic.Row) * 43 + 20);
                inputStackerLeft.TargetLocation = logic;
                shelf1.SetFrameHighlight(logic.Column, logic.Row, Color.Black);
            }
        }
        #endregion

        //private void ShowOpaqueLayer(int alpha)
        //{
        //    try
        //    {
        //        if (m_OpaqueLayer == null)
        //        {
        //            m_OpaqueLayer = new OpaLayer(alpha);
        //            m_OpaqueLayer.Dock = DockStyle.Fill;
        //        }
        //        m_OpaqueLayer.Enabled = true;
        //        m_OpaqueLayer.Visible = true;
        //        m_OpaqueLayer.BringToFront();
        //    }
        //    catch { }
        //}

        //private void HideOpaqueLayer()
        //{
        //    try
        //    {
        //        if (m_OpaqueLayer != null)
        //        {
        //            m_OpaqueLayer.Visible = false;
        //            m_OpaqueLayer.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }
}
