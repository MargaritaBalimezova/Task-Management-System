using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Validations;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class CreateMemberTests
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
            var command = new CreateMemberCommand(commandParameters, this.repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_NameIsNotValid()
        {
            // Arrange
            string name = new string('x', Constants.MEMBER_NAME_MAX_LENGTH + 1);
            var commandParameters = new string[] { name }.ToList();
            var command = new CreateMemberCommand(commandParameters, this.repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_CreateNewMember_When_ValidParameters()
        {
            // Arrange
            string name = new string('x', Constants.MEMBER_NAME_MAX_LENGTH - 1);
            var commandParameters = new string[] { name }.ToList();
            var command = new CreateMemberCommand(commandParameters, this.repository);

            // Act
            command.Execute();

            // Assert
            Assert.IsTrue(this.repository.Members.Count > 0);
        }
    }
}