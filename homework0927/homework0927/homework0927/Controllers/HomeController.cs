using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using homework0927.Models;

namespace homework0927.Controllers
{
    
    public class HomeController : Controller
    {
        StockEntities db = new StockEntities();
        // GET: Home
        public ActionResult Index(ProdInfo prod)
        {
            var q = (from s in db.ProdInfoes
                     select s);
            var result = q.ToList();
            return View(result);
        }

        public ActionResult Login() {
            var viewData = new CustLogin();
            return View(viewData);
        }

        public ActionResult Logout() {

            Session["CustId"]="";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(CustLogin frm)
        {
                var query1 = from o in db.CustLogins
                             where o.CustLoginName == frm.CustLoginName && o.CustPassword== frm.CustPassword
                             select o.CustId;
                var data1 = query1.ToList();
            if (data1.Count() != 1) {
                Session["message"] = "帳號或密碼錯誤";
                return View("Login", frm);
            }
            Session.Remove("message");
            Session["CustId"] = data1[0];
            return RedirectToAction("Index", "Home");
        }
            



        public ActionResult Register() {
            var viewData = new CustomerInfo();
            var q = (from s in db.CustomerInfoes
                     orderby s.CustId descending
                     select s.CustId).FirstOrDefault();
            viewData.CustId = q + 1;
            //Session["CustId"] = q + 1;
            //return Content(Session["CustId"]);
            return View(viewData);
        }

        [HttpPost]
        public ActionResult Register(CustomerInfo data) {
            var loginData = new CustLogin();
            //int q = Convert.ToInt32(Session["CustId"]);
            //return Content(q.ToString());
            try {

                loginData.CustId = data.CustId;
                loginData.CustLoginName = data.CustLoginName;
                loginData.CustPassword = data.CustPassword;
                db.CustomerInfoes.Add(data);
                db.SaveChanges();
                db.CustLogins.Add(loginData);
                db.SaveChanges();
                //var CustLogin = db.CustLogins.Add(loginData);
                return RedirectToAction("Index", "Home");
            } catch {
                //return Content(data.CustId.ToString());
                return View(data);
                
            }
        }


        public ActionResult MyOrder(FormCollection frm)
        {
            
            //取得目前登入使用者Id
            if (string.IsNullOrEmpty(Session["CustId"].ToString())) {
                return RedirectToAction("Index", "Home");
            }
            int userId = Convert.ToInt32(Session["CustId"]);//Session["CustId"]);
            
            //var userName = (from s in db.CustomerInfoes
            //                where s.CustId == userId
            //                select s.CustName).FirstOrDefault();
            //return Content(userId.ToString());
           // using (StockEntities db = new StockEntities()) {
                var q = (from s in db.VW_MyOrder
                         where s.CustId ==  userId
                         select s);
                var result = q.ToList();
                
                return View(result);
           // }
        }

        public ActionResult Detail(int id)
        {
            if (id == 0) {
                return RedirectToAction("Index");
            } else {
                var result = (from q in db.ProdInfoes
                              where q.ProdId == id
                              select q).ToList();
                return View(result);
            }
        }

        // GET: Home/Details/5
        public ActionResult Details(VW_OrderAndDetail frm)
        {
            if(frm == null) {
                return RedirectToAction("Index");
            } else { 
                
                  //var OrderId = Convert.ToInt16(frm.OrderId);
                    int OrderId = Convert.ToInt32(RouteData.Values["id"]);
                int userId = Convert.ToInt32(Session["CustId"]);
                
                //return Content(OrderId.ToString());
                    var result=(from s in db.VW_OrderAndDetail
                        where s.OrderId == OrderId /*&&s.CustId==userId*///Convert.ToInt32(Session["OrderId"])
                        select s).ToList();
                    //var result=(from s1 in db.VW_OrderAndDetail
                    //            where s1.CustId==userId
                    //            select s).ToList()
            //if(result == null){//default(ProdInfo)) {

            //} else {
               // return Content(result.ToString());
                return View(result);
            }
           // return View();
        }


        //[HttpPost]
        //public ActionResult Index(Models.OrderDetail orderPostback)
        //{
        //    if (this.ModelState.IsValid) {
                
        //        //var order = db.OrderInfoes.;
        //        var currentcart = Models.Cart.Operation.GetCurrentCart();
        //        int userId = Convert.ToInt32(Session["CustId"]);
        //        var order = new Models.OrderInfo() {
        //            CustId=userId,
        //            OrderTotal=0,
        //            OrderTime=DateTime.Now
        //        };
        //        db.OrderInfoes.Add(order);
        //        db.SaveChanges();
        //        var q = (from s in db.OrderInfoes
        //                 orderby s.OrderId descending
        //                 select s.OrderId).First();
        //        List<OrderDetail> orderDetails = currentcart.ToOrderDetail(q);
        //        db.OrderDetails.AddRange(orderDetails);
        //        db.SaveChanges();
        //        return Content("訂購成功");
        //    }
        //    return View();
        //}

        // GET: Home/Edit/5
        public ActionResult CustEdit(FormCollection frm)
        {
            if (string.IsNullOrEmpty(Session["CustId"].ToString())) {
                return RedirectToAction("Index", "Home");
            }
            int custid = Convert.ToInt32(Session["CustId"]);
            //var result = (from q in db.CustomerInfoes
            //              where q.CustId == custid
            //              select q).ToList();
            var result = db.CustomerInfoes.Find(custid);
            return View(result);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult CustEdit(CustomerInfo customer)
        {
            try
            {
                // TODO: Add update logic here
                CustomerInfo cust = db.CustomerInfoes.Find(customer.CustId);
                db.Entry(cust).Property(x => x.CustId).IsModified = false;
                db.Entry(cust).Property(x => x.CustLoginName).IsModified = false;
                db.Entry(cust).Property(x => x.CustPassword).IsModified = false;
                cust.CustName = customer.CustName;
                cust.CustCellPhone = customer.CustCellPhone;
                cust.CustRoad = customer.CustRoad;
                cust.PostCategory = customer.PostCategory;
                cust.PostId = customer.PostId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(customer);
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        [HttpPost]
        public ActionResult MyOrderDetail(Ship postback)
        {

            //Cart cart = new Cart();
            if (this.ModelState.IsValid) {   //取得目前購物車
                var currentcart = Cart.Operation.GetCurrentCart();

                //取得目前登入使用者Id
                int userId = Convert.ToInt32(Session["CustId"]);

                using (StockEntities db = new StockEntities()) {
                    //建立Order物件
                    var order = new OrderInfo() {
                        //    UserId = userId,
                        //    RecieverName = postback.RecieverName,
                        //    RecieverPhone = postback.RecieverPhone,
                        //    RecieverAddress = postback.RecieverAddress
                        CustId = userId,
                        OrderTotal = currentcart.TotalAmount, //cartitem.Price,
                        OrderTime = DateTime.Now,

                    };
                    ////加其入Orders資料表後，儲存變更
                    db.OrderInfoes.Add(order);
                    db.SaveChanges();
                    //取得購物車中OrderDetai物件
                    var orderDetails = currentcart.ToOrderDetail(order.OrderId);

                    //將其加入OrderDetails資料表後，儲存變更
                    db.OrderDetails.AddRange(orderDetails);
                    db.SaveChanges();
                }
                return Content("訂購成功");
            }
            return View();
        }

        //public ActionResult MyOrder()
        //{
        //    //取得目前登入使用者Id
        //    int userId = Convert.ToInt32(Session["CustId"]);

        //    using (StockEntities db = new StockEntities()) {
        //        var result = (from s in db.OrderInfoes
        //                      where s.CustId == userId
        //                      select s).ToList();

        //        return View(result);
        //    }
        //}

        //public ActionResult MyOrderDetail(int id)
        //{
        //    using (StockEntities db = new StockEntities()) {
        //        var result = (from s in db.OrderDetails
        //                      where s.OrderId == id
        //                      select s).ToList();

        //        if (result.Count == 0) {
        //            return RedirectToAction("Index");
        //        } else {
        //            return View(result);
        //        }
            }
        }
   // }
//}






