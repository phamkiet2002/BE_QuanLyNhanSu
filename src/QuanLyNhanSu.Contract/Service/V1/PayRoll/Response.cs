namespace QuanLyNhanSu.Contract.Service.V1.PayRoll;
public static class Response
{
    public record PayRollResponse(DateTime FromDate, DateTime ToDate, decimal SalaryGross, decimal SalaryNet, string PayRollStatus, DateTime PaymentDate);
}
