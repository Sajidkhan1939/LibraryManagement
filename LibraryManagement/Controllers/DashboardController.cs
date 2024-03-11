using LibraryManagement.Models;
using LibraryManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class DashboardController : Controller
    {
        
        private readonly BookRepositories repo;
        
        public DashboardController()
        {
            repo = new BookRepositories();
        }
        [Authorize]
        [Authorize(Roles = "Admin")]
        public ActionResult DashboardPage()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult GetBooksDetails()
        {
            try
            {
                var detail = repo.GetBooksDetails();
                return Json(detail, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult GetDetails()
        {
            try
            {
                var detail = repo.GetDetails();
                return Json(detail, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Books()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult GetallBooks()
        {
            var res =repo.AllBooks();
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        
        [Authorize(Roles = "Admin")]
        public ActionResult AddBooks(BooksViewModel dto)
        {
            try
            {
                var userId = "";
                if (User.Identity.IsAuthenticated)
                {
                    userId = User.Identity.GetUserId();
                }
                dto.UserId = userId;
                string imageUrl = null;
                // Get the uploaded file
                HttpPostedFileBase imageFile = Request.Files["imagefile"];
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    // Save the image file and get the URL
                    imageUrl = SaveImage(imageFile);
                }

                int result = repo.AdBook(dto, imageUrl);
                if (result == 1)
                {
                    return Json(new { success = true, message = "Book added successfully" });
                }
                else
                {
                    return Json(new { success = true, message = "Book added successfully" });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Something wrong " + ex.Message });
            }
            
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteBook(int Book_ID)
        {
            try
            {
                repo.DeleteBook(Book_ID);
                return Json(new { success = true, message = "Book deleted successfully" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActionResult IssueBook(int id)
        {
            try
            {
                BooksViewModel dto = new BooksViewModel();
                var userId = "";
                if (User.Identity.IsAuthenticated)
                {
                    userId = User.Identity.GetUserId();
                }
                dto.UserId = userId;
                dto.Book_ID = id;
                repo.IssueBook(dto);
                return Json(new { bookId = dto.Book_ID });
            }
            catch (Exception ex)
            {
                return Json(new { error = "Failed to issue book", message = ex.Message });               
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult RegisterUsers()
        {
            return View();
        }
        public ActionResult GetAdminUsers()
        {
            var res = repo.GetAdminUsers();
            return Json(res,JsonRequestBehavior.AllowGet);
        }
        private string SaveImage(HttpPostedFileBase imageFile)
        {
            if (imageFile == null || imageFile.ContentLength == 0)
            {
                throw new ArgumentException("Image file is empty or not provided.");
            }

            // Generate a unique file name for the image
            string fileName = Path.GetFileName(imageFile.FileName);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

            // Specify the directory where you want to save the image file
            string uploadDir = "~/Images/BookPhotos/";
            string filePath = Path.Combine(uploadDir, uniqueFileName);

            // Save the image file to the specified path
            imageFile.SaveAs(Server.MapPath(filePath));

            // Return the URL of the saved image
            return "/Images/BookPhotos/" + uniqueFileName;
        }
    }
}