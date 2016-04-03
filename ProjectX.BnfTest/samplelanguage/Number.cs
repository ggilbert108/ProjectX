using ProjectX.Bnf;

namespace ProjectX.BnfTest
{
    public class Number : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new Optional(
                    new Terminal("-")),
                new DigitSequence());
        }
    }

    public class DigitSequence : Production
    {
        public override Production GetProduction()
        {
            return new Sequence(
                new Digit(),
                new Optional(
                    new DigitSequence()));
        }
    }
}