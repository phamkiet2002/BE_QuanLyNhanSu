using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Employee.Validators;
public class CreateEmployeeValidator : AbstractValidator<Command.CreateEmployeeCommand>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống.");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email không để trống.");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("Số điện thoại không được để trống.");
        RuleFor(x => x.IdentityCard)
           .NotEmpty().WithMessage("Identity card is required")
           .Matches(@"^\d{12}$").WithMessage("Identity card must be exactly 12 digits");
        RuleFor(x => x.Gender).IsInEnum().WithMessage("Giới tính không được trống.");
        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Ngày sinh không được trống.");

        RuleFor(x => x.Address).NotEmpty().WithMessage("Địa chỉ không được để trống.");
        RuleFor(x => x.JoinDate).NotEmpty().WithMessage("Ngày tham gia không được để trống.");
        RuleFor(x => x.BankName).NotEmpty().WithMessage("Tên ngân hàng không được để trống.");
        RuleFor(x => x.BankAccountNumber).NotEmpty().WithMessage("Số tài khoản ngân hàng không được để trống.");
        RuleFor(x => x.WorkPlaceId).NotNull().WithMessage("Nơi làm việc không được để trống.");
        RuleFor(x => x.DepartmentId).NotNull().WithMessage("Phòng ban không được để trống.");
        RuleFor(x => x.LevelId).NotNull().WithMessage("Cấp bậc không được để trống.");
        RuleFor(x => x.PositionId).NotNull().WithMessage("Chức vụ không được để trống.");
        RuleFor(x => x.Salarys).GreaterThan(0).WithMessage("Mức lương phải lớn hơn 0.");
    }
}
