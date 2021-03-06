﻿namespace UniBook.Web.Controllers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels;

    public class SearchController : BaseController
    {
        private const int MaxBooks = 9;
        private readonly IBookService service;
        private string resultView = "Views/Books/All.cshtml";

        public SearchController(IBookService service)
        {
            this.service = service;
        }

        public IActionResult Index(SearchBookViewModel search, int? id = 1)
        {
            if (search.Author != null)
            {
                return this.RedirectToAction("SearchByAuthor", new { id = id, search = search.Author });
            }

            if (search.BookName != null)
            {
                return this.RedirectToAction("SearchByBookName", new { id = id, search = search.BookName });
            }

            if (search.Year != 0)
            {
                return this.RedirectToAction("SearchByYear", new { id = id, search = search.Year });
            }

            if (search.Genre != null)
            {
                return this.RedirectToAction("SearchByGenre", new { id = id, search = string.Join("&", search.Genre) });
            }

            if (search.FreeBook != null)
            {
                return this.RedirectToAction("SearchByFreeBooks", new { search = id });
            }

            if (search.PaidBook != null)
            {
                return this.RedirectToAction("SearchByPaidBook", new { search = id });
            }

            return this.View("Error");
        }

        public IActionResult SearchByBookName(int id, string search)
        {
            var books = this.service.SearchByBook(search).ToList();

            var result = this.PaginationBooks<ListAllBooksViewModel>(id, books, MaxBooks);
            var viewModel = new BooksListViewModel
            {
                Books = result,
                Years = this.service.GetYears(),
                Genres = this.service.GetGenres(),
                PaginationViewModel = new PaginationViewModel
                {
                    CurrentPage = id,
                    PagesCount = (int)Math.Ceiling(books.Count() / (decimal)MaxBooks),
                    DataCount = books.Count(),
                    Controller = "Search",
                    Action = "SearchByBookName",
                    Search = search,
                },
            };

            return this.View(this.resultView, viewModel);
        }

        public IActionResult SearchByAuthor(int id, string search)
        {
            var authorBooks = this.service.GetAuthorBooks(search).ToList();
            var result = this.PaginationBooks<ListAllBooksViewModel>(id, authorBooks, MaxBooks);
            var viewModel = new BooksListViewModel
            {
                Books = result,
                Years = this.service.GetYears(),
                Genres = this.service.GetGenres(),
                PaginationViewModel = new PaginationViewModel
                {
                    CurrentPage = id,
                    PagesCount = (int)Math.Ceiling(authorBooks.Count() / (decimal)MaxBooks),
                    DataCount = authorBooks.Count(),
                    Controller = "Search",
                    Action = "SearchByAuthor",
                    Search = search,
                },
            };

            return this.View(this.resultView, viewModel);
        }

        public IActionResult SearchByYear(int id, int search)
        {
            var books = this.service.SearchByYear(search).ToList();
            var result = this.PaginationBooks<ListAllBooksViewModel>(id, books, MaxBooks);
            var viewModel = new BooksListViewModel
            {
                Books = result,
                Years = this.service.GetYears(),
                Genres = this.service.GetGenres(),
                PaginationViewModel = new PaginationViewModel
                {
                    CurrentPage = id,
                    PagesCount = (int)Math.Ceiling(books.Count() / (decimal)MaxBooks),
                    DataCount = books.Count(),
                    Controller = "Search",
                    Action = "SearchByYear",
                    Search = search.ToString(),
                },
            };
            return this.View(this.resultView, viewModel);
        }

        public IActionResult SearchByGenre(int id, string search)
        {
            var genres = search.Split("&");
            var books = this.service.SearchByGenres(genres).ToList();

            var result = this.PaginationBooks<ListAllBooksViewModel>(id, books, MaxBooks);
            var viewModel = new BooksListViewModel
            {
                Books = result,
                Years = this.service.GetYears(),
                Genres = this.service.GetGenres(),
                PaginationViewModel = new PaginationViewModel
                {
                    CurrentPage = id,
                    PagesCount = (int)Math.Ceiling(books.Count() / (decimal)MaxBooks),
                    DataCount = books.Count(),
                    Controller = "Search",
                    Action = "SearchByGenre",
                    Search = string.Join("&", search),
                },
            };
            return this.View(this.resultView, viewModel);
        }

        public IActionResult SearchByFreeBooks(int search)
        {
            var books = this.service.SearchFreeBooks().ToList();

            var result = this.PaginationBooks<ListAllBooksViewModel>(search, books, MaxBooks);
            var viewModel = new BooksListViewModel
            {
                Books = result,
                Years = this.service.GetYears(),
                Genres = this.service.GetGenres(),
                PaginationViewModel = new PaginationViewModel
                {
                    CurrentPage = search,
                    PagesCount = (int)Math.Ceiling(books.Count() / (decimal)MaxBooks),
                    DataCount = books.Count(),
                    Controller = "Search",
                    Action = "SearchByFreeBooks",
                },
            };
            return this.View(this.resultView, viewModel);
        }

        public IActionResult SearchByPaidBook(int search)
        {
            var books = this.service.SearchPaidBooks().ToList();

            var result = this.PaginationBooks<ListAllBooksViewModel>(search, books, MaxBooks);
            var viewModel = new BooksListViewModel
            {
                Books = result,
                Years = this.service.GetYears(),
                Genres = this.service.GetGenres(),
                PaginationViewModel = new PaginationViewModel
                {
                    CurrentPage = search,
                    PagesCount = (int)Math.Ceiling(books.Count() / (decimal)MaxBooks),
                    DataCount = books.Count(),
                    Controller = "Search",
                    Action = "SearchByPaidBook",
                },
            };
            return this.View(this.resultView, viewModel);
        }
    }
}
