using QuanLyNhanSu.API.DependencyInjection.Extensions;
using QuanLyNhanSu.API.Middleware;
using QuanLyNhanSu.Application.DependencyInjection.Extensions;
using QuanLyNhanSu.Application.UserCases.V1.Commands.Contract;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Infrastructure.DependencyInjection.Extensions;
using QuanLyNhanSu.Persistence.DependencyInjection.Extensions;
using static QuanLyNhanSu.Contract.Service.V1.Contract.Command;

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


///CORS
var CORS = "MyPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: CORS,
        builder =>
        {
            builder.WithOrigins("https://192.168.4.33:7127", "https://35b9-116-193-67-194.ngrok-free.app", "http://localhost:3000", "http://localhost:3001")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
