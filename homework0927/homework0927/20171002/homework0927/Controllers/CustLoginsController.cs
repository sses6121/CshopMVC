using System.Linq;
using System.Net;
using System.Web.Mvc;
using homework0927.Models;
using System.Data.Entity;

namespace homework0927.Controllers {
    public class CustLoginsController : Controller
    {
        private StockEntities db = new StockEntities();

        // GET: CustLogins
        public ActionResult Index()
        {
            return View(db.CustomerInfoes.ToList());
        }

        // GET: CustLogins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerInfo custLogin = db.CustomerInfoes.Find(id);
            if (custLogin == null)
            {
                return HttpNotFound();
            }
            return View(custLogin);
        }

        // GET: CustLogins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustLogins/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustId,CustLoginName,CustPassword")] CustomerInfo custLogin)
        {
            if (ModelState.IsValid)
            {
                db.CustomerInfoes.Add(custLogin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(custLogin);
        }

        // GET: CustLogins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerInfo custLogin = db.CustomerInfoes.Find(id);
            if (custLogin == null)
            {
                return HttpNotFound();
            }
            return View(custLogin);
        }

        // POST: CustLogins/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustId,CustLoginName,CustPassword")] CustLogin custLogin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(custLogin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(custLogin);
        }

        // GET: CustLogins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerInfo custLogin = db.CustomerInfoes.Find(id);
            if (custLogin == null)
            {
                return HttpNotFound();
            }
            return View(custLogin);
        }

        // POST: CustLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerInfo custLogin = db.CustomerInfoes.Find(id);
            custLogin = db.CustomerInfoes.Find(id);
            db.CustomerInfoes.Remove(custLogin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
