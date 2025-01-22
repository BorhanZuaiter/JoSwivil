namespace Domain.Helpers
{
    public class SearchCriteria
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? Sort { get; set; }
        public string? SortDirection { get; set; }
        public string? Search { get; set; }
        public int? GalleryTypeId { get; set; }
        public SearchCriteria()
        {
            PageSize = 10;
            PageNumber = 1;
            Sort = "Id";
            SortDirection = "DESC";
        }
    }
}
