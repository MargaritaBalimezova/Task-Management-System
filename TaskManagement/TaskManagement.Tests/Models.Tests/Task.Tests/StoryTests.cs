using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Models;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.StoryStatus;
using TaskManagement.Models.Tasks;

namespace TaskManagement.Tests.Models.Tests.Task.Tests
{
    [TestClass]
    public class StoryTests
    {
        private const int MEMBER_NAME_MIN_LENGTH = 5;
        private const int MEMBER_NAME_MAX_LENGTH = 15;
        private const int TaskTitleMinLen = 10;
        private const int TaskTitleMaxLen = 50;
        private const int TaskDescriptionMinLen = 10;
        private const int TaskDescriptionMaxLen = 500;

        [TestMethod]
        public void Constructor_Should_CreateStory_When_ParamsValid()
        {
            //Arrange
            string title = new string('x', TaskTitleMinLen + 1);
            string description = new string('x', TaskDescriptionMaxLen - 1);
            var assignee = new Member(new string('x', MEMBER_NAME_MAX_LENGTH - 1));
            int id = 1;
            //Act
            var story = new Story(title, description, id: id, PriorityType.High,
                SizeType.Medium, assignee);
            //Assert
            Assert.AreEqual(title, story.Title, "Story constructor failed!");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Base class validation failed!")]
        public void Constuctor_Should_ThrowException_When_DescriptionInvalid()
        {
            //Arrange
            string title = new string('x', TaskTitleMinLen + 1);
            string description = new string('x', TaskDescriptionMaxLen + 1);
            var assignee = new Member(new string('x', MEMBER_NAME_MAX_LENGTH - 1));
            int id = 1;

            //Act && Assert
            var story = new Story(title, description, id: id, PriorityType.High,
                SizeType.Medium, assignee);
        }

        [TestMethod]
        public void PriorityGetter_Should_ReturnValidData()
        {
            //Arrange
            string title = new string('x', TaskTitleMinLen + 1);
            string description = new string('x', TaskDescriptionMaxLen - 1);
            var assignee = new Member(new string('x', MEMBER_NAME_MAX_LENGTH - 1));
            var priority = PriorityType.High;
            int id = 1;
            //Act
            var story = new Story(title, description, id: id, priority,
                SizeType.Medium, assignee);
            //Assert
            Assert.AreEqual(priority, story.Priority, "Priority getter failed to return valid data!");
        }


        [TestMethod]
        public void SizeGetter_Should_ReturnValidData()
        {
            //Arrange
            string title = new string('x', TaskTitleMinLen + 1);
            string description = new string('x', TaskDescriptionMaxLen - 1);
            var assignee = new Member(new string('x', MEMBER_NAME_MAX_LENGTH - 1));
            var priority = PriorityType.High;
            var size = SizeType.Medium;
            int id = 1;
            //Act
            var story = new Story(title, description, id: id, priority,
                size, assignee);
            //Assert
            Assert.AreEqual(size, story.Size, "Size getter failed to return valid data!"); ;
        }

        [TestMethod]
        public void Assignee_Should_ReturnValidPrintableData()
        {
            //Arrange
            string title = new string('x', TaskTitleMinLen + 1);
            string description = new string('x', TaskDescriptionMaxLen - 1);
            var assignee = new Member(new string('x', MEMBER_NAME_MAX_LENGTH - 1));
            int id = 1;

            //Act
            var story = new Story(title, description, id: id, PriorityType.High,
                SizeType.Medium, assignee);

            //Assert
            Assert.AreEqual(assignee, story.Assignee, "Assignee failed to be initialized!");
        }

        [TestMethod]
        public void StatusGetter_Should_ReturnValidStatus()
        {
            //Arrange
            string title = new string('x', TaskTitleMinLen + 1);
            string description = new string('x', TaskDescriptionMaxLen - 1);
            var assignee = new Member(new string('x', MEMBER_NAME_MAX_LENGTH - 1));

            //Act
            var story = new Story(title, description, id: 1, PriorityType.High,
                SizeType.Medium, assignee);
            //Assert
            Assert.AreEqual(Status.NotDone, story.Status, "Story status getter failed!");
        }

        [TestMethod]
        public void AddComment_Should_AddCommentToList()
        {
            //Arrange
            string title = new string('x', TaskTitleMinLen + 1);
            string description = new string('x', TaskDescriptionMaxLen - 1);
            var assignee = new Member(new string('x', MEMBER_NAME_MAX_LENGTH - 1));
            var story = new Story(title, description, 1, PriorityType.High,
                SizeType.Medium, assignee);

            //Act
            story.AddComment(new Comment("Comment", assignee.Name));

            //Assert
            Assert.AreEqual(1, story.Comments.Count, "Could not add comment!");
        }

        [TestMethod]
        public void RemoveComment_Should_RemoveCommentFromList()
        {
            //Arrange
            string title = new string('x', TaskTitleMinLen + 1);
            string description = new string('x', TaskDescriptionMaxLen - 1);
            var assignee = new Member(new string('x', MEMBER_NAME_MAX_LENGTH - 1));

            var comment = new Comment("Comment", assignee.Name);

            var story = new Story(title, description, 1, PriorityType.High,
                SizeType.Medium, assignee);

            story.AddComment(comment);

            //Act
            story.RemoveComment(comment);

            //Assert
            Assert.AreEqual(0, story.Comments.Count, "Remove comment failed!");
        }

        [TestMethod]
        public void ActivityLog_Should_LogEveryChange()
        {
            //Arrange
            string title = new string('x', TaskTitleMinLen + 1);
            string description = new string('x', TaskDescriptionMaxLen - 1);
            var assignee = new Member(new string('x', MEMBER_NAME_MAX_LENGTH - 1));

            var comment = new Comment("Comment", assignee.Name);

            //Act
            var story = new Story(title, description, 1, PriorityType.High,
                SizeType.Medium, assignee);

            //Assert
            Assert.AreEqual(1, story.ActivityLog.Count, "Remove comment failed!");
        }

        [TestMethod]
        public void ChangeStatus_Should_ChangeStatus_When_NewStatusPassed()
        {
            string title = new string('x', TaskTitleMinLen + 1);
            string description = new string('x', TaskDescriptionMaxLen - 1);
            var assignee = new Member(new string('x', MEMBER_NAME_MAX_LENGTH - 1));
            int id = 1;
            var status = Status.InProgress;
            var story = new Story(title, description, id: id, PriorityType.High,
                SizeType.Medium, assignee);
            //Act
            story.ChangeStatus(status);
            //Assert
            Assert.AreEqual(status, story.Status, "ChangeStatus failed in Story class!");
        }

        [TestMethod]
        public void ChangeStatus_ShouldNot_ChangeStatus_When_SameStatusPassed()
        {
            string title = new string('x', TaskTitleMinLen + 1);
            string description = new string('x', TaskDescriptionMaxLen - 1);
            var assignee = new Member(new string('x', MEMBER_NAME_MAX_LENGTH - 1));
            int id = 1;
            var status = Status.NotDone;
            var story = new Story(title, description, id: id, PriorityType.High,
                SizeType.Medium, assignee);

            var expected = $"Status of Story with ID {story.Id} {story.Title} is already at {story.Status}.";
            
            //Act

            story.ChangeStatus(status);
            
            //Assert
            Assert.AreEqual(expected, story.ActivityLog[story.ActivityLog.Count - 1].Description , "ChangeStatus failed in Story class!");
        }
    }
}