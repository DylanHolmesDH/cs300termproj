using ChocAnDatabase;
using ChocAnDatabase.records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ManagerTerminal.Tests {
    [TestClass]
    public class CrudServicesTests {
        private Mock<IDatabaseWrapper> _databaseWrapperMock;
        private Mock<IServicesFactory> _servicesFactoryMock;
        private Mock<ICrudValidator> _crudValidatorMock;
        private Mock<IConverter> _converterMock;
        
        private CrudServices _crudServices;

        [TestInitialize]
        public void Setup()
        {
            _databaseWrapperMock = new Mock<IDatabaseWrapper>();
            _servicesFactoryMock = new Mock<IServicesFactory>();
            _crudValidatorMock = new Mock<ICrudValidator>();
            _converterMock = new Mock<IConverter>();

            _crudServices = new CrudServices();
        }

        [DataRow("Option to add memeber", 4, TypeOfCrudAction.AddMember)]
        [DataRow("Option to add memeber", 5, TypeOfCrudAction.AddProvider)]
        [DataRow("Option to add memeber", 6, TypeOfCrudAction.UpdateMember)]
        [DataRow("Option to add memeber", 7, TypeOfCrudAction.UpdateProvider)]
        [DataRow("Option to add memeber", 8, TypeOfCrudAction.RemoveMember)]
        [DataRow("Option to add memeber", 9, TypeOfCrudAction.RemoveProvider)]
        [DataRow("Option to add memeber", 0, TypeOfCrudAction.Unknown)]
        [TestMethod]
        public void DetermineTypeOfCrudAction(
            string testName,
            int optionNumber,
            TypeOfCrudAction expectedResult
            )
        {
            var result = _crudServices.DetermineTypeOfCrudAction(optionNumber);

            Assert.IsInstanceOfType(result, typeof(TypeOfCrudAction));
            Assert.AreEqual(expectedResult, result);
        }

        [DataRow("Unknown report", TypeOfCrudAction.Unknown, 0, "Alex Burbank", false, false, true, "", 1, false, "", 1, 0, false, "Unknown")]
        [DataRow("Unknown report", (TypeOfCrudAction)10, 0, "Alex Burbank", false, false, true, "", 1, false, "", 1, 0, false, "No such action")]
        
        [DataRow("Add member record successful", TypeOfCrudAction.AddMember, 0, "Alex Burbank", false, false, true, "", 1, false, "", 1, 1, true, "")]
        [DataRow("Add member record invalid - Not all fields filled", TypeOfCrudAction.AddMember, 0, "Alex Burbank", false, false, false, "Needs to have a valid number", 0, false, "", 0, 0, false, "Needs to have a valid number")]
        [DataRow("Add member record invalid - already exists in the database", TypeOfCrudAction.AddMember, 0, "Alex Burbank", false, false, true, "", 1, true, "Blah", 0, 0, false, "Blah")]
        [DataRow("Add provider record successful", TypeOfCrudAction.AddProvider, 0, "Alex Burbank", false, false, true, "", 1, false, "", 1, 1, true, "")]
        [DataRow("Add provider record invalid - Not all fields filled", TypeOfCrudAction.AddProvider, 0, "Alex Burbank", false, false, false, "Needs to have a valid number", 0, false, "", 0, 0, false, "Needs to have a valid number")]
        [DataRow("Add provider record invalid - already exists in the database", TypeOfCrudAction.AddProvider, 0, "Alex Burbank", false, false, true, "", 1, true, "Blah", 0, 0, false, "Blah")]

        [DataRow("Update member record successful", TypeOfCrudAction.UpdateMember, 7, "Alex Burbank", true, true, true, "", 1, true, "", 1, 1, true, "")]
        [DataRow("Update member record invalid - Not all fields filled", TypeOfCrudAction.UpdateMember, 7, "Alex Burbank", true, true, false, "Needs to have a valid number", 0, true, "", 0, 0, false, "Needs to have a valid number")]
        [DataRow("Update member record invalid - Does not exist in the database", TypeOfCrudAction.UpdateMember, 7, "Alex Burbank", true, true, true, "", 1, false, "Blah", 0, 0, false, "Blah")]
        [DataRow("Update provider record successful", TypeOfCrudAction.UpdateProvider, 7, "Alex Burbank", true, true, true, "", 1, true, "", 1, 1, true, "")]
        [DataRow("Update provider record invalid - Not all fields filled", TypeOfCrudAction.UpdateProvider, 7, "Alex Burbank", true, true, false, "Needs to have a valid number", 0, true, "", 0, 0, false, "Needs to have a valid number")]
        [DataRow("Update provider record invalid - Does not exist in the database", TypeOfCrudAction.UpdateProvider, 7, "Alex Burbank", true, true, true, "", 1, false, "Blah", 0, 0, false, "Blah")]

        [DataRow("Remove member record successful", TypeOfCrudAction.RemoveMember, 7, "Alex Burbank", true, true, true, "", 1, true, "", 1, 1, true, "")]
        [DataRow("Remove member record invalid - Not all fields filled", TypeOfCrudAction.RemoveMember, 7, "Alex Burbank", true, true, false, "Needs to have a valid number", 0, true, "", 0, 0, false, "Needs to have a valid number")]
        [DataRow("Remove member record invalid - Does not exist in the database", TypeOfCrudAction.RemoveMember, 7, "Alex Burbank", true, true, true, "", 1, false, "Blah", 0, 0, false, "Blah")]
        [DataRow("Remove provider record successful", TypeOfCrudAction.RemoveProvider, 7, "Alex Burbank", true, true, true, "", 1, true, "", 1, 1, true, "")]
        [DataRow("Remove provider record invalid - Not all fields filled", TypeOfCrudAction.RemoveProvider, 7, "Alex Burbank", true, true, false, "Needs to have a valid number", 0, true, "", 0, 0, false, "Needs to have a valid number")]
        [DataRow("Remove provider record invalid - Does not exist in the database", TypeOfCrudAction.RemoveProvider, 7, "Alex Burbank", true, true, true, "", 1, false, "Blah", 0, 0, false, "Blah")]
        [TestMethod]
        public void DoCrudAction(
            string testName,
            TypeOfCrudAction typeOfCrudAction,
            int number,
            string name,
            bool checkForId,
            bool shouldExist,
            bool expectedResultAreAllFieldsFilledIn,
            string expectedMessageAreAllFieldsFilledIn,
            int numberOfTimesDoesExistInDatabase,
            bool expectedResultDoesMemberExistInDatabase,
            string expectedMessageDoesMemberExistInDatabase,
            int numberOfTimesAction,
            int numberOfTimesSave,
            bool expectedResult,
            string expectedMessage
            )
        {
            UserInterfaceRecord userInterfaceRecord = new UserInterfaceRecord() {
                Name = name,
                Number = number,
            };

            var memberRecord = new MemberRecord(new Dictionary<string, object>()) {
                Name = name,
                Number = number,
            };

            var providerRecord = new ProviderRecord(new Dictionary<string, object>()) {
                Name = name,
                Number = number,
            };

            _servicesFactoryMock.Setup(c => c.CreateCrudValidator(_databaseWrapperMock.Object)).Returns(_crudValidatorMock.Object);
            _servicesFactoryMock.Setup(c => c.CreateConverter()).Returns(_converterMock.Object);

            var areAllFieldsFilledIn = (expectedResultAreAllFieldsFilledIn, expectedMessageAreAllFieldsFilledIn);
            var doesMemberExistInDatabase = (expectedResultDoesMemberExistInDatabase, expectedMessageDoesMemberExistInDatabase);

            switch (typeOfCrudAction) {
                case TypeOfCrudAction.AddMember:
                    _converterMock.Setup(c => c.ConvertRecordToMemberRecord(userInterfaceRecord)).Returns(memberRecord);
                    _crudValidatorMock.Setup(c => c.AreAllFieldsFilledIn(memberRecord, checkForId)).Returns(areAllFieldsFilledIn);
                    _crudValidatorMock.Setup(c => c.DoesMemberExistInDatabase(shouldExist, number, name)).Returns(doesMemberExistInDatabase);
                    _databaseWrapperMock.Setup(c => c.AddMember(memberRecord));
                    break;
                case TypeOfCrudAction.AddProvider:
                    _converterMock.Setup(c => c.ConvertRecordToProviderRecord(userInterfaceRecord)).Returns(providerRecord);
                    _crudValidatorMock.Setup(c => c.AreAllFieldsFilledIn(providerRecord, checkForId)).Returns(areAllFieldsFilledIn);
                    _crudValidatorMock.Setup(c => c.DoesProviderExistInDatabase(shouldExist, number, name)).Returns(doesMemberExistInDatabase);
                    _databaseWrapperMock.Setup(c => c.AddProvider(providerRecord));
                    break;
                case TypeOfCrudAction.UpdateMember:
                    _converterMock.Setup(c => c.ConvertRecordToMemberRecord(userInterfaceRecord)).Returns(memberRecord);
                    _crudValidatorMock.Setup(c => c.AreAllFieldsFilledIn(memberRecord, checkForId)).Returns(areAllFieldsFilledIn);
                    _crudValidatorMock.Setup(c => c.DoesMemberExistInDatabase(shouldExist, number, "")).Returns(doesMemberExistInDatabase);
                    _databaseWrapperMock.Setup(c => c.UpdateMember(memberRecord));
                    break;
                case TypeOfCrudAction.UpdateProvider:
                    _converterMock.Setup(c => c.ConvertRecordToProviderRecord(userInterfaceRecord)).Returns(providerRecord);
                    _crudValidatorMock.Setup(c => c.AreAllFieldsFilledIn(providerRecord, checkForId)).Returns(areAllFieldsFilledIn);
                    _crudValidatorMock.Setup(c => c.DoesProviderExistInDatabase(shouldExist, number, "")).Returns(doesMemberExistInDatabase);
                    _databaseWrapperMock.Setup(c => c.UpdateProvider(providerRecord));
                    break;
                case TypeOfCrudAction.RemoveMember:
                    _crudValidatorMock.Setup(c => c.RemovalIdValid(number)).Returns(areAllFieldsFilledIn);
                    _crudValidatorMock.Setup(c => c.DoesMemberExistInDatabase(shouldExist, number, "")).Returns(doesMemberExistInDatabase);
                    _databaseWrapperMock.Setup(c => c.RemoveMember(number));
                    break;
                case TypeOfCrudAction.RemoveProvider:
                    _crudValidatorMock.Setup(c => c.RemovalIdValid(number)).Returns(areAllFieldsFilledIn);
                    _crudValidatorMock.Setup(c => c.DoesProviderExistInDatabase(shouldExist, number, "")).Returns(doesMemberExistInDatabase);
                    _databaseWrapperMock.Setup(c => c.RemoveProvider(number));
                    break;
                default:
                    break;
            }

            _databaseWrapperMock.Setup(c => c.Save());

            var result = _crudServices.DoCrudAction(_databaseWrapperMock.Object, typeOfCrudAction, userInterfaceRecord, _servicesFactoryMock.Object);

            Assert.AreEqual(expectedResult, result.successful);
            Assert.AreEqual(expectedMessage, result.errorMessage);

            _servicesFactoryMock.Verify(c => c.CreateCrudValidator(_databaseWrapperMock.Object), Times.Once);
            _servicesFactoryMock.Verify(c => c.CreateConverter(), Times.Once);

            switch (typeOfCrudAction) {
                case TypeOfCrudAction.AddMember:
                    _converterMock.Verify(c => c.ConvertRecordToMemberRecord(userInterfaceRecord), Times.Once);
                    _crudValidatorMock.Verify(c => c.AreAllFieldsFilledIn(memberRecord, checkForId), Times.Once);
                    _crudValidatorMock.Verify(c => c.DoesMemberExistInDatabase(shouldExist, number, name), Times.Exactly(numberOfTimesDoesExistInDatabase));
                    _databaseWrapperMock.Verify(c => c.AddMember(memberRecord), Times.Exactly(numberOfTimesAction));
                    break;
                case TypeOfCrudAction.AddProvider:
                    _converterMock.Verify(c => c.ConvertRecordToProviderRecord(userInterfaceRecord), Times.Once);
                    _crudValidatorMock.Verify(c => c.AreAllFieldsFilledIn(providerRecord, checkForId), Times.Once);
                    _crudValidatorMock.Verify(c => c.DoesProviderExistInDatabase(shouldExist, number, name), Times.Exactly(numberOfTimesDoesExistInDatabase));
                    _databaseWrapperMock.Verify(c => c.AddProvider(providerRecord), Times.Exactly(numberOfTimesAction));
                    break;
                case TypeOfCrudAction.UpdateMember:
                    _converterMock.Verify(c => c.ConvertRecordToMemberRecord(userInterfaceRecord), Times.Once);
                    _crudValidatorMock.Verify(c => c.AreAllFieldsFilledIn(memberRecord, checkForId), Times.Once);
                    _crudValidatorMock.Verify(c => c.DoesMemberExistInDatabase(shouldExist, number, ""), Times.Exactly(numberOfTimesDoesExistInDatabase));
                    _databaseWrapperMock.Verify(c => c.UpdateMember(memberRecord), Times.Exactly(numberOfTimesAction));
                    break;
                case TypeOfCrudAction.UpdateProvider:
                    _converterMock.Verify(c => c.ConvertRecordToProviderRecord(userInterfaceRecord), Times.Once);
                    _crudValidatorMock.Verify(c => c.AreAllFieldsFilledIn(providerRecord, checkForId), Times.Once);
                    _crudValidatorMock.Verify(c => c.DoesProviderExistInDatabase(shouldExist, number, ""), Times.Exactly(numberOfTimesDoesExistInDatabase));
                    _databaseWrapperMock.Verify(c => c.UpdateProvider(providerRecord), Times.Exactly(numberOfTimesAction));
                    break;
                case TypeOfCrudAction.RemoveMember:
                    _crudValidatorMock.Verify(c => c.RemovalIdValid(number), Times.Once);
                    _crudValidatorMock.Verify(c => c.DoesMemberExistInDatabase(shouldExist, number, ""), Times.Exactly(numberOfTimesDoesExistInDatabase));
                    _databaseWrapperMock.Verify(c => c.RemoveMember(number), Times.Exactly(numberOfTimesAction));
                    break;
                case TypeOfCrudAction.RemoveProvider:
                    _crudValidatorMock.Verify(c => c.RemovalIdValid(number), Times.Once);
                    _crudValidatorMock.Verify(c => c.DoesProviderExistInDatabase(shouldExist, number, ""), Times.Exactly(numberOfTimesDoesExistInDatabase));
                    _databaseWrapperMock.Verify(c => c.RemoveProvider(number), Times.Exactly(numberOfTimesAction));
                    break;
                default:
                    break;
            }

            _databaseWrapperMock.Verify(c => c.Save(), Times.Exactly(numberOfTimesSave));

            _servicesFactoryMock.VerifyNoOtherCalls();
            _converterMock.VerifyNoOtherCalls();
            _crudValidatorMock.VerifyNoOtherCalls();
            _databaseWrapperMock.VerifyNoOtherCalls();
        }
    }
}
