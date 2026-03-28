using API.Filters;
using Application;
using FluentMigrator.Runner.Initialization;
using Infrastructure;
using Infrastructure.Extensions;
using Infrastructure.Migrations;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilters)));

builder.Configuration.ConnectionString();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

app.Run();


void MigrateDatabase() 
{
    DataBaseMigration.Migrate(builder.Configuration.ConnectionString(),
    app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
}
