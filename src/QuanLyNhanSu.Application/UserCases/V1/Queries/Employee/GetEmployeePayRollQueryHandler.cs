using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions.Repositories.Employee;

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

        var query = _employeeRepository.FindAll(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active)
            .Include(x => x.EmployeeDepartments).ThenInclude(x => x.Department)
            .Include(x => x.EmployeePositions).ThenInclude(x => x.Position)
            .Include(x => x.EmployeeLevels).ThenInclude(x => x.Level)
            .Include(x => x.Salarys)
            .Include(x => x.PayRolls)
            .AsQueryable();

        var employee = await query
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        var employeePagedResult = PagedResult<Domain.Entities.Employee>.Create(
            employee ?? new List<Domain.Entities.Employee>(),
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.EmployeePayRollResponse>>(employeePagedResult);

        return Result.Success(result);
    }
}
