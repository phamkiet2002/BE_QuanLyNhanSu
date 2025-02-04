using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.WorkShedule;
public static class Query
{
    public record GetWorkShedulesQuery(string? SearchTerm, string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.WorkSheduleResponse>>;
    public record GetWorkSheduleByIdQuery(Guid Id) : IQuery<Response.WorkSheduleResponse>;
}
