using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Tasks;
using TaskManagement.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagement.Tests.Models.Tests
{
    [TestClass]
    public class BoardTests
    {
        private const int NameMinLen = 5;
        private const int NameMaxLen = 10;

        static string title;
        static string desctription;
        static Board board;
        static PriorityType priority;
        static Severity severity;
        static string assignee;
        static IList<string> steps;
        static Bug bug;


        [ClassInitialize()]
        public static void BoardTests_Classinitialize(TestContext context)
        {
            board = new Board("Board Name");

            title = "Task title";
            desctription = "Task description";
            priority = PriorityType.Medium;
            severity = Severity.Major;
            assignee = "Test Assignee";
            steps = new List<string>();

            bug = new Bug(title, desctription, 1, priority, severity, assignee, steps);
        }

        [TestMethod]
        public void AddEventLog_Should_AddElement_When_InstanceIsCreated()
        {
            Assert.AreEqual(1, board.ActivityLog.Count);
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_NameShorterThanMinLen()
        {
            Assert.ThrowsException<ArgumentException>(() => new Board(new string('a', NameMinLen - 1)));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_NameLargerThanMaxLen()
        {
            Assert.ThrowsException<ArgumentException>(() => new Board(new string('a', NameMaxLen + 1)));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_NameNullValuePassed()
        {
            Assert.ThrowsException<ArgumentException>(() => new Board(null));
        }

        [TestMethod]
        public void Board_Shoud_ImplementIBoardInterface()
        {
            var type = typeof(Board);
            var isAssignable = typeof(IBoard).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Board class does not implement IBoard interface!");
        }

        [TestMethod]
        public void Should_CreateNewBoard_When_ValuesAreCorrect()
        {
            Assert.AreEqual("Board Name", board.Name);                   
        }

        [TestMethod]
        public void BoardTasks_Should_ReturnCopyOfTasksCollection()
        {

            board.BoardTasks.Add(bug);
            Assert.AreEqual(0, board.BoardTasks.Count);
        }
       

        [TestMethod]
        public void BoardTasks_Should_AddTaskToBoard()
        {
            board.AddTaskToBoard(bug);

            Assert.AreEqual(1, board.BoardTasks.Count);
            //Reset
            board.RemoveTaskFromBoard(bug);
        }

        [TestMethod]
        public void BoardTasks_Should_RemoveTaskFromBoard()
        {
            board.AddTaskToBoard(bug);
            board.RemoveTaskFromBoard(bug);

            Assert.AreEqual(0, board.BoardTasks.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddTaskToBoard_Should_Throw_When_AddAnItemTwice()
        {
            board.AddTaskToBoard(bug);
            try
            {
                board.AddTaskToBoard(bug);
            }
            catch (InvalidOperationException)
            {
                board.RemoveTaskFromBoard(bug);
                throw;
            }
          
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveTaskFromBoard_Should_Throw_When_RemoveATaskWhichIsNotOnBoard()
        {           
            board.RemoveTaskFromBoard(bug);
        }


    }
}
