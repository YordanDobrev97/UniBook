using System;
using System.IO;
using UniBook.Data;
using UniBook.Models;

namespace ConsoleClient
{
    class Program
    {
        static void Main()
        {
            var db = new UniBookDbContext();
            //var book = File.ReadAllText("../../../PetdesetNuansaPoTumno.txt");

            //db.Books.Add(new Book
            //{
            //    Title = "Петдесет нюанса по-тъмно",
            //    Author = new Author
            //    {
            //        Name = "Е. Л. Джеймс"
            //    },
            //    ImgUrl = "https://i.ytimg.com/vi/wrZFTAbWuaw/maxresdefault.jpg",
            //    Rating = 0,
            //    Content = book
            //});

            //db.SaveChanges();
        }
    }
}
