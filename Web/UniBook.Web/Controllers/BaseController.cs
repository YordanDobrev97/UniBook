namespace UniBook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        protected IEnumerable<T> PaginationBooks<T>(
            int id,
            IEnumerable<T> data,
            int max)
        {
            int skip = (id - 1) * max;
            var resultData = data.Skip(skip).Take(max).ToList();
            int pageCount = (int)Math.Ceiling(data.Count() / (decimal)max);
            return resultData;
        }
    }
}
