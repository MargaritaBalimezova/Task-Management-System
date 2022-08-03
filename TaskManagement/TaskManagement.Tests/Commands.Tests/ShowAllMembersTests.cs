using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Models;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class ShowAllMembersTests
    {
        private IRepository repository;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
        }

        [TestMethod]
        public void Execute_Should_ShowAllMembers_Ver1()
        {
            // Arrange
            var command = new ShowAllMembersCommand(repository);

            // Act, Assert
            Assert.AreEqual(command.Execute(), "There are no members.");
        }

        [TestMethod]
        public void Execute_Should_ShowAllMembers_Ver2()
        {
            // Arrange
            var member = this.repository.CreateMember("testMemberName");

            var command = new ShowAllMembersCommand(repository);

            // Act, Assert
            Assert.IsTrue(command.Execute().Contains("Number of all members: 1"));
        }
    }
}