
using System.Linq;
using System.Web.Mvc;
using OnlineHotelReservationSystem.Areas.Admin.Models;

namespace OnlineHotelReservationSystem.Areas.Admin.Controllers
{
    public class AdminLogOnController : Controller
    {
       

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogOn(Models.Admin admin)
        {
            if (ModelState.IsValid)
            {
                using (AdminDBContext db=new AdminDBContext())
                {
                    var u =
                        db.Admins.FirstOrDefault(
                            adminCheck => adminCheck.Email == admin.Email && adminCheck.Password == admin.Password);

                    if (u!=null)
                    {
                        return RedirectToAction("ReservationInfoList", "ReservationDeatils", new { area = "Admin" });
                    }
                    else
                    {
                        ModelState.AddModelError("","Invalid Email and Password Combination!!");
                    }
                }
            }

            return View();
        }
    }
}
