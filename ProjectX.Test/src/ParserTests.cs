using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using NUnit.Framework;
using ProjectX.Bnf;
using ProjectX.Language;

namespace ProjectX.Test
{
    [TestFixture]
    public class ParserTests
    {
        [TestCase("simple.txt", ExpectedResult = true)]
        public bool TestProgramValidity(string filename)
        {
            filename = TestHelpers.AdjustFilename(filename);
            string code = File.ReadAllText(filename);
            code = Regex.Replace(code, "\\s+", "");

            Production production = new translation_unit();
            return production.Validate(code);
        }

    }
}