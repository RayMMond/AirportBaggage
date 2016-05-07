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
    public partial class Configuration : Form
    {

        #region 私有字段

        #endregion

        #region 构造
        public Configuration(Dictionary<string, DateTime> flight, Dictionary<string, int> count)
        {
            if (flight != null)
            {
                Flight = new Dictionary<string, DateTime>(flight);
            }
            else
            {
                Flight = new Dictionary<string, DateTime>();
            }
            if (count != null)
            {
                FlightBaggageCount = new Dictionary<string, int>(count);
            }
            else
            {
                FlightBaggageCount = new Dictionary<string, int>();
            }
            //Flight.Add("123", DateTime.Now);
            InitializeComponent();
        }
        #endregion

        #region 属性
        public Dictionary<string, DateTime> Flight { get; set; }

        public Dictionary<string, int> FlightBaggageCount { get; set; }

        public string DepartureFlight { get; set; }
        #endregion

        #region 方法
        private void Configuration_Load(object sender, EventArgs e)
        {
            foreach (var item in Flight)
            {
                listBox.Items.Add(item.Key);
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                tbTime.Text = Flight[(listBox.SelectedItem as string)].ToString("yyyy-MM-dd HH:mm:ss");
                tbFlightBaggageCount.Text = FlightBaggageCount[(listBox.SelectedItem as string)].ToString(); ;
            }

        }

        private void btAddFlight_Click(object sender, EventArgs e)
        {
            AddFlight add = new AddFlight();
            if (add.ShowDialog() == DialogResult.OK)
            {
                AddFlightInfo(add.Flight, add.Time, add.Count);
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;
            if (index >= 0)
            {
                DelectFlightInfo(listBox.SelectedItem as string, index);
            }
        }

        private void btSetFlight_Click(object sender, EventArgs e)
        {
            DepartureFlight = listBox.SelectedItem as string;

        }

        private void tbFlightBaggageCount_TextChanged(object sender, EventArgs e)
        {
            if (StringHelper.IsNumeric((sender as TextBox).Text))
            {
                if (listBox.SelectedItem != null)
                {
                    FlightBaggageCount[listBox.SelectedItem as string] = int.Parse((sender as TextBox).Text);
                }
            }
            else
            {
                MessageBox.Show("请输入正整数！");
            }
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                string flight = listBox.SelectedItem as string;
                int currIndex = listBox.SelectedIndex;
                AddFlight edit = new AddFlight(flight, Flight[flight], FlightBaggageCount[flight]);
                if (edit.ShowDialog() == DialogResult.OK)
                {
                    DelectFlightInfo(flight, currIndex);
                    AddFlightInfo(edit.Flight, edit.Time, edit.Count);
                }
            }
        }

        private void DelectFlightInfo(string flight, int currIndex)
        {
            Flight.Remove(flight);
            FlightBaggageCount.Remove(flight);
            listBox.Items.Clear();
            tbTime.Text = "";
            foreach (var item in Flight)
            {
                listBox.Items.Add(item.Key);
            }
            if (listBox.Items.Count > 0)
            {
                if (currIndex - 1 >= 0)
                {
                    listBox.SelectedIndex = currIndex - 1;
                }
            }
        }

        private void AddFlightInfo(string flight, DateTime date, int Count)
        {
            Flight.Add(flight, date);
            FlightBaggageCount.Add(flight, Count);
            listBox.Items.Clear();
            foreach (var item in Flight)
            {
                listBox.Items.Add(item.Key);
            }
            listBox.SelectedIndex = listBox.Items.Count - 1;
        }
        #endregion

    }
}
