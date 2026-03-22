using MediatorLocator.Command;
using MediatorLocator.Handler;

namespace MediatorLocator.Mediation;

public interface IMediator
{
    void Register<TCommand, T, TResult>(IHandler<TCommand, T, TResult> handler) where TCommand : ICommand<T, TResult>;
    TResult Send<T, TResult>(ICommand<T, TResult> command, T param);
}