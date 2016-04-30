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

namespace AirportBaggage
{
    public partial class MainForm : Form
    {
        #region 私有字段
        private System.Windows.Forms.Timer timer;
        private List<ModelBase> objects;
        private const int FPS = 26;
        private Shelf shelf1;
        private Shelf shelf2;
        #endregion

        #region 构造
        public MainForm()
        {
            InitializeComponent();
            Initialize();
            timer = new System.Windows.Forms.Timer();
            objects = new List<ModelBase>();
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
            timer.Start();
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
            Random rand = new Random();
            Baggage b = new Baggage("aaa", 123, 1);
            shelf1.PutBaggage(b, rand.Next(0,7),rand.Next(0,7));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
#if !DEBUG
            try
            {
#endif
            Next();
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
            shelf1.GetBaggage(0, 0);
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
            shelf1 = new Shelf(50, 50, 0.5, Color.Black);
            shelf1.BaggageDequeued += Shelf1_BaggageDequeued;
            Controls.Add(shelf1);
        }

        private void Shelf1_BaggageDequeued(object sender, BaggageEventArgs e)
        {
            
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
