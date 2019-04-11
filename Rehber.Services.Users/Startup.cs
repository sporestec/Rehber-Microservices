using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MassTransit;
using Rehber.Model.MessageContracts;

namespace Rehber.Services.Users
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
            // Add framework services
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h => { });
            });

            services.AddSingleton<IPublishEndpoint>(bus);
            services.AddSingleton<ISendEndpointProvider>(bus);
            services.AddSingleton<IBus>(bus);

            var timeout = TimeSpan.FromSeconds(10);
            var serviceAddress = new Uri("rabbitmq://localhost/");

            services.AddScoped<IRequestClient<IEmployeeAdded, IEmployeeAdded>>(x =>
                new MessageRequestClient<IEmployeeAdded, IEmployeeAdded>(x.GetRequiredService<IBus>(), serviceAddress, timeout, timeout));

            bus.Start();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
