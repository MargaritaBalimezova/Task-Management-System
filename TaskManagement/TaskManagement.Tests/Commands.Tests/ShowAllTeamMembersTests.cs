using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;
using TaskManagement.Commands;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class ShowAllTeamMembersTests
    {
        private const int ExpectedParamsCount = 1;
        private const string teamName = "DummyTeam";
        private const string memberName = "DummyMember";

        private IRepository repository;
        private ICommandFactory commandFactory;
        private ITeam team;
        private IMember member;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);

            team = this.repository.CreateTeam(teamName);
            member = this.repository.CreateMember(memberName);
        }

        [TestMethod]
        public void Execute_Should_ReturnTheRightAmountOfMembersInTeam()
        {
            //Arrange
            var commandParameters = new string[] { teamName, memberName };
            var command = new ShowTeamMembersCommand(new string[] { teamName }, repository);
            var addMemberCommand = new AddMemberToTeam(commandParameters, repository);

            //Act
            addMemberCommand.Execute();

            //Assert
            Assert.IsTrue(command.Execute().Contains($"Member Name: {this.member.Name}"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ShowAllTeamMembers argument validation failed!")]
        public void Execute_Should_ThrowException_When_DiffParamsCountPassed()
        {
            //Arrange
            var commandParameters = Helpers.GetDummyList(ExpectedParamsCount + 1);
            var command = new ShowTeamMembersCommand(commandParameters, repository);

            //Act & Assert
            command.Execute();
        }
    }
}
