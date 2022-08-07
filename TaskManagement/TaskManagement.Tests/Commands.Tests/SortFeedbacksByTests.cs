using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Tests.Commands.Tests.Common;


namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class SortFeedbacksByTests
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
        [DataRow(1)]
        public void Execute_Should_ThrowException_When_ArgumentsCountDifferentThanExpected(int testValue)
        {
            // Arrange
            var commandParameters = Helpers.GetDummyList(testValue - 1);
            var command = new SortFeedbacksByCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_ParamNotValid()
        {
            // Arrange
            var commandParameters = new string[] { "size" }.ToList();
            var command = new SortFeedbacksByCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_SortFeedback_When_ParamValid_Ver1()
        {
            // Arrange
            var feedback = this.repository.CreateFeedBack( Constants.Title, Constants.Description, 59);

            var commandParameters = new string[] { "title" }.ToList();
            var command = new SortFeedbacksByCommand(commandParameters, repository);

            // Act & Assert
            Assert.IsTrue(command.Execute().Contains($"--FEEDBACK--"));
        }

        [TestMethod]
        public void Execute_Should_SortFeedback_When_ParamValid_Ver2()
        {
            // Arrange
            var feedback = this.repository.CreateFeedBack(Constants.Title, Constants.Description, 59);

            var commandParameters = new string[] { "rating" }.ToList();
            var command = new SortFeedbacksByCommand(commandParameters, repository);

            // Act & Assert
            Assert.IsTrue(command.Execute().Contains($"--FEEDBACK--")); 
        }
    }
}