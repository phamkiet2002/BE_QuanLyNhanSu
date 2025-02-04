using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Enum;
using static QuanLyNhanSu.Domain.Enumerations.AllowanceAndPenaltyEnums;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Enum;
public sealed class GetEnumTypeOfPenaltyQueryHandler : IQueryHandler<Query.GetEnumTypeOfPenaltyQuery, IEnumerable<Response.EnumResponse>>
{
    public async Task<Result<IEnumerable<Response.EnumResponse>>> Handle(Query.GetEnumTypeOfPenaltyQuery request, CancellationToken cancellationToken)
    {
        var enumValues = System.Enum.GetValues(typeof(TypeOfPenalty))
                                .Cast<TypeOfPenalty>()
                                .Select(e => new Response.EnumResponse(e.ToString(), (int)e));
        return Result.Success(enumValues);
    }
}
