using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.Position;
public static class Query
{
    public record GetPositionsQuery(string SearchTerm, string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.PositionResponse>>;
    public record GetPositionByIdQuery(Guid Id) : IQuery<Response.PositionResponse>;
}
