using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class CreateFeedbackTests
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
        [DataRow(3)]
        public void Execute_Should_ThrowException_When_ArgumentsCountDifferentThanExpected(int testValue)
        {
            // Arrange
            var commandParameters = Helpers.GetDummyList(testValue - 1);
            var command = new CreateFeedbackCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_CreateNewFeedback_When_ValidParameters()
        {
            // Arrange
            var taskFeedback = this.repository.CreateFeedBack("TestTile123", "TestDescription", 58);

            var commandParameters = new string[] { "TestTile123", "TestDescription", "58" }.ToList();
            var command = new CreateFeedbackCommand(commandParameters, repository);

            // Act
            command.Execute();

            // Assert
            Assert.IsTrue(repository.Feedbacks.Count > 0);
        }
    }
}