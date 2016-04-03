using ProjectX.Bnf;

namespace ProjectX.BnfTest
{
    public class Digit : CharacterClass
    {
        public Digit() : base("[0-9]")
        {
        }
    }
}