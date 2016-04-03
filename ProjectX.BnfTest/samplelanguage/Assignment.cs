using ProjectX.Bnf;

namespace ProjectX.BnfTest
{
    public class Assignment : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new Identifier(),
                new Terminal(":="),
                new Choice(
                    new Number(),
                    new Identifier(),
                    new String()));
        }
    }
}