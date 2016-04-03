using ProjectX.Bnf;

namespace ProjectX.Language
{
    public class literal : Production
    {
        public override Production GetProduction()
        {
            return new Choice(
                new IntLiteral(),
                new BooleanLiteral(),
                new RealLiteral(),
                new CharacterLiteral(),
                new StringLiteral(),
                new NullLiteral());
        }
    }

    public class IntLiteral : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new Optional(
                    new Terminal("-")),
                new Number());
        }
    }

    public class Number : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new CharacterClass("[0-9]"),
                new Optional(
                    new Number()));
        }
    }

    public class BooleanLiteral : Production
    {
        public override Production GetProduction()
        {
            return new Choice(
                new Terminal("true"),
                new Terminal("false"));
        }
    }

    public class RealLiteral : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new Number(),
                new Optional(
                    new Sequence(
                        new Terminal("."),
                        new Number())));
        }
    }

    public class CharacterLiteral : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new Terminal("'"),
                new CharacterClass("."),
                new Terminal("'"));
        }
    }

    public class StringLiteral : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new Terminal("\""),
                new Optional(
                    new InsideString()),
                new Terminal("\""));
        }
    }

    public class InsideString : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new CharacterClass(".") - '"',
                new Optional(
                    new InsideString()));
        }
    }

    public class NullLiteral : Production
    {
        public override Production GetProduction()
        {
            return new Terminal("null");
        }
    }
}