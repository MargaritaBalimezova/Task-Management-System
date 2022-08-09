using TaskManagement.Commands.Contracts;
using TaskManagement.Core.Contracts;
using System;

namespace TaskManagement.Core
{
    public class Engine : IEngine
    {
        private const string TerminationCommand = "Exit";
        private const string EmptyCommandError = "Command cannot be empty.";
        private const string ReportSeparator = "####################";

        private readonly ICommandFactory commandFactory;

        public Engine(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        public void Start()
        {
            Console.SetWindowSize(175, 35);
            Console.WriteLine("Type help to see all of the commands\n");
            while (true)
            {
                try
                {
                    string inputLine = Console.ReadLine().Trim();

                    if (inputLine == string.Empty)
                    {
                        Console.WriteLine(EmptyCommandError);
                        continue;
                    }

                    if (inputLine.Equals(TerminationCommand, StringComparison.InvariantCultureIgnoreCase))
                    {
                        break;
                    }

                    ICommand command = this.commandFactory.Create(inputLine);
                    string result = command.Execute();
                    Console.WriteLine(result.Trim());
                    Console.WriteLine(ReportSeparator);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(ex.Message))
                    {
                        Console.WriteLine(ex.Message);
                    }
                    else
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
    }
}