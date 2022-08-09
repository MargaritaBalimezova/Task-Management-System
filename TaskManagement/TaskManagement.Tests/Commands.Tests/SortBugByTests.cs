using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class SortBugByTests
    {
        private IRepository repository;
        private ICommandFactory commandFactory;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);
            StreamReader sr = new StreamReader(Constants.fullPath);
            Console.SetIn(sr);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void Execute_ShouldThrow_When_IncorrectCountOfParrammetersArePassed()
        {
            ICommand sortBugBy = this.commandFactory.Create("SortBugBy");

            sortBugBy.Execute();
        }

        [TestMethod]
        public void Execute_Should_SortBugsByTitle()
        {
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");            
            ICommand createBug2 = this.commandFactory.Create($"Createbug {Constants.BugTitle2} {Constants.Description} Low Major");           
            ICommand sortBugByTitle = this.commandFactory.Create("SortBugby title");           

            createBug.Execute();
            StreamReader sr = new StreamReader(Constants.fullPath);
            Console.SetIn(sr);
            createBug2.Execute();
            sortBugByTitle.Execute();

            IEnumerable<IBug> bugs = this.repository.Bugs.OrderBy(x => x.Title);
            StringBuilder sb = new StringBuilder();
            foreach (var item in bugs)
            {
                sb.AppendLine(item.ToString());
            }

            sortBugByTitle.Execute();

            Assert.IsTrue(sortBugByTitle.Execute().Contains(sb.ToString()));
        }

        [TestMethod]
        public void Execute_Should_SortBugsByPriority()
        {
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");
            ICommand createBug2 = this.commandFactory.Create($"Createbug {Constants.BugTitle2} {Constants.Description} Low Major");
            ICommand sortBugByPriority = this.commandFactory.Create("SortBugby priority");

            createBug.Execute();
            StreamReader sr = new StreamReader(Constants.fullPath);
            Console.SetIn(sr);
            createBug2.Execute();
            sortBugByPriority.Execute();

            IEnumerable<IBug> bugs = this.repository.Bugs.OrderByDescending(x => x.Priority);
            StringBuilder sb = new StringBuilder();
            foreach (var item in bugs)
            {
                sb.AppendLine(item.ToString());
            }

            sortBugByPriority.Execute();

            Assert.IsTrue(sortBugByPriority.Execute().Contains(sb.ToString()));
        }

        [TestMethod]
        public void Execute_Should_SortBugsBySeverity()
        {
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");
            ICommand createBug2 = this.commandFactory.Create($"Createbug {Constants.BugTitle2} {Constants.Description} Low Major");
            ICommand sortBugBySeverity = this.commandFactory.Create("SortBugby severity");

            createBug.Execute();
            StreamReader sr = new StreamReader(Constants.fullPath);
            Console.SetIn(sr);
            createBug2.Execute();
            sortBugBySeverity.Execute();

            IEnumerable<IBug> bugs = this.repository.Bugs.OrderBy(x => x.Severity);
            StringBuilder sb = new StringBuilder();
            foreach (var item in bugs)
            {
                sb.AppendLine(item.ToString());
            }

            sortBugBySeverity.Execute();

            Assert.IsTrue(sortBugBySeverity.Execute().Contains(sb.ToString()));
        }


    }
}
