namespace UniBook.Services.Data
{
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;
    using System.Linq;
    using UniBook.Data.Common.Repositories;
    using UniBook.Data.Models;
    using UniBook.Services.Mapping;
    using UniBook.Web.ViewModels;

    public class BookService : IBookService
    {
        private readonly IDeletableEntityRepository<Book> repository;
        
        public BookService(IDeletableEntityRepository<Book> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<string> All()
        {
            var allBooks = this.repository
                .All()
                .Select(b => b.ImageUrl)
                .ToList();

            return allBooks;
        }
    }
}
