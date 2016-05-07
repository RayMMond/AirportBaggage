using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirportBaggage.Model
{
    public partial class Stacker : ModelBase
    {
        #region 字段
        //private ToolTip toolTip;
        private Baggage bag;
        private Point target;
        private bool readyForCollection;
        private Point orientLocation;
        #endregion

        #region 构造
        public Stacker() : this(Color.Transparent) { }
        public Stacker(Color color) : this(1.0, color) { }
        public Stacker(double opacity) : this(opacity, Color.Transparent) { }
        public Stacker(double opacity, Color color) : this(opacity, color, 0, 0) { }
        public Stacker(double opacity, Color color, int x, int y) : base(x, y, opacity)
        {
            InitializeComponent();
            //toolTip = new ToolTip(components);
            //toolTip.UseAnimation = true;
            //toolTip.UseFading = true;
            Size = new Size(20, 15);
            Color = color;
            Speed = 8;
            Rearranging = false;
            ReadyForCollecting = true;
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

        public bool IsEmpty
        {
            get
            {
                return bag == null;
            }
        }

        public Point TargetPoint
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }

        public int Speed { get; set; }

        public bool ReadyForCollecting
        {
            get
            {
                return readyForCollection;
            }
            set
            {
                readyForCollection = value;
            }
        }

        public LogicLocation TargetLocation { get; set; }

        public Point OriginalLocation
        {
            get
            {
                return orientLocation;
            }
            set
            {
                orientLocation = value;
            }
        }

        public bool Rearranging { get; set; }
        #endregion

        #region 事件
        public event EventHandler<LogicLocationEventArgs> TargetPointReached;

        public event EventHandler OriginalPointReached;
        #endregion

        #region 方法

        public void ChangeFlight(string oldFlight, string newFlight)
        {
            if (bag != null)
            {
                if (bag.Flight == oldFlight)
                {
                    bag.Flight = newFlight;
                }
            }
        }

        public void GrabBag(Baggage b)
        {
            if (b == null)
            {
                MessageBox.Show("堆垛机：" + Name + "抓取包裹为空！");
                return;
            }
            bag = b;
            bag.Location = new Point(5, 2);
            Controls.Add(bag);
        }

        public Baggage ReleaseBag()
        {
            Controls.Remove(bag);
            Baggage b = bag;
            bag = null;
            return b;
        }

        public void ClearTargetPoint()
        {
            TargetPoint = Point.Empty;
        }
        
        public override void Next()
        {
            if (TargetPoint != Point.Empty)
            {
                if (TargetPoint != Location)
                {
                    Location = GetNewLocation(TargetPoint, Location);
                }
                else
                {
                    TargetPointReached(this, new LogicLocationEventArgs(TargetLocation));
                }
            }
            else
            {
                if (orientLocation != Location)
                {
                    Location = GetNewLocation(orientLocation, Location);
                }
                else
                {
                    if (OriginalPointReached != null)
                    {
                        OriginalPointReached(this, null);
                    }
                    readyForCollection = true;
                }
            }
        }
        #endregion

        #region 私有方法
        private Point GetNewLocation(Point target, Point curr)
        {
            Point diff = target - new Size(curr);
            int x, y;
            if (Math.Abs(diff.X) >= Speed)
            {
                if (diff.X >= 0)
                {
                    x = Speed;
                }
                else
                {
                    x = -Speed;
                }
            }
            else if (diff.X == 0)
            {
                x = 0;
            }
            else
            {
                x = diff.X;
            }


            if (Math.Abs(diff.Y) >= Speed)
            {
                if (diff.Y >= 0)
                {
                    y = Speed;
                }
                else
                {
                    y = -Speed;
                }
            }
            else if (diff.Y == 0)
            {
                y = 0;
            }
            else
            {
                y = diff.Y;
            }

            return curr + new Size(x, y);
        }



        #endregion
    }
}
