using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagement.Commands.Contracts;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Validations;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class ShowAllTeamBoardsTests
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
            ICommand showAllTeamBoards = this.commandFactory.Create("ShowAllTeamBoards");

            showAllTeamBoards.Execute();
        }

        [TestMethod]
        public void Execute_Should_ShowTeamBoards()
        {

            ICommand createTeam = this.commandFactory.Create($"Createteam {Common.Constants.TeamName}");
            ICommand createBoardInTeam = this.commandFactory.Create($"CreateBoardInTeam {Common.Constants.BoardName} {Common.Constants.TeamName}");
            ICommand showAllTeamBoards = this.commandFactory.Create($"ShowAllTeamBoards {Common.Constants.TeamName}");

            createTeam.Execute();
            createBoardInTeam.Execute();
            showAllTeamBoards.Execute();

            Assert.IsTrue(showAllTeamBoards.Execute().Contains(Constants.BOARD_HEADER));
        }




    }
}
