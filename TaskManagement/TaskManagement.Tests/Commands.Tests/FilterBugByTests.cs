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
using TaskManagement.Models.Tasks;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class FilterBugByTests
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
        public void Execute_ShouldThrow_When_IncorrectCountOfParrammetersArePassed_V1()
        {
            ICommand filterBugBy = this.commandFactory.Create("FilterBugBy Status");

            filterBugBy.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void Execute_ShouldThrow_When_IncorrectCountOfParrammetersArePassed_V2()
        {
            ICommand filterBugBy = this.commandFactory.Create("FilterBugBy statusandassignee");

            filterBugBy.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void Execute_ShouldThrow_When_WrongFilterNameIsPassed()
        {
            ICommand filterBugBy = this.commandFactory.Create("FilterBugBy filter");

            filterBugBy.Execute();
        }

        [TestMethod]
        public void Execute_Should_FilterBugsByAssignee()
        {
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");
            ICommand createBug2 = this.commandFactory.Create($"Createbug {Constants.BugTitle2} {Constants.Description} Low Major");
            ICommand createTeam = this.commandFactory.Create($"Createteam {Constants.TeamName}");
            ICommand createMember = this.commandFactory.Create("Createmember pesho");
            ICommand createMember2 = this.commandFactory.Create("Createmember gosho");
            ICommand addMemberToTeam1 = this.commandFactory.Create($"Addmembertoteam {Constants.TeamName} pesho");
            ICommand addMemberToTeam2 = this.commandFactory.Create($"Addmembertoteam {Constants.TeamName} gosho");
            ICommand assignTask1 = this.commandFactory.Create($"assigntask 1 gosho {Constants.TeamName}");
            ICommand assignTask2 = this.commandFactory.Create($"assigntask 2 pesho {Constants.TeamName}");
            ICommand filterBugByAssignee = this.commandFactory.Create($"FilterBugby assignee pesho");

            createBug.Execute();
            createBug2.Execute();
            createTeam.Execute();
            createMember.Execute();
            createMember2.Execute();
            addMemberToTeam1.Execute();
            addMemberToTeam2.Execute();
            assignTask1.Execute();
            assignTask2.Execute();
            filterBugByAssignee.Execute();

            Assert.IsTrue(filterBugByAssignee.Execute().Contains("pesho"));
            Assert.IsFalse(filterBugByAssignee.Execute().Contains("gosho"));
        }

        [TestMethod]
        public void Execute_Should_FilterBugsByStatus()
        {
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");
            ICommand createBug2 = this.commandFactory.Create($"Createbug {Constants.BugTitle2} {Constants.Description} Low Major");
            ICommand changeBug = this.commandFactory.Create("Changebug 1 status fixed");
            ICommand filterBugByStatus = this.commandFactory.Create("FilterBugBy status fixed");

            createBug.Execute();
            createBug2.Execute();
            changeBug.Execute();
            filterBugByStatus.Execute();

            Assert.IsTrue(filterBugByStatus.Execute().Contains(Constants.BugTitle));
            Assert.IsFalse(filterBugByStatus.Execute().Contains(Constants.BugTitle2));
        }

        [TestMethod]
        public void Execute_Should_FilterBugsByStatusAndAssignee()
        {
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");
            ICommand createBug2 = this.commandFactory.Create($"Createbug {Constants.BugTitle2} {Constants.Description} Low Major");
            ICommand createTeam = this.commandFactory.Create($"Createteam {Constants.TeamName}");
            ICommand createMember = this.commandFactory.Create("Createmember pesho");
            ICommand createMember2 = this.commandFactory.Create("Createmember gosho");
            ICommand addMemberToTeam1 = this.commandFactory.Create($"Addmembertoteam {Constants.TeamName} pesho");
            ICommand addMemberToTeam2 = this.commandFactory.Create($"Addmembertoteam {Constants.TeamName} gosho");
            ICommand assignTask1 = this.commandFactory.Create($"assigntask 1 gosho {Constants.TeamName}");
            ICommand assignTask2 = this.commandFactory.Create($"assigntask 2 pesho {Constants.TeamName}");
            ICommand filterBugByStatusAndAssignee = this.commandFactory.Create("FilterBugby statusandassignee active pesho");

            createBug.Execute();
            createBug2.Execute();
            createTeam.Execute();
            createMember.Execute();
            createMember2.Execute();
            addMemberToTeam1.Execute();
            addMemberToTeam2.Execute();
            assignTask1.Execute();
            assignTask2.Execute();
            filterBugByStatusAndAssignee.Execute();

            Assert.IsTrue(filterBugByStatusAndAssignee.Execute().Contains("pesho"));
            Assert.IsTrue(filterBugByStatusAndAssignee.Execute().Contains(Constants.BugTitle2));
            Assert.IsFalse(filterBugByStatusAndAssignee.Execute().Contains("gosho"));
            Assert.IsFalse(filterBugByStatusAndAssignee.Execute().Contains(Constants.BugTitle));

        }
    }
}
