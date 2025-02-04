using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Position;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Position;

public sealed class GetPositionQueryHandler : IQueryHandler<Query.GetPositionsQuery, PagedResult<Response.PositionResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Position, Guid> _positionRepository;
    private readonly IMapper _mapper;

    public GetPositionQueryHandler(IRepositoryBase<Domain.Entities.Position, Guid> positionRepository, IMapper mapper)
    {
        _positionRepository = positionRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.PositionResponse>>> Handle(Query.GetPositionsQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.Position>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.Position>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.Position>.UpperPageSize
            ? PagedResult<Domain.Entities.Position>.UpperPageSize : request.PageSize;

        var query = _positionRepository.FindAll(x=> x.IsDeleted != true);

        var searchTerms = request.SearchTerm?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(searchTerms))
        {
            query = query.Where(x => x.Name.Contains(searchTerms));
        }

        // sắp xếp theo ngày có hiện lực mới nhất theo asc
        var sortOrder = (request.SortOrder ?? "des").ToLower();
        query = sortOrder == "des"
           ? query.OrderByDescending(x => x.CreatedDate)
           : sortOrder == "asc" ? query.OrderBy(x => x.CreatedDate)
           : query.OrderByDescending(x => x.CreatedDate);

        var positions = await query
           .Skip((PageIndex - 1) * PageSize)
           .Take(PageSize)
           .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        var positionPagedResult = PagedResult<Domain.Entities.Position>.Create(
            positions,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.PositionResponse>>(positionPagedResult);

        return Result.Success(result);
    }

    //public async Task<Result<List<Response.PositionResponse>>> Handle(Query.GetPositionsQuery request, CancellationToken cancellationToken)
    //{
    //    var position = await _positionRepository.FindAll().ToListAsync();
    //    var result = _mapper.Map<List<Response.PositionResponse>>(position);
    //    return Result.Success(result);
    //}
}
