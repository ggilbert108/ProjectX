using System;

namespace ProjectX.Bnf
{
    public abstract class Production
    {
        public bool Matches(string code, bool debug = false)
        {
            int index = 0;

            string log = "";
            bool matches = Matches(code, ref index, ref log, 0);

            if (debug)
            {
                Console.WriteLine(log);
            }

            return matches && index == code.Length;
        }

        public virtual string Emit()
        {
            return "";
        }

        public virtual bool Matches(string code, ref int startIndex, ref string log, int tab)
        {
            Production constituent = GetProduction();
            bool result = constituent.Matches(code, ref startIndex, ref log, tab + 1);
            return result;
        }

        public string GetTabbedString(string str, int tab)
        {
            string spaces = new String(' ', tab);
            return spaces + str;
        }

        public virtual Production GetProduction()
        {
            return this;
        }

        public virtual string GetPartialBnf()
        {
            return GetType().Name;
        }

        public string GetFullBnf()
        {
            return $"{GetType().Name} := {GetPartialBnf()}";
        }
    }
}