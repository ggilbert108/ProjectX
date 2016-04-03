using NUnit.Framework;

namespace ProjectX.BnfTest.tests
{
    [TestFixture]
    public class BnfTests
    {
        [TestCase("", ExpectedResult = false)]
        [TestCase(" ", ExpectedResult = true)]
        [TestCase("  ", ExpectedResult = false)]
        public bool TestWhiteSpace(string code)
        {
            WhiteSpace whitespace = new WhiteSpace();
            return whitespace.Matches(code);
        }

        [TestCase("", ExpectedResult = false)]
        [TestCase(" ", ExpectedResult = true)]
        [TestCase("  ", ExpectedResult = true)]
        public bool TestWhiteSpacePlus(string code)
        {
            WhiteSpacePlus whitespace = new WhiteSpacePlus();
            return whitespace.Matches(code);
        }

        [TestCase("1234", ExpectedResult = true)]
        [TestCase("-1234", ExpectedResult = true)]
        [TestCase("1-234", ExpectedResult = false)]
        [TestCase("1a234", ExpectedResult = false)]
        public bool TestNumber(string code)
        {
            Number number = new Number();
            return number.Matches(code);
        }

        [TestCase("\"abc\"", ExpectedResult = true)]
        [TestCase("\"\"", ExpectedResult = true)]
        [TestCase("\"abc", ExpectedResult = false)]
        [TestCase("abc\"", ExpectedResult = false)]
        [TestCase("a\"bc", ExpectedResult = false)]
        public bool TestString(string code)
        {
            String str = new String();
            return str.Matches(code);
        }


        [TestCase("ABC", ExpectedResult = true)]
        [TestCase("ABC1", ExpectedResult = true)]
        [TestCase("1A", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("abc", ExpectedResult = false)]
        public bool TestIdentifier(string code)
        {
            Identifier identifier = new Identifier();
            return identifier.Matches(code);
        }

        [TestCase("A:=3", ExpectedResult = true)]
        [TestCase("A:=3;", ExpectedResult = false)]
        [TestCase("a:=3", ExpectedResult = false)]
        [TestCase("A:=-10023", ExpectedResult = true)]
        [TestCase("A:=\"Hello\"", ExpectedResult = true)]
        public bool TestAssignment(string code)
        {
            Assignment assignment = new Assignment();
            return assignment.Matches(code);
        }

        [Test]
        public void TestProgram()
        {
            Program program = new Program();

            string code = "PROGRAM DEMO1 " +
                          "BEGIN " +
                          "   A:=3;" +
                          "   B:=45;" +
                          "   H:=100023;" +
                          "   C:=A;" +
                          "   D123:=B34A;" +
                          "   BABOON:=GIRAFFE;" +
                          "   TEXT:=\"Hello World\"; " +
                          "END.";

            Assert.That(program.Matches(code, true));
        }

        public void TestChoiceInsideOfOptional()
        {
            
        }
    }
}