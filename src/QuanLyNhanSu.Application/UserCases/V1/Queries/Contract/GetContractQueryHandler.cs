using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Contract;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Contract;

public sealed class GetContractQueryHandler : IQueryHandler<Query.GetContractsQuery, PagedResult<Response.ContractResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Contract, Guid> _contractRepository;
    private readonly IMapper _mapper;

    public GetContractQueryHandler(IRepositoryBase<Domain.Entities.Contract, Guid> contractRepository, IMapper mapper)
    {
        _contractRepository = contractRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.ContractResponse>>> Handle(Query.GetContractsQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.Contract>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.Contract>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.Contract>.UpperPageSize
            ? PagedResult<Domain.Entities.Contract>.UpperPageSize : request.PageSize;

        var query = _contractRepository.FindAll();

        var searchTerms = request.SearchTerm?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(searchTerms))
        {
            query = query.Where(x => x.Employee.Name.Contains(searchTerms));
        }

        var contracNumber = request.ContracNumber ?? null;
        if (contracNumber != null)
        {
            query = query.Where(x => x.ContracNumber == contracNumber);
        }

        // sắp xếp theo ngày có hiện lực mới nhất theo asc
        var sortOrder = (request.SortOrder ?? "des").ToLower();
        query = sortOrder == "des"
           ? query.OrderByDescending(x => x.CreatedDate)
           : sortOrder == "asc" ? query.OrderBy(x => x.CreatedDate)
           : query.OrderByDescending(x => x.CreatedDate);

        var contracts = await query
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        var contractPagedResult = PagedResult<Domain.Entities.Contract>.Create(
            contracts,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.ContractResponse>>(contractPagedResult);

        return Result.Success(result);

    }

    //public async Task<Result<List<Response.ContractResponse>>> Handle(Query.GetContractsQuery request, CancellationToken cancellationToken)
    //{
    //    var contracts = await _contractRepository.FindAll().ToListAsync();
    //    var result = _mapper.Map<List<Response.ContractResponse>>(contracts);
    //    return Result.Success(result);
    //}
}
