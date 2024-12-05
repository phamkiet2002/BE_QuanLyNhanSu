using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.AttendanceSetting;
public static class Query
{
    public record GetAttendanceSettingQuery(Domain.Enumerations.StatusEnums.Status? Status, string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.AttendanceSettingResponse>>;
    public record GetAttendanceSettingByIdQuery(Guid Id) : IQuery<Response.AttendanceSettingResponse>;
}
