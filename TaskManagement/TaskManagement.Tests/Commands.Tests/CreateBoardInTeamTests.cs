using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Commands.Contracts;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;

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
            ICommand createTeam = this.commandFactory.Create("Createteam TestTeam");
            ICommand createBoardInTeam = this.commandFactory.Create("CreateBoardInTeam TestBoard TestTeam");

         
            createTeam.Execute();
            createBoardInTeam.Execute();

            ITeam team = this.repository.FindTeamByName("TestTeam");
            IBoard board = this.repository.FindBoardByNameInTeam(team, "TestBoard");

            Assert.AreEqual("TestBoard", board.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void Execute_ShouldThrow_When_IncorrectCountOfParrammetersArePassed()
        {
            ICommand createBoardInTeam = this.commandFactory.Create("CreateBoardInTeam TestBoard");

            createBoardInTeam.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Execute_ShouldThrow_When_BoardIsCreatedTwiceInSameTeam()
        {
            ICommand createTeam = this.commandFactory.Create("Createteam TestTeam");
            ICommand createBoardInTeam = this.commandFactory.Create("CreateBoardInTeam TestBoard TestTeam");
           
            createTeam.Execute();
            createBoardInTeam.Execute();
            createBoardInTeam.Execute();
        }
    }
}
