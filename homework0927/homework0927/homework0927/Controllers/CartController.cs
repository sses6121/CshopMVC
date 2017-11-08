using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using homework0927.Models;

namespace homework0927.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCart()
        {
            return PartialView("_CartPartial");
        }
        
        public ActionResult AddToCart(int ProdId)
        {
            var currentCart = Cart.Operation.GetCurrentCart();
            currentCart.AddProduct(ProdId);
            return PartialView("_CartPartial");
        }

        public ActionResult RemoveFromCart(int ProdId)
        {
            var currentCart = Cart.Operation.GetCurrentCart();
            currentCart.RemoveProduct(ProdId);
            return PartialView("_CartPartial");
        }

        public ActionResult ClearCart()
        {
            var currentCart = Cart.Operation.GetCurrentCart();
            currentCart.ClearCart();
            return PartialView("_CartPartial");
        }
    }
}