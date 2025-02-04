using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Level.Validators;
public class UpdateLevelValidator : AbstractValidator<Command.UpdateLevelCommand>
{
    public UpdateLevelValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được để trống.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("tên không được để trống.")
            .MaximumLength(100).WithMessage("tên level không vượt quá 100 ký tự.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Mô tả không được để trống.")
            .MaximumLength(100).WithMessage("Không được dài hơn 100 ký tự.");
    }
}
