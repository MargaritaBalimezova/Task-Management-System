using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Models;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Tasks;

namespace TaskManagement.Tests.Models.Tests
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

            //Act
            var story = new Story(title, description, 1, PriorityType.High,
                SizeType.Medium, assignee);
            //Assert
            Assert.AreEqual(title, story.Title, "Story constructor failed!");
        }
    }
}
