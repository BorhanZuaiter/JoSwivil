namespace Domain.Common
{
    public record TablePageHeader(string Search = "", bool HasCreateButton = false, bool HasDownloadButton = false, bool HasEditGroup = false);

}
