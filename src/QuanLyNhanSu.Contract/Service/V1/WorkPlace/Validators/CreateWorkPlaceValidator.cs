using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.WorkPlace.Validators;

public class CreateWorkPlaceValidator : AbstractValidator<Command.CreateWorkPlaceCommand>
{
    public CreateWorkPlaceValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(10)
            .Matches(@"^\+?\d{10,15}$").WithMessage("Số điện thoại không hợp lệ.");
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Địa chỉ Email không hợp lệ.")
            .MaximumLength(100);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(200);
    }
}
