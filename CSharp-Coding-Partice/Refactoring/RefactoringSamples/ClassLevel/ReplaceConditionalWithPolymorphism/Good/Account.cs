using System;

namespace RefactoringSamples.ClassLevel.ReplaceConditionalWithPolymorphism.Good
{
    public abstract class Account
    {
        public enum AccountType
        {
            Checking,
            Saving
        }

        public abstract AccountType Type { get; }

        public static Account Create(AccountType type)
        {
            switch (type)
            {
                case AccountType.Checking:
                    return new Checking();
                case AccountType.Saving:
                    return new Saving();
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        public abstract decimal CalculateInterest(decimal amount);
    }

    public class Checking : Account
    {
        public override AccountType Type
        {
            get
            {
                return AccountType.Checking;
            }
        }

        public override decimal CalculateInterest(decimal amount)
        {
            return amount;
        }
    }

    public class Saving : Account
    {
        public override AccountType Type
        {
            get
            {
                return AccountType.Saving;
            }
        }

        public override decimal CalculateInterest(decimal amount)
        {
            return amount * 1.01m;
        }
    }
}
