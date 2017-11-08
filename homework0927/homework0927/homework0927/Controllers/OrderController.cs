using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using homework0927.Models;

namespace homework0927.Controllers
{
    public class OrderController :Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            if (Session["CustId"] == null || Session["CustId"].ToString() =="") {
                return RedirectToAction("Register", "Home");
            }
            StockEntities db = new StockEntities();
            int usrId = Convert.ToInt32(Session["CustId"]);
            var q = from s in db.CustomerInfoes
                    where s.CustId == usrId
                    select s;
            var result = q.ToList();
            return View(result);



            // StockEntities db = new StockEntities();
            // int usrId = Convert.ToInt32(Session["CustId"]);
            // var q = from s in db.VW_OrderAndDetailAndCust
            //         where s.CustId == usrId
            //         select s;
            // var result = q.ToList();
            // return View(result);
            //// return View();
        }

        [HttpPost]
        public ActionResult Index(Ship postback)
        {

            StockEntities db = new StockEntities();
            int usrId = Convert.ToInt32(Session["CustId"]);
            var q = from s in db.CustomerInfoes
                    where s.CustId == usrId
                    select s;
            var result = q.ToList();
            var q1 = (from s in db.OrderInfoes
                      orderby s.OrderId descending
                      select s.OrderId).FirstOrDefault();
            int orderId = q1+1;

            //StockEntities db = new StockEntities();
            //int usrId = Convert.ToInt32(Session["CustId"]);
            //    var q = from s in db.VW_OrderAndDetailAndCust
            //            where s.CustId == usrId
            //            select s;
            //    var result = q.ToList();
            
            //Cart cart = new Cart();
            if (this.ModelState.IsValid == false) {
                /*if (this.ModelState.IsValid) {*/   //取得目前購物車
                var currentcart = Cart.Operation.GetCurrentCart();
                var q2 = (from s in db.OrderInfoes
                          orderby s.OrderId descending
                          select s.OrderId).FirstOrDefault();
                int orderid = q2 + 1;
                //取得目前登入使用者Id
                //int userId = Convert.ToInt32(Session["CustId"]);
                //int userId = Convert.ToInt32(Session["CustId"]);

                using (StockEntities db1 = new StockEntities()) {
                    //建立Order物件
                    var order = new OrderInfo() {
                        //    UserId = userId,
                        //    RecieverName = postback.RecieverName,
                        //    RecieverPhone = postback.RecieverPhone,
                        //    RecieverAddress = postback.RecieverAddress
                        OrderId =orderid,
                        CustId = usrId,
                        OrderTotal = currentcart.TotalAmount, //cartitem.Price,
                        OrderTime = DateTime.Now,

                    };
                    ////加其入Orders資料表後，儲存變更
                    db1.OrderInfoes.Add(order);
                    db1.SaveChanges();

                    //var q2 = (from s in db.OrderInfoes
                    //          orderby s.OrderId descending
                    //          select s.OrderId).FirstOrDefault();
                    //int orderid = q2 + 1;
                    //取得購物車中OrderDetai物件
                    //var orderDetails = currentcart.ToOrderDetail(order.OrderId);

                    var orderDetails = currentcart.ToOrderDetail(orderid);
                    //將其加入OrderDetails資料表後，儲存變更
                    db1.OrderDetails.AddRange(orderDetails);
                    db1.SaveChanges();
                }
                var qTotal = (from s in db.OrderDetails
                              where s.OrderId == orderid
                              select s).ToList();
                int sum = 0;
                foreach(var item in qTotal) {
                    sum += item.ProdPrice * item.ProdQuantity;
                }
                //var order = new OrderInfo() {
                //    //    UserId = userId,
                //    //    RecieverName = postback.RecieverName,
                //    //    RecieverPhone = postback.RecieverPhone,
                //    //    RecieverAddress = postback.RecieverAddress
                //    //OrderId =orderId,
                //    CustId = usrId,
                //    OrderTotal = sum, //cartitem.Price,
                //    OrderTime = DateTime.Now,

                //};
                //////加其入Orders資料表後，儲存變更
                //db.OrderInfoes.Add(order);
                //db.SaveChanges();
                //return Content("訂購成功");
                return RedirectToAction("MyOrder", "Home");
            }
            return View(result);
        }

        public ActionResult MyOrder()
        {
            //取得目前登入使用者Id
            int userId = Convert.ToInt32(Session["CustId"]);

            using (StockEntities db = new StockEntities()) {
                var result = (from s in db.OrderInfoes
                              where s.CustId == userId
                              select s).ToList();

                return View(result);
            }
        }

        public ActionResult MyOrderDetail(int id)
        {
            using (StockEntities db = new StockEntities()) {
                var result = (from s in db.OrderDetails
                              where s.OrderId == id
                              select s).ToList();

                if (result.Count == 0) {
                    return RedirectToAction("Index");
                } else {
                    return View(result);
                }
            }
        }



    }
}

