using FluentValidation;
using QuanLyNhanSu.Contract.Service.V1.Employee;

namespace QuanLyNhanSu.Contract.Service.V1.Level.Validators;
public class CreateLevelValidator : AbstractValidator<Command.CreateLevelCommand>
{
    public CreateLevelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("tên không được để trống.")
            .MaximumLength(100).WithMessage("tên level không vượt quá 100 ký tự.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Mô tả không được để trống.")
            .MaximumLength(100).WithMessage("Không được dài hơn 100 ký tự.");
    }
}
