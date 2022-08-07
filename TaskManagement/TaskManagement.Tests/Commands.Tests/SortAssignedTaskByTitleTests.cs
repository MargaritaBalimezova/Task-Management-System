using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{

    [TestClass]
    public class SortAssignedTaskByTitleTests
    {
        private IRepository repository;
        private ICommandFactory commandFactory;

        private IStory story;
        private IBug bug;
        private IMember member;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);

            this.story = this.repository.CreateStory(Constants.Title, Constants.Description, Constants.priority, Constants.size);
            this.bug = this.repository.CreateBug(Constants.Title, Constants.Description, Constants.priority, Severity.Critical, new List<string>());
        }

        [TestMethod]
        public void Execute_Should_ReturnSortedAssignedTasksByTitle()
        {
            //Arrange
            var command = new SortAssignedTasksByTitle(this.repository);

            this.member = this.repository.CreateMember(Constants.MemberName);
            this.story.AddAssignee(member);
            this.bug.AddAssignee(member);

            var sb = new StringBuilder();
            sb.AppendLine(bug.ToString());
            sb.AppendLine(story.ToString());

            //Act
            Assert.IsTrue(command.Execute().Contains(sb.ToString()));
        }

     /*   [TestMethod]
        public void Execute_Should_ReturnNoAssignedTaskMsg_When_ThereIsNotAssignedTasks()
        {
            //Arrange
            var command = new SortAssignedTasksByTitle(this.repository);
            var expected = "There is no assigned tasks for the moment!";

            //Act
            Assert.IsTrue(command.Execute().Contains(expected));
        }*/
    }
}
