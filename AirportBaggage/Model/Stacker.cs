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
        #endregion

        #region 构造
        public Stacker() : this(Color.Black) { }
        public Stacker(Color color) : this(1.0, color) { }
        public Stacker(double opacity) : this(opacity, Color.Transparent) { }
        public Stacker(double opacity, Color color) : this(opacity, color, 0, 0) { }
        public Stacker(double opacity, Color color, int x, int y) : base(x, y, opacity)
        {
            InitializeComponent();
            //toolTip = new ToolTip(components);
            //toolTip.UseAnimation = true;
            //toolTip.UseFading = true;
            Size = new Size(10, 10);
            Color = BackColor = color;
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
        #endregion

        #region 方法
        public void GrabBag(Baggage b)
        {
            if (b == null)
            {
                MessageBox.Show("堆垛机：" + Name + "抓取包裹为空！");
                return;
            }

            bag = b;
        }
        #endregion

    }
}
