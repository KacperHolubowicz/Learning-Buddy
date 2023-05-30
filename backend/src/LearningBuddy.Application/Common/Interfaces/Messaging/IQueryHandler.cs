﻿using MediatR;

namespace LearningBuddy.Application.Common.Interfaces.Messaging
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
    }
}
