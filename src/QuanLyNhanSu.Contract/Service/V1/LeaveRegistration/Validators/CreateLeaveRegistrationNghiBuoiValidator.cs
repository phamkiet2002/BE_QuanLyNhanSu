using FluentValidation;
using static QuanLyNhanSu.Domain.Enumerations.LeaveRegistrationEnums;

namespace QuanLyNhanSu.Contract.Service.V1.LeaveRegistration.Validators;
public class CreateLeaveRegistrationNghiBuoiValidator : AbstractValidator<Command.CreateLeaveRegistrationNghiBuoiCommand>
{
    public CreateLeaveRegistrationNghiBuoiValidator()
    {
        RuleFor(x => x.HalfDayOff)
        .IsInEnum().WithMessage("Buổi phải là giá trị hợp lệ.");
        RuleFor(x => x.DayOff).NotEmpty().WithMessage("Ngày không được để trống");
        RuleFor(x => x.LeaveReason).NotEmpty().WithMessage("Lý do không được để trống");
        //RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("Nhân viên không được để trống");
        RuleFor(x => x.LeaveDateId).NotEmpty().WithMessage("Ngày nghỉ không được để trống");
    }
}
