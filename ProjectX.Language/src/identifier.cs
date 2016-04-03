using ProjectX.Bnf;

namespace ProjectX.Language
{
    public class identifier : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new CharacterClass("[A-Za-z]"),
                new Optional(
                    new IdentifierTail()));
        }
    }

    public class IdentifierTail : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new CharacterClass("[A-Za-z0-9]"),
                new Optional(
                    new IdentifierTail()));
        }
    }
}