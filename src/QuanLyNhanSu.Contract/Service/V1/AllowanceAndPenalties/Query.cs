using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Enumerations;
using static QuanLyNhanSu.Domain.Enumerations.AllowanceAndPenaltyEnums;

namespace QuanLyNhanSu.Contract.Service.V1.AllowanceAndPenalties;
public static class Query
{
    public record GetAllowancesQuery(TypeOfAllowance? TypeOfAllowance, string? WorkPlaceName, string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.AllowanceResponse>>;
    public record GetAllPenaltiesQuery(TypeOfPenalty? TypeOfPenalty, string? WorkPlaceName, string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.PenaltiesResponse>>;
    public record GetAllowanceByIdQuery(Guid Id) : IQuery<Response.AllowanceResponse>;
}
