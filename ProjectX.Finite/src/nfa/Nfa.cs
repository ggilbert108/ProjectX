using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectX.Finite
{
    public class Nfa
    {
        public const char EPS = '\a';

        public readonly NfaState InitialState;
        public readonly NfaState FinalState;

        public HashSet<char> Inputs { get; private set; }

        public Nfa()
        {
            InitialState = new NfaState();
            FinalState = new NfaState();

            Inputs = new HashSet<char>();
        }

        public void AddTransition(NfaState from, NfaState to, char input)
        {
            if(input != EPS)
                Inputs.Add(input);
            from.AddTransition(input, to);
        }

        public void AddInputsFromNfa(Nfa other)
        {
            foreach (char input in other.Inputs)
            {
                Inputs.Add(input);
            }
        }

        public bool Simulate(string input)
        {
            StateSet currentStates = new StateSet();
            currentStates.Add(InitialState);

            currentStates.AddRange(GetEpsClosureMinusSelf(currentStates));

            foreach (char inputChar in input)
            {
                if (!Inputs.Contains(inputChar))
                {
                    return false;
                }

                currentStates = Move(currentStates, inputChar);
                currentStates.AddRange(GetEpsClosureMinusSelf(currentStates));

            }

            currentStates.AddRange(GetEpsClosureMinusSelf(currentStates));

            return currentStates.Contains(FinalState);
        }

        public void Encapsulate(Nfa nfa)
        {
            AddInputsFromNfa(nfa);

            AddTransition(InitialState, nfa.InitialState, EPS);
            AddTransition(nfa.FinalState, FinalState, EPS);
        }

        public void Print()
        {
            InitialState.Print();
        }

        #region Subset Construction Helpers

        public static StateSet GetEpsClosure(StateSet fromSet)
        {
            StateSet closure = new StateSet();
            closure.AddRange(fromSet);

            Stack<NfaState> stack = new Stack<NfaState>();
            foreach (NfaState state in fromSet)
            {
                stack.Push(state);
            }

            while (stack.Count > 0)
            {
                NfaState state = stack.Pop();

                List<NfaState> adjacents = state.OnInput(EPS);
                foreach (NfaState adjacent in adjacents)
                {
                    if (!closure.Contains(adjacent))
                    {
                        closure.Add(adjacent);
                        stack.Push(adjacent);
                    }
                }
            }
            return closure;
        }

        public static StateSet GetEpsClosureMinusSelf(StateSet fromSet)
        {
            StateSet closure = GetEpsClosure(fromSet);
            foreach (NfaState state in fromSet)
            {
                closure.Remove(state);
            }
            return closure;
        }

        public static StateSet Move(StateSet fromSet, char input)
        {
            StateSet result = new StateSet();

            foreach (NfaState from in fromSet)
            {
                result.AddRange(from.OnInput(input));
            }

            return result;
        }

        #endregion

        #region Builders

        public static Nfa BuildBasic(char ch)
        {
            Nfa basic = new Nfa();
            basic.AddTransition(basic.InitialState, basic.FinalState, ch);
            return basic;
        }

        public static Nfa BuildAlternation(Nfa a, Nfa b)
        {
            Nfa alternation = new Nfa();

            alternation.AddInputsFromNfa(a);
            alternation.AddInputsFromNfa(b);

            alternation.AddTransition(alternation.InitialState, a.InitialState, EPS);
            alternation.AddTransition(alternation.InitialState, b.InitialState, EPS);

            alternation.AddTransition(a.FinalState, alternation.FinalState, EPS);
            alternation.AddTransition(b.FinalState, alternation.FinalState, EPS);

            return alternation;
        }

        public static Nfa BuildAlternation(List<Nfa> nfas)
        {
            Nfa alternation = new Nfa();

            foreach (Nfa nfa in nfas)
            {
                alternation.AddInputsFromNfa(nfa);

                alternation.AddTransition(alternation.InitialState, nfa.InitialState, EPS);
                alternation.AddTransition(nfa.FinalState, alternation.FinalState, EPS);
            }

            return alternation;
        }

        public static Nfa BuildConcatenation(Nfa a, Nfa b)
        {
            Nfa concat = new Nfa();

            concat.AddInputsFromNfa(a);
            concat.AddInputsFromNfa(b);

            concat.AddTransition(concat.InitialState, a.InitialState, EPS);
            concat.AddTransition(a.FinalState, b.InitialState, EPS);
            concat.AddTransition(b.FinalState, concat.FinalState, EPS);

            return concat;
        }

        public static Nfa BuildStar(Nfa nfa)
        {
            Nfa star = new Nfa();

            star.AddInputsFromNfa(nfa);

            star.AddTransition(star.InitialState, nfa.InitialState, EPS);
            star.AddTransition(star.InitialState, star.FinalState, EPS);
            star.AddTransition(nfa.FinalState, nfa.InitialState, EPS);
            star.AddTransition(nfa.FinalState, star.FinalState, EPS);

            return star;
        }

        #endregion
    }
}