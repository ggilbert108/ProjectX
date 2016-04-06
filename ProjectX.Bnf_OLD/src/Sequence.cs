using System;
using System.Collections.Generic;
using System.Linq;
using ProjectX.Finite;

namespace ProjectX.Bnf
{
    public class Sequence : Production
    {
        private List<Production> sequence; 

        public Sequence(params Production[] sequence)
        {
            this.sequence = sequence.ToList();
        }

        public override Nfa GetStateMachine(Dictionary<Type, Nfa> outer)
        {
            Nfa result = sequence[0].GetStateMachine(outer);
            for(int i = 1; i < sequence.Count; i++)
            {
                Nfa productionNfa = sequence[i].GetStateMachine(outer);
                result = Nfa.BuildConcatenation(result, productionNfa);
            }

            return result;
        }

        public void AddToSequence(Production production)
        {
            sequence.Add(production);
        }
    }
}