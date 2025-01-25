using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.HomeService
{
    public interface IHomeService
    {
        Task<bool> InsertFeeback(FeedbackDto input);
        List<FAQDto> GetFAQs();
    }
}
