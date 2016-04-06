using System.Collections.Generic;
using System.Linq;

namespace ProjectX.Finite
{
    public class Dfa
    {
        private DfaState start;
        private HashSet<DfaState> accepting;

        public Dfa()
        {
            start = new DfaState();
            accepting = new HashSet<DfaState>();
        }

        public void AddTransition(DfaState from, DfaState to, char input)
        {
            from.AddTransition(input, to);
        }

        public bool Simulate(string input)
        {
            DfaState currentState = start;

            foreach (char currentInput in input)
            {
                if (!currentState.HasTransition(currentInput))
                    return false;

                currentState = currentState.OnInput(currentInput);
            }

            return accepting.Contains(currentState);
        }

        public static Dfa SubsetConstruction(Nfa nfa)
        {
            Dfa result = new Dfa();

            HashSet<StateSet> markedStates = new HashSet<StateSet>();
            HashSet<StateSet> unmarkedStates = new HashSet<StateSet>();

            Dictionary<StateSet, DfaState> dfaStates = new Dictionary<StateSet, DfaState>();

            StateSet nfaInitial = new StateSet();
            nfaInitial.Add(nfa.InitialState);

            StateSet first = Nfa.GetEpsClosure(nfaInitial);
            unmarkedStates.Add(first);

            DfaState dfaInitial = new DfaState();
            dfaStates[first] = dfaInitial;
            result.start = dfaInitial;

            while (unmarkedStates.Count > 0)
            {
                StateSet state = unmarkedStates.First();
                unmarkedStates.Remove(state);
                markedStates.Add(state);

                if (state.Contains(nfa.FinalState))
                {
                    result.accepting.Add(dfaStates[state]);
                }

                foreach (char input in nfa.Inputs)
                {
                    StateSet next = Nfa.GetEpsClosure(Nfa.Move(state, input));

                    if (next.Count == 0)
                        continue;

                    if (!unmarkedStates.Contains(next) &&
                        !markedStates.Contains(next))
                    {
                        unmarkedStates.Add(next);
                        dfaStates[next] = new DfaState();
                    }

                    result.AddTransition(dfaStates[state], dfaStates[next], input);
                }
            }

            return result;
        }
    }
}