using Domain.DTO.AdminDtos;
using Domain.Helpers;

namespace Domain.Services.AdminServices.NewsService
{
    public interface INewsService
    {
        QueryResult<NewsDto> GetList(SearchCriteria model);
        Task<bool> Create(NewsDto input, string userId);
        NewsDto GetById(int Id);
        Task<bool> Edit(NewsDto input, string userId);
        bool Delete(int Id, string userId);

    }
}
