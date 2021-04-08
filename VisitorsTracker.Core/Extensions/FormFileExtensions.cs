using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace VisitorsTracker.Core.Extensions
{
    public static class FormFileExtensions
    {
        public const int ImageMinimumBytes = 4096;
        public static List<string> imageTypes = new List<string> 
        {   "image/jpg",
            "image/jpeg",
            "image/pjpeg",
            "image/gif",
            "image/x-png",
            "image/png" 
        };
        public static List<string> imageExtensions = new List<string>
        {
            ".jpg",
            ".png",
            ".gif",
            ".jpeg"
        };

        public static bool IsImage(this IFormFile postedFile)
        {
            // Check the image mime types
            if (!imageTypes.Any(x => string.Equals(postedFile.ContentType, x,
                StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            // Check the image extension
            if (!imageExtensions.Any(x => string.Equals(Path.GetExtension(postedFile.FileName), x, 
                StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            var stream = postedFile.OpenReadStream();
            // Attempt to read the file and check the first bytes
            try
            {
                if (!stream.CanRead)
                {
                    return false;
                }

                // check whether the image size exceeding the limit or not
                if (postedFile.Length < ImageMinimumBytes)
                {
                    return false;
                }

                byte[] buffer = new byte[ImageMinimumBytes];
                stream.Read(buffer, 0, ImageMinimumBytes);
                string content = Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, 
                    @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            // Try to instantiate new Bitmap, if .NET will throw exception
            // we can assume that it's not a valid image
            try
            {
                using var bitmap = new Bitmap(stream);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                stream.Position = 0;
            }

            return true;
        }
    }
}
