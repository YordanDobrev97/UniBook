using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniBook.Data;
using UniBook.Web.Data;

namespace UniBook.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext db;

        public BookController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var firstBook = db.Books.FirstOrDefault();
            ViewData.Model = firstBook;

            var partContent = firstBook.Content.Substring(0, 200);
            return View(firstBook);
        }

        public IActionResult All()
        {
            return View();
        }
    }
}
