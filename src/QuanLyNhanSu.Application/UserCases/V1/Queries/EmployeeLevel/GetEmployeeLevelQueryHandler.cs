using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.EmployeeLevel;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.EmployeeLevel;

public sealed class GetEmployeeLevelQueryHandler : IQueryHandler<Query.GetEmployeeLevelsQuery, PagedResult<Response.EmployeeLevelResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.EmployeeLevel, Guid> _employeeLevelRepository;
    private readonly IMapper _mapper;

    public GetEmployeeLevelQueryHandler(IRepositoryBase<Domain.Entities.EmployeeLevel, Guid> employeeLevelRepository, IMapper mapper)
    {
        _employeeLevelRepository = employeeLevelRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.EmployeeLevelResponse>>> Handle(Query.GetEmployeeLevelsQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.EmployeeLevel>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.EmployeeLevel>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.EmployeeLevel>.UpperPageSize
            ? PagedResult<Domain.Entities.EmployeeLevel>.UpperPageSize : request.PageSize;

        var employeeLevels = await _employeeLevelRepository.FindAll()
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await _employeeLevelRepository.FindAll().CountAsync(cancellationToken);

        var employeeLevelPagedResult = PagedResult<Domain.Entities.EmployeeLevel>.Create(
            employeeLevels,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.EmployeeLevelResponse>>(employeeLevelPagedResult);

        return Result.Success(result);
    }

    //public async Task<Result<List<Response.EmployeeLevelResponse>>> Handle(Query.GetEmployeeLevelsQuery request, CancellationToken cancellationToken)
    //{
    //    var employeeLevels = await _employeeLevelRepository.FindAll().ToListAsync();
    //    var result = _mapper.Map<List<Response.EmployeeLevelResponse>>(employeeLevels);
    //    return Result.Success(result);
    //}
}
