using AirportBaggage.Model;
using CommonLib.ColorHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBaggage
{
    public class BaggageAdder : INext
    {
        #region 私有字段
        private Dictionary<string, int> baggageCount;
        private Dictionary<string, int> currCount = new Dictionary<string, int>();
        private Dictionary<string, Color> currColor = new Dictionary<string, Color>();
        private StorageStation station;
        private Random rand = new Random();

        #endregion

        #region 构造
        public BaggageAdder(Dictionary<string, int> bc, StorageStation station)
        {
            BaggageCount = new Dictionary<string, int>(bc);
            if (station == null)
            {
                throw new Exception("添加包裹类创建失败！");
            }
            else
            {
                this.station = station;
            }
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
                if (baggageCount != null)
                {
                    foreach (var item in baggageCount)
                    {
                        currCount.Add(item.Key, 0);
                        currColor.Add(item.Key, RandomColor.GetRadomColor());
                    }
                }

            }
        }

        public Dictionary<string, int> CurrCount
        {
            get
            {
                return currCount;
            }
            set
            {
                currCount = value;
            }
        }

        public Dictionary<string, Color> CurrColor
        {
            get
            {
                return currColor;
            }
            set
            {
                currColor = value;
            }
        }
        #endregion

        #region 事件

        #endregion

        #region 方法
        public void Next()
        {
            if (!station.IsFull)
            {
                if (baggageCount.Count != 0)
                {
                    var bags = baggageCount.Where((p, i) => currCount.ElementAt(i).Value <= p.Value);
                    if (bags.Count() > 0)
                    {
                        KeyValuePair<string, int> pair = bags.ElementAt(rand.Next(bags.Count()));
                        Baggage b = new Baggage(pair.Key, currCount[pair.Key], currColor[pair.Key]);
                        station.AddBag(b);
                        currCount[pair.Key] += 1;
                    }
                }
            }
        }
        #endregion
    }
}
