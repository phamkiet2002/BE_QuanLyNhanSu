namespace QuanLyNhanSu.Contract.Service.V1.PayRoll;
public static class Response
{
    public record PayRollResponse
    (
        Guid Id, DateTime FromDate, DateTime ToDate, double ActualWorkingDay, decimal TotalAllowance, decimal TotalPenalties,
        decimal SalaryGross, decimal SalaryNet, string PayRollStatus, DateTime PaymentDate, DateTime CreatedDate);
}
