using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Entities.Identity;
using static QuanLyNhanSu.Contract.Service.V1.LeaveRegistration.Response;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.LeaveRegistration;
public sealed class GetEmployeeIdLeaveRegistrationQueryHandler : IQueryHandler<Query.GetEmployeeByIdLeaveRegistrationQuery, Response.EmployeeByIdLeaveRegistrationResponse>
{
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> _leaveRegistrationRepository;

    public GetEmployeeIdLeaveRegistrationQueryHandler
    (
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository,
        IMapper mapper, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor,
        IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> leaveRegistrationRepository
    )
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _leaveRegistrationRepository = leaveRegistrationRepository;
    }

    public async Task<Result<Response.EmployeeByIdLeaveRegistrationResponse>> Handle(Query.GetEmployeeByIdLeaveRegistrationQuery request, CancellationToken cancellationToken)
    {
        #region ======= đăng nhập ========

        var user = _httpContextAccessor.HttpContext?.User
            ?? throw new Exception("Người dùng không xác thực.");

        var userId = _userManager.GetUserId(user);

        var appUser = await _userManager.FindByIdAsync(userId)
            ?? throw new Exception("Người dùng không tồn tại.");

        var employeeId = appUser.EmployeeId;

        #endregion

        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.LeaveRegistration>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.LeaveRegistration>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.LeaveRegistration>.UpperPageSize
            ? PagedResult<Domain.Entities.LeaveRegistration>.UpperPageSize : request.PageSize;

        var employee = await _employeeRepository.FindAll(x => x.Id == employeeId).FirstOrDefaultAsync(cancellationToken)
            ?? throw new Exception("Nhân viên không tồn tại.");

        var month = request.Month?.Month ?? DateTime.Now.Month;
        var year = request.Month?.Year ?? DateTime.Now.Year;
        var leaveRegistrations = employee.LeaveRegistrations?
        .Where(x => x.CreatedDate.HasValue && x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == year)
        .ToList();

        var sortOrder = (request.SortOrder ?? "des").ToLower();
        employee.LeaveRegistrations = sortOrder == "des"
           ? employee.LeaveRegistrations.OrderByDescending(x => x.CreatedDate).ToList()
           : sortOrder == "asc" ? employee.LeaveRegistrations.OrderBy(x => x.CreatedDate).ToList()
           : employee.LeaveRegistrations.OrderByDescending(x => x.CreatedDate).ToList();

        var pagedLeaveRegistrations = leaveRegistrations
        .Skip((PageIndex - 1) * PageSize)
        .Take(PageSize)
        .ToList();

        var totalCount = leaveRegistrations.Count();

        var leaveRegistrationPagedResult = PagedResult<Domain.Entities.LeaveRegistration>.Create
        (
            pagedLeaveRegistrations,
            PageIndex,
            PageSize,
            totalCount
        );

        var leaveRegistrationPagedResultMapped = _mapper.Map<PagedResult<LeaveRegistrationResponse>>(leaveRegistrationPagedResult);

        //var result = _mapper.Map<Response.EmployeeByIdLeaveRegistrationResponse>(leaveRegistrationPagedResult);

        var result = new Response.EmployeeByIdLeaveRegistrationResponse
        (
            employee.Id,
            employee.MaNV,
            employee.Name,
            leaveRegistrationPagedResultMapped
        );

        return Result.Success(result);
    }
}
