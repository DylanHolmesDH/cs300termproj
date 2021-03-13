using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ProviderTerminal.Tests {
    [TestClass]
    public class TerminalTests {

        private Terminal term;

        [TestInitialize]
        public void Setup() {
            term = new Terminal();
        }

        [TestMethod]
        public void TestGenerateProviderDirectory() {

        }
    }
}
