using ProjectX.Bnf;

namespace ProjectX.BnfTest
{
    public class WhiteSpacePlus : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new WhiteSpace(),
                new Optional(
                    new WhiteSpacePlus()));
        }
    }
}