﻿using JetBrains.Annotations;
using Lykke.Sdk;
using Lykke.Service.IndexHedgingEngine.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using AutoMapper;

namespace Lykke.Service.IndexHedgingEngine
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "IndexHedgingEngine API",
            ApiVersion = "v1"
        };

        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider<AppSettings>(options =>
            {
                options.Extend = (serviceCollection, settings) =>
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.AddProfiles(typeof(AzureRepositories.AutoMapperProfile));
                        cfg.AddProfiles(typeof(AutoMapperProfile));
                    });

                    Mapper.AssertConfigurationIsValid();
                };
                
                options.SwaggerOptions = _swaggerOptions;

                options.Logs = logs =>
                {
                    logs.AzureTableName = "IndexHedgingEngineLog";
                    logs.AzureTableConnectionStringResolver = settings =>
                        settings.IndexHedgingEngineService.Db.LogsConnectionString;
                };
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app)
        {
            app.UseLykkeConfiguration(options => { options.SwaggerOptions = _swaggerOptions; });
        }
    }
}
