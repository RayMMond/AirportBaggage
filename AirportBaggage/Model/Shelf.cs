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
    public partial class Shelf : ModelBase
    {
        #region 字段
        private Frame[,] frames = new Frame[8,8];
        #endregion

        #region 构造
        public Shelf() : this(0, 0) { }
        public Shelf(int x, int y) : this(x, y, 1.0) { }
        public Shelf(int x, int y, Color color) : this(x, y, 1.0, color) { }
        public Shelf(int x, int y, double opacity) : this(x, y, opacity, Color.Empty) { }
        public Shelf(int x, int y, double opacity, Color color) : base(x, y, opacity)
        {
            InitializeComponent();
            Size = new Size(329, 329);
            if (color != Color.Empty)
            {
                Color = color;
            }
            BackColor = Color.Transparent;
            BackgroundImageLayout = ImageLayout.None;

            BackgroundImage = DrawHelper.DrawShelf(Size, Color.FromArgb((int)(Opacity * 255), Color));
            //BorderStyle = BorderStyle.FixedSingle;
            Initialize();
        }
        #endregion

        #region 属性
        public Color Color
        {
            get; set;
        }
        #endregion

        #region 事件
        public event EventHandler<BaggageEventArgs> BaggageDequeued;
        #endregion

        #region 方法
        public void PutBaggage(Baggage b, int x, int y)
        {
            frames[x, y].Enqueue(b);
        }

        public void GetBaggage(int x, int y)
        {
            frames[x, y].Dequeue();
        }

        public void ChangeFlight(string oldFlight, string newFlight)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    frames[i, j].ChangeFlight(oldFlight, newFlight);
                }
            }
        }
        #endregion

        #region 私有方法
        private void Initialize()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    frames[i, j] = new Frame(2 + i * 41, 2 + j * 41, Opacity);
                    frames[i, j].Location = new Point(2 + i * 41, 2 + j * 41);
                    frames[i, j].LocationOfShelf = new Point(i + 1, j + 1);
                    frames[i, j].BaggageDequeued += Shelf_BaggageDequeued;
                    //frames[i, j].SetBorderColor(Color.Black);
                    Controls.Add(frames[i, j]);
                }
            }
            
            
        }

        private void Shelf_BaggageDequeued(object sender, BaggageEventArgs e)
        {
            BaggageDequeued(this, new BaggageEventArgs(e.Baggage));
        }

        #endregion
    }

}
