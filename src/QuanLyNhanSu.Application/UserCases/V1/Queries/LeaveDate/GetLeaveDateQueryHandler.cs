using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.LeaveDate;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.LeaveDate;

public sealed class GetLeaveDateQueryHandler : IQueryHandler<Query.GetLeaveDatesQuery, PagedResult<Response.LeaveDateResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.LeaveDate, Guid> _leaveDateRepository;
    private readonly IMapper _mapper;

    public GetLeaveDateQueryHandler(IRepositoryBase<Domain.Entities.LeaveDate, Guid> leaveDateRepository, IMapper mapper)
    {
        _leaveDateRepository = leaveDateRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.LeaveDateResponse>>> Handle(Query.GetLeaveDatesQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.LeaveDate>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.LeaveDate>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.LeaveDate>.UpperPageSize
            ? PagedResult<Domain.Entities.LeaveDate>.UpperPageSize : request.PageSize;

        var query = _leaveDateRepository.FindAll(x=> x.IsDeleted != true);

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

        var leaveDates = await query
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        var leaveDatePagedResult = PagedResult<Domain.Entities.LeaveDate>.Create(
            leaveDates,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.LeaveDateResponse>>(leaveDatePagedResult);

        return Result.Success(result);
    }

    //public async Task<Result<List<Response.LeaveDateResponse>>> Handle(Query.GetLeaveDatesQuery request, CancellationToken cancellationToken)
    //{
    //    var leaveDates = await _leaveDateRepository.FindAll().ToListAsync();
    //    var result = _mapper.Map<List<Response.LeaveDateResponse>>(leaveDates);
    //    return Result.Success(result);
    //}
}
