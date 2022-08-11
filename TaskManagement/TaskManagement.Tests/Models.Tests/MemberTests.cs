using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Tasks;
using TaskManagement.Validations;

namespace TaskManagement.Tests.Models.Tests
{
    [TestClass]
    public class MemberTests
    {
        private static FeedBack feedback;

        [ClassInitialize()]
        public static void MemberTests_Classinitialize(TestContext context)
        {
            string title = "Task title";
            string desctription = "Task description";
            int id = 1;
            int rating = 50;

            feedback = new FeedBack(title, desctription, id, rating);
        }

        [TestMethod]
        public void Constructor_Should_CreateMember_When_NameValid()
        {
            //Arrange
            string name = new string('x', Constants.MEMBER_NAME_MAX_LENGTH - 1);

            //Act
            var member = new Member(name);

            //Assert
            Assert.AreEqual(name, member.Name, "Created Member successfuly!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException), "Invalid member name!")]
        public void Constructor_Should_Fail_When_NameIsInvalid()
        {
            //Arrange
            string name = new string('x', Constants.MEMBER_NAME_MAX_LENGTH + 1);

            //Act & Assert
            var member = new Member(name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException), "Member name is NULL!")]
        public void Constructor_Should_Fail_When_NameIsNull()
        {
            //Arrange
            string name = null;

            //Act & Assert
            var member = new Member(name);
        }

        [TestMethod]
        public void MemberTasks_Should_AddTaskFromMember()
        {
            //Arrange
            string name = new string('x', Constants.MEMBER_NAME_MAX_LENGTH - 1);
            var member = new Member(name);

            //Act
            member.AddTask(feedback);

            //Assert
            Assert.AreEqual(1, member.Tasks.Count, "Successfuly added task to member!");
        }

        [TestMethod]
        public void MemberTasks_Should_RemoveTaskFromMember()
        {
            //Arrange
            string name = new string('x', Constants.MEMBER_NAME_MAX_LENGTH - 1);
            var member = new Member(name);

            member.AddTask(feedback);

            //Act
            member.RemoveTask(feedback);

            //Assert
            Assert.AreEqual(0, member.Tasks.Count, "Successfuly removed task to member!");
        }

        [TestMethod]
        public void ActivityLog_Should_Log_Every_Change()
        {
            //Arrange
            string name = new string('x', Constants.MEMBER_NAME_MAX_LENGTH - 1);
            var member = new Member(name);

            //Act
            member.AddTask(feedback);

            //Assert
            Assert.AreEqual(2, member.ActivityLog.Count, "Activity log is implemented correctly!");
        }

        [TestMethod]
        public void ShowActivity_Should_Show_Activity_Log()
        {
            //Arrange
            string name = new string('x', Constants.MEMBER_NAME_MAX_LENGTH - 1);
            var member = new Member(name);

            var eventLog = new EventLog(string.Format(Constants.CREATED_MSG, "Member", name));

            //Assert & Act
            Assert.AreEqual(eventLog.ViewInfo(), member.ShowActivity(), "Show activity log is implemented correctly!");
        }

        [TestMethod]
        public void ToString_Should_Return_Member_To_String_Ver1()
        {
            //with tasks to print
            //Arrange
            string name = new string('x', Constants.MEMBER_NAME_MAX_LENGTH - 1);
            var member = new Member(name);

            member.AddTask(feedback);

            //Act
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Constants.MEMBER_HEADER);
            sb.AppendLine($"{Constants.SPACES2}Member Name: {name}");

            sb.AppendLine($"{Constants.SPACES2}{Constants.TASK_HEADER}");
            sb.AppendLine();
            sb.Append($"{feedback.ToString()}");
            sb.AppendLine($"{Constants.SPACES2}{Constants.TASK_HEADER}");
            sb.AppendLine();
            sb.AppendLine(Constants.MEMBER_HEADER);

            //Assert

            Assert.AreEqual(sb.ToString(), member.ToString(), "To String Ver 1 printed successfuly!");
        }

        [TestMethod]
        public void ToString_Should_Return_Member_To_String_Ver2()
        {
            //without tasks to print
            //Arrange
            string name = new string('x', Constants.MEMBER_NAME_MAX_LENGTH - 1);
            var member = new Member(name);
            //Act
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Constants.MEMBER_HEADER);
            sb.AppendLine($"{Constants.SPACES2}Member Name: {name}");

            sb.AppendLine($"{Constants.SPACES2}{Constants.NO_TASK_HEADER}");
            sb.AppendLine();
            sb.AppendLine(Constants.MEMBER_HEADER);

            //Assert

            Assert.AreEqual(sb.ToString(), member.ToString(), "To String Ver 2 printed successfuly!");
        }
    }
}