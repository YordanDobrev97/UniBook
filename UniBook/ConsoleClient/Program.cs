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
            var book = File.ReadAllText("../../../Keri.txt");

            db.Books.Add(new Book
            {
                Title = "Кери",
                Author = new Author
                {
                    Name = "Стивън Кинг"
                },
                ImgUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/a4e40ebdc3e371adff845072e1c73f37/k/e/bfda8cd69bf5273ae8371e3e7f60ddd6/keri-31.jpg",
                Rating = 0,
                Content = book
            });

            db.SaveChanges();
        }
    }
}
