using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.PayRoll.Validators;
public class CreatePayRollValidator : AbstractValidator<Command.CreatePayRollCommand>
{
    public CreatePayRollValidator()
    {
        //RuleFor(x => x.SalaryGross).GreaterThan(0);
        //RuleFor(x => x.SalaryNet).GreaterThan(0);
    }
}
