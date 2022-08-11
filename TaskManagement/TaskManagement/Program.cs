using TaskManagement.Core;
using TaskManagement.Core.Contracts;

namespace TaskManagement
{
    internal class Program
    {
        private static void Main()
        {
            IRepository repository = new Repository();
            ICommandFactory commandFactory = new CommandFactory(repository);
            IEngine cosmeticsEngine = new Engine(commandFactory);
            cosmeticsEngine.Start();
        }
    }
}