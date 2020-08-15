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

            var topLikedBooks = bookService.Top50LikedBooks();

            foreach (var book in topLikedBooks)
            {
                Console.WriteLine(book.Title);
            }
        }
    }
}