using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerneralStoreAPI.Models.Models.POCOs
{
    public class Product
    {
        [Key]
        public string SKU { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public int NumberInInventory { get; set; }

        [Required]
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