using System;
using ProjectX.Bnf;

namespace ProjectX.Language
{
    public class Program
    {
        public static void Main()
        {
            Nonterminal nonterminal = new iteration_statement();
            Console.WriteLine(nonterminal.Matches("w3hile(3abcde)"));
        }
    }
}