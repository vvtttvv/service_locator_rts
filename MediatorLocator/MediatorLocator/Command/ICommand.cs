namespace MediatorLocator.Command;

public interface ICommand<TParam, TResult>
{
    public TResult Execute(TParam param);
}