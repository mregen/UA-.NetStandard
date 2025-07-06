// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Opc.Ua;
using Opc.Ua.Server.UserDatabase;
using Assert = NUnit.Framework.Legacy.ClassicAssert;

namespace Opc.Ua.Server.Tests.UserDatabase
{
    [TestFixture]
    public class LinqUserDatabaseTests
    {
        private static readonly Role TestRole = new Role(new NodeId(1), "TestRole");

        private LinqUserDatabase CreateDatabase()
        {
            return new LinqUserDatabase();
        }

        [Test]
        public void CreateUser_ShouldAddUser()
        {
            var db = CreateDatabase();
            var userName = "testuser";
            var password = Encoding.UTF8.GetBytes("password123");
            var roles = new List<Role> { TestRole };

            var result = db.CreateUser(userName, password, roles);

            Assert.IsTrue(result);
            CollectionAssert.AreEqual(roles, db.GetUserRoles(userName));
        }

        [Test]
        public void CreateUser_ShouldUpdateExistingUser()
        {
            var db = CreateDatabase();
            var userName = "testuser";
            var password1 = Encoding.UTF8.GetBytes("password1");
            var password2 = Encoding.UTF8.GetBytes("password2");
            var roles1 = new List<Role> { TestRole };
            var roles2 = new List<Role> { TestRole, Role.Operator };

            db.CreateUser(userName, password1, roles1);
            var result = db.CreateUser(userName, password2, roles2);

            Assert.IsFalse(result); // Should return false for update
            CollectionAssert.AreEqual(roles2, db.GetUserRoles(userName));
            Assert.IsTrue(db.CheckCredentials(userName, password2));
            Assert.IsFalse(db.CheckCredentials(userName, password1));
        }

        [Test]
        public void DeleteUser_ShouldRemoveUser()
        {
            var db = CreateDatabase();
            var userName = "testuser";
            var password = Encoding.UTF8.GetBytes("password123");
            var roles = new List<Role> { TestRole };

            db.CreateUser(userName, password, roles);
            var result = db.DeleteUser(userName);

            Assert.IsTrue(result);
            Assert.Throws<ArgumentException>(() => db.GetUserRoles(userName));
        }

        [Test]
        public void CheckCredentials_ShouldReturnTrueForCorrectPassword()
        {
            var db = CreateDatabase();
            var userName = "testuser";
            var password = Encoding.UTF8.GetBytes("password123");
            var roles = new List<Role> { TestRole };

            db.CreateUser(userName, password, roles);

            Assert.IsTrue(db.CheckCredentials(userName, password));
        }

        [Test]
        public void CheckCredentials_ShouldReturnFalseForIncorrectPassword()
        {
            var db = CreateDatabase();
            var userName = "testuser";
            var password = Encoding.UTF8.GetBytes("password123");
            var wrongPassword = Encoding.UTF8.GetBytes("wrongpassword");
            var roles = new List<Role> { TestRole };

            db.CreateUser(userName, password, roles);

            Assert.IsFalse(db.CheckCredentials(userName, wrongPassword));
        }

        [Test]
        public void GetUserRoles_ShouldThrowForUnknownUser()
        {
            var db = CreateDatabase();
            Assert.Throws<ArgumentException>(() => db.GetUserRoles("unknown"));
        }

        [Test]
        public void ChangePassword_ShouldUpdatePassword()
        {
            var db = CreateDatabase();
            var userName = "testuser";
            var oldPassword = Encoding.UTF8.GetBytes("oldpassword");
            var newPassword = Encoding.UTF8.GetBytes("newpassword");
            var roles = new List<Role> { TestRole };

            db.CreateUser(userName, oldPassword, roles);

            var result = db.ChangePassword(userName, oldPassword, newPassword);

            Assert.IsTrue(result);
            Assert.IsTrue(db.CheckCredentials(userName, newPassword));
            Assert.IsFalse(db.CheckCredentials(userName, oldPassword));
        }

        [Test]
        public void ChangePassword_ShouldFailWithWrongOldPassword()
        {
            var db = CreateDatabase();
            var userName = "testuser";
            var oldPassword = Encoding.UTF8.GetBytes("oldpassword");
            var wrongOldPassword = Encoding.UTF8.GetBytes("wrongoldpassword");
            var newPassword = Encoding.UTF8.GetBytes("newpassword");
            var roles = new List<Role> { TestRole };

            db.CreateUser(userName, oldPassword, roles);

            var result = db.ChangePassword(userName, wrongOldPassword, newPassword);

            Assert.IsFalse(result);
            Assert.IsTrue(db.CheckCredentials(userName, oldPassword));
            Assert.IsFalse(db.CheckCredentials(userName, newPassword));
        }
    }
}
