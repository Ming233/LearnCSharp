using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterTwo
{
    struct Alien
    {
        public int x;
        public int y;
        public int Lives;

        public Alien(int x, int y)
        {
            this.x = x;
            this.y = y;
            Lives = 3;
        }

        public override string ToString()
        {
            return string.Format("X: {0} Y: {1} Lives: {2}", x, y, Lives);
        }
    }

    enum AlienState
    {
        Sleeping,
        Attacking,
        Destroyed
    };

    public static class Page100
    {
        public static void page100_Main()
        {
            Alien a;
            a.x = 50;
            a.y = 50;
            a.Lives = 4;
            Console.WriteLine("a {0}", a.ToString());

            Alien x = new Alien(100, 100);
            Console.WriteLine("x {0}", x.ToString());

            Alien[] swarm = new Alien[100];
            Console.WriteLine("swarm [10] {0}", swarm[10].ToString());

            Console.WriteLine("Page 101");
            AlienState alientstate = AlienState.Attacking;
            Console.WriteLine(alientstate);

            Console.WriteLine("Page 103");
            for (int i=0; i < swarm.Length; i++)
            {
                swarm[i] = new Alien(0, 0);
                Console.WriteLine("swarm ["+i+"] {0}", swarm[i]);
            }
        }
    }
}
