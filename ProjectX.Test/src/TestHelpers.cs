using System;
using System.IO;
using System.Reflection;
using ProjectX.Bnf;

namespace ProjectX.Test
{
    public static class TestHelpers
    {
        public static string AdjustFilename(string filename)
        {
            var dir = Path.GetDirectoryName(typeof(Nonterminal).Assembly.CodeBase);
            dir = dir.Replace(@"file:\", "");

            return dir + "/testdata/" + filename;
        }

    }
}