using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.StoryStatus;
using TaskManagement.Models.Tasks;
using TaskManagement.Validations;

namespace TaskManagement.Tests.Models.Tests.Task.Tests
{
    [TestClass]
    public class StoryTests
    {
        [TestMethod]
        public void Constructor_Should_CreateStory_When_ParamsValid()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            //Act
            var story = new Story(title, description, id: id, PriorityType.High,
                SizeType.Medium);
            //Assert
            Assert.AreEqual(title, story.Title, "Story constructor failed!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException), "Base class validation failed!")]
        public void Constuctor_Should_ThrowException_When_DescriptionInvalid()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN + 1);
            int id = 1;

            //Act && Assert
            new Story(title, description, id: id, PriorityType.High,
                SizeType.Medium);
        }

        [TestMethod]
        public void PriorityGetter_Should_ReturnValidData()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            var priority = PriorityType.High;
            int id = 1;
            //Act
            var story = new Story(title, description, id: id, priority,
                SizeType.Medium);
            //Assert
            Assert.AreEqual(priority, story.Priority, "Priority getter failed to return valid data!");
        }

        [TestMethod]
        public void PrioritySetter_Should_ChangePriority()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            var priority = PriorityType.High;
            int id = 1;
            var story = new Story(title, description, id: id, priority,
                SizeType.Medium);
            //Act
            story.Priority = PriorityType.Low;

            //Assert
            Assert.AreEqual(PriorityType.Low, story.Priority, "Priority setter failed to assign valid data!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PrioritySetter_ShouldThrow_When_SamePriorityPassed()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            var priority = PriorityType.High;
            int id = 1;
            var story = new Story(title, description, id: id, priority,
                SizeType.Medium);
            //Act
            story.Priority = PriorityType.High;

            //Assert
            Assert.AreEqual(PriorityType.High, story.Priority, "Priority setter failed to assign valid data!");
        }

        [TestMethod]
        public void SizeGetter_Should_ReturnValidData()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            var priority = PriorityType.High;
            var size = SizeType.Medium;
            int id = 1;
            //Act
            var story = new Story(title, description, id: id, priority,
                size);
            //Assert
            Assert.AreEqual(size, story.Size, "Size getter failed to return valid data!");
        }

        [TestMethod]
        public void SizeSetter_Should_ChangeSize()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            var priority = PriorityType.High;
            var size = SizeType.Medium;
            int id = 1;
            var story = new Story(title, description, id: id, priority,
                size);
            //Act
            story.Size = SizeType.Small;
            //Assert
            Assert.AreEqual(SizeType.Small, story.Size, "Size getter failed to return valid data!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SizeSetter_ShouldThrow_When_SameValuePassed()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            var priority = PriorityType.High;
            var size = SizeType.Medium;
            int id = 1;
            var story = new Story(title, description, id: id, priority,
                size);
            //Act
            story.Size = SizeType.Medium;
            //Assert
            Assert.AreEqual(SizeType.Medium, story.Size, "Size getter failed to return valid data!");
        }

        [TestMethod]
        public void Assignee_Should_ReturnValidPrintableData()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            var assignee = new Member(new string('x', Constants.MEMBER_NAME_MAX_LENGTH - 1));
            int id = 1;
            var story = new Story(title, description, id: id, PriorityType.High,
                SizeType.Medium);

            //Act
            story.AddAssignee(assignee);

            //Assert
            Assert.AreEqual(assignee, story.Assignee, "Assignee failed to be initialized!");
        }

        [TestMethod]
        public void StatusGetter_Should_ReturnValidStatus()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);

            //Act
            var story = new Story(title, description, id: 1, PriorityType.High,
                SizeType.Medium);
            //Assert
            Assert.AreEqual(Status.NotDone, story.Status, "Story status getter failed!");
        }

        [TestMethod]
        public void AddComment_Should_AddCommentToList()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            var assignee = new Member(new string('x', Constants.MEMBER_NAME_MAX_LENGTH - 1));
            var story = new Story(title, description, 1, PriorityType.High,
                SizeType.Medium);

            //Act
            story.AddComment(new Comment("Comment", assignee.Name));

            //Assert
            Assert.AreEqual(1, story.Comments.Count, "Could not add comment!");
        }

        [TestMethod]
        public void RemoveComment_Should_RemoveCommentFromList()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            var assignee = new Member(new string('x', Constants.MEMBER_NAME_MAX_LENGTH - 1));
            var comment = new Comment("Comment", assignee.Name);

            var story = new Story(title, description, 1, PriorityType.High,
                SizeType.Medium);

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
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            var assignee = new Member(new string('x', Constants.MEMBER_NAME_MAX_LENGTH - 1));


            //Act
            var story = new Story(title, description, 1, PriorityType.High,
                SizeType.Medium);

            //Assert
            Assert.AreEqual(1, story.ActivityLog.Count, "Remove comment failed!");
        }

        [TestMethod]
        public void ChangeStatus_Should_ChangeStatus_When_NewStatusPassed()
        {
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            var status = Status.InProgress;
            var story = new Story(title, description, id: id, PriorityType.High,
                SizeType.Medium);
            //Act
            story.Status = status;
            //Assert
            Assert.AreEqual(status, story.Status, "ChangeStatus failed in Story class!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ChangeStatus_ShouldThrow_When_SameStatusPassed()
        {
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            var status = Status.NotDone;
            var story = new Story(title, description, id: id, PriorityType.High,
                SizeType.Medium);

           
            //Act

            story.Status = status;

            //Assert
        }
    }
}