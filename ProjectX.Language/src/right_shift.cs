using ProjectX.Bnf;

namespace ProjectX.Language
{
    public class right_shift : Terminal
    {
        public right_shift() : base(">>")
        {
            
        }
    }

    public class right_shift_assignment : Terminal
    {
        public right_shift_assignment() : base(">>=")
        {
            
        }
    }
}