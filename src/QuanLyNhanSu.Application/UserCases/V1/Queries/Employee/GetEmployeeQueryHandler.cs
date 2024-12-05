using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Employee;

public sealed class GetEmployeeQueryHandler : IQueryHandler<Query.GetEmployeesQuery, PagedResult<Response.EmployeeResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeeQueryHandler(IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.EmployeeResponse>>> Handle(Query.GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.Employee>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.Employee>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.Employee>.UpperPageSize
            ? PagedResult<Domain.Entities.Employee>.UpperPageSize : request.PageSize;

        var query = _employeeRepository.FindAll()
            .Include(x => x.EmployeeDepartments).ThenInclude(x => x.Department)
            .Include(x => x.EmployeePositions).ThenInclude(x => x.Position)
            .Include(x => x.EmployeeLevels).ThenInclude(x => x.Level)
            .Include(x => x.PayRolls)
            //.Include(x => x.Salarys)
            .AsQueryable();

        var searchTerms = request.SearchTerm?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(searchTerms))
        {
            query = query.Where(x => x.Name.Contains(searchTerms));
        }

        var workPlaceName = request.WorkPlaceName?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(workPlaceName))
        {
            query = query.Where(x => x.WorkPlace.Name.Contains(workPlaceName));
        }

        var departmentName = request.DepartmentName?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(departmentName))
        {
            query = query.Where(x => x.EmployeeDepartments.Any(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active && x.Department.Name.Contains(departmentName)));
        }

        var positionName = request.PositionName?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(positionName))
        {
            query = query.Where(x => x.EmployeePositions.Any(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active && x.Position.Name.Contains(positionName)));
        }

        var levelName = request.LevelName?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(levelName))
        {
            query = query.Where(x => x.EmployeeLevels.Any(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active && x.Level.Name.Contains(levelName)));
        }

        var status = request.Status ?? Domain.Enumerations.StatusEnums.Status.Active;
        query = query.Where(x => x.Status == status);

        // sắp xếp theo ngày có hiện lực mới nhất theo asc
        var sortOrder = (request.SortOrder ?? "des").ToLower();
        query = sortOrder == "des"
           ? query.OrderByDescending(x => x.CreatedDate)
           : sortOrder == "asc" ? query.OrderBy(x => x.CreatedDate)
           : query.OrderByDescending(x => x.CreatedDate);

        var employee = await query
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        //foreach (var emp in employee)
        //{
        //    emp.EmployeeDepartments = emp.EmployeeDepartments
        //        .Where(dep => dep.Status == Domain.Enumerations.StatusEnums.Status.Active)
        //        .ToList();

        //    emp.EmployeePositions = emp.EmployeePositions
        //        .Where(pos => pos.Status == Domain.Enumerations.StatusEnums.Status.Active)
        //        .ToList();

        //    emp.EmployeeLevels = emp.EmployeeLevels
        //        .Where(lv => lv.Status == Domain.Enumerations.StatusEnums.Status.Active)
        //        .ToList();
        //}

        var totalCount = await query.CountAsync(cancellationToken);

        var employeePagedResult = PagedResult<Domain.Entities.Employee>.Create(
            employee,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.EmployeeResponse>>(employeePagedResult);

        return Result.Success(result);
    }
}
