namespace ProjectX.Bnf
{
    public class Optional : Production
    {
        private Production optional;

        public Optional(Production optional)
        {
            this.optional = optional;
        }

        public override bool Matches(string code, ref int startIndex, ref string log, int tab)
        {
            log += GetTabbedString("Checking optional production : Optional\n", tab);
            log += GetTabbedString($"BNF: {GetFullBnf()}\n", tab);
            if (optional.Matches(code, ref startIndex, ref log, tab + 1))
            {
                log += GetTabbedString("Optional production exists in code : Optional\n", tab);
            }
            else
            {
                log += GetTabbedString("Optional production does not exist in code : Optional\n", tab);
            }

            return true;
        }

        public override string GetPartialBnf()
        {
            return $"[{optional.GetPartialBnf()}]";
        }
    }
}