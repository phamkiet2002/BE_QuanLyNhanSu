using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.LeaveDate;
public static class Query
{
    public record GetLeaveDatesQuery(string SearchTerm, string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.LeaveDateResponse>>;
    public record GetLeaveDateByIdQuery(Guid Id) : IQuery<Response.LeaveDateResponse>;
}
