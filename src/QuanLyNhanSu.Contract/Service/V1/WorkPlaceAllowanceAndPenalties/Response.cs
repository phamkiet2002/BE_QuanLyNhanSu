using Microsoft.VisualBasic;

namespace QuanLyNhanSu.Contract.Service.V1.WorkPlaceAllowanceAndPenalties;
public static class Response
{
    public record WorkPlaceAllowanceResponse(string Status, DateTime CreatedDate, WorkPlace.Response.WorkPlaceResponse WorkPlace);
    public record WorkPlacePenaltiesResponse(string Status, DateTime CreatedDate, WorkPlace.Response.WorkPlaceResponse WorkPlace);
}
