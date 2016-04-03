using System;
using System.Collections.Generic;

namespace ProjectX.Bnf
{
    public class Choice : Production
    {
        private Production[] choices;
        private static HashSet<string> failed; 

        public Choice(params Production[] choices)
        {
            this.choices = choices;
            failed = failed ?? new HashSet<string>();
        }

        public override bool Matches(string code, ref int startIndex, ref string log, int tab)
        {
            string record = GetFullBnf() + "_" + startIndex;
            if (failed.Contains(record))
            {
                log += GetTabbedString("Failed : Choice\n", tab);
                return false;
            }

            log += GetTabbedString("Checking choices. : Choice\n", tab);
            log += GetTabbedString($"BNF: {GetFullBnf()}\n", tab);
            int index = startIndex;

            foreach (var production in choices)
            {
                startIndex = index;
                if (production.Matches(code, ref startIndex, ref log, tab + 1))
                {
                    log += GetTabbedString("Found match in choices : Choice\n", tab);
                    return true;
                }
            }

            Console.WriteLine(record);
            failed.Add(record);
            log += GetTabbedString("Failed because it did not match any of the options : Choice\n", tab);
            startIndex = index;
            return false;
        }

        public override string GetPartialBnf()
        {
            string result = "";
            foreach (var production in choices)
            {
                result += $"{production.GetPartialBnf()}|";
            }
            result = result.Substring(0, result.Length - 1);

            return result;
        }
    }
}