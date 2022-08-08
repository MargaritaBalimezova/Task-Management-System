using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Commands.Contracts;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.BugStatus;
using TaskManagement.Models.Tasks;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class ChangeBugTests
    {
        private IRepository repository;
        private ICommandFactory commandFactory;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);
        }

        [TestMethod]
        public void Execute_Should_ChangeBugStatus_When_CorrectValuesArePassed()
        {

            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");
            ICommand changeBug = this.commandFactory.Create("Changebug 1 status fixed");
            // Act
            createBug.Execute();
            changeBug.Execute();

            IBug bug = (IBug)this.repository.FindTaskById(1);
            Assert.AreEqual(Status.Fixed, bug.Status);
        }

        [TestMethod]
        public void Execute_Should_ChangeBugPriority_When_CorrectValuesArePassed()
        {

            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");
            ICommand changeBug = this.commandFactory.Create("Changebug 1 priority low");
            // Act
            createBug.Execute();
            changeBug.Execute();

            IBug bug = (IBug)this.repository.FindTaskById(1);
            Assert.AreEqual(PriorityType.Low, bug.Priority);
        }

        [TestMethod]
        public void Execute_Should_ChangeBugSeverity_When_CorrectValuesArePassed()
        {

            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");
            ICommand changeBug = this.commandFactory.Create("Changebug 1 severity critical");
            // Act
            createBug.Execute();
            changeBug.Execute();

            IBug bug = (IBug)this.repository.FindTaskById(1);
            Assert.AreEqual(Severity.Critical, bug.Severity);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void Execute_ShouldThrow_When_IncorrectCountOfParrammetersArePassed()
        {
            ICommand changeBug = this.commandFactory.Create("Changebug 1 critical");
            changeBug.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void Execute_ShouldThrow_When_IncorrectFilterIsPassed()
        {
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");
            ICommand changeBug = this.commandFactory.Create("Changebug 1 filter critical");
            createBug.Execute();
            changeBug.Execute();
        }
    }
}
