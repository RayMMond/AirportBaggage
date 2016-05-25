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
        private double acc = 0.2;
        private double currSpeed = 0;
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
                    currSpeed = 0;
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
                    currSpeed = 0;
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
            var a = GetDistance(target, curr);
            var b = GetStopDistance(acc, currSpeed);
            if (a <= b)
            {
                currSpeed -= acc;
            }
            else
            {
                if (currSpeed < Speed)
                {
                    currSpeed += acc;
                }
            }
            currSpeed = currSpeed < 1 ? 1 : currSpeed;
            Point diff = target - new Size(curr);
            int x, y;
            if (Math.Abs(diff.X) >= currSpeed)
            {
                if (diff.X >= 0)
                {
                    x = (int)currSpeed;
                }
                else
                {
                    x = -(int)currSpeed;
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


            if (Math.Abs(diff.Y) >= currSpeed)
            {
                if (diff.Y >= 0)
                {
                    y = (int)currSpeed;
                }
                else
                {
                    y = -(int)currSpeed;
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

        private double GetDistance(Point p, Point p2)
        {
            return Math.Sqrt(Math.Abs(p.X - p2.X) * Math.Abs(p.X - p2.X) + Math.Abs(p.Y - p2.Y) * Math.Abs(p.Y - p2.Y));
        }

        private double GetStopDistance(double a, double v)
        {
            int t = (int)(v / a);
            return v * t - 0.5 * a * t * t;
        }
        #endregion
    }
}
