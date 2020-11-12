namespace UniBook.Common.Extensions
{
    using System.Collections.Generic;

    public class PaginationResult<T> : PaginationResultBase
        where T : class
    {
        public PaginationResult()
        {
            this.Result = new List<T>();
        }

        public IList<T> Result { get; set; }
    }
}
