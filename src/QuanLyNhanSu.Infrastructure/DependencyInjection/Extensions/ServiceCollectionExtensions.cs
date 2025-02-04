using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuanLyNhanSu.Application.Abstractions;
using QuanLyNhanSu.Application.UserCases.V1.Commands.Contract;
using QuanLyNhanSu.Application.UserCases.V1.Queries.Attendace;
using QuanLyNhanSu.Application.UserCases.V1.Queries.Employee;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Contract;
using QuanLyNhanSu.Infrastructure.EPPlus.Abstractions;
using QuanLyNhanSu.Infrastructure.EPPlus.Implementations;
using QuanLyNhanSu.Infrastructure.Hangfires.Abstractions;
using QuanLyNhanSu.Infrastructure.Hangfires.Implementations;
using QuanLyNhanSu.Infrastructure.SignalR;
using QuanLyNhanSu.Infrastructure.SignalR.Implementations;

namespace QuanLyNhanSu.Infrastructure.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHangfireConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"), new Hangfire.SqlServer.SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                });
        });

        services.AddHangfireServer();
        return services;
    }

    public static void ConfigureHangfire(this WebApplication app)
    {
        app.UseHangfireDashboard();

        RecurringJob.AddOrUpdate<IContractHangfire>(
                    "check_contract_expiration",
                    contractHangfire => contractHangfire.CheckAndUpdateContractStatusAsync(),
                    Cron.Daily); //"0 8 * * *"     daily là chạy hằng ngày đúng 12 giờ tối

        RecurringJob.AddOrUpdate<IPayRollHangfire>(
                    "create_monthly_payroll",
                    payrollHangfire => payrollHangfire.CreatePayRollAsync(),
                    Cron.Monthly(1, 0, 0));

        RecurringJob.AddOrUpdate<IAttendanceHangfire>(
                    "check_is_absent",
                    AttendanceHanfire => AttendanceHanfire.CheckAbsentAttendanceAsync(),
                    "0 20 * * *");
    }

    public static void AddServiceHangfireConfiguration(this IServiceCollection services)
    {
        services.AddTransient<ICommandHandler<Command.CheckContractNearExpirationCommand>, CheckContractExpirationCommandHandler>();
        services.AddTransient<IContractHangfire, ContractHangfire>();
        services.AddTransient<ICommandHandler<Contract.Service.V1.PayRoll.Command.CreatePayRollCommand>, Application.UserCases.V1.Commands.PayRoll.CreatePayRollCommandHandler>();
        services.AddTransient<IPayRollHangfire, PayRollHangfire>();
        services.AddTransient<IAttendanceHangfire, AttendanceHanfire>();
        services.AddTransient<ICommandHandler<Contract.Service.V1.Attendance.Command.CheckIsAbsentAttendanceCommand>, Application.UserCases.V1.Commands.Attendance.CheckIsAbsentAttendanceCommandHandler>();
    }

    public static void ConfigureEPPlus(this IServiceCollection services)
    {
        services.AddTransient
            <IQueryHandler<QuanLyNhanSu.Contract.Service.V1.Employee.Query.GetEmployeesMapToAttendanceQuery, PagedResult<QuanLyNhanSu.Contract.Service.V1.Employee.Response.EmployeeMapToAttendanceResponse>>, GetEmployeeMapToAttendanceQueryHandler>();
        services.AddTransient<IExportAttendanceService, ExportAttendanceService>();

        services.AddTransient
            <IQueryHandler<QuanLyNhanSu.Contract.Service.V1.Employee.Query.GetEmplpyeePayRollQuery, PagedResult<QuanLyNhanSu.Contract.Service.V1.Employee.Response.EmployeePayRollResponse>>, GetEmployeePayRollQueryHandler>();
        services.AddTransient<IExportPayRollEmployeeService, ExportPayRollEmployeeService>();

        services.AddTransient
            <IQueryHandler<QuanLyNhanSu.Contract.Service.V1.Employee.Query.GetEmployeesQuery, PagedResult<QuanLyNhanSu.Contract.Service.V1.Employee.Response.EmployeeResponse>>, GetEmployeeQueryHandler>();
        services.AddTransient<IExportEmployeeService, ExportEmployeeService>();
    }

    public static void ConfigureNotification(this WebApplication app)
    {
        app.MapHub<NotificationHub>("/notificationHub").RequireCors("MyPolicy");
    }

    public static void AddServiceNotificationConfiguration(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddTransient<INotificationService, NotificationService>();
    }
}
