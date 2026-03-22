
namespace MediatorLocator.Command;

public class GetLengthCommand : ICommand<string, int>
{
    public int Execute(string str)
    {
        return str.Length;
    }
}

