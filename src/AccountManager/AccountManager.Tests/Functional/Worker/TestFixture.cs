﻿using System;
using System.IO;

using AccountManager.Infrastructure.Configurations;
using AccountManager.Infrastructure.Models;
using AccountManager.Worker.Configurations;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AccountManager.Tests.Functional.Worker
{
    public abstract class TestFixture
    {
        protected readonly IHostBuilder hostBuilder;

        protected TestFixture()
        {
            hostBuilder = new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutoMapperModule(typeof(InfrastructureModule).Assembly));
                    builder.RegisterModule<InfrastructureModule>();
                })
                .ConfigureServices((_, services) =>
                {
                    services.AddHostedService<AccountManager.Worker.Worker>();

                    var dbName = Guid.NewGuid().ToString();
                    services.AddDbContext<AccountManagerDbContext>(opt =>
                        opt.UseInMemoryDatabase(dbName));
                })
                .ConfigureWebHost(conf =>
                {
                    conf.UseTestServer();
                    conf.Configure(_ => { });
                });
        }
    }
}
