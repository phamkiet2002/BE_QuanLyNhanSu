using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.WorkPlaceAllowanceAndPenalties;
public static class Query
{
    public record GetAllowancesQuery(string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.WorkPlaceAllowanceResponse>>;
    public record GetAllPenaltiesQuery(string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.WorkPlacePenaltiesResponse>>;
    public record GetAllowanceByIdQuery(Guid Id) : IQuery<Response.WorkPlaceAllowanceResponse>;
}
