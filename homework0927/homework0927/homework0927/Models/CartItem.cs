using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homework0927.Models
{
    [Serializable]
    public class CartItem
    {
        public int ProdId { get; set; }

        public string ProdName { get; set; }

        public int ProdPrice { get; set; }

        public int ProdQuantity { get; set; }
        
        public int Amount {
            get {
                return this.ProdPrice * this.ProdQuantity;
            }
        }
    }
}