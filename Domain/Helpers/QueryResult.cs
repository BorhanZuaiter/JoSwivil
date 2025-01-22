namespace Domain.Helpers
{
    public class QueryResult<T> where T : class
    {
        public IQueryable<T> Items { get; private set; }
        public IEnumerable<T> Items1 { get; private set; }
        public int TotalCount { get; private set; }
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public QueryResult(IQueryable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            if (totalCount < 0)
                throw new ArgumentOutOfRangeException("totalCount", totalCount, "Incorrect value.");

            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public QueryResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            if (totalCount < 0)
                throw new ArgumentOutOfRangeException("totalCount", totalCount, "Incorrect value.");

            Items1 = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

    }
}
