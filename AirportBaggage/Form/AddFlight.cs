using CommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirportBaggage
{
    public partial class AddFlight : Form
    {
        public AddFlight() : this("", DateTime.Now, 0) { }

        public AddFlight(string flight, DateTime time, int count)
        {
            InitializeComponent();
            tbFlight.Text = Flight = flight;
            dateTimePicker1.Value = Time = time;
            Count = count;
            tbCount.Text = Count.ToString();
        }

        #region 属性
        public string Flight { get; set; }

        public DateTime Time { get; set; }

        public int Count { get; set; }
        #endregion

        private void btOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbFlight.Text))
            {
                Flight = tbFlight.Text;
                Time = dateTimePicker1.Value;
            }
            else
            {
                MessageBox.Show("航班号不能为空！");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void tbCount_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCount.Text))
            {
                if (StringHelper.IsNumeric(tbCount.Text))
                {
                    int i = int.Parse(tbCount.Text);
                    if (i < 0 || i >= 300)
                    {
                        MessageBox.Show("请输入1~300之内的数字");
                    }
                    else
                    {
                        Count = i;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("请输入数字！");
                }
            }
            tbCount.Text = "";
        }
    }
}
