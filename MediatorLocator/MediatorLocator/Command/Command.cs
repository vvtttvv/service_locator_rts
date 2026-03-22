namespace MediatorLocator.Command;

public class Command<TParam, TResult> : ICommand<TParam, TResult>
{ 
    public TResult Execute(TParam param)
    {
        throw new NotImplementedException();
    }
}