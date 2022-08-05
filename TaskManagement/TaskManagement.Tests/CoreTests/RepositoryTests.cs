using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Tests.CoreTests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void CreateBoard_Should_CreateNewBoard()
        {
            //Arrange
            var repository = new Repository();

            //Act
            var board = repository.CreateBoard("newboard");

            //Act & Assert
            Assert.IsInstanceOfType(board, typeof(IBoard), "Board failed to be craeted");
        }

        [TestMethod]
        public void CreateBug_Should_CreateBugAndAddToList()
        {
            //Arrange
            var repository = new Repository();
            var title = "Title dummy";
            var description = "Description dummy";
            PriorityType priority = PriorityType.High;
            Severity severity = Severity.Critical;
            List<string> steps = new List<string> { "log", "go fuck" };

            //Act
            var bug = repository.CreateBug(title, description, priority, severity, steps);

            //Assert
            Assert.AreEqual(bug, repository.Bugs[repository.Bugs.Count - 1],
                "Bug failed to be created!");
        }

        [TestMethod]
        public void CreateFeedback_Should_CreateFeedBackAndAddToList()
        {
            //Arrange
            var repository = new Repository();
            var title = "Title dummy";
            var description = "Description dummy";
            var rating = 99;

            //Act
            var feedback = repository.CreateFeedBack(title, description, rating);

            //Assert
            Assert.AreEqual(feedback, repository.Feedbacks[repository.Feedbacks.Count - 1],
                "Feedback failed to be created!");
        }

        [TestMethod]
        public void CreateStory_Should_CreateStoryAndAddToList()
        {
            //Arrange
            var repository = new Repository();
            var title = "Title dummy";
            var description = "Description dummy";

            //Act
            var story = repository.CreateStory(title, description, PriorityType.High, SizeType.Large);

            //Assert
            Assert.AreEqual(story, repository.Stories[repository.Stories.Count - 1],
                "Story failed to be created!");
        }

        [TestMethod]
        public void CreateMember_Should_CreateMemberAndAddtoList_When_NameUnique()
        {
            //Arrange
            var repository = new Repository();

            //Act
            var member = repository.CreateMember("Stefanos");

            //Assert
            Assert.AreEqual(member, repository.Members[repository.Members.Count - 1],
                "Member failed to create!");
        }

        [TestMethod]
        [ExpectedException(typeof(NameExistsException),
            "Тhe uniqueness of member's name is broken")]
        public void CreateMember_Should_ThrowException_When_NameNotUnique()
        {
            //Arrange
            var repository = new Repository();
            var member = repository.CreateMember("Stefanos");

            //Act & Assert
            var newMember = repository.CreateMember("Stefanos");
        }

        [TestMethod]
        public void CreateTeam_Should_CreateTeamAndAddToList_When_NameUnique()
        {
            //Arrange
            var repository = new Repository();

            //Act
            var team = repository.CreateTeam("NaiDobrite");

            //Assert
            Assert.AreEqual(team, repository.Teams[repository.Teams.Count - 1],
                "Team failed to be created!");
        }

        [TestMethod]
        [ExpectedException(typeof(NameExistsException),
            "Тhe uniqueness of team's name is broken")]
        public void CreateTeam_Should_CreateTeamAndAddToList_When_NameNotUnique()
        {
            //Arrange
            var repository = new Repository();
            repository.CreateTeam("NaiDobrite");

            //Act & Assert
            repository.CreateTeam("NaiDobrite");
        }

        [TestMethod]
        public void AddBoardToTeam_Should_AddBoardToTeam_When_BoardsNameUnique()
        {
            //Arrange
            var repository = new Repository();
            var board = repository.CreateBoard("Board");
            var team = repository.CreateTeam("BestTeam");

            //Act
            repository.AddBoardToTeam(team, board);

            //Assert
            Assert.AreEqual(board, team.Boards[team.Boards.Count - 1],
                "Board failed to be created!");
        }

        [TestMethod]
        [ExpectedException(typeof(NameExistsException),
           "Тhe uniqueness of board's name is broken")]
        public void AddBoardToTeam_Should_AddBoardToTeam_When_BoardsNameNotUnique()
        {
            //Arrange
            var repository = new Repository();
            var board = repository.CreateBoard("Board");
            var board2 = repository.CreateBoard("Board");
            var team = repository.CreateTeam("BestTeam");
            repository.AddBoardToTeam(team, board);

            //Act && Assert
            repository.AddBoardToTeam(team, board2);
        }

        [TestMethod]
        public void FindBoardByNameInTeam_Should_ReturnBoard()
        {
            //Arrange
            var repository = new Repository();
            var board = repository.CreateBoard("Board");
            var team = repository.CreateTeam("BestTeam");
            repository.AddBoardToTeam(team, board);

            //Act && Assert
            Assert.AreEqual(board, repository.FindBoardByNameInTeam(team, "Board")
                , "FindBoardByNameInTeam failed to return the expected board!");
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException),
              "FindBoardByNameInTeam failed to throw exception when board is not found!")]
        public void FindBoardByNameInTeam_Should_ThrowException_When_TeamDoesNotExist()
        {
            //Arrange
            var repository = new Repository();
            var team = repository.CreateTeam("BestTeam");

            //Act && Assert
            repository.FindBoardByNameInTeam(team, "Board");
        }

        [TestMethod]
        public void FindMemberByName_Should_ReturnMember()
        {
            //Arrange
            var name = "dummy";
            var repository = new Repository();
            var member = repository.CreateMember(name);

            // Act & Assert
            Assert.AreEqual(member, repository.FindMemberByName(name)
                , "FindMemberByName failed to return the expected board!");
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException),
              "FindMemberByName failed to throw exception when member is not found!")]
        public void FindMemberByName_Should_ThrowException_When_MemberDoesNotExist()
        {
            //Arrange
            var name = "dummy";
            var repository = new Repository();

            // Act & Assert
            repository.FindMemberByName(name);
        }

        [TestMethod]
        public void FindTaskById_Should_ReturnTask()
        {
            //Arrange
            var repository = new Repository();
            var title = "Title dummy";
            var description = "Description dummy";
            var rating = 99;

            //Act
            var feedback = repository.CreateFeedBack(title, description, rating);

            //Assert
            Assert.AreEqual(feedback, repository.FindTaskById(1),
                "FindTaskById failed to return the expected task");
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException),
              "FindTaskById failed to throw exception when task is not found!")]
        public void FindTaskById_Should_ThrowException_When_TaskDoesNotExist()
        {
            //Arrange
            var repository = new Repository();

            //Assert
            repository.FindTaskById(100);
        }

        [TestMethod]
        public void FindTeamByName_Should_ReturnTeam()
        {
            //Arrange
            var name = "dummy";
            var repository = new Repository();
            var team = repository.CreateTeam(name);

            //Act & Assert
            Assert.AreEqual(team, repository.FindTeamByName(name)
                , "FindTeamByName failed to find an existing team!");
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException),
              "FindTaskById failed to throw exception when task is not found!")]
        public void FindTeamByName_Should_ThrowExcption_When_TeamDoesNotExist()
        {
            //Arrange
            var name = "dummy";
            var repository = new Repository();

            //Act & Assert
            repository.FindTeamByName(name);
        }

        [TestMethod]
        public void IsMemberInTeam_Should_ReturnTrueWhenMemberExists()
        {
            //TODO use create member and team from repository for those tests
            //Arrange
            var team = new Team("Dummy team");
            var member = new Member("Dummy member");
            var repository = new Repository();

            team.AddMember(member);

            //Act & Assert
            Assert.IsTrue(repository.IsMemberInTeam(team, member),
                "IsMemberInTeam failed to find an existing member");
        }

        [TestMethod]
        public void IsMemberInTeam_Should_ReturnFalseWhenMemberDoesNotExists()
        {
            //Arrange
            var team = new Team("Dummy team");
            var member = new Member("Dummy member");
            var repository = new Repository();

            //Act & Assert
            Assert.IsFalse(repository.IsMemberInTeam(team, member),
                "IsMemberInTeam failed to find an existing member");
        }
    }
}