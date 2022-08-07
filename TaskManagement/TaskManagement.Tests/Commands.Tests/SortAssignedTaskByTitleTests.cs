using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Commands.Contracts;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{

    [TestClass]
    public class SortAssignedTaskByTitleTests
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
        public void Execute_Should_ReturnSortedAssignedTasksByTitle()
        {
            ICommand createBug = this.commandFactory.Create($"Createbug {Constants.BugTitle} {Constants.Description} High Major");
            ICommand createStory = this.commandFactory.Create($"Createstory {Constants.Title} {Constants.Description} high large");
            ICommand createTeam = this.commandFactory.Create($"Createteam {Constants.TeamName}");
            ICommand createMember = this.commandFactory.Create($"Createmember {Constants.MemberName}");
            ICommand addMemberToTeam = this.commandFactory.Create($"Addmembertoteam {Constants.TeamName} {Constants.MemberName}");
            ICommand assignTask1 = this.commandFactory.Create($"assigntask 1 {Constants.MemberName} {Constants.TeamName}");
            ICommand assignTask2 = this.commandFactory.Create($"assigntask 2 {Constants.MemberName} {Constants.TeamName}");
            ICommand sortAssignedTasksByTitle = this.commandFactory.Create("SortAssignedTasksBytitle");

            createBug.Execute();
            createStory.Execute();
            createTeam.Execute();
            createMember.Execute();
            addMemberToTeam.Execute();
            assignTask1.Execute();
            assignTask2.Execute();
            sortAssignedTasksByTitle.Execute();

            List<ITask> tasks = this.repository.Bugs.Cast<ITask>().Concat(this.repository.Stories.Cast<ITask>()).OrderBy(t => t.Title).ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var item in tasks)
            {
                sb.AppendLine(item.ToString());
            }


            //Act
            Assert.IsTrue(sortAssignedTasksByTitle.Execute().Contains(sb.ToString()));
        }

        [TestMethod]
        public void Execute_Should_ReturnSpecialMessageIfNoAssignedTasks()
        {
            ICommand sortAssignedTasksByTitle = this.commandFactory.Create("SortAssignedTasksBytitle");
            sortAssignedTasksByTitle.Execute();

            Assert.IsTrue(sortAssignedTasksByTitle.Execute().Contains("There is no assigned tasks for the moment!"));
        }
    }
}
