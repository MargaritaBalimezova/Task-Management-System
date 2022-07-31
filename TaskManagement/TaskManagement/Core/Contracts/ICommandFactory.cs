using TaskManagement.Commands.Contracts;

namespace TaskManagement.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand Create(string commandLine);
    }
}
