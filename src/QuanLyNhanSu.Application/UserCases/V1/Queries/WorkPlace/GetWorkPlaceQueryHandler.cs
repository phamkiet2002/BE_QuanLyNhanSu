using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.WorkPlace;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.WorkPlace;

public sealed class GetWorkPlaceQueryHandler : IQueryHandler<Query.GetWorkPlacesQuery, PagedResult<Response.WorkPlaceResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.WorkPlace, Guid> _workPlaceRepository;
    private readonly IMapper _mapper;

    public GetWorkPlaceQueryHandler(IRepositoryBase<Domain.Entities.WorkPlace, Guid> workPlaceRepository, IMapper mapper)
    {
        _workPlaceRepository = workPlaceRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.WorkPlaceResponse>>> Handle(Query.GetWorkPlacesQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.WorkPlace>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.WorkPlace>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.WorkPlace>.UpperPageSize
            ? PagedResult<Domain.Entities.WorkPlace>.UpperPageSize : request.PageSize;

        var query = _workPlaceRepository.FindAll(x => x.IsDeleted != true);

        var searchTerms = request.SearchTerm?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(searchTerms))
        {
            query = query.Where(x => x.Name.Contains(searchTerms));
        }

        var sortOrder = (request.SortOrder ?? "des").ToLower();
        query = sortOrder == "des"
           ? query.OrderByDescending(x => x.CreatedDate)
           : sortOrder == "asc" ? query.OrderBy(x => x.CreatedDate)
           : query.OrderByDescending(x => x.CreatedDate);

        var workPlaces = await query
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);


        var totalCount = await query.CountAsync(cancellationToken);

        var workPlacePagedResult = PagedResult<Domain.Entities.WorkPlace>.Create(
            workPlaces,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.WorkPlaceResponse>>(workPlacePagedResult);

        return Result.Success(result);
    }
}
