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

        public IActionResult Index(int id)
        {
            var currentBook = this.db.Books.FirstOrDefault(b => b.Id == id);
            ViewData.Model = currentBook;
            ViewData["Start"] = 0;
            
            return View(currentBook);
        }

        public IActionResult All()
        {
            return View();
        }
    }
}
