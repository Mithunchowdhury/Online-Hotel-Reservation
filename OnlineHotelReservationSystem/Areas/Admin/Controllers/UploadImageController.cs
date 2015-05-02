using System;

using System.Linq;

using System.Web.Mvc;
using OnlineHotelReservationSystem.Areas.Admin.Models;
using PagedList;

namespace OnlineHotelReservationSystem.Areas.Admin.Controllers
{
    public class UploadImageController : Controller
    {
       
        ImageGalleryDbContext db = new ImageGalleryDbContext();

        public ActionResult Upload(int? page)
        {
            ViewData["users"] = db.ImagGallaries.ToList().ToPagedList(page ?? 1, 4);
            return View(db.ImagGallaries.ToList().ToPagedList(page ?? 1, 4));
        }
        [HttpPost]
        public ActionResult Upload(ImagGallary imageGallary, int? page)
        {
            if (imageGallary.file == null)
            {
                Response.Write("<div id='uploadImageError'>");
                Response.Write("* You have to select a photo of type jpg");
                Response.Write("</div>");
            }
            if (imageGallary.Description == null)
            {
                Response.Write("<div id='uploadImageErro'>");
                Response.Write("* You must provide a very short description about the Image");
                Response.Write("</div>");
            }
            if (imageGallary.file != null && imageGallary.Description != null)
            {
                try
                {
                    byte[] data = new byte[imageGallary.file.ContentLength];
                    imageGallary.file.InputStream.Read(data, 0, imageGallary.file.ContentLength);
                    imageGallary.Image = data;

                    db.ImagGallaries.Add(imageGallary);
                    db.SaveChanges();
                    imageGallary.file = null;

                    ViewData["users"] = db.ImagGallaries.ToList().ToPagedList(page ?? 1, 4);


                    return View(db.ImagGallaries.ToList().ToPagedList(page ?? 1, 4));

                }
                catch (Exception)
                {
                    Response.Write("<div id='uploadImageError'>");
                    Response.Write("* Sorry,You are allowed to Upload only JPG type Picture .If Your picture is jpg typed then make sure that it is less than 2MB in size");
                    Response.Write("</div>");
                }
            }


            ViewData["users"] = db.ImagGallaries.ToList().ToPagedList(page ?? 1, 4);
            return View(db.ImagGallaries.ToList().ToPagedList(page ?? 1, 4));
        }

        public ActionResult Delete(int id)
        {

            ImagGallary imagId = db.ImagGallaries.Where(m => m.ID == id).FirstOrDefault();

            if (imagId != null)
            {

                try
                {

                    db.ImagGallaries.Remove(imagId);

                    db.SaveChanges();

                }

                catch
                {
                }

            }

            return RedirectToAction("Upload", "UploadImage");
        }


    }
}
