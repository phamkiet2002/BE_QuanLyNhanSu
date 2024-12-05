using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.EmployeeLevel;
public static class Query
{
    public record GetEmployeeLevelsQuery(int PageIndex, int PageSize) : IQuery<PagedResult<Response.EmployeeLevelResponse>>;
    public record GetEmployeeLevelByIdQuery(Guid Id) : IQuery<Response.EmployeeLevelResponse>;
}
