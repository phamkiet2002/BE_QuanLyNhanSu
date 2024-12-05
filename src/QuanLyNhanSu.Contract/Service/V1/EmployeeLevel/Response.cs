using Microsoft.VisualBasic;

namespace QuanLyNhanSu.Contract.Service.V1.EmployeeLevel;
public static class Response
{
    public record EmployeeLevelResponse(Level.Response.LevelResponse Level, string Status, DateTime CreatedDate);
}
