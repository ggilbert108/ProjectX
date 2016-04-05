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
            Dictionary<Type, NfaState> outer = new Dictionary<Type, NfaState>();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Nfa nfa = GetStateMachine(outer);            

            stopwatch.Stop();

            stopwatch.Restart();
            bool result = nfa.Simulate(code);
            stopwatch.Stop();

            return result;
        }

        public override Nfa GetStateMachine(Dictionary<Type, NfaState> outer)
        {
            if (outer.ContainsKey(GetType()))
            {
                Console.WriteLine("recursion found: " + GetType().Name);
                Nfa empty = Nfa.BuildBasic(Nfa.EPS);
                NfaState parent = outer[GetType()];
                empty.AddTransition(empty.FinalState, parent, Nfa.EPS);

                stateMachine.Encapsulate(empty);
                return stateMachine;
            }

            SetProduction();

            outer[GetType()] = stateMachine.InitialState;
            stateMachine.Encapsulate(Production.GetStateMachine(outer));
            outer.Remove(GetType());

            return stateMachine;
        }

        public abstract void SetProduction();
    }
}