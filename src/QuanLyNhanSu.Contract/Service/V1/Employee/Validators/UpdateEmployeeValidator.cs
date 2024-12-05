using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Employee.Validators;
public class UpdateEmployeeValidator : AbstractValidator<Command.UpdateEmployeeCommand>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .Matches(@"^\d+$").WithMessage("Phone must contain only digits")
            .MinimumLength(10).WithMessage("Phone must be at least 10 digits");

        RuleFor(x => x.IdentityCard)
           .NotEmpty().WithMessage("Identity card is required")
           .Matches(@"^\d{12}$").WithMessage("Identity card must be exactly 12 digits");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Gender must be valid");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of Birth is required")
            .LessThan(DateTime.Now).WithMessage("Date of Birth must be in the past");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required")
            .MaximumLength(200).WithMessage("Address cannot exceed 200 characters");

        RuleFor(x => x.BankName)
            .NotEmpty().WithMessage("Bank name is required")
            .MaximumLength(100).WithMessage("Bank name cannot exceed 100 characters");

        RuleFor(x => x.BankAccountNumber)
            .NotEmpty().WithMessage("Bank account number is required")
            .Matches(@"^\d+$").WithMessage("Bank account number must contain only digits")
            .MaximumLength(20).WithMessage("Bank account number cannot exceed 20 digits");
    }
}
