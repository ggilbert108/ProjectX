﻿using ProjectX.Bnf;

namespace ProjectX.Language
{
    public class identifier : Nonterminal
    {
        public override void SetProduction()
        {
            Production =
                new Sequence(
                    new Character("A-Za-z"),
                    new Optional(
                        new IdentifierTail()));
        }
    }

    public class IdentifierTail : Nonterminal
    {
        public override void SetProduction()
        {
            Production =
                new Sequence(
                    new Character("A-Za-z0-9|_"),
                    new Optional(
                        new IdentifierTail()));
        }
    }
}