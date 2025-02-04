using OfficeOpenXml;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Infrastructure.EPPlus.Abstractions;
using static QuanLyNhanSu.Contract.Service.V1.Employee.Response;

namespace QuanLyNhanSu.Infrastructure.EPPlus.Implementations;
public sealed class ExportEmployeeService : IExportEmployeeService
{
    private readonly IQueryHandler<Query.GetEmployeesQuery, PagedResult<Response.EmployeeResponse>> _queryEmployeeHandler;

    public ExportEmployeeService(IQueryHandler<Query.GetEmployeesQuery, PagedResult<Response.EmployeeResponse>> queryEmployeeHandler)
    {
        _queryEmployeeHandler = queryEmployeeHandler;
    }

    public async Task<byte[]> ExportEmployeeToExcel(Query.GetEmployeesQuery request, CancellationToken cancellationToken)
    {

        var modifiedRequest = request with
        {
            PageSize = int.MaxValue,
            PageIndex = 1
        };
        var queryResult = await _queryEmployeeHandler.Handle(modifiedRequest, cancellationToken);
        if (queryResult.IsSuccess && queryResult.Value != null && queryResult.Value.Items.Count() > 0)
        {
            var data = queryResult.Value.Items.ToList();
            return await CreateExcelFile(data);
        }
        else if (queryResult.Value != null && queryResult.Value.Items != null && queryResult.Value.Items.Count() == 0)
        {
            throw new Exception("No data Employee found to export.");
        }
        else if (!queryResult.IsSuccess || queryResult.Value == null || queryResult.Value.Items == null || queryResult.Value.Items.Count() == 0)
        {
            throw new Exception("No data found to export.");
        }
        else
        {
            throw new Exception("Error when export data to excel.");
        }
    }

    private async Task<byte[]> CreateExcelFile(IEnumerable<EmployeeResponse> data)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Nếu chưa cấu hình trước

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Employee Report");
        // Header
        worksheet.Cells[1, 1].Value = "STT";
        worksheet.Cells[1, 2].Value = "Mã nhân viên";
        worksheet.Cells[1, 3].Value = "Tên nhân viên";
        worksheet.Cells[1, 4].Value = "Ngày sinh";
        worksheet.Cells[1, 5].Value = "Giới tính";
        worksheet.Cells[1, 6].Value = "Email";
        worksheet.Cells[1, 7].Value = "Số điện thoại";
        worksheet.Cells[1, 8].Value = "Ngày gia nhập";
        worksheet.Cells[1, 9].Value = "Điểm làm việc";
        worksheet.Cells[1, 10].Value = "Phòng ban";
        worksheet.Cells[1, 11].Value = "Chức vụ";
        worksheet.Cells[1, 12].Value = "Level";
        worksheet.Cells[1, 13].Value = "Lương";
        worksheet.Cells[1, 14].Value = "Ngân hàng";
        worksheet.Cells[1, 15].Value = "Số tài khoản ngân hàng";
        worksheet.Cells[1, 16].Value = "Địa chỉ";

        var range = worksheet.Cells[1, 1, 1, 16];
        range.Style.Font.Bold = true;
        range.AutoFilter = true;
        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

        // Lặp qua dữ liệu lấy từ queryResult để điền vào Excel
        int row = 2;
        foreach (var item in data)
        {
            worksheet.Cells[row, 1].Value = row - 1;
            worksheet.Cells[row, 2].Value = item.MaNV;
            worksheet.Cells[row, 3].Value = item.Name;
            worksheet.Cells[row, 4].Value = item.DateOfBirth.ToString("dd-MM-yyyy") ?? "N/A";
            worksheet.Cells[row, 5].Value = item.Gender ?? "N/A";
            worksheet.Cells[row, 6].Value = item.Email ?? "N/A";
            worksheet.Cells[row, 7].Value = item.Phone ?? "N/A";
            worksheet.Cells[row, 8].Value = item.JoinDate.ToString("dd-MM-yyyy") ?? "N/A";
            worksheet.Cells[row, 9].Value = item.WorkPlace?.Name ?? "N/A";
            var employeeDepartment = item.EmployeeDepartments.FirstOrDefault(x => x.Status == "Active");
            worksheet.Cells[row, 10].Value = employeeDepartment?.Department.Name ?? "N/A";
            var employeePosition = item.EmployeePositions.FirstOrDefault(x => x.Status == "Active");
            worksheet.Cells[row, 11].Value = employeePosition?.Position.Name ?? "N/A";
            var employeeLevel = item.EmployeeLevels.FirstOrDefault(x => x.Status == "Active");
            worksheet.Cells[row, 12].Value = employeeLevel?.Level.Name ?? "N/A";
            var salary = item.Salarys.FirstOrDefault(x => x.Status.ToString() == "Active");
            worksheet.Cells[row, 13].Value = salary?.Salarys ?? 0m;
            worksheet.Cells[row, 14].Value = item.BankName ?? "N/A";
            worksheet.Cells[row, 15].Value = item.BankAccountNumber ?? "N/A";
            worksheet.Cells[row, 16].Value = item.Address ?? "N/A";

            row++;
        }

        var tableRange = worksheet.Cells[1, 1, row - 1, 16];
        tableRange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        tableRange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        tableRange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        tableRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

        // Tự động điều chỉnh kích thước cột
        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        // Xuất file dưới dạng byte array
        return await Task.FromResult(package.GetAsByteArray());
    }
}
