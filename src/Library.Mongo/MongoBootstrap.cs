﻿using Core.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Mongo
{
    public static class MongoBootstrap
    {
        public static void ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoConfiguration>(cfg =>
            {
                cfg.DatabaseName = configuration.GetSection("MongoOptions:Name").Value;
                cfg.ConnectionString = configuration.GetSection("MongoOptions:ConnectionString").Value;
            });

            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddTransient(typeof(IProjectionWriter<>), typeof(MongoProjectionWriter<>));
        }
    }
}
