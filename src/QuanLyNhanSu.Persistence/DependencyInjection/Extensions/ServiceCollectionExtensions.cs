using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions.Repositories.Attendance;
using QuanLyNhanSu.Domain.Abstractions.Repositories.Employee;
using QuanLyNhanSu.Domain.Entities.Identity;
using QuanLyNhanSu.Persistence.Repositories;
using QuanLyNhanSu.Persistence.Repositories.Attendance;
using QuanLyNhanSu.Persistence.Repositories.Employee;

namespace QuanLyNhanSu.Persistence.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSqlConfiguration(this IServiceCollection services)
    {
        services.AddDbContextPool<DbContext, ApplicationDbContext>((provider, builder) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();

            builder
           .EnableDetailedErrors(true)
           .EnableSensitiveDataLogging(true)
           .UseLazyLoadingProxies(true)
           .UseSqlServer(
               connectionString: configuration.GetConnectionString("ConnectionStrings"),
                   sqlServerOptionsAction: optionsBuilder
                       => optionsBuilder
                       .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));
        });
    }

    public static void AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services
           .AddIdentityCore<AppUser>()
           .AddRoles<AppRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Lockout.AllowedForNewUsers = true; // Default true
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2); // Default 5
            options.Lockout.MaxFailedAccessAttempts = 3; // Default 5

            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;
        });

        services.AddHttpContextAccessor();
        services.AddScoped<SignInManager<AppUser>>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"]))
            };
        });

        services.AddAuthorization();
    }

    public static void AddRepositoryBaseConfiguration(this IServiceCollection services)
       => services
           .AddTransient(typeof(Domain.Abstractions.IUnitOfWork), typeof(EFUnitOfWork))
           .AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>))
           .AddTransient(typeof(IAttendanceRepository), typeof(AttendanceRepository))
            .AddTransient(typeof(IEmployeeRepository), typeof(EmployeeRepository));
}
