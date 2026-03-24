using System;
using Domain.Repositories;
using Domain.Repositories.User;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Respositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjectionExtension
{

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        

        services.AddDbContext<MyRecipyBookDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Connection"));
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRepository> ();
        services.AddScoped<IUserWriteOnlyRespository, UserRepository> ();
        services.AddScoped<IUnitOfWork, UnitOfWork> ();

    }

}
