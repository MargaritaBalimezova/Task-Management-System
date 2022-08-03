﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Tasks;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class ChangeFeedbackTests
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
            var command = new ChangeFeedbackCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_ParamToChangeIsNotValid()
        {
            // Arrange
            ITask taskFeedback = (ITask)this.repository.CreateFeedBack("TestTile123", "TestDescription", 58);
            var commandParameters = new string[] { "1", "sth", "Unscheduled" }.ToList();
            var command = new ChangeFeedbackCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ChangeFeedbackStatus()
        {
            // Arrange
            ITask taskFeedback = (ITask)this.repository.CreateFeedBack("TestTile123", "TestDescription", 58);
            var commandParameters = new string[] { "1", "status", "Unscheduled" }.ToList();

            //Act
            var command = new ChangeFeedbackCommand(commandParameters, repository);

            //Assert
            Assert.AreEqual(command.Execute(), "Status of task with 1 was changed.");
        }

        [TestMethod]
        public void Execute_Should_ChangeFeedbackRating()
        {
            // Arrange
            ITask taskFeedback = (ITask)this.repository.CreateFeedBack("TestTile123", "TestDescription", 58);
            var commandParameters = new string[] { "1", "rating", "56" }.ToList();

            //Act
            var command = new ChangeFeedbackCommand(commandParameters, repository);

            //Assert
            Assert.AreEqual(command.Execute(), "Rating of task with 1 was changed.");
        }
    }
}