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
    public class FilterAssignedTaskByTests
    {
        private IRepository repository;
        private ICommandFactory commandFactory;

        private IStory story;
        private IBug bug;
        private IFeedback feedback;
        private IMember member;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);

            this.story = this.repository.CreateStory(Constants.Title, Constants.Description, Constants.priority, Constants.size);
            this.feedback = this.repository.CreateFeedBack(Constants.Title, Constants.Description, 10);
            this.bug = this.repository.CreateBug(Constants.Title, Constants.Description, Constants.priority, Severity.Critical, new List<string>());
            this.member = this.repository.CreateMember(Constants.MemberName);

        }

        [TestMethod]
        public void Execute_Should_ReturnAssignedTasksByStatusActive()
        {
            //Arrange
            var commandParam = new string[] { "statusandassignee", "active", this.member.Name };
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            this.story.AddAssignee(member);
            this.bug.AddAssignee(member);
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(this.bug.Title));
        }

        [TestMethod]
        public void Execute_Should_ReturnAssignedTasksByStatusFixed()
        {
            //Arrange
            var commandParam = new string[] { "statusandassignee", "fixed" , this.member.Name};
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            this.story.AddAssignee(member);
            this.bug.AddAssignee(member);
            this.bug.ChangeStatus(TaskManagement.Models.Enums.BugStatus.Status.Fixed);
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(this.bug.Title));
        }

        [TestMethod]
        public void Execute_Should_ReturnAssignedTasksByStatusNotDone()
        {
            //Arrange
            var commandParam = new string[] { "statusandassignee", "notdone", this.member.Name };
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            this.story.AddAssignee(member);
            this.bug.AddAssignee(member);
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(this.story.Title));
        }

        [TestMethod]
        public void Execute_Should_ReturnAssignedTasksByStatusInProgress()
        {
            //Arrange
            var commandParam = new string[] { "statusandassignee", "inprogress", this.member.Name };
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            this.story.AddAssignee(member);
            this.bug.AddAssignee(member);
            this.story.Status = TaskManagement.Models.Enums.StoryStatus.Status.InProgress;
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(this.story.Title));
        }

        [TestMethod]
        public void Execute_Should_ReturnAssignedTasksByStatusDone()
        {
            //Arrange
            var commandParam = new string[] { "statusandassignee", "done", this.member.Name };
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            this.story.AddAssignee(member);
            this.bug.AddAssignee(member);
            this.story.Status = TaskManagement.Models.Enums.StoryStatus.Status.Done;
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(this.story.Title));
        }

        [TestMethod]
        public void Execute_Should_ReturnTasksByStatusActive()
        {
            //Arrange
            var commandParam = new string[] { "status", "active"};
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            this.story.AddAssignee(member);
            this.bug.AddAssignee(member);
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(this.bug.Title));
        }

        [TestMethod]
        public void Execute_Should_ReturnTasksByStatusFixed()
        {
            //Arrange
            var commandParam = new string[] { "status", "fixed"};
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            this.story.AddAssignee(member);
            this.bug.AddAssignee(member);
            this.bug.ChangeStatus(TaskManagement.Models.Enums.BugStatus.Status.Fixed);
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(this.bug.Title));
        }

        [TestMethod]
        public void Execute_Should_ReturnTasksByStatusNotDone()
        {
            //Arrange
            var commandParam = new string[] {"status", "notdone" };
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            this.story.AddAssignee(member);
            this.bug.AddAssignee(member);
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(this.story.Title));
        }

        [TestMethod]
        public void Execute_Should_ReturnTasksByStatusInProgress()
        {
            //Arrange
            var commandParam = new string[] { "status", "inprogress"};
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            this.story.AddAssignee(member);
            this.bug.AddAssignee(member);
            this.story.Status = TaskManagement.Models.Enums.StoryStatus.Status.InProgress;
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(this.story.Title));
        }

        [TestMethod]
        public void Execute_Should_ReturnTasksByStatusDone()
        {
            //Arrange
            var commandParam = new string[] { "status", "done" };
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            this.story.AddAssignee(member);
            this.bug.AddAssignee(member);
            this.story.Status = TaskManagement.Models.Enums.StoryStatus.Status.Done;
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(this.story.Title));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "StatusType validation failed!")]
        public void Execute_Should_ThrowException_When_StatusTypeNotValid()
        {
            //Arrange
            var commandParam = new string[] { "statusandassignee", "fakeStatus", member.Name };
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            this.story.AddAssignee(member);
            this.bug.AddAssignee(member);
            //Act & Assert
            command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
    "StatusType validation failed!")]
        public void Execute_Should_ThrowException_When_ParamCountWrong()
        {
            //Arrange
            var commandParam = new string[] { "status", "fakeStatus"};
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            this.story.AddAssignee(member);
            this.bug.AddAssignee(member);
            //Act & Assert
            command.Execute();
        }

        [TestMethod]
        public void Execute_Should_ReturnTasksByAssignee()
        {
            //Arrange
            var commandParam = new string[] { "assignee", this.member.Name };
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            this.story.AddAssignee(member);
            this.bug.AddAssignee(member);
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(this.story.Title));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "Argument validation failed when filtring by assignee!")]
        public void Execute_Should_ThrowException_WhenArgumentIsAssigneeAndDiffArgumentCountPassed()
        {
            //Arrange
            var commandParam = new string[] { "assignee", this.member.Name, "fakethirdArgument"};
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            //Act & Assert
            command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
    "Argument validation failed when filtring by assignee and status!")]
        public void Execute_Should_ThrowException_WhenArgumentIsAssigneeAndStatusAndDiffArgumentCountPassed()
        {
            //Arrange
            var commandParam = new string[] { "statusandassignee", "done", this.member.Name, "fakethirdArgument" };
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            //Act & Assert
            command.Execute();
        }

        [TestMethod]
        public void Execute_Should_ReturnNoAssigneeTaskMsg()
        {
            //Arrange
            var expected = "There is no assigned tasks for the moment!";
            var commandParam = new string[] { "status", "active"};
            var command = new FilterAssignedTasksBy(commandParam, this.repository);

            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(expected));
        }
    }
}
