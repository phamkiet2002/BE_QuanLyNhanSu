using QuanLyNhanSu.Contract.Service.V1.Employee;

namespace QuanLyNhanSu.Infrastructure.EPPlus.Abstractions;
public interface IExportEmployeeService
{
    public Task<byte[]> ExportEmployeeToExcel(Query.GetEmployeesQuery request, CancellationToken cancellationToken);
}
