using System;
using System.Collections.Generic;
using System.Linq;
using ProjectX.Finite;

namespace ProjectX.Bnf
{
    public class Choice : Production
    {
        private List<Production> choices;

        public Choice(params Production[] choices)
        {
            this.choices = choices.ToList();
        }

        public Choice(Choice other)
        {
            choices = new List<Production>();
            foreach (Production choice in other.choices)
            {
                choices.Add(choice);
            }
        }

        public override Nfa GetStateMachine(Dictionary<Type, NfaState> outer)
        {
            List<Nfa> choiceNfas = new List<Nfa>();
            foreach (Production production in choices)
            {
                choiceNfas.Add(production.GetStateMachine(outer));
            }

            Nfa result = Nfa.BuildAlternation(choiceNfas);
            return result;
        }

        public void AddChoice(Production choice)
        {
            choices.Add(choice);
        }

        public void RemoveChoice(Production choice)
        {
            choices.Remove(choice);
        }
    }
}