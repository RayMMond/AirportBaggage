using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirportBaggage
{
    public class Algorithm
    {
        #region 私有字段
        List<FrameModel> frames;
        #endregion

        #region 构造
        public Algorithm(int shelfCount, Size size, int frameCount)
        {
            ShelfCount = shelfCount;
            ShelfSize = size;
            FrameCount = frameCount;
            frames = new List<FrameModel>();
            for (int i = 0; i < ShelfCount; i++)
            {
                for (int j = 0; j < ShelfSize.Height; j++)
                {
                    for (int k = 0; k < ShelfSize.Width; k++)
                    {
                        frames.Add(new FrameModel(i,j,k)
                        {
                            Times = new Queue<string>(FrameCount)
                        });
                    }
                }
            }
        }
        #endregion

        #region 属性
        public int ShelfCount { get; private set; }

        public Size ShelfSize { get; private set; }

        public int FrameCount { get; private set; }

        public Dictionary<string, DateTime> FlightTime { get; set; }

        public LogicLocation NeedRearrange { get; set; }
        #endregion

        #region 事件

        #endregion

        #region 方法

        public LogicLocation Push(string flight)
        {
            if (string.IsNullOrEmpty(flight))
            {
                MessageBox.Show("航班号不存在！");
                return null;
            }
            var frameNotFull = frames.Where(f => f.Times.Count < FrameCount);
            foreach (var frame in frameNotFull)
            {
                if (frame.Times.Count == 0)
                {
                    frame.Times.Enqueue(flight);
                    return frame.Location;
                }
                else if (frame.Times.Peek() == flight)
                {
                    frame.Times.Enqueue(flight);
                    return frame.Location;

                }
            }
            if (IsAllFull())
            {
                MessageBox.Show("所有仓库都已满！");
                return null;
            }
            else
            {
                string str = "";
                int index = 0;
                for (int i = 1; i < FlightTime.Count; i++)
                {
                    foreach (var frame in frameNotFull)
                    {
                        str = frame.Times.ElementAt(frame.Times.Count - 1);
                        index = FlightTime.Keys.ToList().IndexOf(str);
                        if (index + i >= FlightTime.Count)
                        {
                            break;
                        }
                        if (flight == FlightTime.Keys.ToList()[index + i])
                        {
                            frame.Times.Enqueue(flight);
                            return frame.Location;
                        }
                    }
                }

                var f = frameNotFull.OrderBy(x => x.Times.Count).ToList()[0];
                f.Times.Enqueue(flight);
                NeedRearrange = f.Location;
                return f.Location;

            }
        }

        public LogicLocation Pop(string flight)
        {
            if (string.IsNullOrEmpty(flight))
            {
                MessageBox.Show("航班号不存在！");
                return null;
            }
            var frameNotEmpty = frames.Where(x => x.Times.Count > 0);
            foreach (var frame in frameNotEmpty)
            {
                if (frame.Times.Peek() == flight)
                {
                    frame.Times.Dequeue();
                    return frame.Location;
                }
            }
            return null;
        }
        #endregion

        #region 私有方法
        private bool IsAllFull()
        {
            if (frames.Where(x => x.Times.Count < FrameCount).Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }

    #region FrameModel
    public class FrameModel
    {
        public FrameModel(int shelf, int row, int col)
        {
            Location = new LogicLocation() { Shelf = shelf, Row = row, Column = col };
        }
        public LogicLocation Location { get; private set; }

        public Queue<string> Times { get; set; }
    }

    #endregion

    #region LogicLocation
    public class LogicLocation
    {
        public int Shelf { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
    #endregion

}
