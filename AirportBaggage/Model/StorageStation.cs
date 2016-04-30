﻿using System;
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
    public partial class StorageStation : ModelBase
    {
        #region 字段
        //private ToolTip toolTip;
        private List<Baggage> baggages;
        #endregion

        #region 构造
        public StorageStation() : this(Color.Transparent) { }
        public StorageStation(Color color) : this(1.0, color) { }
        public StorageStation(double opacity) : this(opacity, Color.Transparent) { }
        public StorageStation(double opacity, Color color) : this(opacity, color, 0, 0) { }
        public StorageStation(double opacity, Color color, int x, int y) : base(x, y, opacity)
        {
            InitializeComponent();
            //toolTip = new ToolTip(components);
            //toolTip.UseAnimation = true;
            //toolTip.UseFading = true;
            baggages = new List<Baggage>(3);
            Size = new Size(50, 15);
            Color = BackColor = color;
            WaitingForCollection = false;
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
                return Count == 0;
            }
        }

        public int Count
        {
            get
            {
                return baggages.Count;
            }
        }

        public bool WaitingForCollection { get; set; }
        #endregion

        #region 事件
        public event EventHandler<BaggageEventArgs> CanCollect;
        #endregion

        #region 方法
        public void AddBag(Baggage b)
        {
            if (b == null)
            {
                MessageBox.Show("包裹为空！");
                return;
            }
            if (Count >= 3)
            {
                MessageBox.Show("入库台已满！");
                return;
            }
            b.Location = new Point(Count * 12 + 2, 2);
            baggages.Add(b);
            Controls.Add(b);
        }

        public void InsertBag(Baggage b)
        {
            if (b == null)
            {
                MessageBox.Show("包裹为空！");
                return;
            }
            if (Count >= 3)
            {
                MessageBox.Show("入库台已满！");
                return;
            }
            b.Location = new Point(Count * 12 + 2, 2);
            baggages.Insert(0,b);
            Controls.Add(b);
            Controls.SetChildIndex(b, 0);
        }

        public Baggage ReleaseBag()
        {
            Baggage b = baggages[0];
            baggages.Remove(b);
            Controls.Remove(b);
            WaitingForCollection = false;
            RefreshBaggages();
            return b;
        }

        public override void Next()
        {
            if (!IsEmpty)
            {
                CanCollect(this, new BaggageEventArgs(baggages[0]));
            }
        }
        #endregion

        #region 私有方法
        private void RefreshBaggages()
        {
            for (int i = 0; i < Count; i++)
            {
                baggages[i].Location = new Point(i * 12 + 2, 2);
            }
        }
        #endregion
    }
}
