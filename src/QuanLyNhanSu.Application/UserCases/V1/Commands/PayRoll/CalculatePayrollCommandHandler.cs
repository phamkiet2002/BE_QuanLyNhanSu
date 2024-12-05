using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.PayRoll;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions.Repositories.Attendance;
using QuanLyNhanSu.Domain.Entities;
using static QuanLyNhanSu.Domain.Enumerations.LeaveRegistrationEnums;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.PayRoll;
public sealed class CalculatePayrollCommandHandler : ICommandHandler<Command.CalculatePayrollResponseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.PayRoll, Guid> _payRollRepository;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IRepositoryBase<Domain.Entities.WorkPlace, Guid> _workPlaceRepository;
    private readonly IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> _leaveRegistrationRepository;
    private readonly IRepositoryBase<Domain.Entities.Attendance, Guid> _attendanceRepository;
    private readonly IRepositoryBase<Domain.Entities.LeaveDate, Guid> _leaveDateRepository;
    private readonly IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> _allowanceAndPenaltiesRepository;
    private readonly IAttendanceRepository _repositoryOfAttendance;

    public CalculatePayrollCommandHandler
    (
        IUnitOfWork unitOfWork,
        IRepositoryBase<Domain.Entities.PayRoll, Guid> payRollRepository,
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository,
        IRepositoryBase<Domain.Entities.WorkPlace, Guid> workPlaceRepository,
        IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> leaveRegistrationRepository,
        IRepositoryBase<Domain.Entities.Attendance, Guid> attendanceRepository,
        IRepositoryBase<Domain.Entities.LeaveDate, Guid> leaveDateRepository,
        IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> allowanceAndPenaltiesRepository,
        IAttendanceRepository repositoryOfAttendance
    )
    {
        _unitOfWork = unitOfWork;
        _payRollRepository = payRollRepository;
        _employeeRepository = employeeRepository;
        _workPlaceRepository = workPlaceRepository;
        _leaveRegistrationRepository = leaveRegistrationRepository;
        _attendanceRepository = attendanceRepository;
        _leaveDateRepository = leaveDateRepository;
        _allowanceAndPenaltiesRepository = allowanceAndPenaltiesRepository;
        _repositoryOfAttendance = repositoryOfAttendance;
    }

    public async Task<Result> Handle(Command.CalculatePayrollResponseCommand request, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.FindAll(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active).ToListAsync();
        // đầu tiên là lấy phụ cấp phạt cho nhân viên thuộc điểm làm việc mà có những phụ cấp phạt đó ===========================================> rồi
        // lấy danh sách đăng ký nghỉ phép của nhân viên ========================================================================================> rồi
        // lấy danh sách chấm công của nhân viên ================================================================================================> rồi
        // lấy danh sách lương cơ bản của nhân viên =============================================================================================> rồi
        // lấy tổng tiền phụ cấp ================================================================================================================> rồi
        // lấy tổng tiền phạt ===> chỉ áp dụng khi nhân viên vi phạm
        // kiểm tra nhân viên có nghỉ quá ngày nghỉ phép không => nếu có thì bị trừ lương
        // kiểm tra nhân viên có đi trễ, về sớm không, nghỉ không phép => nếu có thì bị trừ lương
        // update vào payroll của nhân viên
        foreach (var employee in employees)
        {
            // điểm làm việc mà nhân viên đó đang làm việc
            var workPlace = await _workPlaceRepository.FindAll(x => x.Id == employee.WorkPlaceId && x.IsDeleted != true).ToListAsync();
            // lấy danh sách phụ cấp điểm làm việc đó
            var allowances = _allowanceAndPenaltiesRepository
                .FindAll
                (
                    x =>
                        x.Type == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeAllowanceAndPenalties.Phucap &&
                        x.IsDeleted != true &&
                        x.WorkPlaceAndAllowanceAndPenalties.ToList().Any(x => x.WorkPlaceId == employee.WorkPlaceId && x.Status == Domain.Enumerations.StatusEnums.Status.Active)
                ).ToList();
            // lấy danh sách phạt của điểm làm việc
            var penalties = _allowanceAndPenaltiesRepository
                .FindAll
                (
                    x =>
                        x.Type == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeAllowanceAndPenalties.Phat &&
                        x.IsDeleted != true &&
                        x.WorkPlaceAndAllowanceAndPenalties.ToList().Any(x => x.WorkPlaceId == employee.WorkPlaceId && x.Status == Domain.Enumerations.StatusEnums.Status.Active)
                ).ToList();

            // lấy lương cơ bản của nhân viên
            var salary = employee.Salarys.Where(x => x.EmployeeId == employee.Id && x.Status == Domain.Enumerations.StatusEnums.Status.Active).Select(x => x.Salarys).FirstOrDefault();
            // lấy danh sách nghỉ phép của nhân viên
            var leaveRegistration = _leaveRegistrationRepository
                .FindAll(x => x.EmployeeId == employee.Id && x.PendingApproval == Domain.Enumerations.ApproveEmuns.PendingApproval.Daduyet && x.CreatedDate.Value.Month == DateTime.Now.Month)
                .ToList();
            // lấy danh sách chấm công của nhân viên
            var attendace = _attendanceRepository.FindAll(x => x.EmployeeId == employee.Id && x.CreatedDate.Value.Month == DateTime.Now.Month).ToList();
            // lấy tổng tiền phụ cấp
            var totalAllowance = allowances.Sum(x => x.Money);
            // lấy tổng tiền phạt
            decimal totalPenalty = 0;
            // kiểm tra nhân viên có nghỉ quá ngày nghỉ phép không => nếu có thì bị trừ lương
            var halfDayCount = leaveRegistration
                .Count(x => x.HalfDayOff == HalfDayOff.Sang || x.HalfDayOff == HalfDayOff.Chieu);
            var dayOff = leaveRegistration.Sum(x => (x.EndDate.Value - x.StartDate.Value).TotalDays);
            var totalLeaveDateCount = halfDayCount * 0.5 + dayOff;
            var totalAnnualLeaveDate = _leaveDateRepository.FindAll(x => x.IsDeleted != true && leaveRegistration.Equals(x.Id)).ToList();
            if (totalAnnualLeaveDate.Any(x => x.TotalAnnualLeaveDate < (int)totalLeaveDateCount))
            {
                var totalLeaveDate = totalLeaveDateCount - totalAnnualLeaveDate.Sum(x => x.TotalAnnualLeaveDate);
                var LeaveThanAllowed = penalties
                    .Where(x => x.TypeOfPenalty == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeOfPenalty.Nghiquangayphep)
                    .Sum(x => x.Money);
                totalPenalty += (int)totalLeaveDate * LeaveThanAllowed;
            }
            // kiểm tra nhân viên có đi trễ, về sớm không, nghỉ không phép => nếu có thì bị trừ lương
            var totalIsLate = attendace.Count(x => x.IsLate == true);
            if (totalIsLate > 0)
            {
                // thưc trừ lương
                var isLate = penalties
                    .Where(x => x.TypeOfPenalty == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeOfPenalty.Dimuon)
                    .Sum(x => x.Money);
                totalPenalty += totalIsLate * isLate;
            }
            var totalIsEarlyLeave = attendace.Count(x => x.IsEarlyLeave == true);
            if (totalIsEarlyLeave > 0)
            {
                // thưc trừ lương
                var IsEarlyLeave = penalties
                    .Where(x => x.TypeOfPenalty == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeOfPenalty.Vesom)
                    .Sum(x => x.Money);
                totalPenalty += totalIsLate * IsEarlyLeave;
            }
            // tổng giờ làm việc của nhân viên
            double totalWorkHours = 0;
            var attendaceTimeWorking = await _attendanceRepository.FindAll(x => x.Employee.Status == Domain.Enumerations.StatusEnums.Status.Active).ToListAsync();
            foreach (var item in attendaceTimeWorking)
            {
                if (item.CheckOut > item.CheckIn && item.CheckOut != null)
                {
                    var workShedule = item.Employee.EmployeeDepartments
                        .Select(x => x.Department.WorkShedule)
                        .FirstOrDefault();
                    totalWorkHours +=
                        _repositoryOfAttendance
                        .CalculateWorkingHours(item, workShedule);
                }
            }
            // => đây là ngày mặc định số ngày làm việc trong tháng làm việc trong tháng => có thể thêm bảng cấu hình thời gian làm việc để thay thế cho ngày làm việc mặc định
            int StandardWorkingDays = 22;
            TimeSpan timeTotalWorking = TimeSpan.FromHours(totalWorkHours);
            int actualWorkingDays = (int)timeTotalWorking.TotalHours / 8;

            decimal salaryGross = salary / StandardWorkingDays + actualWorkingDays + totalAllowance;
            decimal salaryNet = salaryGross - totalPenalty;

            // update vào payroll của nhân viên
            var payRoll = await _payRollRepository.FindAll(x => x.EmployeeId == employee.Id).ToListAsync();

            foreach (var item in payRoll)
            {
                item.UpdateSalary
                (
                    salaryGross,
                    salaryNet
                );
            }
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
