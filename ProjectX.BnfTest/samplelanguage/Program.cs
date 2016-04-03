using ProjectX.Bnf;

namespace ProjectX.BnfTest
{
    public class Program : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new Terminal("PROGRAM"), new WhiteSpacePlus(),
                new Identifier(), new WhiteSpacePlus(),
                new Terminal("BEGIN"), new WhiteSpacePlus(),
                new StatementList(),
                new Terminal("END."));
        }
    }

    public class Statement : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new Assignment(),
                new Terminal(";"), 
                new WhiteSpacePlus());
        }
    }

    public class StatementList : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new Statement(),
                new Optional(new StatementList()));
        }
    }
}