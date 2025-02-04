using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Account.Validators;
public class LoginValidator : AbstractValidator<Command.LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
