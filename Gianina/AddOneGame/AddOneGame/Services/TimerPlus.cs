using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace AddOneGame.Services
{
    public class TimerPlus : System.Timers.Timer
    {
        private DateTime m_dueTime;
        private long TimesToRestart;
        public Ticker GameTicker { get; set; }

        public TimerPlus() : base()
        {
            this.Elapsed += this.ElapsedAction;
        }

        public TimerPlus(long seconds) : base()
        {
            this.TimesToRestart = seconds;
            this.Elapsed += this.ElapsedAction;
            this.Interval = 1000;
            this.AutoReset = false;
        }

        protected new void Dispose()
        {
            this.Elapsed -= this.ElapsedAction;
            this.Elapsed += this.ElapsedAction;
            //base.Dispose();
        }

        public long TimeLeft
        {
            get
            {
                Console.WriteLine("THERE: " + this.TimesToRestart);
                return Convert.ToInt64((TimesToRestart - 1) + (this.m_dueTime - DateTime.Now).TotalMilliseconds / 1000);
            }
        }

        public new void Start()
        {
            this.m_dueTime = DateTime.Now.AddMilliseconds(this.Interval);
            base.Start();
        }

        private void ElapsedAction(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("HERE: " + this.TimesToRestart);
            if (this.TimesToRestart >= 0)
            {
                this.m_dueTime = DateTime.Now.AddMilliseconds(this.Interval);
                Console.WriteLine("TIMES TO RESET: " + this.TimesToRestart);

            } else
            {
                this.AutoReset = false;
            }
            this.TimesToRestart--;
        }
    }
}
