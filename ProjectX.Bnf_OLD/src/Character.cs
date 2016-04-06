using System;
using System.Collections.Generic;
using ProjectX.Finite;

namespace ProjectX.Bnf
{
    public class Character : Production
    {
        private readonly char character;

        public Character(char character)
        {
            this.character = character;
        }

        public override Nfa GetStateMachine(Dictionary<Type, Nfa> outer)
        {
            Nfa result = Nfa.BuildBasic(character);
            return Nfa.BuildBasic(character);
        }

        public override int GetHashCode()
        {
            return character.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == GetHashCode();
        }
    }
}