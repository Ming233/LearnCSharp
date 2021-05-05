﻿using OOPFundamentalCommom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFundamental
{
    public class Order : EntityBase, ILoggable
    {
        public Order() //: this (0)
        {

        }

        public Order(int orderId)
        {
            //OrderId = orderId;
            //OrderItems = new List<OrderItem>();
            this.OrderId = orderId;
        }

        public int CustomerId { get; set; }
        public int ShippingAddressId { get; set; }

        public DateTimeOffset? OrderDate { get; set; }
        public int OrderId { get; private set; }
        public List<OrderItem> orderItems { get; set; }

        /// <summary>
        /// Validates the order data.
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            var isValid = true;

            if (OrderDate == null) isValid = false;

            return isValid;
        }

        public override string ToString()
        {
            return OrderDate.Value.Date + " (" + OrderId + ")";
        }

        //public string Log()
        //{
        //    var logString = this.OrderId + ": " +
        //                    "Date: " + this.OrderDate.Value.Date + " " +
        //                    "Status: " + this.EntityState.ToString();
        //    return logString;
        //}

        public string Log() =>
        $"{OrderId}  Date: {OrderDate} Status: {EntityState.ToString()}";
    }
}
