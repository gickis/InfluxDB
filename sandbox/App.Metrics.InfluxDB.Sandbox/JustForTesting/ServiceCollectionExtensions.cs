﻿// <copyright file="ServiceCollectionExtensions.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using System;
using App.Metrics.AspNetCore.Middleware.Options;
using App.Metrics.InfluxDB.Sandbox.JustForTesting;

// ReSharper disable CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
    // ReSharper restore CheckNamespace
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTestStuff(this IServiceCollection services)
        {
            services.AddTransient<Func<double, RequestDurationForApdexTesting>>(
                provider => { return apdexTSeconds => new RequestDurationForApdexTesting(apdexTSeconds); });

            services.AddTransient<RandomStatusCodeForTesting>();

            services.AddTransient(
                provider =>
                {
                    var options = provider.GetRequiredService<AppMetricsMiddlewareOptions>();
                    return new RequestDurationForApdexTesting(options.ApdexTSeconds);
                });

            return services;
        }
    }
}