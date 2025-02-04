using OfficeOpenXml;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Infrastructure.EPPlus.Abstractions;
using static QuanLyNhanSu.Contract.Service.V1.Employee.Response;

namespace QuanLyNhanSu.Infrastructure.EPPlus.Implementations;
public sealed class ExportAttendanceService : IExportAttendanceService
{
    private readonly IQueryHandler<Query.GetEmployeesMapToAttendanceQuery, PagedResult<Response.EmployeeMapToAttendanceResponse>> _queryAttendanceHandler;

    public ExportAttendanceService(IQueryHandler<Query.GetEmployeesMapToAttendanceQuery, PagedResult<Response.EmployeeMapToAttendanceResponse>> queryHandler)
    {
        _queryAttendanceHandler = queryHandler;
    }

    public async Task<byte[]> ExportAttendanceToExcel(Query.GetEmployeesMapToAttendanceQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await _queryAttendanceHandler.Handle(request, cancellationToken);
        if (queryResult.IsSuccess && queryResult.Value != null && queryResult.Value.Items.Any(x => x.Attendances != null && x.Attendances.Count() > 0))
        {
            var data = queryResult.Value.Items;
            return await CreateExcelFile(data);
        }
        else if (queryResult.Value != null && queryResult.Value.Items != null && queryResult.Value.Items.Any(x => x.Attendances == null || x.Attendances.Count() == 0))
        {
            throw new Exception("No data Attendace found to export.");
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

    private async Task<byte[]> CreateExcelFile(IEnumerable<EmployeeMapToAttendanceResponse> data)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Nếu chưa cấu hình trước

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Attendance Report");
        int maxAttendances = data.Max(item => item.Attendances.Count());
        // Header
        worksheet.Cells[1, 1].Value = "STT";
        worksheet.Cells[1, 2].Value = "Mã nhân viên";
        worksheet.Cells[1, 3].Value = "Tên nhân viên";
        worksheet.Cells[1, 4].Value = "Điểm làm việc";
        for (int i = 0; i < maxAttendances; i++)
        {
            worksheet.Cells[1, 5 + i].Value = $"{i} ({data.First().Attendances.ElementAtOrDefault(i)?.CreatedDate.ToString("dd-MM-yyyy") ?? "N/A"})";
        }
        worksheet.Cells[1, 6 + maxAttendances - 1].Value = "Tổng thời gian làm việc";
        worksheet.Cells[1, 7 + maxAttendances - 1].Value = "Số lần đi trễ";
        worksheet.Cells[1, 8 + maxAttendances - 1].Value = "Tổng thời gian đi trễ";
        worksheet.Cells[1, 9 + maxAttendances - 1].Value = "Số lần về sớm";
        worksheet.Cells[1, 10 + maxAttendances - 1].Value = "Tổng thời gian về sớm";
        worksheet.Cells[1, 11 + maxAttendances - 1].Value = "Số lần nghỉ không phép";


        var range = worksheet.Cells[1, 1, 1, 11 + maxAttendances - 1];
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
            worksheet.Cells[row, 4].Value = item.WorkPlace?.Name ?? "N/A";
            int attendanceIndex = 0;
            foreach (var attendance in item.Attendances)
            {
                worksheet.Cells[row, 5 + attendanceIndex].Value =
                    (attendance.CheckIn.ToString("HH:mm") ?? "N/A") + " - " +
                    (attendance.CheckOut.ToString("HH:mm") ?? "N/A");

                attendanceIndex++;
            }
            worksheet.Cells[row, 6 + maxAttendances - 1].Value = item.TotalTimeAttendance?.ToString(@"hh\:mm") ?? "N/A";
            worksheet.Cells[row, 7 + maxAttendances - 1].Value = item.TotalDayLate ?? 0;
            worksheet.Cells[row, 8 + maxAttendances - 1].Value = item.TotalTimeDayLate?.ToString(@"hh\:mm") ?? "N/A";
            worksheet.Cells[row, 9 + maxAttendances - 1].Value = item.TotalDayEarlyLeave ?? 0;
            worksheet.Cells[row, 10 + maxAttendances - 1].Value = item.TotalTimeDayEarlyLeave?.ToString(@"hh\:mm") ?? "N/A";
            worksheet.Cells[row, 11 + maxAttendances - 1].Value = item.TotalDayAbsent ?? 0;
            row++;
        }

        var tableRange = worksheet.Cells[1, 1, row - 1, 11 + maxAttendances - 1];
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
