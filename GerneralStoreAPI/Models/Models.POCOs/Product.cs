using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerneralStoreAPI.Models.Models.POCOs
{
    public class Product
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public int NumberInInventory { get; set; }
        public bool IsInStock
        {
            get
            {
                if (NumberInInventory < 1)
                {
                    return false;
                }
                return true;
            }
        }
    }
}