using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dflat;


namespace DflatTests
{
    [TestClass]
    public class DflatTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Statement statement = new Statement();
            PrepareResult result = Program.PrepareStatement("insert 1 cstack foo@bar.com", statement);

            Assert.AreEqual(PrepareResult.PREPARE_SUCCESS, result);
            Assert.AreEqual(statement.rowToInsert.id, 1, "Incorrect Id");
            Assert.AreEqual(statement.rowToInsert.username, "cstack", "Incorrect username");
            Assert.AreEqual(statement.rowToInsert.email, "foo@bar.com", "Incorrect email");
        }

        [TestMethod]
        public void InsertNeedsMoreArgs()
        {
            Statement statement = new Statement();
            PrepareResult result = Program.PrepareStatement("insert", statement);

            Assert.AreEqual(PrepareResult.PREPARE_SYNTAX_ERROR, result);
        }

        [TestMethod]
        public void Temp()
        {
            string cat = "cat";
            char[] cat2 = cat.ToCharArray();
        }
    }
}
