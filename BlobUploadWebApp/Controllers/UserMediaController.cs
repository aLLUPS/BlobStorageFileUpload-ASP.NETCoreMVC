using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using BlobUploadWebApp.Data;
using BlobUploadWebApp.Models;
using BlobUploadWebApp.Utilities;


namespace BlobUploadWebApp.Controllers
{
    public class UserMediaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserMediaController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            utility = new BlobUtility(accountName, accountKey);
        }

        BlobUtility utility;
        string accountName = "StorageAccountName"; // put your storage account Name here
        string accountKey = ""; // put your storage account access key here

        [Authorize]
        public IActionResult MediaFileUpload()
        {
            string loggedInUserId = _userManager.GetUserId(User);
            List<UserMedium> userMedia = (from a in _context.UserMedia where a.UserId == loggedInUserId select a).ToList();
            ViewBag.PhotoCount = userMedia.Count;
            return View(userMedia);
        }

        [Authorize]
        public ActionResult DeleteMediaFile(int id)
        {
            UserMedium userMedium = _context.UserMedia.Find(id);
            _context.UserMedia.Remove(userMedium);
            _context.SaveChanges();
            string BlobNameToDelete = userMedium.MediaUrl.Split('/').Last();
            utility.DeleteBlob(BlobNameToDelete, "imagecontainer");            // container name
            return RedirectToAction("MediaFileUpload");                             // return page
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadMediaFile(IFormFile file)
        {

            if (file != null)
            {
                string ContainerName = "imagecontainer";            // container name. 
                string fileName = Path.GetFileName(file.FileName);
                Stream imageStream = file.OpenReadStream();
                var result = utility.UploadBlobAsync(fileName, ContainerName, (IFormFile)file);
                if (result != null)
                {
                    string loggedInUserId = _userManager.GetUserId(User);
                    UserMedium usermedium = new UserMedium();

                    try
                    {
                        usermedium.UserId = loggedInUserId;                                 //If the User ID is an int then, Int32.Parse(loggedInUserId);
                        usermedium.MediaUrl = result.Result.Uri.ToString();                 // to insert the url of the blob to the database
                        usermedium.MediaFileName = result.Result.Name;                      // to insert the media file name to the database
                        usermedium.MediaFileType = result.Result.Name.Split('.').Last();    // to insert the media file type to the database
                    }
                    catch
                    {
                        Console.WriteLine($"Unable to parse '{loggedInUserId}'");
                    }

                    _context.UserMedia.Add(usermedium);
                    _context.SaveChanges();
                    return RedirectToAction("MediaFileUpload");
                }
                else
                {
                    return RedirectToAction("MediaFileUpload");
                }
            }
            else
            {
                return RedirectToAction("MediaFileUpload");
            }
        }
    }
}
