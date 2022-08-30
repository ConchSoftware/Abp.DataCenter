﻿using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Abp.DataCenter.EntityFrameworkCore;

public class DataCenterHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<DataCenterHttpApiHostMigrationsDbContext>
{
    public DataCenterHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<DataCenterHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("DataCenter"));

        return new DataCenterHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
