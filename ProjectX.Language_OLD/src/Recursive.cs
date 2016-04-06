using ProjectX.Bnf;

namespace ProjectX.Language
{
    public class Parent : Nonterminal
    {
        public override void SetProduction()
        {
            Production =
                new Child();
        }
    }

    public class Child : Nonterminal
    {
        public override void SetProduction()
        {
            Production = new Sequence(new Wrapper(), new Equiv());
        }
    }

    public class Wrapper : Nonterminal
    {
        public override void SetProduction()
        {
            Production = new Parent();
        }
    }

    public class Equiv : Nonterminal
    {
        public override void SetProduction()
        {
            Production = new Parent();
        }
    }

}