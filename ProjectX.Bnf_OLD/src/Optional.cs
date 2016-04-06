using System;
using System.Collections.Generic;
using ProjectX.Finite;

namespace ProjectX.Bnf
{
    public class Optional : Production
    {
        private Production option;

        public Optional(Production option)
        {
            this.option = option;
        }

        public override Nfa GetStateMachine(Dictionary<Type, Nfa> outer)
        {
            Nfa empty = Nfa.BuildBasic(Nfa.EPS);
            return Nfa.BuildAlternation(empty, option.GetStateMachine(outer));
        }
    }
}