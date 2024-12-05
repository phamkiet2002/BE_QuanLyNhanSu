using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Salary;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Salary;

public sealed class GetSalaryQueryHandler : IQueryHandler<Query.GetSalaryQuery, PagedResult<Response.SalaryResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Salary, Guid> _salaryRepository;
    private readonly IMapper _mapper;

    public GetSalaryQueryHandler(IRepositoryBase<Domain.Entities.Salary, Guid> salaryRepository, IMapper mapper)
    {
        _salaryRepository = salaryRepository;
        _mapper = mapper;
    }
    public async Task<Result<PagedResult<Response.SalaryResponse>>> Handle(Query.GetSalaryQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.Salary>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.Salary>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.Salary>.UpperPageSize
            ? PagedResult<Domain.Entities.Salary>.UpperPageSize : request.PageSize;

        var salaries = await _salaryRepository.FindAll()
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await _salaryRepository.FindAll().CountAsync(cancellationToken);

        var contractPagedResult = PagedResult<Domain.Entities.Salary>.Create(
            salaries,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.SalaryResponse>>(contractPagedResult);

        return Result.Success(result);
    }
}
