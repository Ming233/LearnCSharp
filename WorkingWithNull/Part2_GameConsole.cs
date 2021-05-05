using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithNull
{
    public class Part2_GameConsole
    {
        public static void Part2_GameConsole_Main()
        {
            var player = new Part2_PlayerCharacter();
            player.Name = "";
            //player.DaysSinceLastLogin = 42;

            Part2_PlayerDisplayer.Write(player);
        }
    }

    class Part2_PlayerCharacter
    {
        public string Name { get; set; }
        public int? DaysSinceLastLogin { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsNoob { get; set; }

        public Part2_PlayerCharacter()
        {
            //DateOfBirth = DateTime.MinValue; // magic number
            //DaysSinceLastLogin = -1; // magic number

            DateOfBirth =null; // magic number
            DaysSinceLastLogin = null; // magic number
        }
    }

    class Part2_PlayerDisplayer
    {
        public static void Write(Part2_PlayerCharacter player)
        {
            if (string.IsNullOrWhiteSpace(player.Name))
            {
                Console.WriteLine("Player name is null or all whitespace");
            }
            else
            {
                Console.WriteLine(player.Name);
            }


            if (player.DaysSinceLastLogin == null)
            {
                Console.WriteLine("No value for DaysSinceLastLogin");
            }
            else
            {
                Console.WriteLine(player.DaysSinceLastLogin);
            }

            if (player.DateOfBirth == null)
            {
                Console.WriteLine("No date of birth specified");
            }
            else
            {
                Console.WriteLine(player.DateOfBirth);
            }

            if (player.IsNoob == null)
            {
                Console.WriteLine("Player newbie status is unknown");
            }
            else if (player.IsNoob == true)
            {
                Console.WriteLine("Player is a newbie");
            }
            else
            {
                Console.WriteLine("Player is experienced");
            }
        }
    }
}
