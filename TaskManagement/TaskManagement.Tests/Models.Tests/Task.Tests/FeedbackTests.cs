using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Enums.FeedbackStatus;
using TaskManagement.Models.Tasks;
using TaskManagement.Validations;

namespace TaskManagement.Tests.Models.Tests.Task.Tests
{
    [TestClass]
    public class FeedbackTests
    {
        [TestMethod]
        public void Constructor_Should_CreateFeedback_When_ParamsValid()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            int rating = Constants.RATING_MIN_VALUE + 1;
            //Act
            var feedback = new FeedBack(title, description, id, rating);
            //Assert
            Assert.AreEqual(title, feedback.Title, "Created feedback successfuly!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException), "Base class validation for title failed!")]
        public void Constuctor_Should_ThrowException_When_TitleInvalid()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MAX_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN + 1);
            int id = 1;
            int rating = Constants.RATING_MIN_VALUE + 1;

            //Act && Assert
            var feedback = new FeedBack(title, description, id, rating);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException), "Feedback class validation for rating failed!")]
        public void Constuctor_Should_ThrowException_When_RatingInvalid()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            int rating = Constants.RATING_MAX_VALUE + 1;

            //Act && Assert
            var feedback = new FeedBack(title, description, id, rating);
        }

        [TestMethod]
        public void StatusGetter_Should_ReturnValidStatus()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            int rating = Constants.RATING_MAX_VALUE - 1;

            //Act
            var feedback = new FeedBack(title, description, id, rating);
            //Assert
            Assert.AreEqual(Status.New, feedback.Status, "Feedback status getter succeed!");
        }

        [TestMethod]
        public void RatingGetter_Should_ReturnValidStatus()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            int rating = Constants.RATING_MAX_VALUE - 1;

            //Act
            var feedback = new FeedBack(title, description, id, rating);
            //Assert
            Assert.AreEqual(rating, feedback.Rating, "Feedback rating getter succeed!");
        }

        [TestMethod]
        public void AddComment_Should_AddCommentToList()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            int rating = Constants.RATING_MAX_VALUE - 1;
            var assignee = new Member(new string('x', 10));

            var feedback = new FeedBack(title, description, id, rating);

            //Act
            feedback.AddComment(new Comment("Comment", assignee.Name));

            //Assert
            Assert.AreEqual(1, feedback.Comments.Count, "Added comment successfuly!");
        }

        [TestMethod]
        public void RemoveComment_Should_RemoveCommentFromList()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            int rating = Constants.RATING_MAX_VALUE - 1;
            var assignee = new Member(new string('x', 10));

            var comment = new Comment("Comment", assignee.Name);

            var feedback = new FeedBack(title, description, id, rating);

            feedback.AddComment(comment);

            //Act
            feedback.RemoveComment(comment);

            //Assert
            Assert.AreEqual(0, feedback.Comments.Count, "Removed comment successfuly!");
        }

        [TestMethod]
        public void ActivityLog_Should_LogEveryChange()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            int rating = Constants.RATING_MAX_VALUE - 1;
            var assignee = new Member(new string('x', 10));

            var comment = new Comment("Comment", assignee.Name);

            //Act
            var feedback = new FeedBack(title, description, id, rating);
            feedback.AddComment(comment);

            //Assert
            Assert.AreEqual(2, feedback.ActivityLog.Count, "Activity log is implemented correctly!");
        }

        [TestMethod]
        public void ChangeStatus_Should_Change_Status_When_Not_Same()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            int rating = Constants.RATING_MAX_VALUE - 1;

            var feedback = new FeedBack(title, description, id, rating);

            Status changedStatus = Status.Scheduled;

            //Act
            feedback.ChangeStatus(changedStatus);

            //Assert
            Assert.AreEqual(changedStatus, feedback.Status, "Changed Status successfuly!");
        }

        [TestMethod]
        public void ChangeStatus_Should_Not_Change_Status_When_Not_Same()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            int rating = Constants.RATING_MAX_VALUE - 1;

            var feedback = new FeedBack(title, description, id, rating);

            Status changedStatus = Status.New;

            //Act
            feedback.ChangeStatus(changedStatus);

            //Assert
            Assert.AreEqual(changedStatus, feedback.Status, "Change Rating failed!");
        }

        [TestMethod]
        public void ChangeRating_Should_Change_Rating_When_Not_Same()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            int rating = Constants.RATING_MAX_VALUE - 1;

            var feedback = new FeedBack(title, description, id, rating);

            int changedRating = Constants.RATING_MIN_VALUE + 1;

            //Act
            feedback.ChangeRating(changedRating);

            //Assert
            Assert.AreEqual(changedRating, feedback.Rating, "Changed Rating successfuly!");
        }

        [TestMethod]
        public void ChangeRating_Should_Not_Change_Rating_When_Same()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            int rating = Constants.RATING_MAX_VALUE - 1;

            var feedback = new FeedBack(title, description, id, rating);

            //Act
            feedback.ChangeRating(rating);

            //Assert
            Assert.AreEqual(rating, feedback.Rating, "Changed Rating successfuly!");
        }

        [TestMethod]
        public void AdditionalInfo_Should_Return_Addition_Info()
        {
            //Arrange
            string title = new string('x', Constants.TITLE_MIN_LEN + 1);
            string description = new string('x', Constants.DESCRIPTION_MAX_LEN - 1);
            int id = 1;
            int rating = Constants.RATING_MAX_VALUE - 1;

            var feedback = new FeedBack(title, description, id, rating);

            var sb = new StringBuilder();

            sb.AppendLine(Constants.FEEDBACK_HEADER);
            sb.AppendLine($"Status: {Status.New}");
            sb.AppendLine($"Rating: {rating}");

            //Assert & Act
            Assert.AreEqual(sb.ToString(), feedback.AdditionalInfo(), "Called feedback additional info successfuly!");
        }
    }
}