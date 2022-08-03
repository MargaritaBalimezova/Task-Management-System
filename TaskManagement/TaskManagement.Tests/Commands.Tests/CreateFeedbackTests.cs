﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;

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
        [DataRow(5)]
        public void Execute_Should_ThrowException_When_ArgumentsCountDifferentThanExpected(int testValue)
        {
            // Arrange
            var commandParameters = Helpers.GetDummyList(testValue - 1);
            var command = new CreateFeedbackCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_CreateNewMember_When_ValidParameters()
        {
            // Arrange
            var taskFeedback = this.repository.CreateFeedBack("TestTile123", "TestDescription", 58);

            var team = this.repository.CreateTeam("testTeam");

            var board = this.repository.CreateBoard("testBoard");

            var commandParameters = new string[] { "TestTile123", "TestDescription", "58", "testBoard", "testTeam" }.ToList();
            var command = new CreateMemberCommand(commandParameters, repository);

            // Act
            command.Execute();

            // Assert
            Assert.IsTrue(repository.Feedbacks.Count > 0);
        }
    }
}