using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.WorkShedule.Validators;
public class UpdateWorkSheduleValidator : AbstractValidator<Command.UpdateWorkSheduleCommand>
{
    public UpdateWorkSheduleValidator()
    {
        RuleFor(x => x.Name)
             .NotEmpty()
             .WithMessage("Tên không được để trống.")
             .MaximumLength(100)
             .WithMessage("Tên không vượt 100 ký tự.");

        RuleFor(x => x.StartTime)
          .NotEmpty()
          .WithMessage("Thời gian bắt đầu không được để trống.");
        //.Must(KiemTraTimeSpan).WithMessage("Thời gian không hợp lệ.");

        RuleFor(x => x.EndTime)
            .NotEmpty()
            .WithMessage("Thời gian kết thúc không được để trống.");
        //.Must(KiemTraTimeSpan).WithMessage("Thời gian không hợp lệ.");

        RuleFor(x => x.BreakStartTime)
            .NotEmpty()
            .WithMessage("Thời gian bắt đầu nghỉ không được để trống.");
        //.Must(KiemTraTimeSpan).WithMessage("Thời gian không hợp lệ.");

        RuleFor(x => x.BreakEndTime)
           .NotEmpty()
           .WithMessage("Thời gian kết thúc nghỉ không được để trống.");
        //.Must(KiemTraTimeSpan).WithMessage("Thời gian không hợp lệ.");
    }
    private bool KiemTraTimeSpan(TimeSpan timeSpan)
    {
        return timeSpan.Hours >= 0 && timeSpan.Hours < 24 && timeSpan.Minutes >= 0 && timeSpan.Minutes < 60;
    }
}
