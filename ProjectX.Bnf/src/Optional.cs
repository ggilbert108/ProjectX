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

        public override Nfa GetStateMachine(Dictionary<Type, NfaState> outer)
        {
            Nfa empty = Nfa.BuildBasic(Nfa.EPS);
            return Nfa.BuildStar(option.GetStateMachine(outer));
        }
    }
}