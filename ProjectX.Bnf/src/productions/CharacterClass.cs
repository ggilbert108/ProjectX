using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProjectX.Bnf
{
    public class CharacterClass : Production
    {
        private string pattern;
        private List<char> exceptions;  

        public CharacterClass(string pattern)
        {
            this.pattern = pattern;
            exceptions = new List<char>();
        }

        public override bool Matches(string code, ref int startIndex, ref string log, int tab)
        {
            log += GetTabbedString("Checking character class matching : CharacterClass\n", tab);
            log += GetTabbedString($"BNF: {GetFullBnf()}\n", tab);
            if (startIndex >= code.Length)
            {
                log += GetTabbedString("Failed because the startIndex was greater or equal to length : CharacterClass\n", tab);
                return false;
            }

            string firstChar = code.Substring(startIndex, 1);

            if (Regex.IsMatch(firstChar, pattern) && !exceptions.Contains(firstChar[0]))
            {
                log += GetTabbedString($"Succeed. Regex: {pattern} matches character {firstChar}.  : CharacterClass\n", tab);
                startIndex++;
                return true;
            }

            log += GetTabbedString($"Failed. Regex: {pattern} does not match character {firstChar}  : CharacterClass\n", tab);
            return false;
        }

        public override string GetPartialBnf()
        {
            return pattern;
        }

        public static CharacterClass operator -(CharacterClass charClass, char exception)
        {
            CharacterClass result = new CharacterClass(charClass.pattern);
            result.exceptions.AddRange(charClass.exceptions);
            result.exceptions.Add(exception);

            return result;
        }
    }
}