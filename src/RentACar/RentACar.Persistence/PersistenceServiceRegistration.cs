﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentACar.Application.Services.Repositories;
using RentACar.Persistence.Context;
using RentACar.Persistence.Repositories;

namespace RentACar.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options =>
        {
            //options.UseInMemoryDatabase("RentACarDb");
            options.UseSqlServer(configuration.GetConnectionString("RentACar"));
        });

        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IModelRepository, ModelRepository>();

        return services;
    }
}