using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.NewsService
{
    public interface INewsService
    {
        List<NewsDto> GetNews();
        NewsDto GetById(int id);
    }
}
