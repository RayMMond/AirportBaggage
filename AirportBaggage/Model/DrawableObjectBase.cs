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
    public partial class ModelBase : UserControl, INext
    {
        #region 私有字段
        private double opacity = 1.0;
        protected Color color = Color.Transparent;
        #endregion

        #region 构造
        public ModelBase() : this(0, 0, 1.0) { }
        public ModelBase(int x, int y, double opacity)
        {
            Location = new Point(x, y);
            Opacity = opacity;
            InitializeComponent();
        }
        #endregion

        #region 属性
        //public List<DrawableObjectBase> Children { get; set; }
        public double Opacity
        {
            get
            {
                return opacity;
            }
            set
            {
                opacity = value;
            }
        }

        public int ZIndex { get; set; }
        #endregion

        #region 方法
        public void MoveLocation(int x, int y)
        {
            Location += new Size(x, y);
        }

        public void MoveLocation(Size size)
        {
            Location += size;
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        // 开启 WS_EX_TRANSPARENT,使控件支持透明
        //        cp.ExStyle |= 0x00000020;
        //        return cp;
        //    }
        //}
        #endregion

        #region 虚拟方法
        public virtual void Next() { }
        #endregion
    }


}
