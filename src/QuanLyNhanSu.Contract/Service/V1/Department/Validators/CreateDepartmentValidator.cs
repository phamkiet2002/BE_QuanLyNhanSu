using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Department.Validators;
public class CreateAttendanceValidator : AbstractValidator<Command.CreateDepartmentCommand>
{
    public CreateAttendanceValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("tên không được để trống.")
           .MaximumLength(100).WithMessage("tên level không vượt quá 100 ký tự.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Mô tả không được để trống.")
            .MaximumLength(100).WithMessage("Không được dài hơn 100 ký tự.");

        RuleFor(x => x.WorkPlaceId)
            .NotEmpty().WithMessage("WorkPlaceId không được để trống.");

        RuleFor(x => x.WorkSheduleId)
            .NotEmpty().WithMessage("WorkSheduleId không được để trống.");
    }
}
