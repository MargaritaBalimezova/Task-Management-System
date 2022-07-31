using System;
using System.Collections.Generic;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Models;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Tasks;

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