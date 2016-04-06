using System.Linq;
using ProjectX.Finite;

namespace ProjectX.Bnf
{
    public class Terminal : Sequence
    {
        private string value;

        public Terminal(string value) : base(
            (from ch in value
             select new Character(ch)).ToArray<Production>())
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }
    }
}