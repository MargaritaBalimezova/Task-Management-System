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
    public class ShowTaskActivityCommand
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
        [ExpectedException(typeof(InvalidUserInputException))]
        public void Execute_ShouldThrow_When_IncorrectCountOfParrammetersArePassed()
        {
            ICommand showTaskActivity = this.commandFactory.Create("ShowTaskActivity");

            showTaskActivity.Execute();
        }

        [TestMethod]
        public void Execute_Should_ShowTaskActivity_When_CorrectIdIsPassed()
        {
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");
            ICommand changeBugStatus = this.commandFactory.Create($"Changebug 1 Status fixed");
            ICommand changeBugSeverity = this.commandFactory.Create($"Changebug 1 severity critical");
            ICommand changeBugPriority = this.commandFactory.Create($"Changebug 1 priority low");
            ICommand showTaskActivity = this.commandFactory.Create("ShowTaskActivity 1");

            createBug.Execute();
            changeBugStatus.Execute();
            changeBugSeverity.Execute();
            changeBugPriority.Execute();         
            
            Assert.IsTrue(showTaskActivity.Execute().Contains($"Status of bug with ID 1 {Constants.BugTitle} was changed from {Status.Active} to {Status.Fixed}."));
            Assert.IsTrue(showTaskActivity.Execute().Contains($"Severity of bug with ID 1 {Constants.BugTitle} was changed from {Severity.Major} to {Severity.Critical}!"));
            Assert.IsTrue(showTaskActivity.Execute().Contains($"Priority of bug with ID 1 {Constants.BugTitle} was changed from {PriorityType.High} to {PriorityType.Low}!"));
        }

    }
}
