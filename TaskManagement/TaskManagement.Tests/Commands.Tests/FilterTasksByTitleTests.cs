using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class FilterTasksByTitleTests
    {
        private const int ExpectedParamsCount = 1;

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
            this.repository.CreateFeedBack(Constants.Title, Constants.Description, 10);
            var command = new FilterTasksByTitle(new string[] { "Dummy" }, this.repository);

            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(Constants.Title));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "FilterTasksByTitle argument validation count failed!")]
        public void Execute_Should_ThrowException_When_DiffArgumentCountPassed()
        {
            //Arrange
            var command = new FilterTasksByTitle(Helpers.GetDummyList(ExpectedParamsCount + 1), this.repository);

            //Act & Assert
            command.Execute();
        }
    }
}
