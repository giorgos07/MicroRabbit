using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Commands;
using MicroRabbit.Domain.Core.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MicroRabbit.Infrastructure.Bus
{
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;

        public RabbitMQBus(IMediator mediator) {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        public Task SendCommand<TCommand>(TCommand command) where TCommand : Command {
            return _mediator.Send(command);
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : Event {
            var factory = new ConnectionFactory {
                HostName = "localhost",
                Port = 5672
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()) {
                var eventName = typeof(TEvent).Name;
                channel.QueueDeclare(queue: eventName, durable: false, exclusive: false, autoDelete: true, arguments: null);
                var message = JsonConvert.SerializeObject(@event);
                var messageBody = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: string.Empty, routingKey: eventName, basicProperties: null, body: messageBody);
            }
        }

        public void Subscribe<TEvent, TEventHandler>() where TEvent : Event where TEventHandler : IEventHandler<TEvent> {
            var eventType = typeof(TEvent);
            var eventName = eventType.Name;
            var eventHandlerType = typeof(TEventHandler);
            if (!_eventTypes.Contains(eventType)) {
                _eventTypes.Add(eventType);
            }
            if (_handlers.ContainsKey(eventName)) {
                _handlers.Add(eventName, new List<Type>());
            }
            if (_handlers[eventName].Any(x => x.GetType() == eventHandlerType)) {
                throw new ArgumentException($"Event handler type '{eventHandlerType}' is already registered for event '{eventName}'", nameof(eventHandlerType));
            }
            _handlers[eventName].Add(eventHandlerType);
            StartBasicConsume<TEvent>();
        }

        private void StartBasicConsume<TEvent>() where TEvent : Event {
            var factory = new ConnectionFactory {
                HostName = "localhost",
                Port = 5672,
                DispatchConsumersAsync = true
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()) {
                var eventName = typeof(TEvent).Name;
                channel.QueueDeclare(queue: eventName, durable: false, exclusive: false, autoDelete: true, arguments: null);
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += ConsumerReceived;
                channel.BasicConsume(queue: eventName, autoAck: true, consumer);
            }
        }

        private async Task ConsumerReceived(object sender, BasicDeliverEventArgs @event) {
            var eventName = @event.RoutingKey;
            var message = Encoding.UTF8.GetString(@event.Body);
            try {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            } catch (Exception) {
                throw;
            }
        }

        private async Task ProcessEvent(string eventName, string message) {
            if (!_handlers.ContainsKey(eventName)) {
                return;
            }
            var subscriptions = _handlers[eventName];
            foreach (var subscription in subscriptions) {
                var handlerInstance = Activator.CreateInstance(subscription);
                if (handlerInstance == null) {
                    continue;
                }
                var eventType = _eventTypes.SingleOrDefault(x => x.Name == eventName);
                var @event = JsonConvert.DeserializeObject(message, eventType);
                var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                await (Task)concreteType.GetMethod(nameof(IEventHandler<Event>.Handle)).Invoke(handlerInstance, new object[] { @event });
            }
        }
    }
}
