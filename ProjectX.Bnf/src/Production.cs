using System;
using ProjectX.Finite;

namespace ProjectX.Bnf
{
    public abstract class Production
    {
        public bool Validate(string code)
        {
            StateMachine nfa = GetStateMachine();
            //nfa.Print();
            return nfa.Validate(code);
        }

        public abstract StateMachine GetStateMachine();
    }
}