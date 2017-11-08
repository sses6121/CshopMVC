using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homework0927.Models
{
    public partial class Order
    {
        public Order()
        {
            this.OrderInfo = new HashSet<OrderInfo>();
        }
        public int OrderId { get; set; }
        public int CustId { get; set; }
        public string OrderTotal { get; set; }
        public string OrderTime { get; set; }

        public virtual ICollection<OrderInfo> OrderInfo { get; set; }
    }
}