using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions.Repositories.Employee;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Employee;
public sealed class GetEmployeePayRollQueryHandler : IQueryHandler<Query.GetEmplpyeePayRollQuery, PagedResult<Response.EmployeePayRollResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _repositoryOfEmployeeRepository;

    public GetEmployeePayRollQueryHandler(IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository, IMapper mapper, IEmployeeRepository repositoryOfEmployeeRepository)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _repositoryOfEmployeeRepository = repositoryOfEmployeeRepository;
    }

    public async Task<Result<PagedResult<Response.EmployeePayRollResponse>>> Handle(Query.GetEmplpyeePayRollQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.Employee>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.Employee>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.Employee>.UpperPageSize
            ? PagedResult<Domain.Entities.Employee>.UpperPageSize : request.PageSize;

        var query = _employeeRepository
            .FindAll(x =>
                x.Status == Domain.Enumerations.StatusEnums.Status.Active &&
                x.EmployeePositions.Any(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active && x.Position.PositionRoles.Any(r => r.AppRole.NormalizedName != "ADMIN"))
            )
            .Include(x => x.WorkPlace).ThenInclude(x => x.WifiConfig)
            .Include(x => x.EmployeeDepartments).ThenInclude(x => x.Department)
            .Include(x => x.EmployeePositions).ThenInclude(x => x.Position)
            .Include(x => x.EmployeeLevels).ThenInclude(x => x.Level)
            .Include(x => x.Salarys)
            .Include(x => x.PayRolls)
            .AsQueryable();

        var employeeName = request.EmployeeName?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(employeeName))
        {
            query = query.Where(x => x.Name.Contains(employeeName));
        }

        var workPlaceName = request.WorkPlaceName?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(workPlaceName))
        {
            query = query.Where(x => x.WorkPlace.Name.Contains(workPlaceName));
        }

        var month = request.Month?.Month ?? DateTime.Now.Month;
        var filteredQuery = query.Select(emp => new Response.EmployeePayRollResponse(
            emp.Id,
            emp.MaNV,
            emp.Name,
            emp.JoinDate,
            emp.BankName,
            emp.BankAccountNumber,
            _mapper.Map<QuanLyNhanSu.Contract.Service.V1.WorkPlace.Response.WorkPlaceResponse>(emp.WorkPlace),
            emp.EmployeeDepartments.FirstOrDefault(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active).Department.Name,
            emp.EmployeePositions.FirstOrDefault(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active).Position.Name,
            emp.EmployeeLevels.FirstOrDefault(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active).Level.Name,
            _mapper.Map<List<QuanLyNhanSu.Contract.Service.V1.Salary.Response.SalaryResponse>>(emp.Salarys.Where(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active).ToList()),
            _mapper.Map<List<QuanLyNhanSu.Contract.Service.V1.PayRoll.Response.PayRollResponse>>(emp.PayRolls.Where(p => p.CreatedDate.Value.Month == month).ToList())
        ));

        var employee = await filteredQuery?
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        var employeePagedResult = PagedResult<Response.EmployeePayRollResponse>.Create(
            employee,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.EmployeePayRollResponse>>(employeePagedResult);

        return Result.Success(result);
    }
}
