using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.WorkShedule;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.WorkShedule;

public sealed class GetWorkSheduleQueryHandler : IQueryHandler<Query.GetWorkShedulesQuery, PagedResult<Response.WorkSheduleResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.WorkShedule, Guid> _workSheduleRepository;
    private readonly IMapper _mapper;

    public GetWorkSheduleQueryHandler(IRepositoryBase<Domain.Entities.WorkShedule, Guid> workSheduleRepository, IMapper mapper)
    {
        _workSheduleRepository = workSheduleRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.WorkSheduleResponse>>> Handle(Query.GetWorkShedulesQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.WorkShedule>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.WorkShedule>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.WorkShedule>.UpperPageSize
            ? PagedResult<Domain.Entities.WorkShedule>.UpperPageSize : request.PageSize;

        var query = _workSheduleRepository.FindAll(x => x.IsDeleted != true);

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

        var workShedules = await query
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        var workShedulePagedResult = PagedResult<Domain.Entities.WorkShedule>.Create(
            workShedules,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.WorkSheduleResponse>>(workShedulePagedResult);

        return Result.Success(result);
    }

    //public async Task<Result<List<Response.WorkSheduleResponse>>> Handle(Query.GetWorkShedulesQuery request, CancellationToken cancellationToken)
    //{
    //    var workShedules = await _workSheduleRepository.FindAll().ToListAsync();
    //    var resutl = _mapper.Map<List<Response.WorkSheduleResponse>>(workShedules);
    //    return Result.Success(resutl);
    //}
}
