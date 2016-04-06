using System.Collections.Generic;

namespace ProjectX.Finite
{
    public class DfaState
    {
        private Dictionary<char, DfaState> transitions;

        public DfaState()
        {
            transitions = new Dictionary<char, DfaState>();
        }

        public void AddTransition(char input, DfaState to)
        {
            transitions[input] = to;
        }

        public bool HasTransition(char input)
        {
            return transitions.ContainsKey(input);
        }

        public DfaState OnInput(char input)
        {
            return transitions[input];
        }
    }
}