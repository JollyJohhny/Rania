using Microsoft.AspNet.Identity;
using MyBoutique.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBoutique.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            MyBoutiqueEntities db = new MyBoutiqueEntities();
            var total = db.AspNetUsers.ToList();
            int count = total.Count;
            
            ViewBag.Count = count;
            return View();
        }

        public List<DateTime> LastSevenDays()
        {

            List<DateTime> daterange = new List<DateTime>();
            for (int i = -6; i < 0; i++)
            {
                DateTime t = DateTime.Now.Date;
                daterange.Add(t.AddDays(i).Date);


            }
            DateTime today = DateTime.Now.Date;
            daterange.Add(today.Date);

            return daterange;
        }

        public ActionResult OrdersThisWeek()
        {
            MyBoutiqueEntities db = new MyBoutiqueEntities();
            var AllOrders = db.VenderorsRecords.ToList();
            List<RecordListViewModel> List = new List<RecordListViewModel>();
            List<DateTime> ThisWeek = LastSevenDays();
            foreach(var v in AllOrders)
            {
                foreach(DateTime Date in ThisWeek)
                {
                    if(v.Added_On == Date)
                    {
                        RecordListViewModel r = new RecordListViewModel();
                        foreach(AspNetUser vendor in db.AspNetUsers)
                        {
                            if(vendor.Id == v.Vendor_Id)
                            {
                                r.name = vendor.UserName;
                                break;
                            }
                        }
                        r.Order_Id = v.Order_Id;
                        r.Article_No = v.Article_No;
                        r.No_Of_Suits = Convert.ToInt32(v.No_Of_Suits);
                        r.Colors = Convert.ToInt32(v.Colors);
                        r.Price = Convert.ToInt32(v.Price_For_Each);
                        r.Added_On = Convert.ToDateTime(v.Added_On);
                        r.ImagePath = v.Image_Path;
                        r.Paid_Amount = Convert.ToInt32(v.Paid_Amount);
                        r.Total_Price = r.Price * r.No_Of_Suits;
                        r.Remaining_Amount = r.Total_Price - r.Paid_Amount;

                        List.Add(r);
                    }
                }
            }
            return View(List);
        }



        // GET: Dashboard/Details/5
        public ActionResult ShowAll()
        {
            MyBoutiqueEntities db = new MyBoutiqueEntities();
            var ListOfAll = db.AspNetUsers.ToList();
            List<RegisterViewModel> list = new List<RegisterViewModel>();
            foreach(var v in ListOfAll)
            {
                RegisterViewModel Vendor = new RegisterViewModel();
                Vendor.Id = v.Id;
                Vendor.Name = v.UserName;
                Vendor.Joining_Date = Convert.ToDateTime(v.Joining_Date);
                Vendor.ImagePath = v.Image_Path;
                list.Add(Vendor);
            }
            return View(list);
        }

        // GET: Dashboard/SearchName
        public ActionResult SearchName()
        {
            MyBoutiqueEntities db = new MyBoutiqueEntities();
            List<DateTime> DatesList = new List<DateTime>();
            
            var VenderorRecord = db.VenderorsRecords.ToList();
            foreach (var v in VenderorRecord)
            {
                DateTime date = Convert.ToDateTime(v.Added_On);
                DatesList.Add(date);
            }

            List<string> ArticleNumbers = new List<string>();
            foreach (var v in VenderorRecord)
            {
                string ArticleNumber = v.Article_No;
                ArticleNumbers.Add(ArticleNumber);

            }
            DatesList.Sort();
            ArticleNumbers.Sort();
            ViewBag.ListDates = new SelectList(DatesList);
            ViewBag.ListArticles = new SelectList(ArticleNumbers);
            return View();
        }

        // POST: Dashboard/SearchName
        [HttpPost]
        public ActionResult SearchName(RecordListViewModel collection)
        {
               MyBoutiqueEntities db = new MyBoutiqueEntities();
                List<RecordListViewModel> RecordListByArticle = new List<RecordListViewModel>();

            List<DateTime> DatesList = new List<DateTime>();

            var VenderorRecord = db.VenderorsRecords.ToList();
            foreach (var v in VenderorRecord)
            {
                DateTime date = Convert.ToDateTime(v.Added_On);
                DatesList.Add(date);
            }

            List<string> ArticleNumbers = new List<string>();
            foreach (var v in VenderorRecord)
            {
                string ArticleNumber = v.Article_No;
                ArticleNumbers.Add(ArticleNumber);

            }
            DatesList.Sort();
            ArticleNumbers.Sort();
            
            ViewBag.ListArticles = new SelectList(ArticleNumbers);

            var ArticleBasedList = db.VenderorsRecords.Where(x => x.Article_No == collection.ArticleList).ToList();
                
                foreach (var v in ArticleBasedList)
                {
                    foreach(AspNetUser vendor in db.AspNetUsers)
                    {
                        if(vendor.Id == v.Vendor_Id)
                        {
                            ViewBag.Name = vendor.UserName;
                            ViewBag.Id = v.Order_Id;
                            ViewBag.Article_No = v.Article_No;
                            ViewBag.No_Of_Suits = Convert.ToInt32(v.No_Of_Suits);
                            ViewBag.Colors= Convert.ToInt32(v.Colors);
                            
                            ViewBag.Price_For_Each = Convert.ToInt32(v.Price_For_Each);
                            ViewBag.Date_Of_Delivery = Convert.ToDateTime(v.Added_On);
                            ViewBag.Image = v.Image_Path;
                            ViewBag.Paid_Amount = Convert.ToInt32(v.Paid_Amount);
                            ViewBag.Date = Convert.ToDateTime(v.Added_On);
                            ViewBag.Total_Price = (Convert.ToInt32(v.Price_For_Each)) *(Convert.ToInt32(v.No_Of_Suits));
                            ViewBag.Remaining_Amount = ViewBag.Total_Price - ViewBag.Paid_Amount;
                        break;
                            
                        }
                    }
                    
                }
                
                return View();

               
            
        }



        



        // GET: Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Edit/5
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

        // GET: Dashboard/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Dashboard/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                MyBoutiqueEntities db = new MyBoutiqueEntities();
                var vendor = db.AspNetUsers.Where(x => x.Id == id).First();
                db.Entry(vendor).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
