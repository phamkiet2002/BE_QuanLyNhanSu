using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuanLyNhanSu.Application.UserCases.V1.Commands.Contract;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Service.V1.Contract;
using QuanLyNhanSu.Infrastructure.Hangfires.Abstractions;
using QuanLyNhanSu.Infrastructure.Hangfires.Implementations;

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
    }


    public static void AddServiceHangfireConfiguration(this IServiceCollection services)
    {
        services.AddTransient<ICommandHandler<Command.CheckContractNearExpirationCommand>, CheckContractExpirationCommandHandler>();
        services.AddTransient<IContractHangfire, ContractHangfire>();
        services.AddTransient<ICommandHandler<Contract.Service.V1.PayRoll.Command.CreatePayRollCommand>, Application.UserCases.V1.Commands.PayRoll.CreatePayRollCommandHandler>();
        services.AddTransient<IPayRollHangfire, PayRollHangfire>();
    }

    //public static void AddServiceHangfireConfiguration(this IServiceCollection services)
    //   => services
    //       .AddTransient(typeof(IContractHangfire), typeof(ContractHangfire));
}
