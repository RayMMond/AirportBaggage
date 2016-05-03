using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBaggage
{
    public class BaggageAdder
    {
        #region 私有字段
        private Dictionary<string, int> baggageCount;
        #endregion

        #region 构造
        public BaggageAdder(Dictionary<string, int> bc)
        {
            BaggageCount = bc;
        }
        #endregion

        #region 属性
        public Dictionary<string, int> BaggageCount
        {
            get
            {
                return baggageCount;
            }
            set
            {
                baggageCount = value;
            }
        }
        #endregion

        #region 事件

        #endregion

        #region 方法

        #endregion
    }
}
