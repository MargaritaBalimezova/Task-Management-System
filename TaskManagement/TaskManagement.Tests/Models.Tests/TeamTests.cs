using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Tasks;
using TaskManagement.Validations;

namespace TaskManagement.Tests.Models.Tests
{
    [TestClass]
    public class TeamTests
    {
        [TestMethod]
        public void Constructor_Should_CreateTeam_When_NameValid()
        {
            //Arrange
            string name = new string('x', Constants.TEAM_NAME_MAX_LEN - 1);

            //Act
            var team = new Team(name);

            //Assert
            Assert.AreEqual(name, team.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void Constructor_Should_Fail_When_NameIsInvalid()
        {
            //Arrange
            string name = new string('x', Constants.TEAM_NAME_MAX_LEN + 1);

            //Act & Assert
            var team = new Team(name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void Constructor_Should_Fail_When_NameIsNull()
        {
            //Arrange
            string name = null;

            //Act & Assert
            var team = new Team(name);
        }

        [TestMethod]
        public void AddBoard_Should_AddBoardToTeamList()
        {
            //Arrange
            Board board = new Board(new string('x', Constants.TEAM_NAME_MIN_LEN + 1));
            var team = new Team(new string('x', Constants.TEAM_NAME_MIN_LEN + 1));

            //Act
            team.AddBoard(board);

            //Assert
            Assert.AreEqual(1, team.Boards.Count, "AddBoard to the team failed!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException), "AddBoard failed and added null value!")]
        public void AddBoard_Should_ThrowException_WhenBoardIsNull()
        {
            //Arrange
            Board board = null;
            var team = new Team(new string('x', Constants.TEAM_NAME_MIN_LEN + 1));

            //Act & Assert
            team.AddBoard(board);
        }

        [TestMethod]
        public void RemoveBoard_Should_RemoveBoardFromTeamList()
        {
            //Arrange
            Board board = new Board(new string('x', Constants.TEAM_NAME_MIN_LEN + 1));
            var team = new Team(new string('x', Constants.TEAM_NAME_MIN_LEN + 1));
            team.AddBoard(board);

            //Act
            team.RemoveBoard(board);

            //Assert
            Assert.IsTrue(team.Boards.Count == 0, "Remove from board is not working properly!");
        }

        [TestMethod]
        public void AddMember_Should_AddMemberToTeam()
        {
            //Arrange
            var member = new Member(new string('x', Constants.TEAM_NAME_MIN_LEN + 1));
            var team = new Team(new string('x', Constants.TEAM_NAME_MIN_LEN + 1));

            //Act
            team.AddMember(member);

            //Assert
            Assert.AreEqual(1, team.Members.Count, "AddMember Team method failed!");
        }

        [TestMethod]
        public void RemoveMember_Should_RemoveMemberFromTeam()
        {
            //Arrange
            var member = new Member(new string('x', Constants.TEAM_NAME_MIN_LEN + 1));
            var team = new Team(new string('x', Constants.TEAM_NAME_MIN_LEN + 1));
            team.AddMember(member);

            //Act
            team.RemoveMember(member);

            //Assert
            Assert.IsTrue(team.Members.Count == 0, "RemoveMember from the team failed!");
        }

        [TestMethod]
        public void AddTaskToBoard_Should_AddTaskToGivenBoard()
        {
            //Arrange
            Board board = new Board(new string('x', Constants.TEAM_NAME_MIN_LEN + 1));
            var team = new Team(new string('x', Constants.TEAM_NAME_MIN_LEN + 1));
            var feedback = new FeedBack("this title", "dummy description", id: 1, 96);
            team.AddBoard(board);

            //Act
            team.AddTaskToBoard(feedback, board);

            //Assert
            Assert.ReferenceEquals(feedback, team.Boards[0]);
        }
    }
}