using System;
using System.Collections.Generic;
using NUnit.Framework;
using Opc.Ua.Server.UserDatabase;
using Assert = NUnit.Framework.Legacy.ClassicAssert;

namespace Opc.Ua.Server.Tests
{
    [TestFixture, Category("Server")]
    [SetCulture("en-us"), SetUICulture("en-us")]
    [Parallelizable]
    internal class LinqUserDatabaseTests
    {
        #region Test Methods
        [Test]
        public void CreateInvalidUser()
        {
            // Arrange
            var usersDb = new LinqUserDatabase();

            // Act + Assert
            Assert.Throws<ArgumentException>(
                () => usersDb.CreateUser(null, "PW"u8, new List<Role> { Role.AuthenticatedUser }));
            Assert.Throws<ArgumentException>(
                () => usersDb.CreateUser("Name", null, new List<Role> { Role.AuthenticatedUser }));
            Assert.Throws<ArgumentException>(
                () => usersDb.CreateUser("", "PW"u8, new List<Role> { Role.AuthenticatedUser }));
            Assert.Throws<ArgumentException>(
                () => usersDb.CreateUser("Name", ""u8, new List<Role> { Role.AuthenticatedUser }));
        }

        [Test]
        public void DeleteExistingUser()
        {
            // Arrrange
            var usersDb = new LinqUserDatabase();
            usersDb.CreateUser("TestUser", "PW"u8, new List<Role> { Role.AuthenticatedUser });

            // Act
            var result = usersDb.DeleteUser("TestUser");

            // Assert
            Assert.True(result);
        }

        [Test]
        public void DeleteNonExistingUser()
        {
            // Arrrange
            var usersDb = new LinqUserDatabase();
            usersDb.CreateUser("TestUser", "PW"u8, new List<Role> { Role.AuthenticatedUser });

            // Act
            var result = usersDb.DeleteUser("NoTestUser");

            // Assert
            Assert.False(result);
        }

        [Test]
        public void ChangePwOfExistingUser()
        {
            // Arrange
            var usersDb = new LinqUserDatabase();
            usersDb.CreateUser("TestUser", "PW"u8, new List<Role> { Role.AuthenticatedUser });

            // Act
            var result = usersDb.ChangePassword("TestUser", "PW"u8, "newPW"u8);
            var login = usersDb.CheckCredentials("TestUser", "newPW"u8);
            var loginOldPW = usersDb.CheckCredentials("TestUser", "PW"u8);

            // Assert
            Assert.True(result);
            Assert.True(login);
            Assert.False(loginOldPW);
        }

        [Test]
        public void ChangePwOfNonExistingUser()
        {
            // Arrange
            var usersDb = new LinqUserDatabase();
            usersDb.CreateUser("TestUser", "PW"u8, new List<Role> { Role.AuthenticatedUser });

            // Act
            var result = usersDb.DeleteUser("NoTestUser");

            // Assert
            Assert.False(result);
        }

        [Test]
        public void CheckPWofExistingUser()
        {
            // Arrange
            var usersDb = new LinqUserDatabase();
            usersDb.CreateUser("TestUser", "PW"u8, new List<Role> { Role.AuthenticatedUser });

            // Act
            var result = usersDb.CheckCredentials("TestUser", "PW"u8);
            var loginWrongPw = usersDb.CheckCredentials("TestUser", "newPW"u8);

            // Assert
            Assert.True(result);
            Assert.False(loginWrongPw);
        }

        [Test]
        public void CheckPWofNonExistingUser()
        {
            // Arrange
            var usersDb = new LinqUserDatabase();
            usersDb.CreateUser("TestUser", "PW"u8, new List<Role> { Role.AuthenticatedUser });

            // Act
            var result = usersDb.CheckCredentials("NoTestUser", "PW"u8);

            // Assert
            Assert.False(result);
        }
        #endregion
    }
}
