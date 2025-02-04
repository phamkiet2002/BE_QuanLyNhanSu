using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Level;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Level;

public sealed class GetLevelQueryHandler : IQueryHandler<Query.GetLevelsQuery, PagedResult<Response.LevelResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Level, Guid> _levelRepository;
    private readonly IMapper _mapper;

    public GetLevelQueryHandler(IRepositoryBase<Domain.Entities.Level, Guid> levelRepository, IMapper mapper)
    {
        _levelRepository = levelRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.LevelResponse>>> Handle(Query.GetLevelsQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.Level>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.Level>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.Level>.UpperPageSize
            ? PagedResult<Domain.Entities.Level>.UpperPageSize : request.PageSize;


        var query = _levelRepository.FindAll(x => x.IsDeleted != true);

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

        var levels = await query
           .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync();

        var levelPagedResult = PagedResult<Domain.Entities.Level>.Create(
            levels,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.LevelResponse>>(levelPagedResult);

        return Result.Success(result);
    }

    //public async Task<Result<List<Response.LevelResponse>>> Handle(Query.GetLevelsQuery request, CancellationToken cancellationToken)
    //{
    //    var levels = await _levelRepository.FindAll().ToListAsync();
    //    var result = _mapper.Map<List<Response.LevelResponse>>(levels);
    //    return Result.Success(result);
    //}
}
