using Api.Controllers.V1.UserManagement;
using Application.ServiceConfiguration;
using Carter;
using Domain.Entities.User;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.Identity.Identity.Dtos;
using Infrastructure.Identity.Jwt;
using Infrastructure.Identity.ServiceConfiguration;
using Infrastructure.Persistence.ServiceConfiguration;
using Serilog;
using SharedKernel.Extensions;
using System.Diagnostics;
using WebFramework.Filters;
using WebFramework.Middlewares;
using WebFramework.ServiceConfiguration;
using WebFramework.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(LoggingConfiguration.ConfigureLogger);

var configuration = builder.Configuration;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;

builder.Services.Configure<IdentitySettings>(configuration.GetSection(nameof(IdentitySettings)));

var identitySettings = configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(OkResultAttribute));
    options.Filters.Add(typeof(NotFoundResultAttribute));
    options.Filters.Add(typeof(ContentResultFilterAttribute));
    options.Filters.Add(typeof(ModelStateValidationAttribute));
    options.Filters.Add(typeof(BadRequestResultFilterAttribute));

}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    options.SuppressMapClientErrors = true;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddCarter(configurator: configurator => { configurator.WithEmptyValidators(); });

builder.Services.AddApplicationServices()
    .RegisterIdentityServices(identitySettings)
    .AddPersistenceServices(configuration)
    .AddWebFrameworkServices();

builder.Services.RegisterValidatorsAsServices();
builder.Services.AddExceptionHandler<ExceptionHandler>();


builder.Services.AddAutoMapper(expression =>
{
    expression.AddMaps(typeof(User), typeof(JwtService), typeof(UserController));
});

var app = builder.Build();


await app.EnsureCreatedDatabaseAsync();
await app.ApplyMigrationsAsync();
await app.SeedDefaultUsersAsync();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler(_ => { });
app.UseSwaggerAndUI();

app.MapCarter();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();



