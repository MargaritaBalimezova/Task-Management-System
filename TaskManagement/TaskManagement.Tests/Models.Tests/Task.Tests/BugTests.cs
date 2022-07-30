using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.BugStatus;
using TaskManagement.Models.Tasks;

namespace TaskManagement.Tests.Models.Tests.Task.Tests
{
    [TestClass]
    public class BugTests
    {
        private const int TitleMinLen = 10;
        private const int TitleMaxLen = 50;
        private const int DescriptionMinLen = 10;
        private const int DescriptionMaxLen = 500;

        static IMember assignee;
        static IList<string> stepsToReproduce;
        static string title;
        static string description;
        static int id;
        static Bug bug;

        [ClassInitialize()]
        public static void BugTests_ClassInitialize(TestContext context)
        {
            assignee = new Member("Test Member");
            stepsToReproduce = new List<string>();
            title = "Test Title";
            description = "Test Description";
            id = 1;

            bug = new Bug(title, description, id, PriorityType.Medium, Severity.Major, assignee, stepsToReproduce);
        }

        [TestMethod]
        public void Constuctor_Should_CreateNewBug_When_CorrectValuesPassed()
        {
            Assert.AreEqual("Test Title", bug.Title);
            Assert.AreEqual("Test Description", bug.Description);
            Assert.AreEqual("Test Member", bug.Assignee.Name);
            Assert.AreEqual(PriorityType.Medium, bug.Priority);
            Assert.AreEqual(Severity.Major, bug.Severity);
            Assert.AreEqual("Test Member", assignee.Name,"Bug created successfully!");
        }

        [TestMethod]
        public void Consctor_Should_SetActiveStatusAsDefault_When_CreateBug()
        {
            Assert.AreEqual(Status.Active, bug.Status,"Active status is set as default!");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"Title length is too long!")]
        public void ConstructorShould_Throw_When_TitleLargerThanMax()
        {
            var testTitle = new string('a', TitleMaxLen + 1);
            bug = new Bug(testTitle, description, id, PriorityType.Medium, Severity.Major, assignee, stepsToReproduce);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Title length is too short!")]
        public void ConstructorShould_Throw_When_TitleShorterThanMin()
        {
            var testTitle = new string('a', TitleMinLen - 1);
            bug = new Bug(testTitle, description, id, PriorityType.Medium, Severity.Major, assignee, stepsToReproduce);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"Description length is too long!")]
        public void ConstructorShould_Throw_When_DescriptionLargerThanMax()
        {
            var testDesc = new string('a', DescriptionMaxLen + 1);
            bug = new Bug(title, testDesc, id, PriorityType.Medium, Severity.Major, assignee, stepsToReproduce);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"Description length is too short!")]
        public void ConstructorShould_Throw_When_DescriptionShorterThanMin()
        {
            var testDesc = new string('a', DescriptionMinLen - 1);
            bug = new Bug(title, testDesc, id, PriorityType.Medium, Severity.Major, assignee, stepsToReproduce);
        }

        [TestMethod]
        public void Should_ReturnCopyOfStepsToReproduce()
        {
            bug.StepsToReproduce.Add("1.Test step");
            Assert.AreEqual(0, stepsToReproduce.Count,"Copy of a list returned successfully!");
        }

        [TestMethod]
        public void ChangeStatusMethod_Should_ChangeStatus_When_StatusIsPassed()
        {
            bug.ChangeStatus(Status.Fixed);
            Assert.AreEqual(Status.Fixed, bug.Status,"Status changed successfully!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),"You can not change to the same status that is already assigned!")]
        public void ChangeStatusMethod_ShouldNot_ChangeStatus_When_StatusIsTheSame()
        {
            bug.ChangeStatus(Status.Fixed);
        }

        [TestMethod]
        public void EventLog_Should_AddEveryEventHappened()
        {
            bug.ChangeStatus(Status.Active);
            Assert.AreEqual(3, bug.ActivityLog.Count,"All of the events are savet successfully!");
        }


    }
}
