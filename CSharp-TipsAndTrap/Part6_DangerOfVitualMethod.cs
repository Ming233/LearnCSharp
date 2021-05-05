using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_TipsAndTrap.Part4
{
    partial class Part6_DangerOfVitualMethod
    {

        static void Part6_DangerOfVitualMethod_Main()
        {
            Project project1 = new Project { Name = "Better UI" };
            //This call AgileProject constuctor.
            //The AgileProject consturctor calls project conturctor
            //The project call initialize method which is overriden by the Agile project.
            //The Agile project fail because name is not set yet
            // Project project2 = new AgileProject { Name = "Better UI" };
        }
    }



    public class AgileProject : Project
    {
        protected int Length { get; set; }
        public AgileProject()
        {
            Name = "New Agile Project (unnamed)";
        }
        protected override void Initialize()
        {
            Length = Name.Length;
        }
    }
    public class Project
    {
        public string Name { get; set; }

        public Clock TimeLeft { get; set; } // Analog
        public DigitalClock TimeElapsed { get; set; } // Digital

        public Project()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Clock
    {
    }

    public class DigitalClock
    {
    }
}
