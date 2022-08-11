using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaskManagement.Commands.Contracts;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class CreateBoardInTeamTests
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
        public void Execute_Should_CreateNewBoardInTeam_When_CorrectValuesArePassed()
        {
            ICommand createTeam = this.commandFactory.Create($"Createteam {Constants.TeamName}");
            ICommand createBoardInTeam = this.commandFactory.Create($"CreateBoardInTeam {Constants.BoardName} {Constants.TeamName}");

         
            createTeam.Execute();
            createBoardInTeam.Execute();

            ITeam team = this.repository.FindTeamByName(Constants.TeamName);
            IBoard board = this.repository.FindBoardByNameInTeam(team, Constants.BoardName);

            Assert.AreEqual(Constants.BoardName, board.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void Execute_ShouldThrow_When_IncorrectCountOfParrammetersArePassed()
        {
            ICommand createBoardInTeam = this.commandFactory.Create($"CreateBoardInTeam {Constants.BoardName}");

            createBoardInTeam.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Execute_ShouldThrow_When_BoardIsCreatedTwiceInSameTeam()
        {
            ICommand createTeam = this.commandFactory.Create($"Createteam {Constants.TeamName}");
            ICommand createBoardInTeam = this.commandFactory.Create($"CreateBoardInTeam {Constants.BoardName} {Constants.TeamName}");
           
            createTeam.Execute();
            createBoardInTeam.Execute();
            createBoardInTeam.Execute();
        }
    }
}
