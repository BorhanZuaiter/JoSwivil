using Domain.DTO.AdminDtos;
using Domain.Helpers;

namespace Domain.Services.AdminServices.FeedbackService
{
    public interface IFeedbackService
    {
        QueryResult<FeedbackDto> GetList(SearchCriteria model);
        Task<bool> SetContactedAsync(int id);
    }
}
