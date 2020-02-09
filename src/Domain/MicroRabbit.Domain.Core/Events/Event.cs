using System;

namespace MicroRabbit.Domain.Core.Events
{
    public abstract class Event
    {
        protected Event() {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; protected set; }
    }
}
