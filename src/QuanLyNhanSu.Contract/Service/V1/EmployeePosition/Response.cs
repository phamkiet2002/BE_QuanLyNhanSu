using Microsoft.VisualBasic;

namespace QuanLyNhanSu.Contract.Service.V1.EmployeePosition;
public static class Response
{
    public record EmployeePositionResponse(Position.Response.PositionResponse Position, string Status, DateTime CreatedDate);
}
