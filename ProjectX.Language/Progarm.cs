using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ProjectX.Language
{
    public class Progarm
    {
        public static void Main()
        {
            string code = File.ReadAllText("data/test.txt");

            Console.WriteLine(code + "\n\n");

            Regex whitespace = new Regex("\\s");
            code = whitespace.Replace(code, "");

            class_declaration classDec = new class_declaration();
            Console.WriteLine(classDec.Matches(code));
        }
    }
}