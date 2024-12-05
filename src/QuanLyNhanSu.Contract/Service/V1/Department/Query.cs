using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.Department;

public static class Query
{
    public record GetDepartmentsQuery(string SearchTerm, string? WorkPlaceName, string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.DepartmentResponse>>;
    public record GetDepartmentByIdQuery(Guid Id) : IQuery<Response.DepartmentResponse>;
}
