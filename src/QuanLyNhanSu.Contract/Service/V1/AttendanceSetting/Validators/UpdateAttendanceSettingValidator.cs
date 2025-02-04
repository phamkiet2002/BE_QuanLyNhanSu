using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.AttendanceSetting.Validators;
public class UpdateAttendanceSettingValidator : AbstractValidator<Command.UpdateAttendanceSettingCommand>
{
    public UpdateAttendanceSettingValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Không được để trống.");
        RuleFor(x => x.MaximumLateAllowed)
                .NotEmpty().WithMessage("Không được để trống.")
                .Must(time => time.TotalMinutes > 0).WithMessage("Số phút muộn cần lớn hơn 0.");

        RuleFor(x => x.MaxEarlyLeaveAllowed)
            .NotEmpty().WithMessage("Không được để trống.")
            .Must(time => time.TotalMinutes > 0).WithMessage("Số phút về sớm cần lớn hơn 0.");
    }
}
