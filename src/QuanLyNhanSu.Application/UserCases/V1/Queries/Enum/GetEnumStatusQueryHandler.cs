using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Enum;
using static QuanLyNhanSu.Domain.Enumerations.StatusEnums;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Enum;
public sealed class GetEnumStatusQueryHandler : IQueryHandler<Query.GetEnumStatusQuery, IEnumerable<Response.EnumResponse>>
{
    public async Task<Result<IEnumerable<Response.EnumResponse>>> Handle(Query.GetEnumStatusQuery request, CancellationToken cancellationToken)
    {
        var enumValues = System.Enum.GetValues(typeof(Status))
                                .Cast<Status>()
                                .Select(e => new Response.EnumResponse(e.ToString(), (int)e));
        return Result.Success(enumValues);
    }
}
