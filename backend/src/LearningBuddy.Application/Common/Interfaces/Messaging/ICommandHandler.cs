﻿using MediatR;

namespace LearningBuddy.Application.Common.Interfaces.Messaging
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
    }
}
