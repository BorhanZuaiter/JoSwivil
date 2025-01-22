using Domain.DTO.AdminDtos;
using Domain.Entities;
using Domain.Helpers;

namespace Domain.Services.AdminServices.FAQService
{
    public class FAQService : IFAQService
    {
        private readonly VODContext _db;
        private readonly StorageConnection _storageConnection;

        public FAQService(VODContext db, StorageConnection storageConnection)
        {
            _db = db;
            _storageConnection = storageConnection;
        }
        public QueryResult<FAQDto> GetList(SearchCriteria model)
        {
            var res = _db.FAQs
                .WhereIf(!string.IsNullOrEmpty(model.Search), r => r.Question.Contains(model.Search))
                .Where(r => r.IsDeleted == false)
                .OrderByDescending(r => r.Id)
                .Select(r => new FAQDto
                {
                    Id = r.Id,
                    Question = r.Question,
                    Answer = r.Answer,
                }).ToQueryResult(model.PageNumber, model.PageSize);
            return res;
        }
        public async Task<bool> Create(FAQDto input, string userId)
        {
            var obj = new FAQ();
            obj.Question = input.Question;
            obj.Answer = input.Answer;
            obj.CreatedBy = userId;
            obj.CreatedOn = DateTime.Now;
            _db.FAQs.Add(obj);
            return _db.SaveChanges() > 0;
        }
        public FAQDto GetById(int Id)
        {
            var obj = _db.FAQs.Where(r => r.Id == Id).Select(a => new FAQDto
            {
                Id = a.Id,
                Question = a.Question,
                Answer = a.Answer,

            }).FirstOrDefault();
            return obj;
        }
        public async Task<bool> Edit(FAQDto input, string userId)
        {
            var obj = _db.FAQs.Where(r => r.Id == input.Id).FirstOrDefault();
            obj.Question = input.Question;
            obj.Answer = input.Answer;
            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.FAQs.Update(obj);
            return _db.SaveChanges() > 0;
        }
        public bool Delete(int Id, string userId)
        {
            var obj = _db.FAQs.Where(r => r.Id == Id).FirstOrDefault();
            obj.IsDeleted = true;
            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.FAQs.Update(obj);
            return _db.SaveChanges() > 0;
        }
    }
}
