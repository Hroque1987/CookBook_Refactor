using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace Infrastructure.Extensions;

public static class ConfigurationExtension 
{
    public static string ConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("Connection")!;
    }
}
