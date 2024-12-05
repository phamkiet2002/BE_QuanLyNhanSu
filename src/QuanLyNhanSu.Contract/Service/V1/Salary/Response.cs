namespace QuanLyNhanSu.Contract.Service.V1.Salary;

public static class Response
{
    public record SalaryResponse(Guid Id, decimal Salarys, string Status);
}
