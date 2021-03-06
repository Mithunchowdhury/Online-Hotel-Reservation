﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineHotelReservationSystem.Areas.Admin.Models
{
    public class ImageMetaData
    {
    }

    [MetadataType(typeof(ImageMetaData))]
    public partial class ImagGallary
    {

        [ValidateFile]
        public HttpPostedFileBase file { get; set; }
    }
    public class ValidateFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int MaxContentLength = 1024 * 1024 * 3; //3 MB
            string[] AllowedFileExtensions = new string[] { ".jpg" };

            var file = value as HttpPostedFileBase;

            if (file == null)
                return false;

            if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = "Please upload Your Photo of type: " + string.Join(", ", AllowedFileExtensions);
                return false;
            }

            if (file.ContentLength > MaxContentLength)
            {
                ErrorMessage = "Your Photo is too large, maximum allowed size is : " + (MaxContentLength / 1024).ToString() + "MB";
                return false;
            }


            return true;
        }

    }
}