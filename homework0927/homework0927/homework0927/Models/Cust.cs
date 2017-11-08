using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homework0927.Models
{
    public class Cust
    {
        StockEntities db = new StockEntities();

        public int UserId { get;  set; }

        public string GetUrderName(int UserId)
        {
            //使用Order類別中的UserId到AspNetUsers資料表中搜尋出UserName
            
                var result = (from s in db.CustomerInfoes
                              where s.CustId == this.UserId
                              select s.CustName).FirstOrDefault();

                //回傳找到的UserName
                return result;
            
        }

        public string GetUrderCellPhone(int UserId)
        {
            //使用Order類別中的UserId到AspNetUsers資料表中搜尋出UserName
            //using (Models.StockEntities db = new StockEntities()) {
                var result = (from s in db.CustomerInfoes
                              where s.CustId == this.UserId
                              select s.CustCellPhone).FirstOrDefault();

                //回傳找到的UserName
                return result;
            //}
        }

        public string GetUrderAddress(int UserId)
        {
            //使用Order類別中的UserId到AspNetUsers資料表中搜尋出UserName
            //using (Models.StockEntities db = new StockEntities()) {
                var resultPost = (from s in db.CustomerInfoes
                                    where s.CustId == this.UserId
                                    select s.PostCategory).FirstOrDefault();
                var resultCountryName = (from s in db.PostInfoes
                                         where s.PostId == resultPost
                                         select s.PostName).FirstOrDefault();
                var result = resultCountryName;
                    result += (from s in db.CustomerInfoes
                              where s.CustId == this.UserId
                              select s.CustRoad).FirstOrDefault();

                //回傳找到的UserName
                return result;
            //}
        }

        public int GetUrderOrderId(int UserId)
        //public List<int> GetUrderOrderId(int UserId)
        {
            //使用Order類別中的UserId到AspNetUsers資料表中搜尋出UserName

            var result = (from s in db.VW_OrderAndDetail
                          where s.CustId == this.UserId
                          select s.OrderId).FirstOrDefault();

            //回傳找到的UserName
            return result;

        }

        public string GetUrderProd(int ProdId)
        //public List<string> GetUrderProd(int UserId)

        {
            //使用Order類別中的UserId到AspNetUsers資料表中搜尋出UserName
            //List < string > result=null;
            var result= (from s in db.ProdInfoes
                          where s.ProdId == ProdId
                          select s.ProdName).FirstOrDefault();
            
            ////for (int i = 0; i < resultProdId.Count(); i++) {
            // var   result = (from s in db.ProdInfoes
            //              where s.ProdId == resultProdId//Convert.ToInt32(resultProdId)//[i]
            //              select s.ProdName).FirstOrDefault();
            //}
            //回傳找到的UserName
            return result;
            

        }

        public int GetOrderDetailTotal(int ProdPrice,int ProdQuantity)
        {
            int sum = 0;
            sum = ProdPrice * ProdQuantity;
            return sum;
        }


}
}