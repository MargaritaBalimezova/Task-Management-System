using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class SortTasksByTitleTests
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
        public void Execute_Should_ReturnSortedTasksByTitle()
        {
            //Arrange
            var story1 = this.repository.CreateStory(Constants.Title, Constants.Description, Constants.priorityHigh, Constants.sizeLarge);
            var story2 = this.repository.CreateStory(Constants.Title2, Constants.Description, Constants.priorityMedium, Constants.sizeMedium);

            var command = new SortTasksByTitle(this.repository);

            var sb = new StringBuilder();
            sb.AppendLine(story1.ToString());
            sb.AppendLine("####################");
            sb.AppendLine(story2.ToString());
            sb.AppendLine("####################");
            //Act && Assert
            Assert.IsTrue(command.Execute().Contains(sb.ToString()));
        }
    }
}
