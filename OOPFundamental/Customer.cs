using OOPFundamentalCommom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFundamental
{
    public class Customer : EntityBase, ILoggable
    {
        public Customer() : this(0)
        {
        }

        public Customer(int customerId)
        {
            this.CustomerId = customerId;
            AddressList = new List<Address>();
            Customer.InstanceCount += 1;
        }

        public List<Address> AddressList { get; set; }

        public int CustomerType { get; set; }
        //Customer.InstanceCount+=1   This is Static member means it won't wipe out by create new object.
        public static int InstanceCount { get; set; }

        private string _lastName;
        public string LastName
        {
            get
            {
                // Any code here
                return _lastName;
            }
            set
            {
                // Any code here
                _lastName = value;
            }
        }

        public string FirstName { get; set; }

        public string EmailAddress { get; set; }

        public int CustomerId { get; private set; }

        public string FullName
        {
            get
            {
                string fullName = LastName;
                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    if (!string.IsNullOrWhiteSpace(fullName))
                    {
                        fullName += ", ";
                    }
                    fullName += FirstName;
                }
                return fullName;
            }
        }

        public override bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrWhiteSpace(LastName)) isValid = false;
            if (string.IsNullOrWhiteSpace(EmailAddress)) isValid = false;

            return isValid;
        }

        public override string ToString()
        {
            return FullName;
        }

        //public string Log()
        //{
        //    var logString = this.CustomerId + ": " +
        //                    this.FullName + " " +
        //                    "Email: " + this.EmailAddress + " " +
        //                    "Status: " + this.EntityState.ToString();
        //    return logString;
        //}

        //
        public string Log() =>
            $"{CustomerId}: {FullName} Email: {EmailAddress} Status: {EntityState.ToString()}";
    }
}
