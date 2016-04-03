using ProjectX.Bnf;

namespace ProjectX.BnfTest
{
    public class Identifier : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new AlphaCharacter(),
                new Optional(
                    new IdentifierTail()));
        }
    }

    public class IdentifierTail : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new Choice(
                    new AlphaCharacter(),
                    new Digit()),
                new Optional(
                    new IdentifierTail()));
        }
    }
}