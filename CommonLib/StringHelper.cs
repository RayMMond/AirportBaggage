using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public static class StringHelper
    {
        public static bool IsNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
            return reg1.IsMatch(str);
        }
    }
}
