using MediatorLocator.Command;

namespace MediatorLocator.Handler;

public interface IHandler<TCommand, TParam, TResult> where TCommand : ICommand<TParam, TResult>
{
    public TResult Handle(TParam param);
}