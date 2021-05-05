using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEventsAndDelegates
{
    class Part2
    {
        public delegate int WorkPerformedHandler(int hours, WorkType workType);

        public static void Part2_Main()
        {
            WorkPerformedHandler del1 = new WorkPerformedHandler(WorkPerformed1);
            WorkPerformedHandler del2 = new WorkPerformedHandler(WorkPerformed2);
            WorkPerformedHandler del3 = new WorkPerformedHandler(WorkPerformed3);

            Console.WriteLine("Calling Delegate");
            del1(5, WorkType.Golf);
            del2(10, WorkType.GenerateReports);

            DoWork(del1);


            Console.WriteLine("\nMerget together");
            del1 += del2 + del3;
            del1(5, WorkType.Golf);

            Console.WriteLine("\nReturn something using delegate");
            int finalHours = del1(10, WorkType.GenerateReports);
            Console.WriteLine(finalHours);

            var worker = new Part2_Worker();


            Console.Read();
        }

        //static void Worker_WorkPerformed(object sender, WorkPerformedEventArgs e)
        //{
        //    Console.WriteLine("Hours worked: " + e.Hours + " " + e.WorkType);
        //}

        //static void Worker_WorkCompleted(object sender, EventArgs e)
        //{
        //    Console.WriteLine("Worker is done");
        //}

        static void DoWork(WorkPerformedHandler del)
        {
            Console.WriteLine("\nDo work");
            del(5, WorkType.GoToMeetings);
        }

        static int WorkPerformed1(int hours, WorkType workType)
        {
            Console.WriteLine("WorkPerformed1 called " + hours.ToString() + " WT: " + workType);
            return hours + 1;
        }

        static int WorkPerformed2(int hours, WorkType workType)
        {
            Console.WriteLine("WorkPerformed2 called " + hours.ToString() + " WT: " + workType);
            return hours + 2;
        }

        static int WorkPerformed3(int hours, WorkType workType)
        {
            Console.WriteLine("WorkPerformed3 called " + hours.ToString() + " WT: " + workType);
            return hours + 3;
        }
    }

    //Worker file
    public delegate int WorkPerformedHandler_EventVer1(int hours, WorkType worktype);
    //This can hand over more data
    //public delegate int WorkPerformedHandler_EventVer2(object sender, WorkPerformedEventArgs e);

    public class Part2_Worker
    {
        //public event EventHandler<WorkPerformedEventArgs> WorkPerformed;
        public event WorkPerformedHandler_EventVer1 WorkPerformed1;
        public event EventHandler<WorkPerformedEventArgs> WorkPerformed2;
        public event EventHandler WorkCompleted;

        public void DoWork(int hours, WorkType workType)
        {
            for (int i = 0; i < hours; i++)
            {
                System.Threading.Thread.Sleep(1000);
                OnWorkPerformed(i + 1, workType);
            }
            OnWorkCompleted();
        }

        protected virtual void OnWorkPerformed(int hours, WorkType workType)
        {
            //if (WorkPerformed1 != null)
            //{
            //    WorkPerformed1(hours, workType);
            //}

            var del = WorkPerformed1 as WorkPerformedHandler_EventVer1;
            if (del != null)
            {
                del(hours, workType);
                //del(this, new WorkPerformedEventArgs(hours, workType));
            }

            var del2 = WorkPerformed2 as EventHandler<WorkPerformedEventArgs>;
            if (del2 != null)
            {
                del2(this, new WorkPerformedEventArgs(hours, workType));
            }
        }

        protected virtual void OnWorkCompleted()
        {
            //if (WorkCompleted != null)
            //{
            //    WorkCompleted(this, EventArgs.Empty);
            //}

            var del = WorkCompleted as EventHandler;
            if (del != null)
            {
                del(this, EventArgs.Empty);
            }
        }
    }

}
