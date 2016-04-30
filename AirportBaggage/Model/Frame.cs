using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLib.DrawHelper;

namespace AirportBaggage.Model
{
    public partial class Frame : ModelBase
    {
        #region 字段
        private Queue<Baggage> baggages = new Queue<Baggage>(9);
        private ToolTip toolTip;
        #endregion

        #region 构造
        public Frame() : this(0, 0) { }
        public Frame(int x, int y) : this(x, y, 1.0) { }
        public Frame(int x, int y, Color color) : this(x, y, 1.0, color) { }
        public Frame(int x, int y, double opacity) : this(x, y, opacity, Color.Transparent) { }
        public Frame(int x, int y, double opacity, Color color) : base(x, y, opacity)
        {
            Size = new Size(38, 38);
            BackColor = color;
            BackgroundImageLayout = ImageLayout.None;
            InitializeComponent();
            toolTip = new ToolTip(components);
            toolTip.UseAnimation = true;
            toolTip.UseFading = true;
        }
        #endregion

        #region 属性
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                BackColor = color;
            }
        }

        public bool CanEnqueue
        {
            get { return baggages.Count < 9; }
        }

        public bool CanDequeue
        {
            get { return baggages.Count <= 0; }
        }

        public Point LocationOfShelf { get; set; }

        #endregion

        #region 事件
        public event EventHandler<BaggageEventArgs> BaggageDequeued;
        #endregion

        #region 方法

        public void Enqueue(Baggage bag)
        {
            int c = baggages.Count;
            bag.Opacity = Opacity;
            bag.Location = new Point(1 + (c % 3) * 12, 1 + (c / 3) * 12);
            Controls.Add(bag);
            baggages.Enqueue(bag);
        }

        public void ChangeFlight(string oldFlight, string newFlight)
        {
            foreach (var bag in baggages)
            {
                if (bag.Flight == oldFlight)
                {
                    bag.Flight = newFlight;
                }
            }
        }

        public void Dequeue()
        {
            Baggage bag = baggages.Dequeue();
            Controls.Remove(bag);
            RefreshFrame();
            BaggageDequeued(this, new BaggageEventArgs(bag));
        }

        public void SetBorderColor(Color color)
        {
            BackgroundImage = DrawHelper.DrawBorder(Size, Color.FromArgb((int)(Opacity * 255), color));
        }

        public void ClearBorder()
        {
            BackgroundImage = null;
        }
        #endregion

        #region 私有方法
        private void RefreshFrame()
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].Location = new Point(1 + (i % 3) * 12, 1 + (i / 3) * 12);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            string tip = "货架位置：{0}行，{1}列\n";
            tip += "当前存储状态：{2}/{3}";
            tip = string.Format(tip,
                LocationOfShelf.Y.ToString(),
                LocationOfShelf.X.ToString(),
                baggages.Count.ToString(), "9");
            toolTip.Show(tip, this);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            toolTip.Hide(this);
        }
        #endregion
    }
}
