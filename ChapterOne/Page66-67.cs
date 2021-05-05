using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChapterOne
{
    class Page66_67
    {
        public static void Page62_delegate_Main()
        {
            Alarm2 alarm2 = new Alarm2();

            //This part correct? book did not mentioned this part. I add myself. this will throw exception before going into the CMD.
            alarm2.OnAlarmRaised2 += AlarmListener1;
            alarm2.OnAlarmRaised2 += AlarmListener2;

            try
            {
                alarm2.RaiseAlarm2("My home");
                Console.WriteLine("Alarm Rised");
            }
            catch(AggregateException agg)
            {
                foreach(Exception ex in agg.InnerExceptions)
                    Console.WriteLine(ex.Message);
            }
        }

        static void AlarmListener1(object source, AlarmEventArgs args)
        {
            Console.WriteLine("Alarm arg listener 1 called");
            Console.WriteLine("Alarm in {0}", args.Location);
            throw new Exception("Bang");
        }
        static void AlarmListener2(object source, AlarmEventArgs args)
        {
            Console.WriteLine("Alarm arg listener 2 called");
            Console.WriteLine("Alarm in {0}", args.Location);
            throw new Exception("Boom");
        }

    }

    //EventHandler alarm
    class Alarm2
    {
        //Delegate for the alarm event
        public event EventHandler<AlarmEventArgs> OnAlarmRaised2 = delegate { };

        //call to raised an alarm
        //public void RaiseAlarm2(string location)
        //{
        //    OnAlarmRaised2(this, new AlarmEventArgs(location));
        //}

        public void RaiseAlarm2(string location)
        {
            List<Exception> exceptionList = new List<Exception>();

            foreach (Delegate handler in OnAlarmRaised2.GetInvocationList())
            {
                try
                {
                    handler.DynamicInvoke(this, new AlarmEventArgs(location));
                }
                catch (TargetInvocationException e)
                {
                    exceptionList.Add(e.InnerException);
                }
            }
            if (exceptionList.Count > 0)
                throw new AggregateException(exceptionList);

        }
        
    }



    //EventHandler data
    class AlarmEventArgs : EventArgs
    {
        public string Location { get; set; }

        public AlarmEventArgs(string location)
        {
            Location = location;
        }
    }

    //event based blass


}


