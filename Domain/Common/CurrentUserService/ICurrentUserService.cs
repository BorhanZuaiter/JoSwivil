namespace Domain.Common
{
    public interface ICurrentUserService
    {
        string Name { get; }
        string UserId { get; }
    }
}