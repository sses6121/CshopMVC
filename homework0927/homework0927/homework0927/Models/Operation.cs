using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using homework0927.Models;

namespace homework0927.Models
{
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
}