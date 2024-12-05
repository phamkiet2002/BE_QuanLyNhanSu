using Microsoft.VisualBasic;

namespace QuanLyNhanSu.Contract.Service.V1.AllowanceAndPenalties;
public static class Response
{
    //public record AllowanceAndPenaltiesResponse(Guid Id, TypeAllowanceAndPenalties? Type, TypeOfAllowance? TypeOfAllowance,
    //    TypeOfPenalty? TypeOfPenalty, decimal Money, DateTime EffectiveDate, string Note);

    //public record AllowanceResponse(Guid Id, string Type, string TypeOfAllowance, decimal Money, DateTime EffectiveDate, string Note, bool IsAllWorkPlace,
    //    List<WorkPlaceAllowanceAndPenalties.Response.WorkPlaceAllowanceAndPenaltiesResponse> WorkPlaceAndAllowanceAndPenalties);

    public record AllowanceResponse(Guid Id, string Type, string TypeOfAllowance, decimal Money, DateTime EffectiveDate, string Note, string IsAllWorkPlace,
        List<WorkPlaceAllowanceAndPenalties.Response.WorkPlaceAllowanceResponse> WorkPlaceAndAllowanceAndPenalties);
    public record PenaltiesResponse(Guid Id, string Type, string TypeOfPenalty, decimal Money, DateTime EffectiveDate, string Note, string IsAllWorkPlace,
        List<WorkPlaceAllowanceAndPenalties.Response.WorkPlacePenaltiesResponse> WorkPlaceAndAllowanceAndPenalties);
}
