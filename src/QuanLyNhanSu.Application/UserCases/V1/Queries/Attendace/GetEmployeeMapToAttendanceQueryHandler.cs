using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Attendace;
public sealed class GetEmployeeMapToAttendanceQueryHandler : IQueryHandler<Query.GetEmployeesMapToAttendanceQuery, PagedResult<Response.EmployeeMapToAttendanceResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeeMapToAttendanceQueryHandler(IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.EmployeeMapToAttendanceResponse>>> Handle(Query.GetEmployeesMapToAttendanceQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.Employee>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.Employee>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.Employee>.UpperPageSize
            ? PagedResult<Domain.Entities.Employee>.UpperPageSize : request.PageSize;

        var employeeToAttendances = await _employeeRepository.FindAll(x=> x.Status == Domain.Enumerations.StatusEnums.Status.Active)
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await _employeeRepository.FindAll().CountAsync(cancellationToken);

        var employeeToAttendancePagedResult = PagedResult<Domain.Entities.Employee>.Create(
            employeeToAttendances,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.EmployeeMapToAttendanceResponse>>(employeeToAttendancePagedResult);

        return Result.Success(result);
    }
}
