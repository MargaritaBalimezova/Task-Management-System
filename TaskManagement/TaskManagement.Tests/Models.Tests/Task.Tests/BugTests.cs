using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.BugStatus;
using TaskManagement.Models.Tasks;
using TaskManagement.Validations;

namespace TaskManagement.Tests.Models.Tests.Task.Tests
{
    [TestClass]
    public class BugTests
    {
        private static IMember assignee;
        private static IList<string> stepsToReproduce;
        private static string title;
        private static string description;
        private static int id;
        private static Bug bug;

        [ClassInitialize()]
        public static void BugTests_ClassInitialize(TestContext context)
        {
            assignee = new Member("Test Member");
            stepsToReproduce = new List<string>();
            title = "Test Title";
            description = "Test Description";
            id = 1;

            bug = new Bug(title, description, id, PriorityType.Medium, Severity.Major, stepsToReproduce);
        }

        [TestMethod]
        public void Constuctor_Should_CreateNewBug_When_CorrectValuesPassed()
        {
            Assert.AreEqual("Test Title", bug.Title);
            Assert.AreEqual("Test Description", bug.Description);
            Assert.AreEqual(PriorityType.Medium, bug.Priority);
            Assert.AreEqual(Severity.Major, bug.Severity);
            Assert.AreEqual("Test Member", assignee.Name, "Bug created successfully!");
        }

        [TestCleanup()]
        public void BugTests_Clean()
        {
            bug = new Bug(title, description, id, PriorityType.Medium, Severity.Major, stepsToReproduce);
        }

        [TestMethod]
        public void Consctor_Should_SetActiveStatusAsDefault_When_CreateBug()
        {
            Assert.AreEqual(Status.Active, bug.Status, "Active status is set as default!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException), "Title length is too long!")]
        public void ConstructorShould_Throw_When_TitleLargerThanMax()
        {
            var testTitle = new string('a', Constants.TITLE_MAX_LEN + 1);
            bug = new Bug(testTitle, description, id, PriorityType.Medium, Severity.Major, stepsToReproduce);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException), "Title length is too short!")]
        public void ConstructorShould_Throw_When_TitleShorterThanMin()
        {
            var testTitle = new string('a', Constants.TITLE_MIN_LEN - 1);
            bug = new Bug(testTitle, description, id, PriorityType.Medium, Severity.Major, stepsToReproduce);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException), "Description length is too long!")]
        public void ConstructorShould_Throw_When_DescriptionLargerThanMax()
        {
            var testDesc = new string('a', Constants.DESCRIPTION_MAX_LEN + 1);
            bug = new Bug(title, testDesc, id, PriorityType.Medium, Severity.Major, stepsToReproduce);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException), "Description length is too short!")]
        public void ConstructorShould_Throw_When_DescriptionShorterThanMin()
        {
            var testDesc = new string('a', Constants.DESCRIPTION_MIN_LEN - 1);
            bug = new Bug(title, testDesc, id, PriorityType.Medium, Severity.Major, stepsToReproduce);
        }

        [TestMethod]
        public void Should_ReturnCopyOfStepsToReproduce()
        {
            bug.StepsToReproduce.Add("1.Test step");
            Assert.AreEqual(0, stepsToReproduce.Count, "Copy of a list returned successfully!");
        }

        [TestMethod]
        public void ChangeStatusMethod_Should_ChangeStatus_When_StatusIsPassed()
        {
            bug.ChangeStatus(Status.Fixed);
            Assert.AreEqual(Status.Fixed, bug.Status, "Status changed successfully!");
            BugTests_Clean();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "You can not change to the same status that is already assigned!")]
        public void ChangeStatusMethod_ShouldThrow_When_SameStatusIsPassed()
        {
            bug.ChangeStatus(Status.Active);
        }

        [TestMethod]
        public void EventLog_Should_AddEveryEventHappened()
        {
            bug.ChangeStatus(Status.Fixed);
            Assert.AreEqual(2, bug.ActivityLog.Count, "All of the events are savet successfully!");
        }

        [TestMethod]
        public void ConstructorShould_AddComment()
        {
            var comment = new Comment("Test content", "Test Author");

            bug.AddComment(comment);
            Assert.AreEqual(1, bug.Comments.Count, "Comment added successfully!");
        }

        [TestMethod]
        public void ConstructorShould_RemoveComment()
        {
            var comment = new Comment("Test content", "Test Author");

            bug.RemoveComment(comment);
            Assert.AreEqual(0, bug.Comments.Count, "Comment removed successfully!");
        }
    }
}