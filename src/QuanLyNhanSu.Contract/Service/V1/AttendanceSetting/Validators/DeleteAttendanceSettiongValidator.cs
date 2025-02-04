using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.AttendanceSetting.Validators;
public class DeleteAttendanceSettiongValidator : AbstractValidator<Command.DeleteAttendanceSettingCommand>
{
    public DeleteAttendanceSettiongValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}
