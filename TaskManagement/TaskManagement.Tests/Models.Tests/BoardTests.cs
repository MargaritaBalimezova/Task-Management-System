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
        static int id;
        static Board board;
        static PriorityType priority;
        static Severity severity;
        static IMember assignee;
        static IList<string> steps;
        static Bug bug;


        [ClassInitialize()]
        public static void BoardTests_Classinitialize(TestContext context)
        {
            board = new Board("Board Name");

            title = "Task title";
            desctription = "Task description";
            id = 1;
            priority = PriorityType.Medium;
            severity = Severity.Major;
            assignee = new Member("Test Assignee");
            steps = new List<string>();

            bug = new Bug(title, desctription,id, priority, severity, steps);
        }

        [TestMethod]
        public void AddEventLog_Should_AddElement_When_InstanceIsCreated()
        {
            Assert.AreEqual(1, board.ActivityLog.Count,"Event added to the log succesfully!");
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_NameShorterThanMinLen()
        {
            Assert.ThrowsException<ArgumentException>(() => 
            new Board(new string('a', NameMinLen - 1)),$"Board name must be  longer than {NameMinLen}!");
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_NameLargerThanMaxLen()
        {
            Assert.ThrowsException<ArgumentException>(() => 
            new Board(new string('a', NameMaxLen + 1)), $"Board name must be  shorter than {NameMaxLen}!");
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_NameNullValuePassed()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            new Board(null),"Board name can not be null or empty!");
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
            Assert.AreEqual("Board Name", board.Name,"Board created successfully!");                   
        }

        [TestMethod]
        public void BoardTasks_Should_ReturnCopyOfTasksCollection()
        {

            board.BoardTasks.Add(bug);
            Assert.AreEqual(0, board.BoardTasks.Count,"Successfully returned a copy of board tasks list!");
        }
       

        [TestMethod]
        public void BoardTasks_Should_AddTaskToBoard()
        {
            board.AddTaskToBoard(bug);

            Assert.AreEqual(1, board.BoardTasks.Count,"Task added successfully to board!");
            //Reset
            board.RemoveTaskFromBoard(bug);
        }

        [TestMethod]
        public void BoardTasks_Should_RemoveTaskFromBoard()
        {
            board.AddTaskToBoard(bug);
            board.RemoveTaskFromBoard(bug);

            Assert.AreEqual(0, board.BoardTasks.Count, "Task removed successfully from board!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),"This task is already on the board!")]
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
        [ExpectedException(typeof(InvalidOperationException), "This task does not exist on this board!")]
        public void RemoveTaskFromBoard_Should_Throw_When_RemoveATaskWhichIsNotOnBoard()
        {           
            board.RemoveTaskFromBoard(bug);
        }


    }
}
