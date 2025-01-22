namespace Domain.DTO.AdminDtos
{
    public class TableDto<T>
    {
        public TableDto()
        {
            this.DataList = new List<T>();
        }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public string Search { get; set; }
        public List<T> DataList { get; set; }
    }
    public class TableDto
    {
        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string? Search { get; set; }
    }
}
