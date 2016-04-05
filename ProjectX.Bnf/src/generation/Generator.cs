using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.CSharp;

namespace ProjectX.Bnf.generation
{
    public class Generator
    {
        private string filename;

        public Generator(string filename)
        {
            this.filename = filename;
        }

        public void Generate(string destinationAddress, string outFileName, string namespaceName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);


            CodeNamespace myNamespace = new CodeNamespace(namespaceName);
            XmlNode root = doc.DocumentElement;
            foreach (XmlNode production in root.SelectNodes("production"))
            {
                var productionType = GenerateProduction(production);
                myNamespace.Types.Add(productionType);
            }

            myNamespace.Imports.Add(new CodeNamespaceImport("ProjectX.Bnf"));
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            compileUnit.Namespaces.Add(myNamespace);
            WriteToFile(compileUnit, destinationAddress + outFileName);
        }

        private void WriteToFile(CodeCompileUnit compileUnit, string fileName)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();

            string sourceFile;
            if (provider.FileExtension[0] == '.')
            {
                sourceFile = fileName + provider.FileExtension;
            }
            else
            {
                sourceFile = fileName + "." + provider.FileExtension;
            }

            using (StreamWriter sw = new StreamWriter(sourceFile, false))
            {
                IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");

                provider.GenerateCodeFromCompileUnit(compileUnit, tw,
                    new CodeGeneratorOptions());

                tw.Close();
            }
        }

        private CodeTypeDeclaration GenerateProduction(XmlNode productionNode)
        {
            XmlNode nonterminalNode = productionNode.FirstChild;
            string name = nonterminalNode.InnerText.Replace("-", "_");
            var productionType = new CodeTypeDeclaration(name);
            
            productionType.BaseTypes.Add(new CodeTypeReference(typeof(Production)));
            var getProductionMethod = GetProductionMethod();

            XmlNode expressionNode = productionNode.FirstChild.NextSibling;
            var productionCreation = ParseExpression(expressionNode);

            getProductionMethod.Statements.Add(new CodeAssignStatement(
                new CodeVariableReferenceExpression("production"), productionCreation));
            getProductionMethod.Statements.Add(new CodeMethodReturnStatement(
                new CodeVariableReferenceExpression("production")));


            productionType.Members.Add(getProductionMethod);

            return productionType;
        }

        private CodeObjectCreateExpression ParseExpression(XmlNode expression)
        {
            CodeObjectCreateExpression production;

            XmlNode expressionType = expression.FirstChild;
            switch (expressionType.Name)
            {
                case "terminal":
                    production = GetTerminal(expressionType);
                    break;
                case "nonterminal":
                    production = GetNonterminal(expressionType);
                    break;
                case "choice":
                    production = GetChoice(expressionType);
                    break;
                case "optional":
                    production = GetOptional(expressionType);
                    break;
                case "sequence":
                    production = GetSequence(expressionType);
                    break;
                default:
                    production = null;
                    break;
            }

            return production;
        }

        private CodeObjectCreateExpression GetTerminal(XmlNode terminalNode)
        {
            string text = terminalNode.InnerText.Replace("-", "_");
            var terminal = new CodeObjectCreateExpression(typeof (Terminal), 
                new CodePrimitiveExpression(text));

            return terminal;
        }

        private CodeObjectCreateExpression GetNonterminal(XmlNode nonterminalNode)
        {
            string name = nonterminalNode.InnerText.Replace("-", "_");

            var result = new CodeObjectCreateExpression(name);
            return result;
        }

        private CodeObjectCreateExpression GetChoice(XmlNode choiceNode)
        {
            var parameters = new List<CodeObjectCreateExpression>();
            foreach (XmlNode childExpression in choiceNode.ChildNodes)
            {
                parameters.Add(ParseExpression(childExpression));
            }

            var choice = new CodeObjectCreateExpression(typeof(Choice), parameters.ToArray());
            return choice;
        }

        private CodeObjectCreateExpression GetOptional(XmlNode optionalNode)
        {
            var parameters = new List<CodeObjectCreateExpression>();
            foreach (XmlNode childExpression in optionalNode.ChildNodes)
            {
                parameters.Add(ParseExpression(childExpression));
            }

            var optional = new CodeObjectCreateExpression(typeof(Optional), parameters.ToArray());
            return optional;
        }

        private CodeObjectCreateExpression GetSequence(XmlNode sequenceNode)
        {
            var parameters = new List<CodeObjectCreateExpression>();
            foreach (XmlNode childExpression in sequenceNode.ChildNodes)
            {
                parameters.Add(ParseExpression(childExpression));
            }

            var sequence = new CodeObjectCreateExpression(typeof(Sequence), parameters.ToArray());
            return sequence;
        }

        private CodeMemberMethod GetProductionMethod()
        {
            var method = new CodeMemberMethod();
            method.Name = "GetProduction";
            method.Attributes = MemberAttributes.Override | MemberAttributes.Public;
            method.ReturnType = new CodeTypeReference(typeof(Production));

            method.Statements.Add(new CodeVariableDeclarationStatement(
                typeof (Production), "production"));

            return method;
        }
    }
}