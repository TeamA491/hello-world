using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamA.Exogredient.UnitTestServices;
using TeamA.Exogredient.DataHelpers;
using TeamA.Exogredient.AppConstants;
using TeamA.Exogredient.DAL;
using System.Collections.Generic;

namespace TeamA.Exogredient.TestsNoDatabase
{
    [TestClass]
    public class UserManagementServiceTests
    {
        [DataTestMethod]
        [DataRow(true, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", 0, "Customer", "23123123")]
        public void UserManagementService_CheckUserExistence_UserExistsSuccess(bool isTemp, string username, string firstname, string lastname, string email,
                                                                                                             string phoneNumber, string password, int isDisabled, string userType, string salt)
        {
            // Arrange: Create user 
            bool createResult = UserManagementService.CreateUser(isTemp, username, firstname, lastname, email, phoneNumber, password, isDisabled, userType, salt);
            Assert.IsTrue(createResult);


            // Assert: Check that an existing user returns true.
            bool result = UserManagementService.CheckUserExistence(username);
            Assert.IsTrue(result);

            // Cleanup: delete the created user.
            bool deleteResult = UserManagementService.DeleteUser(username);
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow("blahblah")]
        [DataRow("123123321")]
        public void UserManagementService_CheckUserExistence_UserDoesNotExistsFailure(string username)
        {
            // Assert: Check that an non existing user returns false.
            bool result = UserManagementService.CheckUserExistence(username);
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow(true, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", 0, "Customer", "12345678")]
        public void UserManagementService_CheckPhoneNumberExistence_PhoneNumberExistsSuccess(bool isTemp, string username, string firstname, string lastname, string email,
                                                                                                             string phoneNumber, string password, int isDisabled, string userType, string salt)
        {
            // Arrange: Create users with the phonenumber inputted. 
            bool createResult = UserManagementService.CreateUser(isTemp, username, firstname, lastname, email, phoneNumber, password, isDisabled, userType, salt);
            Assert.IsTrue(createResult);

            // Assert: check that an existing phonenumber returns true.
            bool result = UserManagementService.CheckPhoneNumberExistence(phoneNumber);
            Assert.IsTrue(result);

            // Cleanup: Delete Created user.
            bool deleteResult = UserManagementService.DeleteUser(username);
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow("0000000000")]
        public void UserManagementService_CheckPhoneNumberExistence_PhoneNumberDoesNotExistsFailure(string phoneNumber)
        {
            // Act: Check that a nonexistent phonenumber returns false. 
            bool result = UserManagementService.CheckPhoneNumberExistence(phoneNumber);
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow(true, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", 0, "Customer", "12345678")]
        public void UserManagementService_CheckEmailExistence_EmailExistsSuccess(bool isTemp, string username, string firstname, string lastname, string email,
                                                                                                             string phoneNumber, string password, int isDisabled, string userType, string salt)
        {
            // Arrange: Create user. 
            bool createResult = UserManagementService.CreateUser(isTemp, username, firstname, lastname, email, phoneNumber, password, isDisabled, userType, salt);
            Assert.IsTrue(createResult);

            // Act: check that an existing email returns true.
            bool result = UserManagementService.CheckEmailExistence(email);
            Assert.IsTrue(result);

            // Cleanup: Delete that user
            bool deleteResult = UserManagementService.DeleteUser(username);
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow("JasonDoesNotExists@gmail.com")]
        public void UserManagementService_CheckEmailExistence_EmailDoesNotExistsFailure(string email)
        {
            // Act: check that a non existing email returns false.
            bool result = UserManagementService.CheckEmailExistence(email);
            Assert.IsFalse(result);
        }


        [DataTestMethod]
        [DataRow(true, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", 1, "Customer", "12345678")]
        public void UserManagementService_CheckIfUserDisabled_UserIsDisabledSuccess(bool isTemp, string username, string firstname, string lastname, string email,
                                                                                                             string phoneNumber, string password, int isDisabled, string userType, string salt)
        {
            // Arrange: Create a disabled user.
            bool createResult = UserManagementService.CreateUser(isTemp, username, firstname, lastname, email, phoneNumber, password, isDisabled, userType, salt);
            Assert.IsTrue(createResult);

            // Act: Check that the disabled is disabled.
            bool result = UserManagementService.CheckIfUserDisabled(username);
            Assert.IsTrue(result);

            // Cleanup: Delete that user.
            bool deleteResult = UserManagementService.DeleteUser(username);
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow(true, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", 0, "Customer", "12345678")]
        public void UserManagementService_CheckIfUserDisabled_UserIsNotDisabledSuccess(bool isTemp, string username, string firstname, string lastname, string email,
                                                                                                             string phoneNumber, string password, int isDisabled, string userType, string salt)
        {
            // Arrange: Create user that is not diabled
            bool createResult = UserManagementService.CreateUser(isTemp, username, firstname, lastname, email, phoneNumber, password, isDisabled, userType, salt);
            Assert.IsTrue(createResult);

            // Act: Check that the non disabled returns false.
            bool result = UserManagementService.CheckIfUserDisabled(username);
            Assert.IsFalse(result);

            // Cleanup: Delete that user.
            bool deleteResult = UserManagementService.DeleteUser(username);
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow("127.0.0.1")]
        public void UserManagementService_CheckIfIPLocked_IpIsDisabledSuccess(string ipAddress)
        {
            // Arrange: Insert Ip into ip table.
            bool lockResult = UserManagementService.CreateIP(ipAddress);
            Assert.IsTrue(lockResult);

            // Arrange: Attempt to faile to register 3 times. 
            // IPs: Are only locked this way.
            for (int i = 0; i < 3; i++)
            {
                bool registerResult = UserManagementService.IncrementRegistrationFailures(ipAddress, Constants.RegistrationTriesResetTime, Constants.MaxRegistrationAttempts);
                Assert.IsTrue(registerResult);
            }


            // Act
            bool result = UserManagementService.CheckIfIPLocked(ipAddress);

            // Assert: Check that an IP that is inserted returns true.
            Assert.IsTrue(result);

            // Cleanup: Delete the IP.
            UnitTestIPAddressDAO ipDAO = new UnitTestIPAddressDAO();
            bool deleteResult = ipDAO.DeleteByIds(new List<string>() { ipAddress });
            Assert.IsTrue(deleteResult);

        }

        [DataTestMethod]
        [DataRow("127.0.0.9")]
        public void UserManagementService_CheckIPLock_IpIsNotDisabledSuccess(string ipAddress)
        {
            // Act:  Check that an non existent ip returns ArgumentExcpetions because ip does not exists.
            bool result;
            try
            {
                UserManagementService.CheckIfIPLocked(ipAddress);
                result = false;
            }
            catch (ArgumentException ae)
            {
                result = true;
            }
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow(false, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", 0, "Customer", "12345678")]
        public void UserManagementService_CreateUser_CreateNonExistentUserSuccess(bool isTemp, string username, string firstName, string lastName, string email,
                                                       string phoneNumber, string password, int disabled, string userType, string salt)
        {
            // Act: Create the user.
            bool createResult = UserManagementService.CreateUser(isTemp, username, firstName, lastName, email, phoneNumber, password, disabled, userType, salt);

            // Assert: that user creation was successful 
            Assert.IsTrue(createResult);

            // Read that user and assert that it has all the correct columns 
            UserObject user = UserManagementService.GetUserInfo(username);
            bool readResult;
            if (user.TempTimestamp == 0 && user.Username == username && user.FirstName == firstName && user.LastName == lastName && user.Email == email &&
                user.PhoneNumber == phoneNumber && user.Password == password && user.Disabled == disabled && user.UserType == userType && user.Salt == salt)
            {
                readResult = true;
            }
            else
            {
                readResult = false;
            }
            Assert.IsTrue(readResult);

            // Cleanup: Delete that user
            bool deleteResult = UserManagementService.DeleteUser(username);
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow(true, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", 0, "Customer", "87654321")]
        public void UserManagementService_DeleteUser_DeleteUserSuccess(bool isTemp, string username, string firstName, string lastName, string email,
                                                string phoneNumber, string password, int disabled, string userType, string salt)
        {
            // Arrange: Create a user to be deleted
            bool createResult = UserManagementService.CreateUser(isTemp, username, firstName, lastName, email, phoneNumber, password, disabled, userType, salt);
            Assert.IsTrue(createResult);

            // Act: Delete the user 
            bool result = UserManagementService.DeleteUser(username);
            Assert.IsTrue(result);

            // Assert: that the user is properly deleted from the table with CheckUserExistence
            bool existResult = UserManagementService.CheckUserExistence(username);
            Assert.IsFalse(existResult);
        }

        [DataTestMethod]
        [DataRow(false, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", 0, "Customer", "87654321")]
        public void UserManagementService_MakeTempPerm_ChangeTempToPermSuccess(bool isTemp, string username, string firstName, string lastName, string email,
                                         string phoneNumber, string password, int disabled, string userType, string salt)
        {
            // Arrange: Create a temporary user to be deleted.
            bool createResult = UserManagementService.CreateUser(isTemp, username, firstName, lastName, email, phoneNumber, password, disabled, userType, salt);
            Assert.IsTrue(createResult);

            // Act: Make the temporary user perm
            bool result = UserManagementService.MakeTempPerm(username);
            Assert.IsTrue(result);

            // Assert: that the user is infact permanent.
            UserObject user = UserManagementService.GetUserInfo(username);

            bool readResult;
            // TempTimestamp == 0 when the user is permanent.
            if (user.TempTimestamp == 0 && user.Username == username && user.FirstName == firstName && user.LastName == lastName && user.Email == email &&
                user.PhoneNumber == phoneNumber && user.Password == password && user.Disabled == disabled && user.UserType == userType && user.Salt == salt)
            {
                readResult = true;
            }
            else
            {
                readResult = false;
            }
            Assert.IsTrue(readResult);

            // Cleanup: Delete that user
            bool deleteResult = UserManagementService.DeleteUser(username);
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow(false, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", 0, "Customer", "12345678", "1233", 123123123123)]
        public void UserManagementService_StoreEmailCode_StoreEmailCodeForUserSuccess(bool isTemp, string username, string firstName, string lastName, string email,
                                         string phoneNumber, string password, int disabled, string userType, string salt, string emailCode, long emailCodeTimestamp)
        {
            // Arrange: Create a temporary user to be deleted
            UserManagementService.CreateUser(isTemp, username, firstName, lastName, email, phoneNumber, password, disabled, userType, salt);

            // Act: Store an email code for a user.
            bool result = UserManagementService.StoreEmailCode(username, emailCode, emailCodeTimestamp);
            Assert.IsTrue(result);

            // Assert: Read the email code and check if it did infact change.
            UserObject user = UserManagementService.GetUserInfo(username);
            bool readResult;
            if (user.TempTimestamp == 0 && user.Username == username && user.FirstName == firstName && user.LastName == lastName && user.Email == email &&
                 user.PhoneNumber == phoneNumber && user.Password == password && user.Disabled == disabled && user.UserType == userType && user.Salt == salt)
            {
                readResult = true;
            }
            else
            {
                readResult = false;
            }
            Assert.IsTrue(readResult);

            // Cleanup: Delete that user
            bool deleteResult = UserManagementService.DeleteUser(username);
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow(false, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", 0, "Customer", "12345678", "1233", 10000000)]
        public void UserManagementService_RemoveEmailCode_RemoveEmailCodeForUserSuccess(bool isTemp, string username, string firstName, string lastName, string email,
                                         string phoneNumber, string password, int disabled, string userType, string salt, string emailCode, long emailCodeTimestamp)
        {
            // Arrange: Create a user.
            bool createResult = UserManagementService.CreateUser(isTemp, username, firstName, lastName, email, phoneNumber, password, disabled, userType, salt);
            Assert.IsTrue(createResult);

            // Arrange: Store an email code for that user.
            bool storeResult = UserManagementService.StoreEmailCode(username, emailCode, emailCodeTimestamp);
            Assert.IsTrue(storeResult);

            // Act: Remove the email code for that user.
            bool result = UserManagementService.RemoveEmailCode(username);
            Assert.IsTrue(result);

            // Read that user and check if infact that email code is removed.
            UserObject user = UserManagementService.GetUserInfo(username);
            bool readResult;
            if (user.TempTimestamp == 0 && user.Username == username && user.FirstName == firstName && user.LastName == lastName && user.Email == email &&
                 user.PhoneNumber == phoneNumber && user.Password == password && user.Disabled == disabled && user.UserType == userType && user.Salt == salt &&
                 user.EmailCode == "" && user.EmailCodeTimestamp == 0 && user.EmailCodeFailures == 0)
            {
                readResult = true;
            }
            else
            {
                readResult = false;
            }
            Assert.IsTrue(readResult);

            // Cleanup: Delete that user
            bool deleteResult = UserManagementService.DeleteUser(username);
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow(false, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", Constants.EnabledStatus, "Customer", "12345678")]
        public void UserManagementService_DisableUserName_DisableExistingUserSuccess(bool isTemp, string username, string firstName, string lastName, string email,
                                         string phoneNumber, string password, int disabled, string userType, string salt)
        {
            // Arrange: Create the user that is not disabled
            bool createResult = UserManagementService.CreateUser(isTemp, username, firstName, lastName, email, phoneNumber, password, disabled, userType, salt);
            Assert.IsTrue(createResult);

            // Act: disable that user 
            bool result = UserManagementService.DisableUser(username);
            Assert.IsTrue(result);

            // Assert: Check that the user is disabled.
            UserObject user = UserManagementService.GetUserInfo(username);
            bool readResult;
            // User is disabled if user.Disabled == Constants.DisabledStatus
            if (user.TempTimestamp == Constants.NoValueLong && user.Username == username && user.FirstName == firstName && user.LastName == lastName && user.Email == email &&
                 user.PhoneNumber == phoneNumber && user.Password == password && user.Disabled == Constants.DisabledStatus && user.UserType == userType && user.Salt == salt &&
                 user.EmailCode == Constants.NoValueString && user.EmailCodeTimestamp == Constants.NoValueLong && user.EmailCodeFailures == Constants.NoValueInt)
            {
                readResult = true;
            }
            else
            {
                readResult = false;
            }
            Assert.IsTrue(readResult);

            // Delete the created user 
            bool deleteResult = UserManagementService.DeleteUser(username);
            Assert.IsTrue(deleteResult);
        }


        [DataTestMethod]
        [DataRow("username")]
        public void UserManagementService_DisableUserName_DisableNonExistingUserFailure(string username)
        {
            // Act: disabling a non existent user should throw an ArgumentException because the user doesn't exists.
            bool result;
            try
            {
                UserManagementService.DisableUser(username);
                result = false;
            }
            catch (ArgumentException ae)
            {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow(false, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", 1, "Customer", "12345678")]
        public void UserManagementService_DisableUser_DisableADisabledUserFailure(bool isTemp, string username, string firstName, string lastName, string email,
                                         string phoneNumber, string password, int disabled, string userType, string salt)
        {
            // Arrange: Create a user that is Disabled
            bool createResult = UserManagementService.CreateUser(isTemp, username, firstName, lastName, email, phoneNumber, password, disabled, userType, salt);
            Assert.IsTrue(createResult);

            // Act: Disabling an already disabled user should return false.
            bool result = UserManagementService.DisableUser(username);
            Assert.IsFalse(result);

            // Cleanup: Delete the user.
            bool deleteResult = UserManagementService.DeleteUser(username);
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow(false, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", 1, "Customer", "12345678")]
        public void UserManagementService_EnableUser_EnableADisabledUserSuccess(bool isTemp, string username, string firstName, string lastName, string email,
                                         string phoneNumber, string password, int disabled, string userType, string salt)
        {
            // Arrange: Create a disabled user.
            bool createResult = UserManagementService.CreateUser(isTemp, username, firstName, lastName, email, phoneNumber, password, disabled, userType, salt);
            Assert.IsTrue(createResult);

            // Act: Perform an enable operation on a disabled user.
            bool enableResult = UserManagementService.EnableUser(username);
            Assert.IsTrue(enableResult);

            // Assert: Check that the user is enabled.
            UserObject user = UserManagementService.GetUserInfo(username);
            bool readResult;
            // User is disabled if user.Disabled == Constants.DisabledStatus
            if (user.TempTimestamp == Constants.NoValueLong && user.Username == username && user.FirstName == firstName && user.LastName == lastName && user.Email == email &&
                 user.PhoneNumber == phoneNumber && user.Password == password && user.Disabled == Constants.EnabledStatus && user.UserType == userType && user.Salt == salt &&
                 user.EmailCode == Constants.NoValueString && user.EmailCodeTimestamp == Constants.NoValueLong && user.EmailCodeFailures == Constants.NoValueInt)
            {
                readResult = true;
            }
            else
            {
                readResult = false;
            }
            Assert.IsTrue(readResult);

            // Cleanup: Delete that created user
            bool deleteResult = UserManagementService.DeleteUser(username);
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow(false, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", 0, "Customer", "12345678")]
        public void UserManagementService_EnableUser_EnableAEnabledUserFailure(bool isTemp, string username, string firstName, string lastName, string email,
                                         string phoneNumber, string password, int disabled, string userType, string salt)
        {
            // Arrange: Create a user that is enabled
            bool createResult = UserManagementService.CreateUser(isTemp, username, firstName, lastName, email, phoneNumber, password, disabled, userType, salt);
            Assert.IsTrue(createResult);

            // Act: Enable and already enabled user should return false.
            bool result = UserManagementService.EnableUser(username);
            Assert.IsFalse(result);

            // Cleanup: Delete that user.
            bool deleteResult = UserManagementService.DeleteUser(username);
            Assert.IsTrue(deleteResult);
        }

        [DataTestMethod]
        [DataRow(false, "username", "mr.DROP", "TABLE", "blahblah@gmail.com", "1234567891", "password", 0, "Customer", "12345678")]
        public void UserManagementService_ChangePassword_ChangePasswordOfExistingUserSuccess(bool isTemp, string username, string firstName, string lastName, string email,
                                         string phoneNumber, string password, int disabled, string userType, string salt)
        {
            // Arrange: Create the user.
            bool createResult = UserManagementService.CreateUser(isTemp, username, firstName, lastName, email, phoneNumber, password, disabled, userType, salt);
            Assert.IsTrue(createResult);

            // Act: Change the user password digest.
            bool passwordResult = UserManagementService.ChangePassword(username, password, salt);
            Assert.IsTrue(passwordResult);

            // Assert: Read the user and make sure his password now matches the change.
            UserObject user = UserManagementService.GetUserInfo(username);
            bool readResult;
            if (user.TempTimestamp == 0 && user.Username == username && user.FirstName == firstName && user.LastName == lastName && user.Email == email &&
                 user.PhoneNumber == phoneNumber && user.Password == password && user.Disabled == disabled && user.UserType == userType && user.Salt == salt)
            {
                readResult = true;
            }
            else
            {
                readResult = false;
            }
            Assert.IsTrue(readResult);

            // Cleanup: Delete that user.
            bool deleteResult = UserManagementService.DeleteUser(username);
            Assert.IsTrue(deleteResult);
        }

        public void UserManagementService_NotifySystemAdmin_SendEmailToSystemAdminSuccess(string body)
        {
            // Act: Check that a successfull message to system admin returns true.
            bool notifyResult = UserManagementService.NotifySystemAdmin(body, Constants.SystemAdminEmailAddress);
            Assert.IsTrue(notifyResult);
        }
    }
}
