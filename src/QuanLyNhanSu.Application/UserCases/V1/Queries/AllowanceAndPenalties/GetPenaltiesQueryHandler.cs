using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.AllowanceAndPenalties;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using static QuanLyNhanSu.Domain.Enumerations.AllowanceAndPenaltyEnums;
using static QuanLyNhanSu.Domain.Enumerations.StatusEnums;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.AllowanceAndPenalties;

public sealed class GetPenaltiesQueryHandler : IQueryHandler<Query.GetAllPenaltiesQuery, PagedResult<Response.PenaltiesResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> _penaltiesRepository;
    private readonly IMapper _mapper;

    public GetPenaltiesQueryHandler(IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> penaltiesRepository, IMapper mapper)
    {
        _penaltiesRepository = penaltiesRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.PenaltiesResponse>>> Handle(Query.GetAllPenaltiesQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.AllowanceAndPenalties>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.AllowanceAndPenalties>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.AllowanceAndPenalties>.UpperPageSize
            ? PagedResult<Domain.Entities.AllowanceAndPenalties>.UpperPageSize : request.PageSize;

        var query = _penaltiesRepository.FindAll(x => x.Type == TypeAllowanceAndPenalties.Phat && x.IsDeleted != true)
            .Include(x => x.WorkPlaceAndAllowanceAndPenalties).ThenInclude(x => x.WorkPlace)
            .AsQueryable();

        var typeOfPenalty = request.TypeOfPenalty;
        if (typeOfPenalty != null)
        {
            query = query.Where(x => x.TypeOfPenalty == typeOfPenalty);
        }

        var workPlaceName = request.WorkPlaceName?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(workPlaceName))
        {
            query = query.Where(x => x.WorkPlaceAndAllowanceAndPenalties.Any(x => x.Status == Status.Active && x.WorkPlace.Name.Contains(workPlaceName)));
        }

        // sắp xếp theo ngày có hiện lực mới nhất theo des
        var sortOrder = (request.SortOrder ?? "des").ToLower();
        query = sortOrder == "des"
           ? query.OrderByDescending(x => x.WorkPlaceAndAllowanceAndPenalties.Max(x => x.CreatedDate))
           : sortOrder == "asc" ? query.OrderBy(x => x.WorkPlaceAndAllowanceAndPenalties.Max(x => x.CreatedDate))
           : query.OrderByDescending(x => x.WorkPlaceAndAllowanceAndPenalties.Max(x => x.CreatedDate));

        var penalties = await query
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        var penaltiesPagedResult = PagedResult<Domain.Entities.AllowanceAndPenalties>.Create(
            penalties,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.PenaltiesResponse>>(penaltiesPagedResult);

        return Result.Success(result);
    }

    //public async Task<Result<List<Response.PenaltiesResponse>>> Handle(Query.GetAllPenaltiesQuery request, CancellationToken cancellationToken)
    //{
    //    var penalties = await _penaltiesRepository.FindAll(x => x.Type == AllowanceAndPenaltyEnums.TypeAllowanceAndPenalties.phat).ToListAsync();
    //    var result = _mapper.Map<List<Response.PenaltiesResponse>>(penalties);
    //    return Result.Success(result);
    //}
}
