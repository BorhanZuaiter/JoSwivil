using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.NewsService
{
    public class NewsService : INewsService
    {
        private readonly VODContext _db;

        public NewsService(VODContext db)
        {
            _db = db;
        }
        public List<NewsDto> GetNews()
        {
            var List = _db.News.OrderByDescending(x => x.Id)
                .Where(r => r.IsDeleted == false).Select(a => new NewsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    AutherName = a.AutherName,
                    Description = a.Description,
                    PrivewImageUrl = a.PrivewImageUrl,
                    DetailsImageUrl = a.DetailsImageUrl,
                    Date = a.Date,
                }).ToList();

            return List;
        }
        public NewsDto GetById(int id)
        {
            var res = _db.News.Where(r => r.IsDeleted == false && r.Id == id).Select(a => new NewsDto
            {
                Id = a.Id,
                Name = a.Name,
                AutherName = a.AutherName,
                Description = a.Description,
                PrivewImageUrl = a.PrivewImageUrl,
                DetailsImageUrl = a.DetailsImageUrl,
                Date = a.Date,
            }).FirstOrDefault();
            return res;
        }
    }
}
