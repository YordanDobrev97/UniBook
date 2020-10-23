﻿namespace UniBook.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using UniBook.Web.ViewModels;

    public interface IBookService
    {
        IEnumerable<ListAllBooksViewModel> All();

        ContentBookViewModel ReadBook(int id);

        DetailsBookViewModel Details(int id);
    }
}