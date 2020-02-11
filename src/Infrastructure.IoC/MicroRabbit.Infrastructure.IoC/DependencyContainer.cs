using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infrastructure.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace MicroRabbit.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void ConfigureServices(IServiceCollection services) {
            services.AddTransient<IEventBus, RabbitMQBus>();
        }
    }
}
