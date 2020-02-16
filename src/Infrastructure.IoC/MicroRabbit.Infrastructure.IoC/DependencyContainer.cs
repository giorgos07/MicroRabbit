using MediatR;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Services;
using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Data.Repositories;
using MicroRabbit.Banking.Domain.CommandHandlers;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infrastructure.Bus;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Application.Services;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Data.Repositories;
using MicroRabbit.Transfer.Domain.EventHandlers;
using MicroRabbit.Transfer.Domain.Events;
using MicroRabbit.Transfer.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MicroRabbit.Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static void ConfigureServices(IServiceCollection services) {
            // Domain bus.
            services.AddSingleton<IEventBus, RabbitMQBus>(serviceProvider => {
                var mediator = serviceProvider.GetService<IMediator>();
                var serviceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
                return new RabbitMQBus(mediator, serviceScopeFactory);
            });
            // Services.
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAccountTransferService, AccountTransferService>();
            // Commands
            services.AddTransient<IRequestHandler<AccountTransferCommand, bool>, AccountTransferCommandHandler>();
            // Events
            services.AddTransient<IEventHandler<AccountTransferCreatedEvent>, AccountTransferCreatedEventHandler>();
            // Event Handlers.
            services.AddTransient<AccountTransferCreatedEventHandler>();
            // Data services.
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountTransferRepository, AccountTransferRepository>();
            services.TryAddScoped<BankingDbContext>();
            services.TryAddScoped<TransferDbContext>();
        }
    }
}
