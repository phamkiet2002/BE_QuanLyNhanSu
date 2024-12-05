using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.Enum;
public static class Query
{
    public record GetEnumTypeOfAllowanceQuery() : IQuery<IEnumerable<Response.EnumResponse>>;
    public record GetEnumTypeOfPenaltyQuery() : IQuery<IEnumerable<Response.EnumResponse>>;
    public record GetEnumPendingApprovalQuery() : IQuery<IEnumerable<Response.EnumResponse>>;
    public record GetEnumStatusQuery() : IQuery<IEnumerable<Response.EnumResponse>>;
}
