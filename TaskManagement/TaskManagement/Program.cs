using System;
using System.Collections.Generic;
using TaskManagement.Models;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Tasks;

namespace TaskManagement
{
    internal class Program
    {
        private static void Main()
        {
            Board board = new Board("Board Name");

            var title = "Task title";
            var desctription = "Task description";
            var priority = PriorityType.Medium;
            var severity = Severity.Major;
            var assignee = "Test Assignee";
            IList<string> steps = new List<string>();

            Bug bug = new Bug(title, desctription, 1, priority, severity, assignee, steps);
            // Act
            board.AddTaskToBoard(bug);
            Console.WriteLine(board.PrintBoardTasks());
            // Assert
            
        }
    }
}