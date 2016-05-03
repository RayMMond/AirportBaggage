using AirportBaggage.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirportBaggage
{
    public class Algorithm: INext
    {
        #region 私有字段
        List<FrameModel> frames;
        private DateTime checkTime = DateTime.Now;
        #endregion

        #region 构造
        public Algorithm(int shelfCount, Size size, int frameCount)
        {
            ShelfCount = shelfCount;
            ShelfSize = size;
            FrameCount = frameCount;
            NeedRearrangeLocation = new Queue<LogicLocation>();
            FlightTime = new Dictionary<string, DateTime>();
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

        public Dictionary<string, int> FlightBaggageCount { get; set; }

        public Queue<LogicLocation> NeedRearrangeLocation { get; set; }
        #endregion

        #region 事件
        public event EventHandler<LogicLocationEventArgs> Rearrange;

        public event EventHandler<string> DepartureTimeReached;
        #endregion

        #region 方法

        public LogicLocation Push(string flight, int shelf)
        {
            if (string.IsNullOrEmpty(flight))
            {
                MessageBox.Show("航班号不存在！");
                return null;
            }
            var frameNotFull = frames.Where(f => f.Times.Count < FrameCount && f.Location.Shelf == shelf);
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
                NeedRearrangeLocation.Enqueue(f.Location);
                return f.Location;

            }
        }

        public LogicLocation Pop(string flight, int shelf)
        {
            if (string.IsNullOrEmpty(flight))
            {
                MessageBox.Show("航班号不存在！");
                return null;
            }
            var frameNotEmpty = frames.Where(x => x.Times.Count > 0 && x.Location.Shelf == shelf);
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

        public void Pop(LogicLocation location)
        {
            var frameNotEmpty = frames.Where(x => x.Location == location);
            foreach (var frame in frameNotEmpty)
            {
                frame.Times.Dequeue();
                break;
            }

        }

        public LogicLocation Peek(string flight, int shelf)
        {
            if (string.IsNullOrEmpty(flight))
            {
                MessageBox.Show("航班号不存在！");
                return null;
            }
            var frameNotEmpty = frames.Where(x => x.Times.Count > 0 && x.Location.Shelf == shelf);
            foreach (var frame in frameNotEmpty)
            {
                if (frame.Times.Peek() == flight)
                {
                    return frame.Location;
                }
            }
            return null;
        }

        public int GetShelfNumber()
        {
           return frames.Where(f => f.Times.Count < FrameCount).ElementAt(0).Location.Shelf;
        }

        public bool CanPop(string flight, int shelf)
        {
            var temp = frames.Where(x => x.Times.Count > 0 && x.Location.Shelf == shelf);
            foreach (var frame in temp)
            {
                if (frame.Times.Peek() == flight)
                {
                    return true;
                }
            }
            return false;
        }

        public void Next()
        {
            CheckForDepartureTime();
            if (NeedRearrangeLocation.Count > 0)
            {
                FrameModel f = frames.Where(x => x.Location == NeedRearrangeLocation.Peek()).ElementAt(0);
                if (f.Times.Count > 0)
                {
                    for (int i = 1; i < f.Times.Count; i++)
                    {
                        if (FlightTime[f.Times.ElementAt(i - 1)] > FlightTime[f.Times.ElementAt(i)])
                        {
                            return;
                        }
                    }
                }
                NeedRearrangeLocation.Dequeue();
            }
            if (NeedRearrangeLocation.Count > 0)
            {
                if (Rearrange != null)
                {
                    Rearrange(this, new LogicLocationEventArgs(NeedRearrangeLocation.Peek()));
                }
            }
        }

        public void ChangeTime(string flight, DateTime newTime)
        {
            FlightTime[flight] = newTime;
            CheckForRearrange();
            CheckForDepartureTime();
        }

        public void ChangeFlight(string oldFlight, string newFlight)
        {
            try
            {
                DateTime t = FlightTime[oldFlight];
                FlightTime.Remove(oldFlight);
                FlightTime.Add(newFlight, t);
            }
            catch
            {
                MessageBox.Show("更改航班号失败！");
                return;
            }


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

        private void CheckForRearrange()
        {
            foreach (var f in frames.Where(x => x.Times.Count != 0))
            {
                for (int i = 1; i < f.Times.Count; i++)
                {
                    if (FlightTime[f.Times.ElementAt(i - 1)] > FlightTime[f.Times.ElementAt(i)])
                    {
                        NeedRearrangeLocation.Enqueue(f.Location);
                        break;
                    }
                }
            }
        }

        private void CheckForDepartureTime()
        {
            if (DateTime.Now - checkTime > new TimeSpan(0,1,0))
            {
                foreach (var item in FlightTime)
                {
                    if (DateTime.Now - item.Value <= new TimeSpan(0,1,0))
                    {
                        if (DepartureTimeReached != null)
                        {
                            DepartureTimeReached(this, item.Key);
                        }
                    }
                }
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

    public class LogicLocationEventArgs : EventArgs
    {
        #region 构造
        public LogicLocationEventArgs(LogicLocation location) : base()
        {
            Location = location;
        }
        #endregion

        #region 属性
        public LogicLocation Location { get; set; }
        #endregion

    }
    #endregion

}
