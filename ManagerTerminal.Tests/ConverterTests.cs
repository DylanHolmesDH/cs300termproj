using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ManagerTerminal.Tests {
    [TestClass]
    public class ConverterTests {
        private Converter _converter;
        private UserInterfaceRecord _userInterfaceRecord;
        private int _nextIdAvailable;

        [TestInitialize]
        public void Setup() {
            _converter = new Converter();

            _userInterfaceRecord = new UserInterfaceRecord() {
                Name = "Alex Burbank",
                Address = "111 Blah st.",
                City = "New York City",
                State = "New York",
                Zip = 111
            };

            _nextIdAvailable = 7;
        }

        [TestMethod]
        public void ConvertRecordToMemberRecord() {
            var result = _converter.ConvertRecordToMemberRecord(_userInterfaceRecord);

            Assert.AreEqual("Alex Burbank", result.Name);
            Assert.AreEqual("111 Blah st.", result.Address);
            Assert.AreEqual("New York City", result.City);
            Assert.AreEqual("New York", result.State);
            Assert.AreEqual(111, result.Zip);
        }

        [TestMethod]
        public void ConvertRecordToProviderRecord() {
            var result = _converter.ConvertRecordToProviderRecord(_userInterfaceRecord);

            Assert.AreEqual("Alex Burbank", result.Name);
            Assert.AreEqual("111 Blah st.", result.Address);
            Assert.AreEqual("New York City", result.City);
            Assert.AreEqual("New York", result.State);
            Assert.AreEqual(111, result.Zip);
        }
    }
}
