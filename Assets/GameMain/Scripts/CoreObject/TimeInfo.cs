using System;

namespace Suture
{
    /// <summary>
    /// 时间信息
    /// </summary>
    public class TimeInfo : IDisposable
    {
        public static TimeInfo Instance = new TimeInfo();

        private int timeZone;

        private readonly DateTime dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public long ServerMinusClientTime { private get; set; }

        public long FrameTime;

        private TimeInfo()
        {
            this.FrameTime = this.ClientNow();
        }

        public int TimeZone
        {
            get { return this.timeZone; }
            set
            {
                //C# 时间戳与DateTime相互转换 https://blog.csdn.net/qq_35247337/article/details/100016195
                this.timeZone = value;
                dt = dt1970.AddHours(TimeZone);
            }
        }

        public void Update()
        {
            this.FrameTime = this.ClientNow();
        }

        
        /// <summary> 
        /// 根据时间戳获取时间 
        /// </summary>  
        public DateTime ToDateTime(long timeStamp)
        {
            return dt.AddTicks(timeStamp * 10000);
        }

        /// <summary>
        /// 线程安全
        /// </summary>
        /// <returns></returns>
        private long ClientNow()
        {
            return (DateTime.Now.Ticks - this.dt1970.Ticks) / 10000;
        }

        public long ServerNow()
        {
            //return ClientNow() + Game.TimeInfo.ServerMinusClientTime;
            return ClientNow() + ServerMinusClientTime;
        }

        public long ClientFrameTime()
        {
            return this.FrameTime;
        }
        
        public long ServerFrameTime()
        {
            //   return this.FrameTime + Game.TimeInfo.ServerMinusClientTime;
            return this.FrameTime + ServerMinusClientTime;
        }

        public long Transition(DateTime d)
        {
            return (d.Ticks - dt.Ticks) / 10000;
        }
        
        public void Dispose()
        {
            Instance = null;
        }
    }
}