using System;
using System.Collections.Generic;
using ProjectX.Finite;

namespace ProjectX.Bnf
{
    public abstract class Nonterminal : Production
    {
        protected Production Production;
        private readonly StateMachine nfa;

        private static Dictionary<Type, StateMachine> created = new Dictionary<Type, StateMachine>(); 

        protected Nonterminal()
        {
            string typeName = GetType().Name;
            nfa = new StateMachine($"{typeName}:begin", $"{typeName}:end");
        }

        public override StateMachine GetStateMachine()
        {
            if (created.ContainsKey(GetType()))
            {
                StateMachine parent = created[GetType()];

                //setup channel through which the parent feeds back to the child
                int channel = nfa.InitialState.SetAddChannel();
                nfa.AddTransition(nfa.InitialState, parent.InitialState, StateMachine.EPS);
                nfa.AddTransition(parent.FinalState, nfa.FinalState, StateMachine.EPS, channel);
            }
            else
            {
                created[GetType()] = nfa;
                
                SetProduction();
                StateMachine inner = Production.GetStateMachine();

                nfa.Encapsulate(inner);
            }

            return nfa;
        }

        public abstract void SetProduction();
    }
}