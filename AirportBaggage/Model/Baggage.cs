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
    public partial class Baggage : ModelBase
    {
        #region 字段
        private ToolTip toolTip;
        private string flight;
        private int number;
        private Color oldColor = Color.Empty;
        private bool tooltipShowing = false;
        #endregion

        #region 构造
        public Baggage(string flight, int number) : this(flight,number,Color.Black) { }
        public Baggage(string flight, int number, Color color) : this(flight, number, 1.0, color) { }
        public Baggage(string flight, int number, double opacity) : this(flight, number, opacity, Color.Black) { }
        public Baggage(string flight, int number, double opacity, Color color) : this(flight, number, opacity, color, 0, 0) { }
        public Baggage(string flight, int number, double opacity, Color color, int x, int y):base(x, y, opacity)
        {
            this.flight = flight;
            this.number = number;
            InitializeComponent();
            toolTip = new ToolTip(components);
            toolTip.UseAnimation = true;
            toolTip.UseFading = true;
            Size = new Size(10, 10);
            Color = color;
            BorderStyle = BorderStyle.FixedSingle;
            Name = flight + "_" + number.ToString();
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

        public string Flight
        {
            get { return flight; }
            set
            {
                flight = value;
                Name = flight + "_" + number.ToString();
                toolTip.SetToolTip(this, string.Format("航班号：{0}\n序号：{1}", flight, number.ToString()));
            }
        }

        public int Number
        {
            get { return number; }
            set
            {
                number = value;
                Name = flight + "_" + number.ToString();
                toolTip.SetToolTip(this, string.Format("航班号：{0}\n序号：{1}", flight, number.ToString()));
            }
        }

        #endregion

        #region 方法
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (oldColor == Color.Empty)
                oldColor = Color;
                Color = Color.LightPink;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
                Color = oldColor;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (tooltipShowing)
            {
                Color = oldColor;
                toolTip.Hide(this);
                tooltipShowing = false;
            }
            else
            {
                Color = Color.Red;
                toolTip.Show(string.Format("航班号：{0}\n序号：{1}", flight, number.ToString()), this, 10, 10);
                tooltipShowing = true;
            }
        }

        //protected override void OnLostFocus(EventArgs e)
        //{
        //    base.OnLostFocus(e);
        //    Color = oldColor;
        //    toolTip.Hide(this);
        //}

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            if (Focused)
                toolTip.Show(string.Format("航班号：{0}\n序号：{1}", flight, number.ToString()), this, 10, 10);
        }

        #endregion

    }

    public class BaggageEventArgs : EventArgs
    {
        #region 构造
        public BaggageEventArgs(Baggage b) : base()
        {
            Baggage = b;
        }
        #endregion

        #region 属性
        public Baggage Baggage { get; set; }
        #endregion

    }
}
