using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class AddCommentTests
    {
        private const int ExpectedParamsCount = 3;
        private const string MemberName = "DummyMember";
        private const string Comment = "this is dummy comment";

        private IRepository repository;
        private ICommandFactory commandFactory;
        private string title;
        private string description;
        private PriorityType priority;
        private SizeType size;
        private IStory story;
        private IMember member;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);
            this.title = "StoryDummy";
            this.description = "DescriptionDummy";
            this.priority = PriorityType.High;
            this.size = SizeType.Medium;

            this.story = this.repository.CreateStory(title, description, priority, size);
            this.member = this.repository.CreateMember(MemberName);
        }

        [TestMethod]
        public void Execute_Should_AddCommentToTask()
        {
            //Arrange
            var commandParams = new string[] {this.story.Id.ToString()
                                               ,MemberName
                                               ,Comment};

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
                                               ,MemberName
                                               ,Comment};

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
