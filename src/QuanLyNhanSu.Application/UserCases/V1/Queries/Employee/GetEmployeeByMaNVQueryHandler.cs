using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Employee;
public sealed class GetEmployeeByMaNVQueryHandler : IQueryHandler<Query.GetEmployeeByMaNVQuery, Response.EmployeeResponse>
{
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeeByMaNVQueryHandler(IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<Result<Response.EmployeeResponse>> Handle(Query.GetEmployeeByMaNVQuery request, CancellationToken cancellationToken)
    {
        var employeeByMaNV = await _employeeRepository.FindAll(x => x.MaNV == request.MaNV).AsNoTracking().FirstOrDefaultAsync(cancellationToken)
            ?? throw new EmployeeException.EmployeeNotFoundException(request.MaNV);

        var result = _mapper.Map<Response.EmployeeResponse>(employeeByMaNV);
        return Result.Success(result);
    }
}
