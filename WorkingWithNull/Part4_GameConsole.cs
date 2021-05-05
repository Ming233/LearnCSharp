using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithNull
{
    public class Part4_GameConsole
    {
        public static void Part4_GameConsole_Main()
        {
            Part4_PlayerCharacter sarah = new Part4_PlayerCharacter(new Part4_DiamondSkinDefence())
            {
                Name = "Sarah"
            };

            Part4_PlayerCharacter amrit = new Part4_PlayerCharacter(Part4_SpecialDefence.Null)
            {
                Name = "Amrit"
            };

            Part4_PlayerCharacter gentry = new Part4_PlayerCharacter(Part4_SpecialDefence.Null)
            {
                Name = "Gentry"
            };

            sarah.Hit(10);
            amrit.Hit(10);
            gentry.Hit(10);


            Console.ReadLine();
        }
    }

    class Part4_PlayerCharacter
    {
        private readonly Part4_SpecialDefence _specialDefence;

        public Part4_PlayerCharacter(Part4_SpecialDefence specialDefence)
        {
            _specialDefence = specialDefence;
        }

        public string Name { get; set; }
        public int Health { get; set; } = 100;

        public void Hit(int damage)
        {
            //int damageReduction = 0;

            //if (_specialDefence != null)
            //{  
            //    damageReduction = _specialDefence.CalculateDamageReduction(damage);
            //}

            //int totalDamageTaken = damage - damageReduction;

            int totalDamageTaken = damage - _specialDefence.CalculateDamageReduction(damage);

            Health -= totalDamageTaken;

            Console.WriteLine($"{Name}'s health has been reduced by {totalDamageTaken} to {Health}.");
        }
    }

    public class Part4_DiamondSkinDefence : Part4_SpecialDefence
    {
        public override int CalculateDamageReduction(int totalDamage)
        {
            return 1;
        }
    }

    public class Part4_IronBonesDefence : Part4_SpecialDefence
    {
        public override int CalculateDamageReduction(int totalDamage)
        {
            return 5;
        }
    }

    public abstract class Part4_SpecialDefence
    {
        public abstract int CalculateDamageReduction(int totalDamage);

        public static Part4_SpecialDefence Null { get; } = new NullDefence();

        private class NullDefence : Part4_SpecialDefence
        {
            public override int CalculateDamageReduction(int totalDamage)
            {
                return 0; // no operation /  "do nothing" behavior
            }
        }
    }
}
