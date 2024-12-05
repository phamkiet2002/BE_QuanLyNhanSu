using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.EmployeePosition;
public static class Query
{
    public record GetEmployeePositionsQuery(int PageIndex, int PageSize) : IQuery<PagedResult<Response.EmployeePositionResponse>>;
    public record GetEmployeePositionByIdQuery(Guid Id) : IQuery<Response.EmployeePositionResponse>;
}
