using Lumix.API.Extensions;
using Lumix.API.Infrastructure;
using Lumix.Application;
using Lumix.Application.Auth;
using Lumix.Application.Services;
using Lumix.Core.Interfaces.Services;
using Lumix.Infrastructure;
using Lumix.Infrastructure.Authenfication;
using Lumix.Infrastructure.Authenfication.Jwt;
using Lumix.Infrastructure.Config;
using Lumix.Persistence;
using Lumix.Persistence;
using Lumix.Persistence.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "allowSpecificOrigins";
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));
var configuration = builder.Configuration;
var services = builder.Services;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .WithOrigins("https://localhost:7252") 
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithExposedHeaders("*");
        });
});

// Add services
services.AddHttpContextAccessor();
services.AddApiAuthentication(configuration);
services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));
services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

services.Configure<AwsOptions>(
    builder.Configuration.GetSection("AWS"));

services.AddApplication();
services.AddInfrastructure();
services.AddPersistence(configuration);
builder.Services.AddProblemDetails();
services.AddExceptionHandler<GlobalExceptionHandler>();
services.AddAutoMapper(typeof(DataBaseMappings));
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<IFollowService, FollowService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IAuthService, AuthService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
// Middleware
app.UseCors("AllowAll");
app.UseUserId();
app.UseAuthorization();
app.MapControllers();
app.Run();