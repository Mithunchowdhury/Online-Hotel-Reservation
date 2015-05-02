using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineHotelReservationSystem.Areas.Customer.Models;
using OnlineHotelReservationSystem.Models;
using PagedList;

namespace OnlineHotelReservationSystem.Areas.Admin.Controllers
{
    public class ReservationDeatilsController : Controller
 {
        ReservationDbContext db = new ReservationDbContext();

     public ActionResult ReservationInfoList(int? page)
     {
         
         return View(db.Reservations.OrderBy(name => name.Fullname).ToList().ToPagedList(page ?? 1, 3));
        
     }

     public ActionResult All(int? page)
     {


         return View("_All", db.Reservations.ToList().ToPagedList(page ?? 1, 3));
     }

     [HttpPost]
     public ActionResult ReservationInfoList(int? page, string searchName)
     {
         if (searchName.Length >= 2)
         {

             TempData["Searchname"] = "for Name" + " => " + searchName;
             return View(db.Reservations.Where(x => x.Fullname.StartsWith(searchName) || searchName == null).OrderBy(name => name.Fullname).ToList().ToPagedList(page ?? 1, 15));

         }

         if (searchName.Length < 2)
         {
             TempData["Searchname"] = "Please Enter minimum 2 Characters to make a Successful Search";
         }

         return View(db.Reservations.Where(x => x.Fullname.StartsWith(searchName) || searchName == null).OrderBy(name => name.Fullname).ToList().ToPagedList(page ?? 1, 15));


     }

     public ActionResult EditList(int id = 0)
     {
         Reservation singleListInfo = db.Reservations.Find(id);
         if (singleListInfo == null)
         {
             return HttpNotFound();
         }
         return PartialView("_EditList", singleListInfo);


     }
     [AcceptVerbs(HttpVerbs.Post)]
     public ActionResult EditList(int id, String cmd)
     {
         Reservation reserinfo = new Reservation();
         reserinfo = db.Reservations.Single(x => x.Id.Equals(id));
         UpdateModel(reserinfo, new string[] { "Standard", "Suit", "Deluxi", "Delux" });
         if (ModelState.IsValid)
         {

             if (cmd == "Save")
             {

                 try
                 {

                     db.Reservations.Add(reserinfo);

                     db.SaveChanges();

                     return RedirectToAction("ReservationInfoList");

                 }

                 catch { }

             }

             else
             {

                 try
                 {

                     Reservation singleInfo = db.Reservations.Where(m => m.Id == reserinfo.Id).FirstOrDefault();

                     if (singleInfo != null)
                     {
                         singleInfo.Standard = reserinfo.Standard;
                         singleInfo.Suit = reserinfo.Suit;
                         singleInfo.Deluxi = reserinfo.Deluxi;
                         singleInfo.Delux = reserinfo.Delux;
                         db.SaveChanges();

                     }

                     return RedirectToAction("ReservationInfoList");

                 }

                 catch { }

             }

         }



         if (Request.IsAjaxRequest())
         {

             return PartialView("_EditList", reserinfo);

         }

         else
         {

             return View("EditList", reserinfo);

         }
     }


     public ActionResult Details(int id)
     {

         Reservation singledetails = db.Reservations.Where(m => m.Id == id).FirstOrDefault();

         if (singledetails != null)
         {

             if (Request.IsAjaxRequest())
             {

                 return PartialView("_Details", singledetails);

             }

             else
             {

                 return View("Details", singledetails);

             }

         }

         return View("ReservationInfoList");

     }

     public JsonResult GetCustomers(string term)
     {
         
         List<string> customers;
         customers = db.Reservations.Where(x => x.Fullname.StartsWith(term))
.Select(y => y.Fullname).Distinct().ToList();


         return Json(customers, JsonRequestBehavior.AllowGet);
     }

     public ActionResult Delete(int id)
     {

         Reservation customerId = db.Reservations.Where(m => m.Id == id).FirstOrDefault();

         if (customerId != null)
         {

             try
             {

                 db.Reservations.Remove(customerId);

                 db.SaveChanges();

             }

             catch { }

         }

         return RedirectToAction("ReservationInfoList");

     }

        public ActionResult V()
        {
            return View();
        }

    }
}
