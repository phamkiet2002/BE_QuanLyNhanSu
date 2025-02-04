using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.EmployeePosition;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.EmployeePosition;

public sealed class GetEmployeePositionQueryHandler : IQueryHandler<Query.GetEmployeePositionsQuery, PagedResult<Response.EmployeePositionResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.EmployeePosition, Guid> _employeePositionRepository;
    private readonly IMapper _mapper;

    public GetEmployeePositionQueryHandler(IRepositoryBase<Domain.Entities.EmployeePosition, Guid> employeePositionRepository, IMapper mapper)
    {
        _employeePositionRepository = employeePositionRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.EmployeePositionResponse>>> Handle(Query.GetEmployeePositionsQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.EmployeePosition>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.EmployeePosition>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.EmployeePosition>.UpperPageSize
            ? PagedResult<Domain.Entities.EmployeePosition>.UpperPageSize : request.PageSize;

        var employeePositions = await _employeePositionRepository.FindAll()
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await _employeePositionRepository.FindAll().CountAsync(cancellationToken);

        var employeePositionPagedResult = PagedResult<Domain.Entities.EmployeePosition>.Create(
            employeePositions,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.EmployeePositionResponse>>(employeePositionPagedResult);

        return Result.Success(result);
    }
}
