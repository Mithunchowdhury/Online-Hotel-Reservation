using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using OnlineHotelReservationSystem.Areas.Customer.Models;


namespace OnlineHotelReservationSystem.Areas.Customer.Controllers
{
    public class CustomerReservationController : Controller
    {
        ReservationDbContext db = new ReservationDbContext();
        public ActionResult Reserve()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Reserve(Reservation reservation)
        {
            Regex regForPhone = new Regex(@"[01]\d{9}");
            if (ModelState.IsValid)
            {
                try
                {
                if (reservation.Country == "Bangladesh")
                {
                    int banPhoneNum = Convert.ToInt32(reservation.Phonenumber);
                    if (regForPhone.IsMatch(banPhoneNum.ToString()))
                    {
                        if (reservation.Standard != null || reservation.Suit != null || reservation.Deluxi != null ||
                     reservation.Delux != null)
                        {

                            db.Reservations.Add(reservation);
                            db.SaveChanges();
                            return RedirectToAction("SuccessfulMessage", "CustomerReservation", new { area = "Customer" });
                        }
                        if (reservation.Standard == null || reservation.Suit == null || reservation.Deluxi == null ||
                 reservation.Delux == null)
                        {

                            Response.Write("<div id='resRoomReq'>");
                            Response.Write("You Have To Select at least one Room type from four");
                            Response.Write("</div>");

                        }
                    }
                    else
                    {
                        Response.Write("<div id='resRoomReq' >");
                        Response.Write("Sorry,Your Provided Phone Number is not a valid Bangladeshi Phone Number.For this you must enter 11 digit valid number");
                        Response.Write("</div>");

                    }
                }

                else
                {
                    if (reservation.Standard != null || reservation.Suit != null || reservation.Deluxi != null ||
                  reservation.Delux != null)
                    {

                        db.Reservations.Add(reservation);
                        db.SaveChanges();
                        return RedirectToAction("SuccessfulMessage", "CustomerReservation", new { area = "Customer" });
                    }
                    if (reservation.Standard == null || reservation.Suit == null || reservation.Deluxi == null ||
             reservation.Delux == null)
                    {

                        Response.Write("<div id='resRoomReq'>");
                        Response.Write("You Have To Select at least one Room");
                        Response.Write("</div>");

                    }
                }






                 }

                catch (Exception msg)
                {


                    Response.Write("<div id='resRoomReq'>");
                    Response.Write(msg.Message);
                    Response.Write("</div>");
                }
            }

            return View();
        }

        public ActionResult SuccessfulMessage()
        {
            return View();
        }
        public ActionResult RoomDeatils()
        {
            return View();
        }


        public JsonResult CheckIsEmailExist(string email)
        {

            return Json(!db.Reservations.Any(x => x.Email.Equals(email)), JsonRequestBehavior.AllowGet);
        }
    }
}
