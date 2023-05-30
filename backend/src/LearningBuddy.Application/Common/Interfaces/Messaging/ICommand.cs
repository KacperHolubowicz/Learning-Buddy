using MediatR;

namespace LearningBuddy.Application.Common.Interfaces.Messaging
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
