using ChocAnDatabase;
using ChocAnDatabase.records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ManagerTerminal.Tests {
    [TestClass]
    public class CrudValidatorTests {
        private Mock<IDatabaseWrapper> _databaseWrapperMock;
        private ICrudValidator _crudValidator;

        [TestInitialize]
        public void Setup() {
            _databaseWrapperMock = new Mock<IDatabaseWrapper>();
            _crudValidator = new CrudValidator(_databaseWrapperMock.Object);
        }

        [TestMethod]
        public void AreAllFieldsFilledIn_memberRecordNull() {
            var result = _crudValidator.AreAllFieldsFilledIn((MemberRecord) null, false);

            Assert.AreEqual(false, result.successful);
            Assert.AreEqual("Member record is null", result.errorMessage);
        }

        [TestMethod]
        public void AreAllFieldsFilledIn_providerRecordNull()
        {
            var result = _crudValidator.AreAllFieldsFilledIn((ProviderRecord)null, false);

            Assert.AreEqual(false, result.successful);
            Assert.AreEqual("Provider record is null", result.errorMessage);
        }

        [DataRow("Good Lower Bound Zip", 1, "n", "a", "c", "s", 11111, true, true, "")]
        [DataRow("Good Upper Bound Zip", 1, "n", "a", "c", "s", 99999, true, true, "")]
        [DataRow("Number is 0", 0, "n", "a", "c", "s", 11111, true, false, "Needs to have a valid number")]
        [DataRow("Number is -1", -1, "n", "a", "c", "s", 11111, true, false, "Needs to have a valid number")]
        [DataRow("Name Null", 1, null, "a", "c", "s", 11111, true, false, "Needs to have a name")]
        [DataRow("Name Blank", 1, "", "a", "c", "s", 11111, true, false, "Needs to have a name")]
        [DataRow("Name Space", 1, " ", "a", "c", "s", 11111, true, false, "Needs to have a name")]
        [DataRow("Address Null", 1, "n", null, "c", "s", 11111, true, false, "Needs to have an address")]
        [DataRow("Address Blank", 1, "n", "", "c", "s", 11111, true, false, "Needs to have an address")]
        [DataRow("Address Space", 1, "n", " ", "c", "s", 11111, true, false, "Needs to have an address")]
        [DataRow("City Null", 1, "n", "a", null, "s", 11111, true, false, "Needs to have a city")]
        [DataRow("City Blank", 1, "n", "a", "", "s", 11111, true, false, "Needs to have a city")]
        [DataRow("City Space", 1, "n", "a", " ", "s", 11111, true, false, "Needs to have a city")]
        [DataRow("State Null", 1, "n", "a", "c", null, 11111, true, false, "Needs to have a state")]
        [DataRow("State Blank", 1, "n", "a", "c", "", 11111, true, false, "Needs to have a state")]
        [DataRow("State Space", 1, "n", "a", "c", " ", 11111, true, false, "Needs to have a state")]
        [DataRow("Zip Code Lower Bound", 1, "n", "a", "c", "s", 9999, true, false, "Needs to have a 5 digit zip code")]
        [DataRow("Zip Code Upper Bound", 1, "n", "a", "c", "s", 100000, true, false, "Needs to have a 5 digit zip code")]

        [DataRow("Not Checking Number - Good Lower Bound Zip", -1, "n", "a", "c", "s", 11111, false, true, "")]
        [DataRow("Not Checking Number - Good Upper Bound Zip", -1, "n", "a", "c", "s", 99999, false, true, "")]
        [DataRow("Not Checking Number - Number is 0", 0, "n", "a", "c", "s", 11111, false, true, "")]
        [DataRow("Not Checking Number - Number is -1", -1, "n", "a", "c", "s", 11111, false, true, "")]
        [TestMethod]
        public void AreAllFieldsFilledIn_member(
            string testName,
            int number,
            string name,
            string address,
            string city,
            string state,
            int zip,
            bool checkForId,
            bool expectedResult,
            string expectedMessage
            )
        {
            var memberRecord = new MemberRecord(new Dictionary<string, object>())
            {
                Number = number,
                Name = name,
                Address = address,
                City = city,
                State = state,
                Zip = zip
            };

            var result = _crudValidator.AreAllFieldsFilledIn(memberRecord, checkForId);

            Assert.AreEqual(expectedResult, result.successful);
            Assert.AreEqual(expectedMessage, result.errorMessage);
        }

        [DataRow("Good Lower Bound Zip", 1, "n", "a", "c", "s", 11111, true, true, "")]
        [DataRow("Good Upper Bound Zip", 1, "n", "a", "c", "s", 99999, true, true, "")]
        [DataRow("Number is 0", 0, "n", "a", "c", "s", 11111, true, false, "Needs to have a valid number")]
        [DataRow("Number is -1", -1, "n", "a", "c", "s", 11111, true, false, "Needs to have a valid number")]
        [DataRow("Name Null", 1, null, "a", "c", "s", 11111, true, false, "Needs to have a name")]
        [DataRow("Name Blank", 1, "", "a", "c", "s", 11111, true, false, "Needs to have a name")]
        [DataRow("Name Space", 1, " ", "a", "c", "s", 11111, true, false, "Needs to have a name")]
        [DataRow("Address Null", 1, "n", null, "c", "s", 11111, true, false, "Needs to have an address")]
        [DataRow("Address Blank", 1, "n", "", "c", "s", 11111, true, false, "Needs to have an address")]
        [DataRow("Address Space", 1, "n", " ", "c", "s", 11111, true, false, "Needs to have an address")]
        [DataRow("City Null", 1, "n", "a", null, "s", 11111, true, false, "Needs to have a city")]
        [DataRow("City Blank", 1, "n", "a", "", "s", 11111, true, false, "Needs to have a city")]
        [DataRow("City Space", 1, "n", "a", " ", "s", 11111, true, false, "Needs to have a city")]
        [DataRow("State Null", 1, "n", "a", "c", null, 11111, true, false, "Needs to have a state")]
        [DataRow("State Blank", 1, "n", "a", "c", "", 11111, true, false, "Needs to have a state")]
        [DataRow("State Space", 1, "n", "a", "c", " ", 11111, true, false, "Needs to have a state")]
        [DataRow("Zip Code Lower Bound", 1, "n", "a", "c", "s", 9999, true, false, "Needs to have a 5 digit zip code")]
        [DataRow("Zip Code Upper Bound", 1, "n", "a", "c", "s", 100000, true, false, "Needs to have a 5 digit zip code")]

        [DataRow("Not Checking Number - Good Lower Bound Zip", -1, "n", "a", "c", "s", 11111, false, true, "")]
        [DataRow("Not Checking Number - Good Upper Bound Zip", -1, "n", "a", "c", "s", 99999, false, true, "")]
        [DataRow("Not Checking Number - Number is 0", 0, "n", "a", "c", "s", 11111, false, true, "")]
        [DataRow("Not Checking Number - Number is -1", -1, "n", "a", "c", "s", 11111, false, true, "")]
        [TestMethod]
        public void AreAllFieldsFilledIn_provider(
            string testName,
            int number,
            string name,
            string address,
            string city,
            string state,
            int zip,
            bool checkForId,
            bool expectedResult,
            string expectedMessage
            )
        {
            var providerRecord = new ProviderRecord(new Dictionary<string, object>())
            {
                Number = number,
                Name = name,
                Address = address,
                City = city,
                State = state,
                Zip = zip
            };

            var result = _crudValidator.AreAllFieldsFilledIn(providerRecord, checkForId);

            Assert.AreEqual(expectedResult, result.successful);
            Assert.AreEqual(expectedMessage, result.errorMessage);
        }

        #region DoesMemberExistInDatabase

        [TestMethod]
        public void DoesMemberExistInDatabase_SupposedToAndExists() {
            MemberRecord memberRecord = new MemberRecord(new Dictionary<string, object>()) {
                Name = "Alex Burbank",
                Number = 1,
                City = "OC",
                State = "OR",
                Zip = 1,
            };

            _databaseWrapperMock.Setup(c => c.FetchMember(7)).Returns(memberRecord);

            var result = _crudValidator.DoesMemberExistInDatabase(true, 7);

            Assert.AreEqual(true, result.exists);
            Assert.AreEqual("", result.errorMessage);

            _databaseWrapperMock.Verify(c => c.FetchMember(7), Times.Once);

            _databaseWrapperMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void DoesMemberExistInDatabase_SupposedToAndDoesNotExists()
        {
            _databaseWrapperMock.Setup(c => c.FetchMember(7)).Returns((MemberRecord) null);

            var result = _crudValidator.DoesMemberExistInDatabase(true, 7);

            Assert.AreEqual(false, result.exists);
            Assert.AreEqual("Member record does not exist", result.errorMessage);

            _databaseWrapperMock.Verify(c => c.FetchMember(7), Times.Once);

            _databaseWrapperMock.VerifyNoOtherCalls();
        }


        [TestMethod]
        public void DoesMemberExistInDatabase_NotSupposedToAndExist()
        {
            MemberRecord memberRecord = new MemberRecord(new Dictionary<string, object>())
            {
                Name = "Alex Burbank",
                Number = 1,
                City = "OC",
                State = "OR",
                Zip = 1,
            };

            _databaseWrapperMock.Setup(c => c.FetchMemberByName("Blah")).Returns(memberRecord);

            var result = _crudValidator.DoesMemberExistInDatabase(false, 0, "Blah");

            Assert.AreEqual(false, result.exists);
            Assert.AreEqual("Member record already exists", result.errorMessage);

            _databaseWrapperMock.Verify(c => c.FetchMemberByName("Blah"), Times.Once);

            _databaseWrapperMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void DoesMemberExistInDatabase_NotSupposedToAndDoesNotExist()
        {
            _databaseWrapperMock.Setup(c => c.FetchMemberByName("Blah")).Returns((MemberRecord) null);

            var result = _crudValidator.DoesMemberExistInDatabase(false, 0, "Blah");

            Assert.AreEqual(true, result.exists);
            Assert.AreEqual("", result.errorMessage);

            _databaseWrapperMock.Verify(c => c.FetchMemberByName("Blah"), Times.Once);

            _databaseWrapperMock.VerifyNoOtherCalls();
        }

        #endregion DoesMemberExistInDatabase

        #region DoesProviderExistInDatabase

        [TestMethod]
        public void DoesProviderExistInDatabase_SupposedToAndExists()
        {
            ProviderRecord providerRecord = new ProviderRecord(new Dictionary<string, object>())
            {
                Name = "Alex Burbank",
                Number = 1,
                City = "OC",
                State = "OR",
                Zip = 1,
            };

            _databaseWrapperMock.Setup(c => c.FetchProvider(7)).Returns(providerRecord);

            var result = _crudValidator.DoesProviderExistInDatabase(true, 7);

            Assert.AreEqual(true, result.exists);
            Assert.AreEqual("", result.errorMessage);

            _databaseWrapperMock.Verify(c => c.FetchProvider(7), Times.Once);

            _databaseWrapperMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void DoesProviderExistInDatabase_SupposedToAndDoesNotExists()
        {
            _databaseWrapperMock.Setup(c => c.FetchProvider(7)).Returns((ProviderRecord)null);

            var result = _crudValidator.DoesProviderExistInDatabase(true, 7);

            Assert.AreEqual(false, result.exists);
            Assert.AreEqual("Provider record does not exist", result.errorMessage);

            _databaseWrapperMock.Verify(c => c.FetchProvider(7), Times.Once);

            _databaseWrapperMock.VerifyNoOtherCalls();
        }


        [TestMethod]
        public void DoesProviderExistInDatabase_NotSupposedToAndExist()
        {
            ProviderRecord providerRecord = new ProviderRecord(new Dictionary<string, object>())
            {
                Name = "Alex Burbank",
                Number = 1,
                City = "OC",
                State = "OR",
                Zip = 1,
            };

            _databaseWrapperMock.Setup(c => c.FetchProviderByName("Blah")).Returns(providerRecord);

            var result = _crudValidator.DoesProviderExistInDatabase(false, 0, "Blah");

            Assert.AreEqual(false, result.exists);
            Assert.AreEqual("Provider record already exists", result.errorMessage);

            _databaseWrapperMock.Verify(c => c.FetchProviderByName("Blah"), Times.Once);

            _databaseWrapperMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void DoesProviderExistInDatabase_NotSupposedToAndDoesNotExist()
        {
            _databaseWrapperMock.Setup(c => c.FetchProviderByName("Blah")).Returns((ProviderRecord)null);

            var result = _crudValidator.DoesProviderExistInDatabase(false, 0, "Blah");

            Assert.AreEqual(true, result.exists);
            Assert.AreEqual("", result.errorMessage);

            _databaseWrapperMock.Verify(c => c.FetchProviderByName("Blah"), Times.Once);

            _databaseWrapperMock.VerifyNoOtherCalls();
        }

        #endregion DoesProviderExistInDatabase

        [DataRow("1", 1, true, "")]
        [DataRow("0", 0, false, "Needs to have a valid number")]
        [DataRow("-1", -1, false, "Needs to have a valid number")]
        [TestMethod]
        public void RemovalIdValid(
            string testName,
            int number,
            bool expectedResult,
            string expectedMessage
        )
        {
            var result = _crudValidator.RemovalIdValid(number);

            Assert.AreEqual(expectedResult, result.successful);
            Assert.AreEqual(expectedMessage, result.errorMessage);
        }

    }
}
