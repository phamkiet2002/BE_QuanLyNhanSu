using System.Net.WebSockets;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.AllowanceAndPenalties;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Enumerations;
using static QuanLyNhanSu.Domain.Enumerations.AllowanceAndPenaltyEnums;
using static QuanLyNhanSu.Domain.Enumerations.StatusEnums;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.AllowanceAndPenalties;

public sealed class GetAllowanceQueryHandler : IQueryHandler<Query.GetAllowancesQuery, PagedResult<Response.AllowanceResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> _allowanceRepository;
    private readonly IMapper _mapper;

    public GetAllowanceQueryHandler(IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> allowanceAndPenaltiesRepository, IMapper mapper)
    {
        _allowanceRepository = allowanceAndPenaltiesRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.AllowanceResponse>>> Handle(Query.GetAllowancesQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.AllowanceAndPenalties>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.AllowanceAndPenalties>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.AllowanceAndPenalties>.UpperPageSize
            ? PagedResult<Domain.Entities.AllowanceAndPenalties>.UpperPageSize : request.PageSize;

        var query = _allowanceRepository.FindAll(x => x.Type == TypeAllowanceAndPenalties.Phucap && x.IsDeleted != true)
            .Include(x => x.WorkPlaceAndAllowanceAndPenalties)
            .ThenInclude(x => x.WorkPlace)
            .AsQueryable();

        var typeOfAllowance = request.TypeOfAllowance;
        if (typeOfAllowance != null)
        {
            query = query.Where(x => x.TypeOfAllowance == typeOfAllowance);
        }

        var workPlaceName = request.WorkPlaceName?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(workPlaceName))
        {
            query = query.Where(x => x.WorkPlaceAndAllowanceAndPenalties.Any(x => x.Status == Status.Active && x.WorkPlace.Name.Contains(workPlaceName)));
        }

        // sắp xếp theo ngày có hiện lực mới nhất theo asc
        var sortOrder = (request.SortOrder ?? "des").ToLower();
        query = sortOrder == "des"
           ? query.OrderByDescending(x => x.WorkPlaceAndAllowanceAndPenalties.Max(x => x.CreatedDate))
           : sortOrder == "asc" ? query.OrderBy(x => x.WorkPlaceAndAllowanceAndPenalties.Max(x => x.CreatedDate))
           : query.OrderByDescending(x => x.WorkPlaceAndAllowanceAndPenalties.Max(x => x.CreatedDate));

        var allowances = await query
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        var allowancePagedResult = PagedResult<Domain.Entities.AllowanceAndPenalties>.Create(
            allowances,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.AllowanceResponse>>(allowancePagedResult);

        return Result.Success(result);
    }
}
