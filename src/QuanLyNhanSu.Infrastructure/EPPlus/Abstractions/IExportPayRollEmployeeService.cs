using QuanLyNhanSu.Contract.Service.V1.Employee;

namespace QuanLyNhanSu.Infrastructure.EPPlus.Abstractions;
public interface IExportPayRollEmployeeService
{
    public Task<byte[]> ExportPayRollEmployeeToExcel(Query.GetEmplpyeePayRollQuery request, CancellationToken cancellationToken);
}
