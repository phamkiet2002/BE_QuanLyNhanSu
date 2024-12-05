using QuanLyNhanSu.Contract.Abstractions.Message;
using static QuanLyNhanSu.Domain.Enumerations.AllowanceAndPenaltyEnums;

namespace QuanLyNhanSu.Contract.Service.V1.AllowanceAndPenalties;
public static class Command
{
    public record CreateAllowanceCommand(TypeOfAllowance? TypeOfAllowance,
        decimal Money, DateTime EffectiveDate, string Note, bool? IsAllWorkPlace, Guid? WorkPlaceId = null) : ICommand;
    public record CreatePenaltiesCommand(TypeOfPenalty? TypeOfPenalty,
        decimal Money, DateTime EffectiveDate, string Note, bool? IsAllWorkPlace, Guid? WorkPlaceId = null) : ICommand;

    public record UpdateAllowanceCommand(Guid Id, TypeOfAllowance? TypeOfAllowance,
       decimal Money, DateTime EffectiveDate, string Note, bool? IsAllWorkPlace, Guid? WorkPlaceId = null) : ICommand;

    public record UpdatePenaltiesCommand(Guid Id, TypeOfPenalty? TypeOfPenalty,
        decimal Money, DateTime EffectiveDate, string Note, bool? IsAllWorkPlace, Guid? WorkPlaceId = null) : ICommand;

    public record DeleteAllowanceAndPenaltiesCommand(Guid Id) : ICommand;
}
