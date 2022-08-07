using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Enums.FeedbackStatus;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class FilterFeedbacksByTests
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
        [DataRow(2)]
        public void Execute_Should_ThrowException_When_ArgumentsCountDifferentThanExpected(int testValue)
        {
            // Arrange
            var commandParameters = Helpers.GetDummyList(testValue - 1);
            var command = new FilterFeedbacksByCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_ParamsAreNotValid()
        {
            // Arrange
            var commandParameters = new string[] { "rating", "active" }.ToList();
            var command = new FilterFeedbacksByCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_FilterFeedback_When_ParamsAreValid()
        {
            // Arrange
            var feedback = this.repository.CreateFeedBack(Constants.Title, Constants.Description, 59);

            var commandParameters = new string[] { "status", "New" }.ToList();
            var command = new FilterFeedbacksByCommand(commandParameters, repository);

            // Act & Assert
            Assert.IsTrue(command.Execute().Contains("--FEEDBACK--"));
        }
    }
}