using System;
using System.Reflection;
using Domain.Repositories;
using Domain.Repositories.User;
using FluentMigrator.Runner;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Respositories;
using Infrastructure.Extensions;
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
        AddFluetMigrator(services, configuration);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        

        services.AddDbContext<MyRecipyBookDbContext>(options =>
        {
            options.UseSqlServer(configuration.ConnectionString());
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRepository> ();
        services.AddScoped<IUserWriteOnlyRespository, UserRepository> ();
        services.AddScoped<IUnitOfWork, UnitOfWork> ();

    }

    public static void AddFluetMigrator(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
           options
           .AddSqlServer()
           .WithGlobalConnectionString(configuration.ConnectionString())
           .ScanIn(Assembly.Load("Infrastructure")).For.All();
        });
    }

}
