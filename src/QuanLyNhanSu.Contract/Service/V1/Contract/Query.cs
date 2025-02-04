using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.Contract;
public static class Query
{
    public record GetContractsQuery(string? SearchTerm, int? ContracNumber, string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.ContractResponse>>;
    public record GetContractByIdQuery(Guid Id) : IQuery<Response.ContractResponse>;
}
