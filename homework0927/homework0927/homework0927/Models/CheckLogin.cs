using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using homework0927.Models;
using System.Text.RegularExpressions;

namespace homework0927.Models {
        public class Check { 
        //public int Custchecklogin(CustLogin custlogin,string x,string y) {
        //StockEntities db = new StockEntities();
        //    int z=0;
        //        var query = from o in db.CustLogins
        //            where o.CustLoginName == custlogin.CustLoginName
        //            select o;
        //    try {
        //        List<CustLogin> lgin = query.ToList();
        //        if (lgin.Count() > 0) {
        //            if (custlogin.CustPassword == lgin[0].CustPassword)
        //                return z=1;
        //        }
        //    } catch { return z=0; }
        //    return z=-1;
        //}
        //catch (Exception ex) {
        //    return false;
        //    }
        //    return false;
        //}

        public bool checkCellPhone(CustomerInfo customerInfo) {
            StockEntities db = new StockEntities();
            if (Regex.Match(customerInfo.CustCellPhone, @"^\d{ 10,}$").Success)
                return true;
            else
                return false;
        }
    }
}