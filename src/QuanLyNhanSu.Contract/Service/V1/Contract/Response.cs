namespace QuanLyNhanSu.Contract.Service.V1.Contract;
public static class Response
{
    public record ContractResponse(Guid Id, int ContracNumber, DateTime SignDate, DateTime EffectiveDate, DateTime ExpirationDate, Employee.Response.EmployeeMapToContractResponse Employee , DateTime CreatedDate);
}
