﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rehber.Model.MessageContracts;
using Rehber.Services.Elasticsearch.MasstransitConsumers;

namespace Rehber.Services.Elasticsearch
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            IBusControl _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h => { });

                cfg.ReceiveEndpoint(host, "ElasticSearch", e =>
                {
                    e.Consumer(() => new EmployeeAddedConsumer());
                    e.Consumer(() => new EmployeeDeletedConsumer());
                    e.Consumer(() => new EmployeeUpdatedConsumer());
                });
            });
            _bus.StartAsync();

            ElasticsearchDbIndexer eIndexer = new ElasticsearchDbIndexer();
            eIndexer.DeleteAllIndexes();
            eIndexer.IndexAllEmployeesAndUnits();
            //eIndexer.IndexAllUnits();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.Run(async (context) =>
            {
                if (context.Request.Path == "/" || context.Request.Path == "/version")
                {
                    await context.Response.WriteAsync("Rehber Elasticsearch API v1");
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
            });

        }
    }
}
