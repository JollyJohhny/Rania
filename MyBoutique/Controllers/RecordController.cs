using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyBoutique.Models;

namespace MyBoutique.Controllers
{
    public class RecordController : Controller
    {
        // GET: Record
        public ActionResult Index()
        {
            MyBoutiqueEntities db = new MyBoutiqueEntities();
            string id =User.Identity.GetUserId();
            var records = db.VenderorsRecords.Where(x => x.Vendor_Id ==id).ToList() ;
            List<RecordListViewModel> RecordList = new List<RecordListViewModel>();
            int Pending_Amount = 0;
            int Total_Purchasing = 0;
            int Paid_Amount = 0;
            foreach(var v in records)
            {
                RecordListViewModel r = new RecordListViewModel();
                r.Order_Id = v.Order_Id;
                r.Article_No = v.Article_No;
                r.No_Of_Suits =Convert.ToInt32(v.No_Of_Suits);
                r.Colors = Convert.ToInt32(v.Colors);
                r.Price = Convert.ToInt32(v.Price_For_Each);
                r.Added_On = Convert.ToDateTime(v.Added_On);
                r.ImagePath = v.Image_Path;
                r.Paid_Amount =Convert.ToInt32(v.Paid_Amount);
                r.Total_Price = r.Price * r.No_Of_Suits;
                r.Remaining_Amount = r.Total_Price - r.Paid_Amount;
                Pending_Amount = Pending_Amount + Convert.ToInt32(r.Remaining_Amount);
                Total_Purchasing = Total_Purchasing + Convert.ToInt32(r.Total_Price);
                Paid_Amount = Paid_Amount + Convert.ToInt32(v.Paid_Amount);
                RecordList.Add(r);
            }
            ViewBag.PendingAmount = Pending_Amount;
            ViewBag.TotalPurchasing = Total_Purchasing;
            ViewBag.PaidAmount = Paid_Amount;
            return View(RecordList);
        }

        public ActionResult Search()
        {
            MyBoutiqueEntities db = new MyBoutiqueEntities();
            List<DateTime> DatesList = new List<DateTime>();
            string id = User.Identity.GetUserId();
            var VenderorRecord = db.VenderorsRecords.Where(x => x.Vendor_Id == id).ToList();
            foreach(var v in VenderorRecord)
            {
                DateTime date = Convert.ToDateTime(v.Added_On);
                DatesList.Add(date);
            }

            List<string> ArticleNumbers = new List<string>();
            foreach(var v in VenderorRecord)
            {
                string ArticleNumber = v.Article_No;
                ArticleNumbers.Add(ArticleNumber);

            }
            DatesList.Sort();
            ArticleNumbers.Sort();
            ViewBag.ListDates = new SelectList(DatesList);
            ViewBag.ListArticles =new SelectList (ArticleNumbers);
            List<RecordListViewModel> empty = new List<RecordListViewModel>();
            return View(empty);
        }

        [HttpPost]
        public ActionResult SearchByDate(RecordListViewModel collection)
        {
            MyBoutiqueEntities db = new MyBoutiqueEntities();
            List<RecordListViewModel> RecordListByDate = new List<RecordListViewModel>();
            List<RecordListViewModel> RecordListByArticle = new List<RecordListViewModel>();
            string id = User.Identity.GetUserId();
            var VenderorRecord = db.VenderorsRecords.Where(x => x.Vendor_Id == id).ToList();
            

            

            
                var DateBasedList = db.VenderorsRecords.Where(x => x.Added_On == collection.DateList).ToList();
                
                foreach (var v in DateBasedList)
                {
                    RecordListViewModel r = new RecordListViewModel();
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

                    RecordListByDate.Add(r);
                }
                return View(RecordListByDate);
            
            
            
        }

        [HttpPost]
        public ActionResult SearchByArticle(RecordListViewModel collection)
        {
            MyBoutiqueEntities db = new MyBoutiqueEntities();
            List<RecordListViewModel> RecordListByDate = new List<RecordListViewModel>();
            List<RecordListViewModel> RecordListByArticle = new List<RecordListViewModel>();
            string id = User.Identity.GetUserId();
            var VenderorRecord = db.VenderorsRecords.Where(x => x.Vendor_Id == id).ToList();
           



            
                var ArticleBasedList = db.VenderorsRecords.Where(x => x.Article_No == collection.ArticleList).ToList();
                foreach (var v in ArticleBasedList)
                {
                    RecordListViewModel r = new RecordListViewModel();
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

                    RecordListByArticle.Add(r);
                }
                return View(RecordListByArticle);
            }


        
        // GET: Record/Details/5
        public ActionResult Details(int id)
        {
            MyBoutiqueEntities db = new MyBoutiqueEntities();
            //string VendorId = User.Identity.GetUserId();
            var v = db.VenderorsRecords.Where(x => x.Order_Id == id).First();
            RecordListViewModel r = new RecordListViewModel();
            r.Article_No = v.Article_No;
            r.No_Of_Suits = Convert.ToInt32(v.No_Of_Suits);
            r.Colors = Convert.ToInt32(v.Colors);
            r.Price = Convert.ToInt32(v.Price_For_Each);
            r.Added_On = Convert.ToDateTime(v.Added_On);
            r.ImagePath = v.Image_Path;
            r.Paid_Amount = Convert.ToInt32(v.Paid_Amount);
            r.Total_Price = r.Price * r.No_Of_Suits;
            r.Remaining_Amount = r.Total_Price - r.Paid_Amount;
            var Name = db.AspNetUsers.Where(x => x.Id == v.Vendor_Id).First();
            r.name = Name.UserName;
            ViewBag.pic = v.Image_Path;
            return View(r);
        }

        public ActionResult ChangeImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeImage(RegisterViewModel collection)
        {
            string filename = Path.GetFileNameWithoutExtension(collection.Image.FileName);
            string ext = Path.GetExtension(collection.Image.FileName);
            filename = filename + DateTime.Now.Millisecond.ToString();
            filename = filename + ext;
            string filetodb = "/Image/" + filename;
            filename = Path.Combine(Server.MapPath("~/Image/"), filename);
            collection.Image.SaveAs(filename);
            MyBoutiqueEntities db = new MyBoutiqueEntities();
            string id = User.Identity.GetUserId();
            var user = db.AspNetUsers.Where(x => x.Id == id).First();
            user.Image_Path = filetodb;
            db.SaveChanges();
            return RedirectToAction("Index", "Account", new { Message = "Image Updated" });
        }

        // GET: Record/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Record/Create
        [HttpPost]
        public ActionResult Create(RecordViewModel collection)
        {
            try
            {
                MyBoutiqueEntities db = new MyBoutiqueEntities();

                if (collection.Image != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(collection.Image.FileName);
                    string ext = Path.GetExtension(collection.Image.FileName);
                    filename = filename + DateTime.Now.Millisecond.ToString();
                    filename = filename + ext;
                    string filetodb = "/Image/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                    collection.Image.SaveAs(filename);
                    collection.ImagePath = filetodb;
                }
                else
                {
                    collection.ImagePath = "/Content/Images/dress.png";
                }


                VenderorsRecord record = new VenderorsRecord();
                string id = User.Identity.GetUserId();
                record.Vendor_Id = id;
                record.Article_No = collection.Article_No;
                record.No_Of_Suits = collection.No_Of_Suits;
                record.Colors = collection.Colors;
                record.Price_For_Each = collection.Price;
                record.Paid_Amount = collection.Paid_Amount;
                if (collection.Added_On == DateTime.MinValue)
                {
                    record.Added_On = DateTime.Now;
                }
                else
                {
                    record.Added_On = Convert.ToDateTime(collection.Added_On);
                }

                record.Image_Path = collection.ImagePath;
                db.VenderorsRecords.Add(record);
                db.SaveChanges();
                return RedirectToAction("Index", "Account", new { Message = "Added To Record" });
            }
            catch
            {
                return View();
            }
            
                
            
        }

        // GET: Record/Edit/5
        public ActionResult Edit(int id)
        {
            MyBoutiqueEntities db = new MyBoutiqueEntities();
            var v = db.VenderorsRecords.Where(x => x.Order_Id == id).First();
            RecordListViewModel r = new RecordListViewModel();
            r.Article_No = v.Article_No;
            r.No_Of_Suits = Convert.ToInt32(v.No_Of_Suits);
            r.Colors = Convert.ToInt32(v.Colors);
            r.Price = Convert.ToInt32(v.Price_For_Each);
            r.Added_On = Convert.ToDateTime(v.Added_On);
            r.ImagePath = v.Image_Path;
            r.Paid_Amount = Convert.ToInt32(v.Paid_Amount);
            r.Total_Price = r.Price * r.No_Of_Suits;
            r.Remaining_Amount = r.Total_Price - r.Paid_Amount;
            return View(r);
        }

        // POST: Record/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RecordListViewModel collection)
        {
            try
            {
                MyBoutiqueEntities db = new MyBoutiqueEntities();
                var v = db.VenderorsRecords.Where(x => x.Order_Id == id).First();
                if (collection.Image != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(collection.Image.FileName);
                    string ext = Path.GetExtension(collection.Image.FileName);
                    filename = filename + DateTime.Now.Millisecond.ToString();
                    filename = filename + ext;
                    string filetodb = "/Image/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                    collection.Image.SaveAs(filename);
                    collection.ImagePath = filetodb;
                }
                else
                {
                    collection.ImagePath = v.Image_Path;
                }
                if(collection.Added_On == DateTime.MinValue)
                {
                    collection.Added_On = DateTime.Now.Date;
                }
                else
                {
                    collection.Added_On =Convert.ToDateTime(v.Added_On);
                }
                RecordListViewModel r = new RecordListViewModel();
                v.Article_No = collection.Article_No;
                v.No_Of_Suits = Convert.ToInt32(collection.No_Of_Suits);
                v.Colors = Convert.ToInt32(collection.Colors);
                v.Price_For_Each = Convert.ToInt32(collection.Price);
                v.Added_On = Convert.ToDateTime(collection.Added_On);
                v.Image_Path = collection.ImagePath;
                v.Paid_Amount = Convert.ToInt32(collection.Paid_Amount);
                v.Total_Price = v.Price_For_Each * v.No_Of_Suits;
                v.Remaining_Amount = v.Total_Price - v.Paid_Amount;
                db.SaveChanges();
                return RedirectToAction("Index", "Account", new { Message = "Record Updated" });
            }
            catch
            {
                return View();
            }
        }

        // GET: Record/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Record/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                MyBoutiqueEntities db = new MyBoutiqueEntities();
                var ent = db.VenderorsRecords.Where(x => x.Order_Id == id).First();
                db.Entry(ent).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();

                return RedirectToAction("Index", "Account", new { Message = "Record Deleted" });
            }
            catch
            {
                return View();
            }
        }
    }
}
