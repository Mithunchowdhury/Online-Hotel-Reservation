using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineHotelReservationSystem.Areas.Admin.Models;

namespace OnlineHotelReservationSystem.Controllers
{
    public class ImageGalleryController : Controller
    {
        ImageGalleryDbContext db = new ImageGalleryDbContext();
        public ActionResult Gallery()
        {
          
            return View( db.ImagGallaries.ToList());
        }

    }
}
