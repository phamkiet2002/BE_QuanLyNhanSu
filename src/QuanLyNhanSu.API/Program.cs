using QuanLyNhanSu.API.DependencyInjection.Extensions;
using QuanLyNhanSu.API.Middleware;
using QuanLyNhanSu.Application.DependencyInjection.Extensions;
using QuanLyNhanSu.Infrastructure.DependencyInjection.Extensions;
using QuanLyNhanSu.Persistence.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwagger();

//configuration dbcontext
builder.Services.AddSqlConfiguration();

// add mediatr
builder.Services.AddConfigureMediatR();

//add automapper
builder.Services.AddConfigurationAutoMapper();

//add repository
builder.Services.AddRepositoryBaseConfiguration();

// Middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Add Identity
builder.Services.AddIdentityConfiguration(builder.Configuration);

// Add Hangfire
builder.Services.AddHangfireConfiguration(builder.Configuration);

// Add Service Hangfire
builder.Services.AddServiceHangfireConfiguration();

// Add EPPlus
builder.Services.ConfigureEPPlus();

// Add SignalR
builder.Services.AddServiceNotificationConfiguration();

// CORS
var CORS = "MyPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: CORS,
        builder =>
        {
            builder
            .WithOrigins
            (
                "http://localhost:3000",
                "http://localhost:3001",
                "https://8958-171-227-248-23.ngrok-free.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
            //.WithMethods("GET", "POST", "PUT", "DELETE");
        });
});

var app = builder.Build();

// sử dụng middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ConfigureSwagger();
}

//CORS
app.UseCors(CORS);

// use Hangfire
app.ConfigureHangfire();

// use SignalR
app.ConfigureNotification();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
