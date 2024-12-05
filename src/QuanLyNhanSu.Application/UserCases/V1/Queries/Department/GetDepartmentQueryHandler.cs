using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Department;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using static QuanLyNhanSu.Contract.Service.V1.Department.Response;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Department;

public sealed class GetDepartmentQueryHandler : IQueryHandler<Query.GetDepartmentsQuery, PagedResult<DepartmentResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Department, Guid> _departmentRepository;
    private readonly IMapper _mapper;

    public GetDepartmentQueryHandler(IRepositoryBase<Domain.Entities.Department, Guid> departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<DepartmentResponse>>> Handle(Query.GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.Employee>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.Department>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.Department>.UpperPageSize
            ? PagedResult<Domain.Entities.Employee>.UpperPageSize : request.PageSize;

        var query = _departmentRepository.FindAll(x => x.WorkPlace.IsDeleted != true && x.IsDeleted != true);

        var searchTerms = request.SearchTerm?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(searchTerms))
        {
            query = query.Where(x => x.Name.Contains(searchTerms));
            //query = query.Where(x => EF.Functions.Collate(x.Name, "Latin1_General_CI_AS").Contains(searchTerms));
        }

        // tìm kiếm theo tên nơi làm việc
        var workPlaceName = request.WorkPlaceName?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(workPlaceName))
        {
            query = query.Where(x => x.WorkPlace.Name.Contains(workPlaceName));
        }

        // sắp xếp theo ngày có hiện lực mới nhất theo asc
        var sortOrder = (request.SortOrder ?? "des").ToLower();
        query = sortOrder == "des"
           ? query.OrderByDescending(x => x.CreatedDate)
           : sortOrder == "asc" ? query.OrderBy(x => x.CreatedDate)
           : query.OrderByDescending(x => x.CreatedDate);

        var department = await query
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        var departmentPagedResult = PagedResult<Domain.Entities.Department>.Create(
            department,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.DepartmentResponse>>(departmentPagedResult);

        return Result.Success(result);
    }
}
