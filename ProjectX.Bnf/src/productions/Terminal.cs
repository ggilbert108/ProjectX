
using System;

namespace ProjectX.Bnf
{
    public class Terminal : Production
    {
        private string value;

        public Terminal(string value)
        {
            this.value = value;
        }

        public override bool Matches(string code, ref int startIndex, ref string log, int tab)
        {
            log += GetTabbedString("Checking terminal : Terminal\n", tab);
            log += GetTabbedString($"BNF: {GetFullBnf()}\n", tab);
            if (startIndex >= code.Length)
            {
                log += GetTabbedString("Failed because index was at least length : Terminal\n", tab);
                return false;
            }

            string codeFromStartIndex = code.Substring(startIndex);
            log += GetTabbedString("Code under inspection " + codeFromStartIndex + "\n", tab);

            if (codeFromStartIndex.StartsWith(value))
            {
                startIndex += value.Length;
                return true;
            }

            log += GetTabbedString("Failed because code did not match value : Terminal\n", tab);
            return false;
        }

        public override string GetPartialBnf()
        {
            return value;
        }
    }
}