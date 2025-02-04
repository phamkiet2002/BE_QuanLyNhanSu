using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.LeaveDate.Validators;
public class UpdateLeaveDateVatidator : AbstractValidator<Command.UpdateLeaveDateCommand>
{
    public UpdateLeaveDateVatidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được để trống.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống.")
            .MaximumLength(100).WithMessage("Tên không vượt quá 100 ký tự.");

        RuleFor(x => x.TotalAnnualLeaveDate).NotEmpty().WithMessage("Số ngày nghỉ phép không được để trống.")
            .GreaterThan(0).WithMessage("Số ngày nghỉ không được nhỏ hơn 0.");

        RuleFor(x => x.MaximumDaysOffPerMonth).NotEmpty().WithMessage("Số ngày nghỉ tối đa trong tháng không được để trống.")
            .GreaterThan(0).WithMessage("Số ngày nghỉ tối đa trong tháng không được nhỏ hơn 0.");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả không được để trống.")
            .MaximumLength(100).WithMessage("Mô tả không được vượt quá 100 ký tự.");
    }
}
