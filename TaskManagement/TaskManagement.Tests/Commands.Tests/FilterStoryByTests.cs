using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums.StoryStatus;
using TaskManagement.Models.Tasks;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class FilterStoryByTests
    {
        private IRepository repository;
        private ICommandFactory commandFactory;

        private IMember member1;
        private IMember member2;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);

            this.member1 = this.repository.CreateMember(Constants.MemberName);
            this.member2 = this.repository.CreateMember(Constants.MemberName2);
        }

        [TestMethod]
        public void Execute_Should_FilterStoryByStatus_When_StatusPassed()
        {
            //Arrange
            var story = this.repository.CreateStory(Constants.Title, Constants.Description,
                            Constants.priorityHigh, Constants.sizeLarge);
            var story2 = (Story)this.repository.CreateStory(Constants.Title2, Constants.Description,
                            Constants.priorityMedium, Constants.sizeMedium);

            var commandParameters = new string[] { "status",  story.Status.ToString() };
            var command = new FilterStoryBy(commandParameters, this.repository);
            story2.Status = Status.Done;
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(Constants.Title));
            Assert.IsFalse(command.Execute().Contains(Constants.Title2));
        }

        [TestMethod]
        public void Execute_Should_FilterStoryByAssignee_When_AssigneePassed()
        {
            //Arrange
            var story1 = this.repository.CreateStory(Constants.Title, Constants.Description,
                            Constants.priorityHigh, Constants.sizeLarge);
            var story2 = this.repository.CreateStory(Constants.Title2, Constants.Description,
                            Constants.priorityMedium, Constants.sizeMedium);

            story1.AddAssignee(member1);
            story2.AddAssignee(member2);

            var commandParameters = new string[] { "Assignee", member1.Name };
            var command = new FilterStoryBy(commandParameters, this.repository);
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(member1.Name));
            Assert.IsFalse(command.Execute().Contains(member2.Name));
        }

        [TestMethod]
        public void Execute_Should_FilterStoryByStatusAndAssignee_When_StatusAndAssigneePassed()
        {
            //Arrange
            var story1 = this.repository.CreateStory(Constants.Title, Constants.Description,
                            Constants.priorityHigh, Constants.sizeLarge);
            var story2 = this.repository.CreateStory(Constants.Title2, Constants.Description,
                            Constants.priorityMedium, Constants.sizeMedium);

            story1.AddAssignee(member1);
            story2.AddAssignee(member2);

            var commandParameters = new string[] { "StatusAndAssignee", story1.Status.ToString(), member1.Name };
            var command = new FilterStoryBy(commandParameters, this.repository);
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(member1.Name));
            Assert.IsFalse(command.Execute().Contains(member2.Name));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "FilterStoryBy argument validation failed!")]
        public void Execute_Should_ThrowException_When_InvalidParamPassed()
        {
            //Arrange
            var commandParameters = new string[] { "FakeParam", Constants.priorityHigh.ToString() };
            var command = new FilterStoryBy(commandParameters, this.repository);
            //Act & Assert
            command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void Execute_ShouldThrow_When_IncorrectCountOfParrammetersArePassed_V1()
        {
            var commandParams = new string[] {"Status"};
            var command = new FilterStoryBy(commandParams, this.repository);

            command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void Execute_ShouldThrow_When_IncorrectCountOfParrammetersArePassed_V2()
        {
            var commandParams = new string[] {"statusandassignee" };
            var command = new FilterStoryBy(commandParams, this.repository);

            command.Execute();
        }
    }
}
