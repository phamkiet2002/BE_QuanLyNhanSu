using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.Attendance;

public static class Query
{
    public record GetAttendancesQuery(int PageIndex, int PageSize) : IQuery<PagedResult<Response.AttendanceResponse>>;
    public record GetAttendanceByIdQuery(Guid Id) : IQuery<Response.AttendanceResponse>;
}
