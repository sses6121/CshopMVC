using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            var findItem = this.cartItems.Where(s => s.Id == ProdId)
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
                findItem.Quantity += 1;
            }
            return true;
        }
        
        private bool AddProduct(ProdInfo product)
        {
            var cartItem = new CartItem() {
                Id = product.ProdId,
                Name = product.ProdName,
                Price = product.ProdPrice,
                DefaultImageURL = @"~/images/" + product.ProdName + ".jpeg",
                Quantity = 1
            };
            this.cartItems.Add(cartItem);
            return true;
        }

        public bool RemoveProduct(int ProdId)
        {
            var findItem = this.cartItems
                            .Where(s => s.Id == ProdId)
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
                    ProdId = cartItem.Id,
                    ProdPrice = cartItem.Price,
                    ProdQuantity = cartItem.Quantity,
                    OrderId = orderId
                });
            }
            return result;
        }

        #region IEnumerator
        public IEnumerator<CartItem> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}