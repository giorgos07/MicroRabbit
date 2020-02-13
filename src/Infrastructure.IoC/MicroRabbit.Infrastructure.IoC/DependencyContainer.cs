using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Services;
using MicroRabbit.Banking.Data.Repositories;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infrastructure.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace MicroRabbit.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void ConfigureServices(IServiceCollection services) {
            // Domain bus.
            services.AddTransient<IEventBus, RabbitMQBus>();
            // Application services.
            services.AddTransient<IAccountService, AccountService>();
            // Data services.
            services.AddTransient<IAccountRepository, AccountRepository>();
        }
    }
}
