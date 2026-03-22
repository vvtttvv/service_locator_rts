using MediatorLocator.Command;

namespace MediatorLocator.Handler;

public class ConcreteCommandHandler :  IHandler<GetLengthCommand, string, int>
{
    private GetLengthCommand _cmd = new GetLengthCommand();
    public int Handle(string param)
    {
        return _cmd.Execute(param);
    }
}