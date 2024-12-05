using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.Employee;

public static class Query
{
    public record GetEmployeesQuery
        (
            string? SearchTerm,
            string? WorkPlaceName, string? DepartmentName, string? PositionName, string? LevelName, Domain.Enumerations.StatusEnums.Status? Status,
            string? SortOrder, int PageIndex, int PageSize
        ) : IQuery<PagedResult<Response.EmployeeResponse>>;
    public record GetEmployeesMapToAttendanceQuery(int PageIndex, int PageSize) : IQuery<PagedResult<Response.EmployeeMapToAttendanceResponse>>;
    public record GetEmployeeByMaNVQuery(string MaNV) : IQuery<Response.EmployeeResponse>;
    public record GetEmployeeByIdQuery(Guid Id) : IQuery<Response.EmployeeResponse>;
    public record GetEmplpyeePayRollQuery(int PageIndex, int PageSize) : IQuery<PagedResult<Response.EmployeePayRollResponse>>;
}
