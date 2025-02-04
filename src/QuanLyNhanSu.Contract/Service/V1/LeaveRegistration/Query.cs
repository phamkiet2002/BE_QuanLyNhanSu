using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using static QuanLyNhanSu.Domain.Enumerations.ApproveEmuns;

namespace QuanLyNhanSu.Contract.Service.V1.LeaveRegistration;

public static class Query
{
    public record GetLeaveRegistrationTypeOfLeaveDayOffsQuery(string SearchTerm, PendingApproval PendingApproval, string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.LeaveRegistrationTypeOfLeaveDayOffResponse>>;

    public record GetLeaveRegistrationTypeOfLeaveHalfDayOffsQuery(string SearchTerm, PendingApproval PendingApproval, string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.LeaveRegistrationTypeOfLeaveHalfDayOffResponse>>;
}
