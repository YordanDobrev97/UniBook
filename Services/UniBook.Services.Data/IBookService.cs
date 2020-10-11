namespace UniBook.Services.Data
{
    using System.Collections.Generic;

    using UniBook.Web.ViewModels;

    public interface IBookService
    {
        IEnumerable<string> All();
    }
}
