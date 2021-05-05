using OOPFundamentalCommom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFundamental
{
    public class Product : EntityBase, ILoggable
    {
        public Product()
        {

        }

        public Product(int productId)
        {
            this.ProductId = productId;
        }

        public Decimal? CurrentPrice { get; set; }
        public int ProductId { get; private set; }
        public string ProductDescription { get; set; }

        private string _ProductName;

        public string ProductName
        {
            get
            {
                return _ProductName.InsertSpaces();
            }
            set { _ProductName = value; }
        }

        public static string testhaha()
        {
            string a;
            a = "a";
            //this is extention method
            return a.teststring();
        }

        ///// <summary>
        ///// Retrieve one product.
        ///// </summary>
        //public Product Retrieve(int productId)
        //{
        //    // Code that retrieves the defined product
        //    return new Product();
        //}

        ///// <summary>
        ///// Saves the current product.
        ///// </summary>
        ///// <returns></returns>
        //public bool Save()
        //{
        //    // Code that saves the defined product
        //    return true;
        //}

        /// <summary>
        /// Validates the product data.
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrWhiteSpace(ProductName)) isValid = false;
            if (CurrentPrice == null) isValid = false;

            return isValid;
        }

        public override string ToString()
        {
            return ProductName;
        }

        //public string Log()
        //{
        //    var logString = this.ProductId + ": " +
        //                    this.ProductName + " " +
        //                    "Detail: " + this.ProductDescription + " " +
        //                    "Status: " + this.EntityState.ToString();
        //    return logString;
        //}

        public string Log() =>
                $"{ProductId}: {_ProductName} Detail: {ProductDescription} Status: {EntityState.ToString()}";
    }
}
