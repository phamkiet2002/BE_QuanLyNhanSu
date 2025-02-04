using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Entities.Identity;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Employee;
public sealed class GetEmployeeByIdQueryHandler : IQueryHandler<Query.GetEmployeeByIdQuery, Response.EmployeeResponse>
{
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetEmployeeByIdQueryHandler
    (
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository, IMapper mapper, 
        UserManager<AppUser> userManager, 
        IHttpContextAccessor httpContextAccessor
    )
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<Response.EmployeeResponse>> Handle(Query.GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {

        Guid employeeId;

        if (request.Id == Guid.Empty)
        {
            var user = _httpContextAccessor.HttpContext?.User
                ?? throw new UnauthorizedAccessException("Người dùng không xác thực.");

            var userId = _userManager.GetUserId(user);
            var appUser = await _userManager.FindByIdAsync(userId)
                ?? throw new Exception("Người dùng không tồn tại.");

            employeeId = appUser.EmployeeId
                ?? throw new Exception("Người dùng không liên kết với nhân viên.");
        }
        else
        {
            employeeId = request.Id;
        }

        var employeeById = await _employeeRepository.FindByIdAsync(employeeId)
            ?? throw new EmployeeException.EmployeeNotFoundException(employeeId);
        var result = _mapper.Map<Response.EmployeeResponse>(employeeById);
        return Result.Success(result);
    }
}
