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
        public AddFlight()
        {
            InitializeComponent();
        }

        #region 属性
        public string Flight { get; set; }

        public DateTime Time { get; set; }
        #endregion

        private void btOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                Flight = textBox1.Text;
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
    }
}
