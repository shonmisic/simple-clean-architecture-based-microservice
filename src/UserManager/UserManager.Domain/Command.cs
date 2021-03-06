﻿using System;

namespace UserManager.Domain
{
    public abstract class Command : Command<Void>
    {
        protected Command() { }
        protected Command(Guid correlationId) : base(correlationId) { }
    }

    public abstract class Command<TResponse> : BaseCommand<TResponse> 
    {
        protected Command() { }
        protected Command(Guid correlationId) : base(correlationId) { }
    }

    public abstract class BaseCommand : BaseCommand<Void>
    {
    }

    public abstract class BaseCommand<TResponse> : IRequest<TResponse>
    {
        protected BaseCommand()
        {
            CommandId = Guid.NewGuid();
            CorrelationId = CommandId;
        }

        protected BaseCommand(Guid correlationId)
        {
            CommandId = Guid.NewGuid();
            CorrelationId = correlationId;
        }

        public Guid CommandId { get; }
        public Guid CorrelationId { get; }
    }
}
