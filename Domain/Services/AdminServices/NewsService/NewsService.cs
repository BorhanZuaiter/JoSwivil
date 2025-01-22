using Domain.DTO.AdminDtos;
using Domain.Entities;
using Domain.Helpers;

namespace Domain.Services.AdminServices.NewsService
{
    public class NewsService : INewsService
    {
        private readonly VODContext _db;
        private readonly StorageConnection _storageConnection;

        public NewsService(VODContext db, StorageConnection storageConnection)
        {
            _db = db;
            _storageConnection = storageConnection;
        }
        public QueryResult<NewsDto> GetList(SearchCriteria model)
        {
            var res = _db.News
                .WhereIf(!string.IsNullOrEmpty(model.Search), r => r.Name.Contains(model.Search))
                .Where(r => r.IsDeleted == false)
                .OrderByDescending(r => r.Id)
                .Select(r => new NewsDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    AutherName = r.AutherName,
                    Description = r.Description,
                    Date = r.Date,
                }).ToQueryResult(model.PageNumber, model.PageSize);
            return res;
        }
        public async Task<bool> Create(NewsDto input, string userId)
        {
            var obj = new News();
            obj.Name = input.Name;
            obj.AutherName = input.AutherName;
            obj.Description = input.Description;
            obj.Date = input.Date;
            if (input.DetailsImage is not null)
                obj.DetailsImageUrl = await _storageConnection.SaveOnStorage(input.DetailsImage, "News");

            if (input.PrivewImage is not null)
                obj.PrivewImageUrl = await _storageConnection.SaveOnStorage(input.PrivewImage, "News");

            obj.CreatedBy = userId;
            obj.CreatedOn = DateTime.Now;
            _db.News.Add(obj);
            return _db.SaveChanges() > 0;
        }
        public NewsDto GetById(int Id)
        {
            var obj = _db.News.Where(r => r.Id == Id).Select(a => new NewsDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                AutherName = a.AutherName,
                Date = a.Date,
                PrivewImageUrl = a.PrivewImageUrl,
                DetailsImageUrl = a.DetailsImageUrl,

            }).FirstOrDefault();
            return obj;
        }
        public async Task<bool> Edit(NewsDto input, string userId)
        {
            var obj = _db.News.Where(r => r.Id == input.Id).FirstOrDefault();
            obj.Name = input.Name;
            obj.Description = input.Description;
            obj.AutherName = input.AutherName;
            obj.Date = input.Date;
            if (input.DetailsImage is not null)
                obj.DetailsImageUrl = await _storageConnection.SaveOnStorage(input.DetailsImage, "News");

            if (input.PrivewImage is not null)
                obj.PrivewImageUrl = await _storageConnection.SaveOnStorage(input.PrivewImage, "News");

            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.News.Update(obj);
            return _db.SaveChanges() > 0;
        }
        public bool Delete(int Id, string userId)
        {
            var obj = _db.News.Where(r => r.Id == Id).FirstOrDefault();
            obj.IsDeleted = true;
            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.News.Update(obj);
            return _db.SaveChanges() > 0;
        }
    }
}
