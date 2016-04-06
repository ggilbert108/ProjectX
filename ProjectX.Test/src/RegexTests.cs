using NUnit.Framework;
using ProjectX.Finite;

namespace ProjectX.Test
{
    [TestFixture]
    public class RegexTests
    {
        [TestCase('a', "a", ExpectedResult = true)]
        [TestCase('b', "a", ExpectedResult = false)]
        [TestCase('a', "b", ExpectedResult = false)]
        [TestCase("A-Z", "C", ExpectedResult = true)]
        [TestCase("A-Z", "c", ExpectedResult = false)]
        public bool TestBasic(object objCharacter, string input)
        {
            Character character = ObjToCharacter(objCharacter);
            StateMachine nfa = StateMachine.BuildBasic(character);
            return nfa.Validate(input);
        }

        [TestCase("a", ExpectedResult = true)]
        [TestCase("e", ExpectedResult = false)]
        public bool TestCharacterClassSubtraction(string input)
        {
            Character az = new Character("a-z") - 'e';
            StateMachine azNfa = StateMachine.BuildBasic(az);

            return azNfa.Validate(input);
        }

        [Test]
        public void TestAlternation()
        {
            StateMachine a = StateMachine.BuildBasic('a');
            StateMachine b = StateMachine.BuildBasic('b');
            StateMachine ab = StateMachine.BuildAlternation(a, b);

            Assert.That(ab.Validate("a"));
            Assert.That(ab.Validate("b"));
            Assert.False(ab.Validate("c"));
        }

        [Test]
        public void TestConcat()
        {
            StateMachine a = StateMachine.BuildBasic('a');
            StateMachine b = StateMachine.BuildBasic('b');
            StateMachine ab = StateMachine.BuildConcatenation(a, b);

            Assert.That(ab.Validate("ab"));
            Assert.False(ab.Validate("a"));
            Assert.False(ab.Validate("b"));
        }

        public Character ObjToCharacter(object objCharacter)
        {
            Character character;
            if (objCharacter is char)
            {
                character = (char)objCharacter;
            }
            else
            {
                character = (string)objCharacter;
            }
            return character;
        }
    }
}