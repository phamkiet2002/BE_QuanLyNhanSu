using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions.Repositories.Attendance;
using QuanLyNhanSu.Domain.Abstractions.Repositories.Employee;
using QuanLyNhanSu.Domain.Abstractions.Repositories.WifiConfig;
using QuanLyNhanSu.Domain.Entities.Identity;
using QuanLyNhanSu.Persistence.Repositories;
using QuanLyNhanSu.Persistence.Repositories.Attendance;
using QuanLyNhanSu.Persistence.Repositories.Employee;
using QuanLyNhanSu.Persistence.Repositories.WifiConfig;

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
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
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
        services.AddSignalR();
        services.AddAuthorization();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ADMIN", x =>
            {
                x.RequireRole("ADMIN");
            });

            options.AddPolicy("HR_MANAGER", x =>
            {
                x.RequireRole("HR_MANAGER");
            });

            options.AddPolicy("DEPARTMENT_MANAGER", x =>
            {
                x.RequireRole("DEPARTMENT_MANAGER");
            });

            options.AddPolicy("EMPLOYEE", x =>
            {
                x.RequireRole("EMPLOYEE");
            });
        });
    }

    public static void AddRepositoryBaseConfiguration(this IServiceCollection services)
       => services
           .AddTransient(typeof(Domain.Abstractions.IUnitOfWork), typeof(EFUnitOfWork))
           .AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>))
           .AddTransient(typeof(IAttendanceRepository), typeof(AttendanceRepository))
           .AddTransient(typeof(IEmployeeRepository), typeof(EmployeeRepository))
           .AddTransient(typeof(IWifiCongfigRepository), typeof(WifiCongfigRepository));
}
