using MediatR;

namespace LearningBuddy.Application.Common.Interfaces.Messaging
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
