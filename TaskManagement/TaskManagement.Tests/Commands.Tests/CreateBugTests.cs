using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Commands.Contracts;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Tasks;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class CreateBugTests
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
        public void Execute_Should_CreateNewBug_When_CorrectValuesArePassed()
        {
           
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");                        
            // Act
            createBug.Execute();
            
            //Assert
            IBug bug = this.repository.Bugs.FirstOrDefault(b => b.Id == 1); 
            Assert.AreEqual(Constants.BugTitle, bug.Title);
            Assert.AreEqual(Constants.Description, bug.Description);
            Assert.AreEqual(PriorityType.High, bug.Priority);
            Assert.AreEqual(Severity.Major, bug.Severity);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void Execute_ShouldThrow_When_IncorrectCountOfParrammetersArePassed()
        {
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} Major");

            createBug.Execute();
        }
    }
}
