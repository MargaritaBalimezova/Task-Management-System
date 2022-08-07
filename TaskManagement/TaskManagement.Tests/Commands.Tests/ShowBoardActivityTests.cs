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
using TaskManagement.Validations;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class ShowBoardActivityTests
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
            ICommand showBoardActivity = this.commandFactory.Create("ShowBoardActivity");

            showBoardActivity.Execute();
        }

        [TestMethod]
        public void Execute_Should_ShowTeamBoardActivity()
        {

            ICommand createTeam = this.commandFactory.Create($"Createteam {Common.Constants.TeamName}");
            ICommand createBoardInTeam = this.commandFactory.Create($"CreateBoardInTeam {Common.Constants.BoardName} {Common.Constants.TeamName}");
            ICommand showBoardActivity = this.commandFactory.Create($"ShowBoardActivity {Common.Constants.BoardName} {Common.Constants.TeamName}");

            createTeam.Execute();
            createBoardInTeam.Execute();
            showBoardActivity.Execute();
            Assert.IsTrue(showBoardActivity.Execute().Contains($"Successfuly created Board with name {Common.Constants.BoardName}!"));
        }

    }
}
