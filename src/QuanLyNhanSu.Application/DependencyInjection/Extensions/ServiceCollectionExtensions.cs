using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QuanLyNhanSu.Application.Behaviors;

namespace QuanLyNhanSu.Application.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigureMediatR(this IServiceCollection services)
        => services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>))
            .AddValidatorsFromAssembly(
                Contract.AssemblyReference.Assembly,
                includeInternalTypes: true
            );

    public static IServiceCollection AddConfigurationAutoMapper(this IServiceCollection services)
        => services.AddAutoMapper(typeof(Application.AssemblyReference));
}
