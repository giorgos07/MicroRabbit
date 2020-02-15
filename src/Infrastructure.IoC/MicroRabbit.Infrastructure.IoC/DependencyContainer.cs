using MediatR;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Services;
using MicroRabbit.Banking.Data.Repositories;
using MicroRabbit.Banking.Domain.CommandHandlers;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infrastructure.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace MicroRabbit.Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static void ConfigureServices(IServiceCollection services) {
            // Domain bus.
            services.AddTransient<IEventBus, RabbitMQBus>();
            // Application services.
            services.AddTransient<IAccountService, AccountService>();
            // Commands
            services.AddTransient<IRequestHandler<AccountTransferCommand, bool>, AccountTransferCommandHandler>();
            // Data services.
            services.AddTransient<IAccountRepository, AccountRepository>();
        }
    }
}
