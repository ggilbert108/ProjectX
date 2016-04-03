using ProjectX.Bnf;

namespace ProjectX.BnfTest
{
    public class AlphaCharacter : CharacterClass
    {
        public AlphaCharacter() : base("[A-Z]")
        {
        }
    }
}