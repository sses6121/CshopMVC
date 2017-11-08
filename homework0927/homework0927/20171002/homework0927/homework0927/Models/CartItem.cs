using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homework0927.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }
        
        public int Amount {
            get {
                return this.Price * this.Quantity;
            }
        }
        public string DefaultImageURL { get; set; }
    }
}