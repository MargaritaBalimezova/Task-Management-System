using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Tests;

namespace TaskManagement.Commands
{
    [TestClass]
    public class AddMemberToTeamTests
    {
        private const int ExpectedParamsCount = 2;
        private const string teamName = "DummyTeam";
        private const string memberName = "DummyMember";

        private IMember member;
        private ITeam team;
        private IRepository repository;
        private ICommandFactory commandFactory;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);

            this.member = this.repository.CreateMember(memberName);
            this.team = this.repository.CreateTeam(teamName);
        }

        [TestMethod]
        public void Execute_Should_AddMemberToTeam_When_ParamsValid()
        {
            //Arrange
            var commandParameters = new string[] { teamName, memberName };
            var command = new AddMemberToTeam(commandParameters, repository);
            //Act
            command.Execute();
            //Assert
            Assert.AreEqual(member, this.repository.Teams[this.repository.Teams.Count - 1]
                                                    .Members[0], "AddMemberToTeam failed to add member to give team!");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "AddMemberToTeam parameters count validation failed!")]
        public void Execute_Should_ThrowException_When_ArgumentCountDiffThanExpected()
        {
            //Arrange
            var commandParameters = Helpers.GetDummyList(ExpectedParamsCount - 1);
            var command = new AddMemberToTeam(commandParameters, repository);

            //Act & Asssert
            command.Execute();
        }
    }
}