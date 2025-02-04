using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.Salary;

public static class Query
{
    public record GetSalaryQuery(string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.SalaryResponse>>;
    public record GetSalaryByIdQuery(Guid Id) : IQuery<Response.SalaryResponse>;
}
