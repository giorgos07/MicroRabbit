using System;
using System.IO;
using System.Reflection;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infrastructure.IoC;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.EventHandlers;
using MicroRabbit.Transfer.Domain.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MicroRabbit.Transfer.Api
{
    /// <summary>
    /// The entry point for configuring the web application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Creates a new instance of <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        /// <summary>
        /// Represents a set of key/value application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Transfer Microservice",
                    Version = "v1",
                    Contact = new OpenApiContact {
                        Name = "Georgios Manoltzas",
                        Email = "g.manoltzas@indice.gr"
                    },
                    Description = "Microservice for transfers."
                });
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath)) {
                    options.IncludeXmlComments(xmlPath);
                }
            });
            services.AddMediatR(typeof(Startup));
            services.AddDbContext<TransferDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("TransferDbConnection"));
            });
            DependencyContainer.ConfigureServices(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Defines a class that provides the mechanisms to configure an application's request pipeline.</param>
        /// <param name="environment">Provides information about the web hosting environment an application is running in.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment) {
            if (environment.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Transfer Microservice v1");
            });
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app) {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<AccountTransferCreatedEvent, AccountTransferCreatedEventHandler>();
        }
    }
}
