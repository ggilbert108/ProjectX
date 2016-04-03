using ProjectX.Bnf.generation;

namespace ProjectX.Bnf
{
    public class Program
    {
        public static void Main()
        {
            Generator generator = new Generator("data/csharp.xml");
            generator.Generate("../../../ProjectX.Language/src/", "Productions", "ProjectX.Language");
        }
    }
}