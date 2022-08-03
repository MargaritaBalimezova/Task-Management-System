using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class CreateMemberTests
    {
        private const int MEMBER_NAME_MIN_LENGTH = 5;
        private const int MEMBER_NAME_MAX_LENGTH = 15;

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
            var command = new CreateMemberCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_NameIsNotValid()
        {
            // Arrange
            string name = new string('x', MEMBER_NAME_MAX_LENGTH + 1);
            var commandParameters = new string[] { name }.ToList();
            var command = new CreateMemberCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_CreateNewMember_When_ValidParameters()
        {
            // Arrange
            string name = new string('x', MEMBER_NAME_MAX_LENGTH - 1);
            var commandParameters = new string[] { name }.ToList();
            var command = new CreateMemberCommand(commandParameters, repository);

            // Act
            command.Execute();

            // Assert
            Assert.IsTrue(repository.Members.Count > 0);
        }
    }
}