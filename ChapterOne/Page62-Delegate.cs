using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterOne
{
    class Page62_Delegate
    {
        static void AlarmListener1()
        {
            Console.WriteLine("Alarm listener 1 called");
        }
        static void AlarmListener2()
        {
            Console.WriteLine("Alarm listener 2 called");
        }

        public static void Page62_delegate_Main()
        {
            Alarm alarm = new Alarm();

            alarm.OnAlarmRaised += AlarmListener1;
            alarm.OnAlarmRaised += AlarmListener2;

            alarm.RaiseAlarm();

            Console.WriteLine("Alarm Rised");


            alarm.OnAlarmRaised -= AlarmListener2;

            alarm.RaiseAlarm();
            Console.WriteLine("Alarm unsubcribe");
        }
    }

    class Alarm
    {
        public Action OnAlarmRaised { get; set; }

        public void RaiseAlarm()
        {
            if (OnAlarmRaised != null)
            {
                OnAlarmRaised();
            }
        }
    }
}
