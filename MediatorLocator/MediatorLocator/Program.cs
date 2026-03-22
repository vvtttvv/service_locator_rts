using MediatorLocator.Command;
using MediatorLocator.Handler;
using MediatorLocator.Mediation;

namespace MediatorLocator;

public class Program
{
    static void Main()
    {
        ICommand<string, int> cmd = new GetLengthCommand();
        IHandler<GetLengthCommand, string, int> handler = new ConcreteCommandHandler();
        IMediator mediator = new Mediator();
        mediator.Register(handler);
        Console.WriteLine(mediator.Send(cmd, "Johnny"));
        
    }
}
