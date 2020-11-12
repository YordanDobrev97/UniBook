namespace UniBook.Common.Extensions
{
    using System;
    using System.Linq;

    public static class LinqExtensions
    {
        public static PaginationResult<T> GetPaged<T>(
            this IQueryable<T> query,
            int page,
            int pageSize)
            where T : class
        {
            var result = new PaginationResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count(),
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Result = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }
    }
}
