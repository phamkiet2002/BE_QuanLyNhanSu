namespace QuanLyNhanSu.Contract.Service.V1.Department;

public static class Response
{
    public record DepartmentResponse(Guid Id, string Name, string Description, 
        WorkShedule.Response.WorkSheduleResponse WorkShedule, WorkPlace.Response.WorkPlaceResponse WorkPlace, DateTime CreatedDate);
}
