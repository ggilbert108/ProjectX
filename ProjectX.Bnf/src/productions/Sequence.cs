using System;
using System.Collections.Generic;

namespace ProjectX.Bnf
{
    public class Sequence : Production
    {
        private Production[] sequence;
        private static HashSet<string> failed; 

        public Sequence(params Production[] sequence)
        {
            this.sequence = sequence;
            failed = failed ?? new HashSet<string>();
        }

        public override bool Matches(string code, ref int startIndex, ref string log, int tab)
        {
            string record = GetFullBnf() + "_" + startIndex;
            if (failed.Contains(record))
            {
                log += GetTabbedString("Failed : Sequence\n", tab);
                return false;
            }

            log += GetTabbedString("Checking sequence : Sequence\n", tab);
            log += GetTabbedString($"BNF: {GetFullBnf()}\n", tab);
            int index = startIndex;
            foreach (var production in sequence)
            {
                if (!production.Matches(code, ref startIndex, ref log, tab + 1))
                {
                    failed.Add(record);
                    log += GetTabbedString("Code does not match production : Sequence\n", tab);
                    startIndex = index;
                    return false;
                }
            }

            log += GetTabbedString("Code matches all productions in the sequence : Sequence\n", tab);
            return true;
        }

        public override string GetPartialBnf()
        {
            string result = "";
            foreach (var production in sequence)
            {
                result += $"{production.GetPartialBnf()}, ";
            }
            result = result.Substring(0, result.Length - 2);

            return result;
        }
    }
}