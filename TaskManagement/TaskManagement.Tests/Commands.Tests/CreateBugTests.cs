using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using TaskManagement.Commands.Contracts;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class CreateBugTests
    {
        private IRepository repository;
        private ICommandFactory commandFactory;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);
            StreamReader sr = new StreamReader(Constants.fullPath);
            Console.SetIn(sr);
        }

        [TestMethod]
        public void Execute_Should_CreateNewBug_When_CorrectValuesArePassed()
        {
           
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");            
            createBug.Execute();                
            
                
            IBug bug = this.repository.Bugs.FirstOrDefault(b => b.Id == 1); 
            Assert.AreEqual(Constants.BugTitle, bug.Title);
            Assert.AreEqual(Constants.Description, bug.Description);
            Assert.AreEqual(PriorityType.High, bug.Priority);
            Assert.AreEqual(Severity.Major, bug.Severity);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void Execute_ShouldThrow_When_IncorrectCountOfParrammetersArePassed()
        {
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} Major");

            createBug.Execute();
        }

        [TestMethod]
        public void Execute_Should_WriteSpecialMessage_When_NoStepsAreInsert()
        {
            var sw = new StringWriter();
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");
            StreamReader sr = new StreamReader(Constants.fullPath2);
            Console.SetIn(sr);
            Console.SetOut(sw);

            createBug.Execute();
            
            string result = sw.ToString();

            Assert.IsTrue(result.Contains("You must provide steps to reproduce before ending the program!"));
        }

        [TestMethod]
        public void Execute_Should_WriteSpecialMessage_When_EmptyStepIsInsert()
        {
            var sw = new StringWriter();
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");
           
            Console.SetOut(sw);

            createBug.Execute();

            string result = sw.ToString();

            Assert.IsTrue(result.Contains("This input can not be empty!Please enter your step again!"));
        }
    }
}
