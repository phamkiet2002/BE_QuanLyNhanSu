using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Entities.Identity;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Attendace;
public sealed class GetEmployeeIdMapToAttendaceQueryHandler : IQueryHandler<Query.GetEmployeeByIdAttendaceQuery , Response.EmployeeMapToAttendanceResponse>
{
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetEmployeeIdMapToAttendaceQueryHandler
    (
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository, 
        IMapper mapper, UserManager<AppUser> userManager, 
        IHttpContextAccessor httpContextAccessor
    )
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<Response.EmployeeMapToAttendanceResponse>> Handle(Query.GetEmployeeByIdAttendaceQuery request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User
            ?? throw new Exception("Người dùng không xác thực.");

        var userId = _userManager.GetUserId(user);

        var appUser = await _userManager.FindByIdAsync(userId)
            ?? throw new Exception("Người dùng không tồn tại.");

        var employeeId = appUser.EmployeeId;

        var employee = await _employeeRepository.FindAll(x => x.Id == employeeId).FirstOrDefaultAsync()
            ?? throw new Exception("Nhân viên không tồn tại.");

        var employeeById = await _employeeRepository.FindByIdAsync(employeeId.Value)
            ?? throw new EmployeeException.EmployeeNotFoundException(employeeId.Value);
        var result = _mapper.Map<Response.EmployeeMapToAttendanceResponse>(employeeById);
        return Result.Success(result);
    }
}
