using System;
using ProjectX.Bnf;

namespace ProjectX.Language
{
    public class Program
    {
        public static void Main()
        {
            Nonterminal nonterminal = new ptr_operator();
            Console.WriteLine(nonterminal.Matches("declarator"));
        }
    }
}