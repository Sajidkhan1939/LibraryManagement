using LibraryManagement.Models;
using LibraryManagementSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepositories repo;
        private readonly BookRepositories bookrepo;

        public UserController()
        {
            repo = new UserRepositories();
            bookrepo = new BookRepositories();
        }
        [Authorize(Roles ="User")]
        public ActionResult BooksCart()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Main()
        {
            return View();
        }
        public ActionResult GetallBooks(BooksViewModel dto)
        {
            var res = repo.AllBooks(dto);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "User")]
        public ActionResult GetBook(BooksViewModel dto)
        {            
            var res = repo.AllBooks(dto);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "User")]
        public ActionResult UserissuedBooks(BooksViewModel dto)
        {
            var id = "";
            if (User.Identity.IsAuthenticated)
            {
                id = User.Identity.GetUserId();
            }
            dto.UserId = id;
            var res = repo.AllUserBooks(dto);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "User")]
        public ActionResult UserBooks()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public ActionResult ReturnBook(int id)
        {
            var res = repo.ReturnBook(id);
            Console.WriteLine(res);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}