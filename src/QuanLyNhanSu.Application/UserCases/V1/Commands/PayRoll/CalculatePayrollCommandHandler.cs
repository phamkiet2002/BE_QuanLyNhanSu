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
                .Count(x => x.HalfDayOff == HalfDayOff.Sang || x.HalfDayOff == HalfDayOff.Chieu && x.TypeOfLeave == TypeOfLeave.Nghibuoi);
            var dayOff = leaveRegistration
                .Where(x => x.StartDate.HasValue && x.EndDate.HasValue && x.TypeOfLeave == TypeOfLeave.Nghingay)
                .Sum(x =>
                {
                    DateTime startDate = DateTime.Parse(x.StartDate.Value.ToString()).Date;
                    DateTime endDate = DateTime.Parse(x.EndDate.Value.ToString()).Date;

                    var daysInRange = Enumerable.Range(0, (endDate - startDate).Days + 1)
                        .Select(offset => startDate.AddDays(offset));

                    var workingDays = daysInRange
                        .Where(day => day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday);
                    return workingDays.Count();
                });
            var totalLeaveDateCount = halfDayCount * 0.5 + dayOff;
            var totalAnnualLeaveDate = _leaveDateRepository
                .FindAll(x => x.IsDeleted != true && x.IsHoliday != true)
                .AsEnumerable()
                .Where(x => leaveRegistration.Any(lr => lr.LeaveDateId == x.Id))
                .ToList();
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
                var isLate = penalties
                    .Where(x => x.TypeOfPenalty == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeOfPenalty.Dimuon)
                    .Sum(x => x.Money);
                totalPenalty += totalIsLate * isLate;
            }
            var totalIsEarlyLeave = attendace.Count(x => x.IsEarlyLeave == true);
            if (totalIsEarlyLeave > 0)
            {
                var IsEarlyLeave = penalties
                    .Where(x => x.TypeOfPenalty == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeOfPenalty.Vesom)
                    .Sum(x => x.Money);
                totalPenalty += totalIsLate * IsEarlyLeave;
            }
            // vắng mặt quá thời gian cho phép
            var countTotalOvertimeOutsideWorkHours = attendace.Count(x => x.OvertimeOutsideWorkHours == true);
            if (countTotalOvertimeOutsideWorkHours > 0)
            {
                var totalTimeOvertimeOutsideWorkHours = penalties
                    .Where(x => x.TypeOfPenalty == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeOfPenalty.RaNgoaiQuaGio)
                    .Sum(x => x.Money);
                totalPenalty += countTotalOvertimeOutsideWorkHours * totalTimeOvertimeOutsideWorkHours;
            }
            // nghỉ không phép
            var totalIsAbsent = attendace.Count(x => x.IsAbsent == true);
            if (totalIsAbsent > 0)
            {
                var IsAbsent = penalties
                    .Where(x => x.TypeOfPenalty == Domain.Enumerations.AllowanceAndPenaltyEnums.TypeOfPenalty.Tunghikhongphep)
                    .Sum(x => x.Money);
                totalPenalty += totalIsAbsent * IsAbsent;
            }
            // tổng giờ làm việc của nhân viên
            double totalWorkHours = 0;
            var attendaceTimeWorking = await _attendanceRepository.FindAll(x => x.EmployeeId == employee.Id && x.Employee.Status == Domain.Enumerations.StatusEnums.Status.Active).ToListAsync();
            foreach (var item in attendaceTimeWorking)
            {
                if (item.PendingApproval != Domain.Enumerations.ApproveEmuns.PendingApproval.Chuaduyet && item.PendingApproval != Domain.Enumerations.ApproveEmuns.PendingApproval.Tuchoi)
                {
                    if (item.CheckIn.HasValue && item.CheckOut.HasValue && item.CheckOut > item.CheckIn)
                    {
                        var workShedule = item.Employee.EmployeeDepartments.Where(x => x.EmployeeId == employee.Id)
                            .Select(x => x.Department.WorkShedule)
                            .FirstOrDefault();
                        totalWorkHours += _repositoryOfAttendance.CalculateWorkingHours(item, workShedule);
                    }
                }
            }

            //lấy tổng số ngày trong tháng đã trừ thứ 7 và chủ nhật
            int workingDayInMonth = 0;

            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, day);

                if (currentDay.DayOfWeek != DayOfWeek.Saturday && currentDay.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDayInMonth++;
                }
            }
            // lấy tổng số ngày nghỉ trong 1 tháng trừ thứ 7 chủ nhật
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            // Lấy danh sách các ngày nghỉ trong tháng hiện tại
            var totalHolidaysInMonth = _leaveDateRepository.FindAll(x => x.IsHoliday == true)
                .Where(x => x.StartDate.Value.Year == currentYear && x.EndDate.Value.Year == currentYear)
                .Where(x => x.StartDate.Value.Month == currentMonth || x.EndDate.Value.Month == currentMonth)
                .ToList()
                .SelectMany(x =>
                {
                    DateTime start = x.StartDate.Value;
                    DateTime end = x.EndDate.Value;

                    return Enumerable.Range(0, (end - start).Days + 1)
                                     .Select(offset => start.AddDays(offset));
                })
                .Where(date => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday) // Loại bỏ Thứ 7 và Chủ Nhật
                .Distinct() // Loại trùng lặp ngày
                .Count();

            int StandardWorkingDays = workingDayInMonth - totalHolidaysInMonth;

            TimeSpan timeTotalWorking = TimeSpan.FromHours(totalWorkHours);
            //double actualWorkingDays = (int)timeTotalWorking.TotalHours / 8;
            double actualWorkingDays = Math.Floor(timeTotalWorking.TotalHours / 8 * 10) / 10;

            decimal salaryGross = salary / StandardWorkingDays * (int)actualWorkingDays + totalAllowance;
            decimal salaryNet = salaryGross - totalPenalty;

            // update vào payroll của nhân viên
            var payRoll = await _payRollRepository.FindAll(x => x.EmployeeId == employee.Id).ToListAsync();

            if (payRoll != null)
            {
                foreach (var item in payRoll)
                {
                    if (item.CreatedDate.Value.Month == DateTime.Now.Month && item.PayRollStatus != Domain.Enumerations.PayRollEnums.PayRollStatus.PAID)
                    {
                        item.UpdateSalary
                        (
                            actualWorkingDays > 0 ? actualWorkingDays : 0,
                            totalAllowance > 0 ? totalAllowance : 0m,
                            totalPenalty > 0 ? totalPenalty : 0m,
                            salaryGross,
                            salaryNet
                        );
                        _payRollRepository.Update(item);
                    }
                }
            }
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
