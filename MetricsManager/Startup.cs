using System;
using System.Net.Http;
using AutoMapper;
using FluentMigrator.Runner;
using MetricsManager.Client;
using MetricsManager.Client.Interfaces;
using MetricsManager.DAL;
using MetricsManager.Job;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyNamespace;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            string _connectionString = Configuration.GetConnectionString("DefaultConnection");
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<SingletonJobFactory>>();
            services.AddSingleton(typeof(ILogger), logger);

            services.AddControllers();
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
            services.AddSingleton<IAgentRepository, AgentRepository>();

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString(_connectionString)
                .ScanIn(typeof(Startup).Assembly).For.Migrations())
                .AddLogging(lb => lb
                .AddFluentMigratorConsole());

            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p => p
                .WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));

            //services.AddHttpClient<IClient, MyNamespace.Client>()
            //    .AddTransientHttpErrorPolicy(p => p
            //    .WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));

            services.AddTransient(s =>
            {
                return s.GetRequiredService<IHttpClientFactory>().CreateClient(string.Empty);
            });

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<AgentMetricJob>();

            services.AddSingleton(new JobSchedule(
                jobType: typeof(AgentMetricJob),
                cronExpression: "0/30 * * * * ?"
                ));

            services.AddHostedService<QuartzHostedService>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            migrationRunner.MigrateUp();
        }
    }
}
