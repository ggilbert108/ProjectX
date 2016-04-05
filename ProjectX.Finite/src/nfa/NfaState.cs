using System;
using System.Collections.Generic;

namespace ProjectX.Finite
{
    public class NfaState
    {
        private static int currentId = 0;
        private MultiMap<char, NfaState> transitions;
        private int id;

        public NfaState()
        {
            transitions = new MultiMap<char, NfaState>();
            id = currentId++;
        }

        public void AddTransition(char input, NfaState to)
        {
            transitions.Add(input, to);
        }

        public List<NfaState> OnInput(char input)
        {
            return transitions[input];
        }

        public void Print()
        {
            HashSet<int> printed = new HashSet<int>();
            Print(printed);
        }

        public void Print(HashSet<int> printed)
        {
            printed.Add(id);
            Console.WriteLine($"State {id}");

            foreach (KeyValuePair<char, NfaState> pair in transitions.Map)
            {
                string toPrint = (pair.Key == '\a') ? "EPS" : "" + pair.Key;
                Console.WriteLine($"\t{toPrint} -> {pair.Value.id}");
            }

            foreach (KeyValuePair<char, NfaState> pair in transitions.Map)
            {
                if (printed.Contains(pair.Value.id))
                    continue;

                pair.Value.Print(printed);
            }
        }
    }
}