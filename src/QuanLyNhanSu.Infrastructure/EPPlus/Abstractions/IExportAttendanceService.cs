using QuanLyNhanSu.Contract.Service.V1.Employee;

namespace QuanLyNhanSu.Infrastructure.EPPlus.Abstractions;
public interface IExportAttendanceService
{
    public Task<byte[]> ExportAttendanceToExcel(Query.GetEmployeesMapToAttendanceQuery request, CancellationToken cancellationToken);
}
