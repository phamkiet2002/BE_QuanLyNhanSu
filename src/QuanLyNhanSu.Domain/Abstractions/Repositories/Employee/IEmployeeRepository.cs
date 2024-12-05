namespace QuanLyNhanSu.Domain.Abstractions.Repositories.Employee;
public interface IEmployeeRepository : IRepositoryBase<Domain.Entities.Employee, Guid>
{
    public double CalculateStandardWorkingDay(Domain.Entities.Employee employee);
    public decimal CalculateTotalAllowance(Domain.Entities.Employee employee);
    public decimal CalculateTotalPenalties(Domain.Entities.Employee employee);
}
