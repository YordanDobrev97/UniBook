namespace UniBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper.QueryableExtensions;
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

        public IEnumerable<ListAllBooksViewModel> All()
        {
            var allBooks = this.repository
                .All()
                .Select(b => new ListAllBooksViewModel
                {
                    ImageUrl = b.ImageUrl,
                    Id = b.Id,
                }).ToList();

            return allBooks;
        }

        public DetailsBookViewModel Details(int id)
        {
            var book = this.repository.All()
                .Where(x => x.Id == id)
                .Select(e => new DetailsBookViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Author = e.Author.Name,
                    ImageUrl = e.ImageUrl,
                    Description = e.Description,
                }).FirstOrDefault();

            return book;
        }

        public ContentBookViewModel ReadBook(int id)
        {
            var book = this.repository.All()
                .Where(b => b.Id == id)
                .Select(b => new ContentBookViewModel
                {
                    Title = b.Name,
                    Content = b.Body,
                }).FirstOrDefault();

            return book;
        }
    }
}
