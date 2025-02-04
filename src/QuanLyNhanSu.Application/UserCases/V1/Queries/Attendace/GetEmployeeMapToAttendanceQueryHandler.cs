using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions.Repositories.Attendance;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Attendace;
public sealed class GetEmployeeMapToAttendanceQueryHandler : IQueryHandler<Query.GetEmployeesMapToAttendanceQuery, PagedResult<Response.EmployeeMapToAttendanceResponse>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IAttendanceRepository _attendanceRepository;

    public GetEmployeeMapToAttendanceQueryHandler(IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository, IMapper mapper, IAttendanceRepository attendanceRepository)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _attendanceRepository = attendanceRepository;
    }

    public async Task<Result<PagedResult<Response.EmployeeMapToAttendanceResponse>>> Handle(Query.GetEmployeesMapToAttendanceQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.Employee>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.Employee>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.Employee>.UpperPageSize
            ? PagedResult<Domain.Entities.Employee>.UpperPageSize : request.PageSize;

        var query = _employeeRepository
            .FindAll(x =>
                x.Status == Domain.Enumerations.StatusEnums.Status.Active &&
                x.EmployeePositions.Any(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active && x.Position.PositionRoles.Any(r => r.AppRole.NormalizedName != "ADMIN"))
            )
            .Include(x => x.WorkPlace)
            .Include(x => x.Attendances)
            .Include(x => x.EmployeeDepartments)
            .AsQueryable();

        var workPlaceName = request.WorkPlaceName?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(workPlaceName))
        {
            query = query.Where(x => x.WorkPlace != null && x.WorkPlace.Name.Contains(workPlaceName));
        }

        var month = request.Month?.Month ?? DateTime.Now.Month;

        // Lấy dữ liệu từ database
        var employees = await query
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        // Tính toán dựa trên danh sách đã tải
        var employeeToAttendances = employees.Select(x =>
        {
            var filteredAttendances = x.Attendances?
                .Where(p => p.CreatedDate.HasValue && p.CreatedDate.Value.Month == month)
                .ToList() ?? new List<Attendance>();

            var workShedule = x.EmployeeDepartments?
                .Where(dept => dept.Department != null && filteredAttendances.Any(att => att.EmployeeId == dept.EmployeeId))
                .Select(dept => dept.Department.WorkShedule)
                .FirstOrDefault();

            var timeWorkShedule = workShedule != null
                ? (workShedule.BreakEndTime - workShedule.BreakStartTime)
                : TimeSpan.Zero;

            var totalWorkingHours = filteredAttendances
                .Where(a => a.CheckIn.HasValue && a.CheckOut.HasValue && a.CheckOut > a.CheckIn)
                .Sum(a =>
                {
                    var workDuration = a.CheckOut.Value - a.CheckIn.Value;

                    var checkInTimeOfDay = a.CheckIn.Value.TimeOfDay;
                    var checkOutTimeOfDay = a.CheckOut.Value.TimeOfDay;

                    if (checkOutTimeOfDay < workShedule.BreakStartTime || checkInTimeOfDay > workShedule.BreakEndTime)
                    {
                        return workDuration.TotalHours;
                    }

                    return (workDuration - timeWorkShedule).TotalHours;
                });

            return new Response.EmployeeMapToAttendanceResponse(
                x.Id,
                x.MaNV,
                x.Name,
                x.WorkPlace != null
                    ? _mapper.Map<QuanLyNhanSu.Contract.Service.V1.WorkPlace.Response.WorkPlaceResponse>(x.WorkPlace)
                    : null,
                _mapper.Map<List<QuanLyNhanSu.Contract.Service.V1.Attendance.Response.AttendanceResponse>>(filteredAttendances),
                TimeSpan.FromHours(totalWorkingHours),
                filteredAttendances.Count(p => p.IsLate.HasValue && p.IsLate.Value),
                TimeSpan.FromMinutes(
                    filteredAttendances.Where(p => p.LateTime.HasValue)
                        .Sum(p => (p.LateTime.Value - DateTime.MinValue).TotalMinutes)
                ),
                filteredAttendances.Count(p => p.IsEarlyLeave.HasValue && p.IsEarlyLeave.Value),
                TimeSpan.FromMinutes(
                    filteredAttendances.Where(p => p.EarlyLeaveTime.HasValue)
                        .Sum(p => (p.EarlyLeaveTime.Value - DateTime.MinValue).TotalMinutes)
                ),
                filteredAttendances.Count(p => p.IsAbsent.HasValue && p.IsAbsent.Value)
            );
        }).ToList();

        var totalCount = await _employeeRepository
            .FindAll(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active)
            .CountAsync(cancellationToken);

        var employeeToAttendancePagedResult = PagedResult<Response.EmployeeMapToAttendanceResponse>.Create(
            employeeToAttendances,
            PageIndex,
            PageSize,
            totalCount
        );

        var result = _mapper.Map<PagedResult<Response.EmployeeMapToAttendanceResponse>>(employeeToAttendancePagedResult);

        return Result.Success(result);
    }
}
