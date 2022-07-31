using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core;
using TaskManagement.Models;

namespace TaskManagement.Tests.CoreTests
{
    [TestClass]
    public class RepositoryTests
    {
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
