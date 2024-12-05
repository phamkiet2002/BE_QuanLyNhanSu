using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Enum;
using static QuanLyNhanSu.Domain.Enumerations.AllowanceAndPenaltyEnums;


namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Enum;
public sealed class GetEnumTypeOfAllowanceQueryHandler : IQueryHandler<Query.GetEnumTypeOfAllowanceQuery, IEnumerable<Response.EnumResponse>>
{
    public async Task<Result<IEnumerable<Response.EnumResponse>>> Handle(Query.GetEnumTypeOfAllowanceQuery request, CancellationToken cancellationToken)
    {
        var enumValues = System.Enum.GetValues(typeof(TypeOfAllowance))
                                .Cast<TypeOfAllowance>()
                                .Select(e => new Response.EnumResponse(e.ToString(), (int)e));
        return Result.Success(enumValues);
    }
}

