using System;

namespace ProjectX.Bnf
{
    public class Terminal : Sequence
    {
        public Terminal(string value)
        {
            foreach (char ch in value)
            {
                AddToSequence(new Character(ch));
            }
        }
    }
}