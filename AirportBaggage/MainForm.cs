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

namespace AirportBaggage
{
    public partial class MainForm : Form
    {
        #region 私有字段
        private System.Windows.Forms.Timer timer;

        private const int FPS = 15;
        private const int Speed = 8;

        private Algorithm algo = new Algorithm(2, new Size(8, 8), 9);


        private List<ModelBase> objects;
        private Shelf shelf1;
        //private Shelf shelf2;
        private StorageStation station;
        private Stacker inputStackerLeft;
        //private Stacker inputStackerRight;
        //private Stacker outputStackerLeft;
        //private Stacker outputStackerRight;

        #endregion

        #region 构造
        public MainForm()
        {
            objects = new List<ModelBase>();

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
            station.AddBag(new Baggage("flight1", 1, 1.0, RandomColor.GetRadomColor()));
            station.AddBag(new Baggage("flight2", 1, 1.0, RandomColor.GetRadomColor()));
            station.AddBag(new Baggage("flight3", 1, 1.0, RandomColor.GetRadomColor()));
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
            inputStackerLeft.Location = new Point(shelf1.Location.X + shelf1.Size.Width - inputStackerLeft.Size.Width, shelf1.Top - inputStackerLeft.Size.Height - 10);
            inputStackerLeft.BorderStyle = BorderStyle.FixedSingle;
            inputStackerLeft.ZIndex = 2;
            inputStackerLeft.Speed = Speed;
            inputStackerLeft.Name = "inputStackerLeft";
            inputStackerLeft.TargetPointReached += InputStackerLeft_TargetPointReached;
            Controls.Add(inputStackerLeft);
            objects.Add(inputStackerLeft);

            SetDisplayOrder();
        }

        private void InputStackerLeft_TargetPointReached(object sender, LogicLocationEventArgs e)
        {
            shelf1.PutBaggage((sender as Stacker).ReleaseBag(), e.Location.Row, e.Location.Column);
            shelf1.ClearFrameHighlight(e.Location.Row, e.Location.Column);
            inputStackerLeft.TargetPoint = Point.Empty;
        }

        private void SetDisplayOrder()
        {
            foreach (var item in objects.OrderBy(x=>x.ZIndex))
            {
                item.BringToFront();
            }
        }

        private void Shelf1_BaggageDequeued(object sender, BaggageEventArgs e)
        {
            
        }

        private void Station_CanCollect(object sender, BaggageEventArgs e)
        {
            int shelfNumber = algo.GetShelfNumber();
            if (shelfNumber == 0)
            {
                if (inputStackerLeft.ReadyForCollecting)
                {
                    LogicLocation l = algo.Push(e.Baggage.Flight);
                    int x, y;
                    x = shelf1.Location.X;
                    y = shelf1.Location.Y;
                    inputStackerLeft.GrabBag(station.ReleaseBag());
                    inputStackerLeft.TargetPoint = new Point(x + (l.Row) * 43 + 20, y + (l.Column) * 43 + 20);
                    inputStackerLeft.TargetLocation = l;
                    shelf1.SetFrameHighlight(l.Row, l.Column, Color.Black);
                }
            }
            else if (shelfNumber == 1)
            {

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
