using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirportBaggage
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
#if !DEBUG
            try
            {
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
#if !DEBUG
            }
            catch (Exception e)
            {
                MessageBox.Show("未捕获异常：" + e.Message);
            }
#endif
        }
    }
}
