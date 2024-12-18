using System.Text;
using Lumix.API.Extensions;
using Lumix.API.Infrastructure;
using Lumix.Application;
using Lumix.Core.Interfaces.Services;
using Lumix.Infrastructure;
using Lumix.Infrastructure.Authenfication;
using Lumix.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Lumix.Persistence;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "allowSpecificOrigins";
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));
var configuration = builder.Configuration;
var services = builder.Services;

// Add services
services.AddApiAuthentication(configuration);
services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));
services.AddControllers();

services.AddApplication();
services.AddInfrastructure();
services.AddPersistence(configuration);
builder.Services.AddProblemDetails();
services.AddExceptionHandler<GlobalExceptionHandler>();

services.AddAutoMapper(typeof(DataBaseMappings));
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<IUserService, UserService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();