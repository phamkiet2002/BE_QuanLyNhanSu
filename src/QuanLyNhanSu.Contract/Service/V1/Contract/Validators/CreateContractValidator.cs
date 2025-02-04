using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Contract.Validators;
public class CreateContractValidator : AbstractValidator<Command.CreateContractCommand>
{
    public CreateContractValidator()
    {
        RuleFor(x => x.ContracNumber)
            .NotEmpty().WithMessage("Không được để trống.")
            .GreaterThan(0).WithMessage("Số hợp đồng cần lớn hơn 0.");

        RuleFor(x => x.SignDate)
            .NotEmpty().WithMessage("Không được để trống.");

        RuleFor(x => x.EffectiveDate)
            .NotEmpty().WithMessage("Không được để trống.");

        RuleFor(x => x.ExpirationDate)
            .NotEmpty().WithMessage("Không được để trống.");

        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("Chưa chọn cho nhân viên.");
    }
}
