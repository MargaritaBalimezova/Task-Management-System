using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class AddCommentTests
    {
        private const int ExpectedParamsCount = 3;

        private IRepository repository;
        private ICommandFactory commandFactory;
        private IStory story;
        private IMember member;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);

            this.story = this.repository.CreateStory(Constants.Title, Constants.Description,
                Constants.priority, Constants.size);
            this.member = this.repository.CreateMember(Constants.MemberName);
        }

        [TestMethod]
        public void Execute_Should_AddCommentToTask()
        {
            //Arrange
            var commandParams = new string[] {this.story.Id.ToString()
                                               ,Constants.MemberName
                                               ,Constants.Comment};

            var command = new AddCommentCommand(commandParams, repository);

            //Act
            command.Execute();

            //Assert
            Assert.IsTrue(this.repository.Stories[this.repository.Stories.Count - 1].Comments.Count > 0,
                "AddComment failed to add comment to task");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "AddComment id validation failed!")]
        public void Execute_Should_ThrowException_When_TaskIdDoesNotExists()
        {
            //Arrange
            var commandParams = new string[] {  "kuche"
                                               ,Constants.MemberName
                                               ,Constants.Comment};

            var command = new AddCommentCommand(commandParams, repository);

            //Act & Assert
            command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),"AddMemberToTeam parameters count validation failed!")]
        public void Execute_Should_ThrowException_When_ArgumentCountDiffThanExpected()
        {
            //Arrange
            var commandParameters = Helpers.GetDummyList(ExpectedParamsCount - 1);
            var command = new AddCommentCommand(commandParameters, repository);

            //Act & Asssert
            command.Execute();
        }
    }
}
