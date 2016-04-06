using System;
using System.Collections.Generic;
using System.Diagnostics;
using ProjectX.Finite;

namespace ProjectX.Bnf
{
    public abstract class Nonterminal : Production
    {
        protected Production Production;
        private readonly Nfa stateMachine;

        protected Nonterminal()
        {
            stateMachine = new Nfa();
        }

        public bool Matches(string code)
        {
            Dictionary<Type, Nfa> outer = new Dictionary<Type, Nfa>();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Nfa nfa = GetStateMachine(outer);
            //nfa.Print();

            stopwatch.Stop();

            stopwatch.Restart();
            bool result = nfa.Simulate(code);
            stopwatch.Stop();

            return result;
        }

        public override Nfa GetStateMachine(Dictionary<Type, Nfa> outer)
        {
            if (outer.ContainsKey(GetType()))
            {
                Nfa parent = outer[GetType()];

                stateMachine.AddTransition(stateMachine.InitialState, stateMachine.FinalState, Nfa.EPS);
                stateMachine.AddTransition(stateMachine.FinalState, parent.InitialState, Nfa.EPS);
                parent.AddTransition(parent.FinalState, stateMachine.FinalState, Nfa.EPS);

                return stateMachine;
            }

            SetProduction();

            outer[GetType()] = stateMachine;
            stateMachine.Encapsulate(Production.GetStateMachine(outer));

            return stateMachine;
        }

        public abstract void SetProduction();
    }
}