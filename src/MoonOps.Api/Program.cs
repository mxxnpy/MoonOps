using MoonOps.Api.Configuration;
using MoonOps.Api.Controllers;
using MoonOps.Application.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => 
    options.Conventions.Add(new ControllerRouteConvention()));

builder.Services.AddApplicationServices()
    .AddHttpContextAccessor();

OpenApiConfiguration.ConfigureServices(builder.Services);

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

OpenApiConfiguration.Configure(app);
ScalarConfiguration.Configure(app);

app.Run();

public partial class Program { }

