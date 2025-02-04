using FluentValidation;
using QuanLyNhanSu.Contract.Service.V1.Employee;

namespace QuanLyNhanSu.Contract.Service.V1.LeaveRegistration.Validators;
public class CreateLeaveRegistrationNghiNgayValidator : AbstractValidator<Command.CreateLeaveRegistrationNghiNgayCommand>
{
    public CreateLeaveRegistrationNghiNgayValidator()
    {
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("Ngày bắt đầu không được để trống");
        RuleFor(x => x.EndDate).NotEmpty().WithMessage("Ngày kết thúc không được để trống");
        RuleFor(x => x.LeaveReason).NotEmpty().WithMessage("Lý do không được để trống");
        //RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("Nhân viên không được để trống");
        RuleFor(x => x.LeaveDateId).NotEmpty().WithMessage("Ngày nghỉ không được để trống");
    }
}
