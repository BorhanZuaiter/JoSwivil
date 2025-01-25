using Domain.DTO.AdminDtos;
using Domain.Helpers;
using Domain.Services.AdminServices.FeedbackService;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services.AdminServices.Feedbackervice
{
    public class Feedbackervice : IFeedbackService
    {
        private readonly VODContext _db;

        public Feedbackervice(VODContext db)
        {
            _db = db;
        }
        public QueryResult<FeedbackDto> GetList(SearchCriteria model)
        {
            var res = _db.Feedback
                .WhereIf(!string.IsNullOrEmpty(model.Search), r => r.Name.Contains(model.Search) || r.Email.Contains(model.Search)
                || r.Email.Contains(model.Search))
                .Where(r => r.IsDeleted == false)
                .OrderByDescending(r => r.Id)
                .Select(r => new FeedbackDto
                {
                    Id = r.Id,
                    Email = r.Email,
                    Name = r.Name,
                    Message = r.Message,
                    Iscontacted = r.Iscontacted,
                }).ToQueryResult(model.PageNumber, model.PageSize);
            return res;
        }
        public async Task<bool> SetContactedAsync(int id)
        {
            var feedback = await _db.Feedback.FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
            if (feedback == null)
            {
                return false;
            }
            feedback.Iscontacted = true;
            _db.Feedback.Update(feedback);

            var result = await _db.SaveChangesAsync();
            return result > 0;
        }
    }
}
