using System;
using System.Text.RegularExpressions;
using ProjectX.Bnf;
using ProjectX.Language;

namespace ProjectX.Test
{
    public class Program
    {
        public static void Main()
        {
            ParserTests tests = new ParserTests();
            Console.WriteLine(tests.TestProgramValidity("simple.txt"));
        } 
    }
}