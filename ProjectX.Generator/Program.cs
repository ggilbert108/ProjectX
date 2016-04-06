using System;

namespace ProjectX.Generator
{
    public class Program
    {
        public static void Main()
        {
            Generator.Generate("data/cpp.xml", "../../../ProjectX.Language/src/", "Productions", "ProjectX.Language");
            //Generator.Generate("data/datafile.xml", "../../../ProjectX.Language/src/", "Productions", "ProjectX.Language");
        }
    }
}