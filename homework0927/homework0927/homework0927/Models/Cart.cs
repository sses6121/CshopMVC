using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace homework0927.Models
{
    
    [Serializable]
    public class Cart :IEnumerable<CartItem>
    {
        
        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }

        private List<CartItem> cartItems;

        public int Count {
            get {
                return this.cartItems.Count;
            }
        }

        public int TotalAmount {
            get {
                int totalAmount = 0;
                foreach(var cartItem in this.cartItems) {
                    totalAmount = totalAmount + cartItem.Amount;
                }
                return totalAmount;
            }
        }

        public bool AddProduct(int ProdId)
        {
            var findItem = this.cartItems.Where(s => s.ProdId == ProdId)
                            .Select(s => s)
                            .FirstOrDefault();
            if (findItem == default(CartItem)) {
                using (StockEntities db = new StockEntities()) {
                    var product = (from s in db.ProdInfoes
                                   where s.ProdId == ProdId
                                   select s).FirstOrDefault();
                    if(product !=default(ProdInfo)) {
                        this.AddProduct(product);
                    }
                }

            } else {
                findItem.ProdQuantity += 1;
            }
            return true;
        }
        
        private bool AddProduct(ProdInfo product)
        {
            var cartItem = new CartItem() {
                ProdId = product.ProdId,
                ProdName = product.ProdName,
                ProdPrice = product.ProdPrice,
                //DefaultImageURL = @"~/images/" + product.ProdName + ".jpeg",
                ProdQuantity = 1
            };
            this.cartItems.Add(cartItem);
            return true;
        }

        public bool RemoveProduct(int ProdId)
        {
            var findItem = this.cartItems
                            .Where(s => s.ProdId == ProdId)
                            .Select(s => s)
                            .FirstOrDefault();
            if(findItem == default(CartItem)) {

            } else {
                this.cartItems.Remove(findItem);
            }
            return true;
        }

        public bool ClearCart()
        {
            this.cartItems.Clear();
            return true;
        }

        public List<OrderDetail> ToOrderDetail(int orderId)
        {
            var result = new List<OrderDetail>();
            foreach(var cartItem in this.cartItems) {
                result.Add(new OrderDetail() {
                    
                    OrderId = orderId,
                    ProdId = cartItem.ProdId,
                    ProdPrice = cartItem.ProdPrice,
                    ProdQuantity = cartItem.ProdQuantity, 
                });
            }
            return result;
        }
        public class Operation
        {
            [WebMethod(EnableSession = true)]
            public static Cart GetCurrentCart()
            {
                if (HttpContext.Current != null) {
                    if (HttpContext.Current.Session["Cart"] == null) {
                        var order = new Cart();
                        HttpContext.Current.Session["Cart"] = order;
                    }
                    return (Cart)HttpContext.Current.Session["Cart"];
                } else {
                    throw new InvalidCastException("System.Web.HttpContext.Current為空，請檢查");
                }
            }
        }

        #region IEnumerator
        public IEnumerator<CartItem> GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }
        #endregion
    }
}