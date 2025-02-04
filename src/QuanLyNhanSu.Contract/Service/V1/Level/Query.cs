using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.Level;
public static class Query
{
    public record GetLevelsQuery(string SearchTerm, string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.LevelResponse>>;
    public record GetLevelByIdQuery(Guid Id) : IQuery<Response.LevelResponse>;
}
