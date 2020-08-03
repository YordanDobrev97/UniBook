using System;
using System.IO;
using UniBook.Data;
using UniBook.Models;
using UniBook.Services;
using UniBook.Services.Interfaces;

namespace ConsoleClient
{
    class Program
    {
        static void Main()
        {
            var db = new UniBookDbContext();
            IBookService bookService = new BookService(db);

            var currentBook = bookService.FindById(1);

            Console.WriteLine(currentBook.Title);
        }
    }
}
