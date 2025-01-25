using Domain.DTO.UIDtos;
using Domain.Entities;

namespace Domain.Services.UIServices.HomeService
{
    public class HomeService : IHomeService
    {
        private readonly VODContext _db;

        public HomeService(VODContext db)
        {
            _db = db;
        }
        public async Task<bool> InsertFeeback(FeedbackDto input)
        {
            var obj = new Feedback();
            obj.Message = input.Message;
            obj.Email = input.Email;
            obj.Name = input.Name;
            obj.Iscontacted = false;
            obj.CreatedOn = DateTime.Now;
            _db.Feedback.Add(obj);
            return _db.SaveChanges() > 0;
        }
        public List<FAQDto> GetFAQs()
        {
            var List = _db.FAQs.OrderByDescending(x => x.Id)
                .Where(r => r.IsDeleted == false).Select(a => new FAQDto
                {
                    Id = a.Id,
                    Answer = a.Answer,
                    Question = a.Question

                }).ToList();

            return List;
        }
    }
}
