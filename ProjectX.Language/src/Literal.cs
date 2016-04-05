using System;
using System.Collections.Generic;
using ProjectX.Bnf;
using ProjectX.Finite;

namespace ProjectX.Language
{
    public class literal : Nonterminal
    {
        public override void SetProduction()
        {
            Production = 
                new Choice(
                    new integer_literal(),
                    new RealLiteral(),
                    new BooleanLiteral(),
                    new string_literal());
        }
    }

    public class integer_literal : Nonterminal
    {
        public override void SetProduction()
        {
            Production = 
                new Sequence(
                    new Optional(
                        new Character('-')),
                    new Number());
        }
    }

    public class RealLiteral : Nonterminal
    {
        public override void SetProduction()
        {
            Production =
                new Sequence(
                    new Optional(
                        new Character('-')),
                    new Number(),
                    new Optional(
                        new Sequence(
                            new Character('.'),
                            new Number())));
        }
    }

    public class Number : Nonterminal
    {
        public override void SetProduction()
        {
            Production = 
                new Sequence(
                    new CharacterClass("[0-9]"),
                    new Optional(
                        new Number()));
        }
    }

    public class BooleanLiteral : Nonterminal
    {
        public override void SetProduction()
        {
            Production =
                new Choice(
                    new Terminal("true"),
                    new Terminal("false"));
        }
    }

    public class string_literal : Nonterminal
    {
        public override void SetProduction()
        {
            Production = 
                new Sequence(
                    new Terminal("\""),
                    new StringChars(),
                    new Terminal("\""));
        }
    }

    public class StringChars : Nonterminal
    {
        public override void SetProduction()
        {
            Production = 
                new Sequence(
                    new CharacterClass(".") - '"',
                    new Optional(
                        new StringChars()));
        }
    }
}