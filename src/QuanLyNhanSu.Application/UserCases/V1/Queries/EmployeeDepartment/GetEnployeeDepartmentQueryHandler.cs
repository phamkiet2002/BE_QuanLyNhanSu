using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.EmployeeDepartment;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.EmployeeDepartment;

public sealed class GetEnployeeDepartmentQueryHandler : IQueryHandler<Query.GetEmployeeDepartmentsQuery, PagedResult<Response.EmployeeDepartmentResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.EmployeeDepartment, Guid> _employeeDepartmentRepository;
    private readonly IMapper _mapper;

    public GetEnployeeDepartmentQueryHandler(IRepositoryBase<Domain.Entities.EmployeeDepartment, Guid> employeeDepartmentRepository, IMapper mapper)
    {
        _employeeDepartmentRepository = employeeDepartmentRepository; 
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.EmployeeDepartmentResponse>>> Handle(Query.GetEmployeeDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.EmployeeDepartment>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.EmployeeDepartment>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.EmployeeDepartment>.UpperPageSize
            ? PagedResult<Domain.Entities.EmployeeDepartment>.UpperPageSize : request.PageSize;

        var employeeDepartments = await _employeeDepartmentRepository.FindAll()
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await _employeeDepartmentRepository.FindAll().CountAsync(cancellationToken);

        var employeeLevelPagedResult = PagedResult<Domain.Entities.EmployeeDepartment>.Create(
            employeeDepartments,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.EmployeeDepartmentResponse>>(employeeLevelPagedResult);

        return Result.Success(result);

    }
}
