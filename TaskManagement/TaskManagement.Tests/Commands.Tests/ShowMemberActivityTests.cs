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
    public class ShowMemberActivityTests
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
            var command = new ShowMemberActivityCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ShowMemberAcitity_When_ValidParameters()
        {
            // Arrange
            var member = this.repository.CreateMember("testMemberName");
            var commandParameters = new string[] { "testMemberName" }.ToList();
            var command = new ShowMemberActivityCommand(commandParameters, repository);

            // Act, Assert
            Assert.IsTrue(command.Execute().Contains("Successfuly created Member with name testMemberName!"));
        }
    }
}