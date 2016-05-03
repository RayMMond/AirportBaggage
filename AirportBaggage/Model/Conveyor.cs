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
    public partial class Conveyor : ModelBase
    {
        #region 字段
        private Queue<Baggage> baggages = new Queue<Baggage>();
        private Size step;
        private int speed;
        private string flight;
        #endregion

        #region 构造
        public Conveyor() : this(Color.Transparent) { }
        public Conveyor(Color color) : this(1.0, color) { }
        public Conveyor(double opacity) : this(opacity, Color.Transparent) { }
        public Conveyor(double opacity, Color color) : this(opacity, color, 0, 0) { }
        public Conveyor(double opacity, Color color, int x, int y) : base(x, y, opacity)
        {
            InitializeComponent();
            //toolTip = new ToolTip(components);
            //toolTip.UseAnimation = true;
            //toolTip.UseFading = true;
            BorderStyle = BorderStyle.FixedSingle;
            Size = new Size(15, 70);
            Color = color;
            Speed = 4;
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
                return baggages.Count == 0;
            }
        }

        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
                step = new Size(0, speed);
            }
        }

        public string Flight
        {
            get
            {
                return flight;
            }
            set
            {
                flight = value;
                FlightChanged(this, flight);
            }
        }
        #endregion

        #region 事件
        public event EventHandler CollectBaggage;

        public event EventHandler<string> FlightChanged;
        #endregion

        #region 方法
        public void Push(Baggage b)
        {
            if (b == null)
            {
                MessageBox.Show("输送机：" + Name + "抓取包裹为空！");
                return;
            }
            b.Location = new Point(2, 2 + baggages.Count * 12);
            baggages.Enqueue(b);
            Controls.Add(b);
        }

        public Baggage ReleaseBag()
        {
            if (baggages.Count <= 0)
            {
                MessageBox.Show("输送机：" + Name + "为空！无法释放！");
                return null;
            }
            Baggage b = baggages.Dequeue();
            Controls.Remove(b);
            return b;
        }


        public override void Next()
        {
            if (baggages.Count > 0)
            {
                foreach (var bag in baggages)
                {
                    bag.MoveLocation(step);
                }
                if (baggages.ElementAt(0).Location.Y + baggages.ElementAt(0).Size.Height >= Size.Height)
                {
                    ReleaseBag();
                }
            }
            if (!string.IsNullOrEmpty(Flight))
            {
                CollectBaggage(this, null);
            }
        }
        #endregion

        #region 私有方法

        #endregion
    }
}
