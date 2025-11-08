using Microsoft.Extensions.DependencyInjection;
using MoonOps.Application.Services;
using MoonOps.Application.Repositories;
using MoonOps.Domain.Interfaces;
using MoonOps.Domain.Entities;

namespace MoonOps.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
services.AddScoped<IRepository<Integrator>, InMemoryRepository<Integrator>>()
    .AddScoped<IntegratorService>()
      .AddTransient<Handlers.AuthTokenKeeperHandler>();
}