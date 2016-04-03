using ProjectX.Bnf;

namespace ProjectX.BnfTest
{
    public class String : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new Terminal("\""),
                new Optional(new InnerString()),
                new Terminal("\""));
        }
    }

    public class InnerString : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new AllCharacters() - '"',
                new Optional(
                    new InnerString()));
        }
    }
}