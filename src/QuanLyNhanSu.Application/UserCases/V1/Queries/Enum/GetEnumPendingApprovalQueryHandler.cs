using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Enum;
using static QuanLyNhanSu.Domain.Enumerations.ApproveEmuns;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Enum;
public sealed class GetEnumPendingApprovalQueryHandler : IQueryHandler<Query.GetEnumPendingApprovalQuery, IEnumerable<Response.EnumResponse>>
{
    public async Task<Result<IEnumerable<Response.EnumResponse>>> Handle(Query.GetEnumPendingApprovalQuery request, CancellationToken cancellationToken)
    {
        var enumValues = System.Enum.GetValues(typeof(PendingApproval))
                                .Cast<PendingApproval>()
                                .Select(e => new Response.EnumResponse(e.ToString(), (int)e));
        return Result.Success(enumValues);
    }
}
