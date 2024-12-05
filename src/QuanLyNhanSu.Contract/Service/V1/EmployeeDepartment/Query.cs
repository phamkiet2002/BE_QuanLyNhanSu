using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.EmployeeDepartment;

public static class Query
{
    public record GetEmployeeDepartmentsQuery(int PageIndex, int PageSize) : IQuery<PagedResult<Response.EmployeeDepartmentResponse>>;
    public record GetEmployeeLevelByIdQuery(Guid Id) : IQuery<Response.EmployeeDepartmentResponse>;
}
