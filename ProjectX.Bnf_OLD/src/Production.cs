using System;
using System.Collections.Generic;
using ProjectX.Finite;

namespace ProjectX.Bnf
{
    public abstract class Production
    {
        public abstract Nfa GetStateMachine(Dictionary<Type, Nfa> outer);
    }
}