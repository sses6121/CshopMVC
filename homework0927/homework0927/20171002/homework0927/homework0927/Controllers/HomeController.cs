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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login() {
            var viewData = new CustLogin();
            return View(viewData);
        }

        [HttpPost]
        public ActionResult Login(CustLogin frm)
        {
                var query1 = from o in db.CustLogins
                             where o.CustLoginName == frm.CustLoginName && o.CustPassword== frm.CustPassword
                             select o.CustId;
                var data1 = query1.ToList();
            if (data1.Count() != 1) {
                Session["message"] = "帳號或密碼錯誤登入失敗";
                return View("Login", frm);
            }
            Session.Remove("message");
            Session["CustId"] = data1[0];
            return RedirectToAction("Index", "Home");
        }
            



        public ActionResult Register() {
            var viewData = new CustomerInfo();
            return View(viewData);
        }

        [HttpPost]
        public ActionResult Register(CustomerInfo data) {

            try {
                db.CustomerInfoes.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            } catch {
                return View(data);
            }
        }


        public ActionResult MyOrder()
        {
            //取得目前登入使用者Id
            int userId = Convert.ToInt32(Session["Custid"]);

            using (StockEntities db = new StockEntities()) {
                var result = (from s in db.VW_OrderAndDetail
                              where s.CustId == userId
                              select s).ToList();

                return View(result);
            }
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            var userId = Convert.ToInt32(Session["Custid"]);
            var result=(from s in db.ProdInfoes
                        where s.ProdId==id
                        select s).FirstOrDefault();
            if(result == default(ProdInfo)) {
                return RedirectToAction("Index");
            } else {
                return View(result);
            }
           // return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
    }
}





//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Login(Login form)
//{
//    if (ModelState.IsValid) {
//        //驗證資料庫登入
//        //這邊請使用自行驗證摟
//        string Sql = " AccountName == @0 and Password == @1 and IsUsed == @2";
//        var query = (from u in db.Account.Where(Sql, form.AccountName, form.Password, "true")
//                     select u).ToList();

//        if (query.Count() != 1) {
//            ModelState.AddModelError(string.Empty, "帳號或密碼錯誤登入失敗");
//            return View("Index", form);
//        }

//        try {
//            query[0].LoginIP = PublicFunction.GetIpAddress();
//            query[0].LoginDate = DateTime.Now;
//            db.SaveChanges();
//        } catch (Exception ex) {
//            ModelState.AddModelError(string.Empty, ex.Message.ToString());
//            return View("Index", form);
//        }


//        bool isPersistent = false;//如果票證將存放於持續性 Cookie 中 (跨瀏覽器工作階段儲存)，則為 true，否則為 false。 如果票證是存放於 URL 中，則忽略這個值。
//        string userData = "";//可放使用者自訂的內容
//        string mAccountID = query[0].AccountID.ToString();

//        //寫cookie
//        //使用 Cookie 名稱、版本、到期日、核發日期、永續性和使用者特定的資料，初始化 FormsAuthenticationTicket 類別的新執行個體。 此 Cookie 路徑設定為在應用程式的組態檔中建立的預設值。
//        //使用 Cookie 名稱、版本、目錄路徑、核發日期、到期日期、永續性和使用者定義的資料，初始化 FormsAuthenticationTicket 類別的新執行個體。
//        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
//          mAccountID,//使用者ID
//          DateTime.Now,//核發日期
//          DateTime.Now.AddMinutes(1800),//到期日期 30分鐘 
//          isPersistent,//永續性
//          userData,//使用者定義的資料
//          FormsAuthentication.FormsCookiePath);

//        string encTicket = FormsAuthentication.Encrypt(ticket);
//        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

//        //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
//        //cookie.Expires = ticket.Expiration;
//        //Response.Cookies.Add(cookie);

//        //FormsAuthentication.RedirectFromLoginPage(strUsername, isPersistent);

//        if (form.ReturnUrl != null) {
//            return Redirect(form.ReturnUrl.ToString());
//        } else {
//            return RedirectToAction("Index", "Admin");
//        }

//    }
//    //return RedirectToAction("Index", "Login", null);
//    return View("Index", form);
//}
