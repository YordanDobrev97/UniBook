using ConsoleClient.Seeding;
using System;
using System.IO;
using UniBook.Data;
using UniBook.Models;
using UniBook.Services;
using UniBook.Services.Interfaces;
using UniBook.Web.Data;

namespace ConsoleClient
{
    class Program
    {
        static void Main()
        {
            UniBookDbContext db = new UniBookDbContext();
            BookSeeder.Seed(db);
        }
    }
}