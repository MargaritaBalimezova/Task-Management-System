using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class RemoveMemberFromTeamTests
    {
        private const int ExpectedParamsCount = 2;

        private IMember member;
        private ITeam team;
        private IRepository repository;
        private ICommandFactory commandFactory;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);

            this.team = this.repository.CreateTeam(Constants.TeamName);
            this.member = this.repository.CreateMember(Constants.MemberName);
            this.team.AddMember(member);
        }

        [TestMethod]
        public void Execute_Should_RemoveMemberFromTeam_When_ParamsValid()
        {
            //Arrange
            var commandParameters = new string[] { Constants.TeamName, Constants.MemberName };
            var command = new RemoveMemberFromTeam(commandParameters, this.repository);
            //Act
            command.Execute();
            //Assert
            Assert.IsFalse(this.repository.IsMemberInTeam(this.team, this.member),
                "RemoveMemberFromTeam command failed to remove the the member!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "AddMemberToTeam parameters count validation failed!")]
        public void Execute_Should_ThrowException_When_ArgumentCountDiffThanExpected()
        {
            //Arrange
            var commandParameters = Helpers.GetDummyList(ExpectedParamsCount - 1);
            var command = new RemoveMemberFromTeam(commandParameters, this.repository);

            //Act & Asssert
            command.Execute();
        }

        [TestMethod]
        public void Execute_Should_UnassignTasksFromRemovedMember()
        {
            //Arrange
            var story = this.repository.CreateStory(Constants.Title, Constants.Description, Constants.priorityHigh, Constants.sizeMedium);
            var bug = this.repository.CreateBug(Constants.Title, Constants.Description,Constants.priority, Severity.Critical, new List<string>());
            bug.AddAssignee(member);
            story.AddAssignee(member);
            member.AddTask(bug);
            member.AddTask(story);
            var commandParameters = new string[] { Constants.TeamName, Constants.MemberName };
            var command = new RemoveMemberFromTeam(commandParameters, this.repository);
            //Act
            command.Execute();
            //Assert
            Assert.IsTrue(story.Assignee == null, "RemoveMemberFromTeam command failed to unassigne tasks before removing member!");
            Assert.IsTrue(bug.Assignee == null, "RemoveMemberFromTeam command failed to unassigne tasks before removing member!");
        }
    }
}