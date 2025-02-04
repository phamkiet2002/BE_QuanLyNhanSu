using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.LeaveDate.Validators;
public class CreateLeaveDateVatidator : AbstractValidator<Command.CreateLeaveDateCommand>
{
    public CreateLeaveDateVatidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống.")
            .MaximumLength(100).WithMessage("Tên không vượt quá 100 ký tự.");

        RuleFor(x => x.TotalAnnualLeaveDate).NotEmpty().WithMessage("Số ngày nghỉ phép không được để trống.")
            .GreaterThan(0).WithMessage("Số ngày nghỉ không được nhỏ hơn 0.");

        RuleFor(x => x.MaximumDaysOffPerMonth).NotEmpty().WithMessage("Số ngày nghỉ tối đa trong tháng không được để trống.")
            .GreaterThan(0).WithMessage("Số ngày nghỉ tối đa trong tháng không được nhỏ hơn 0.");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả không được để trống.")
            .MaximumLength(100).WithMessage("Mô tả không được vượt quá 100 ký tự.");

        RuleFor(x => x.IsHoliday).NotNull().WithMessage("Loại ngày nghỉ Không được để trống.");

        RuleFor(x => x.StartDate).NotEmpty().When(x => x.IsHoliday == true).WithMessage("Ngày bắt đầu không được để trống.");
        RuleFor(x => x.EndDate).NotEmpty().When(x => x.IsHoliday == true).WithMessage("Ngày bắt đầu không được để trống.");
    }
}
