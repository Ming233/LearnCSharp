using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterOne
{
    class Page75
    {
        public static void main()
        {
			try
			{
				Console.WriteLine("Exception Object try");
				Console.WriteLine("Please enter integer");
				string numberText = Console.ReadLine();
				int result = int.Parse(numberText);
				Console.WriteLine("You entered {0}" , result);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Message: " + ex.Message);
				Console.WriteLine("StackTrace: " + ex.StackTrace);
				Console.WriteLine("HelpLink: " + ex.HelpLink);
				Console.WriteLine("TargetSite: " + ex.TargetSite);
				Console.WriteLine("Source: " + ex.Source);
			}
			finally
			{
				Console.WriteLine("Thanks and bye");
			}

        }
    }
}
