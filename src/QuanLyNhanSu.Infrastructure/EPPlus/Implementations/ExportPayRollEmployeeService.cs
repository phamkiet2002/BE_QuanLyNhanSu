using OfficeOpenXml;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Infrastructure.EPPlus.Abstractions;
using static QuanLyNhanSu.Contract.Service.V1.Employee.Response;

namespace QuanLyNhanSu.Infrastructure.EPPlus.Implementations;
public sealed class ExportPayRollEmployeeService : IExportPayRollEmployeeService
{
    private readonly IQueryHandler<Query.GetEmplpyeePayRollQuery, PagedResult<Response.EmployeePayRollResponse>> _queryPayRollHandler;

    public ExportPayRollEmployeeService(IQueryHandler<Query.GetEmplpyeePayRollQuery, PagedResult<EmployeePayRollResponse>> queryPayRollHandler)
    {
        _queryPayRollHandler = queryPayRollHandler;
    }

    public async Task<byte[]> ExportPayRollEmployeeToExcel(Query.GetEmplpyeePayRollQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await _queryPayRollHandler.Handle(request, cancellationToken);
        if (queryResult.IsSuccess && queryResult.Value != null && queryResult.Value.Items.Any(x => x.PayRolls != null && x.PayRolls.Count() > 0))
        {
            var data = queryResult.Value.Items;
            return await CreateExcelFile(data);
        }
        else if (queryResult.Value != null && queryResult.Value.Items != null && queryResult.Value.Items.Any(x => x.PayRolls == null || x.PayRolls.Count() == 0))
        {
            throw new Exception("No data PayRoll found to export.");
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

    private async Task<byte[]> CreateExcelFile(IEnumerable<EmployeePayRollResponse> data)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Nếu chưa cấu hình trước

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("PayRoll Report");
        int maxPayRoll = data.Max(item => item.PayRolls.Count());
        // Header
        worksheet.Cells[1, 1].Value = "Kỳ lương";
        worksheet.Cells[1, 2].Value = "STT";
        worksheet.Cells[1, 3].Value = "Mã nhân viên";
        worksheet.Cells[1, 4].Value = "Tên nhân viên";
        worksheet.Cells[1, 5].Value = "Ngày gia nhập";
        worksheet.Cells[1, 6].Value = "Điểm làm việc";
        worksheet.Cells[1, 7].Value = "Phòng ban";
        worksheet.Cells[1, 8].Value = "Chức vụ";
        worksheet.Cells[1, 9].Value = "Level";
        worksheet.Cells[1, 10].Value = "Lương cơ bản";
        for (int i = 0; i < maxPayRoll; i++)
        {
            worksheet.Cells[1, 11 + i].Value = "Số ngày làm việc thực tế";
            worksheet.Cells[1, 12 + i].Value = "Tổng phụ cấp";
            worksheet.Cells[1, 13 + i].Value = "Tổng phạt";
            worksheet.Cells[1, 14 + i].Value = "Tổng lương";
            worksheet.Cells[1, 15 + i].Value = "Lương thực nhận";
            worksheet.Cells[1, 16 + i].Value = "Trạng thái";
        }
        worksheet.Cells[1, 17].Value = "Ngân hàng";
        worksheet.Cells[1, 18].Value = "Số tài khoản ngân hàng";

        var range = worksheet.Cells[1, 1, 1, 18 + maxPayRoll - 1];
        range.Style.Font.Bold = true;
        range.AutoFilter = true;
        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

        // Lặp qua dữ liệu lấy từ queryResult để điền vào Excel
        int row = 2;
        foreach (var item in data)
        {
            worksheet.Cells[row, 1].Value = item.PayRolls.FirstOrDefault().FromDate.ToString("MM/yyyy") + "-" + item.PayRolls.FirstOrDefault().ToDate.ToString("MM/yyyy");
            worksheet.Cells[row, 2].Value = row - 1;
            worksheet.Cells[row, 3].Value = item.MaNV;
            worksheet.Cells[row, 4].Value = item.Name;
            worksheet.Cells[row, 5].Value = item.JoinDate.ToString("dd-MM-yyyy") ?? "N/A";
            worksheet.Cells[row, 6].Value = item.WorkPlace?.Name ?? "N/A";
            worksheet.Cells[row, 7].Value = item.DepartmentName ?? "N/A";
            worksheet.Cells[row, 8].Value = item.PositionName ?? "N/A";
            worksheet.Cells[row, 9].Value = item.LevelName ?? "N/A";
            var salary = item.Salarys.FirstOrDefault(x => x.Status.ToString() == "Active");
            worksheet.Cells[row, 10].Value = salary?.Salarys ?? 0m;
            int payRollIndex = 0;
            foreach (var payRoll in item.PayRolls)
            {
                worksheet.Cells[row, 11 + payRollIndex].Value = payRoll?.ActualWorkingDay ?? 0.0;
                worksheet.Cells[row, 12 + payRollIndex].Value = payRoll?.TotalAllowance ?? 0;
                worksheet.Cells[row, 13 + payRollIndex].Value = payRoll?.TotalPenalties ?? 0;
                worksheet.Cells[row, 14 + payRollIndex].Value = payRoll?.SalaryGross ?? 0;
                worksheet.Cells[row, 15 + payRollIndex].Value = payRoll?.SalaryNet ?? 0;
                worksheet.Cells[row, 16].Value = payRoll.PayRollStatus == "PAID" ? "Đã thanh toán." : "Chưa thanh toán";
                payRollIndex++;
            }
            worksheet.Cells[row, 17].Value = item.BankName;
            worksheet.Cells[row, 18].Value = item.BankAccountNumber;

            row++;
        }

        var tableRange = worksheet.Cells[1, 1, row - 1, 18 + maxPayRoll - 1];
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
