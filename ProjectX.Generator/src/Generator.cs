using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.CSharp;
using ProjectX.Bnf;

namespace ProjectX.Generator
{
    public static class Generator
    {
        public static void Generate(string inFilename, string destinationFolder, string outFilename, string namespaceName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(inFilename);

            CodeNamespace myNamespace = new CodeNamespace(namespaceName);

            XmlNode root = doc.DocumentElement;
            foreach (XmlNode productionNode in root.ChildNodes)
            {
                myNamespace.Types.Add(GetProduction(productionNode));
            }

            OutputCode(myNamespace, destinationFolder + outFilename);
        }

        private static void OutputCode(CodeNamespace myNamespace, string outFilename)
        {
            CodeCompileUnit compileunit = new CodeCompileUnit();
            compileunit.Namespaces.Add(myNamespace);

            CSharpCodeProvider provider = new CSharpCodeProvider();

            // Build the output file name.
            string sourceFile;
            if (provider.FileExtension[0] == '.')
            {
                sourceFile = outFilename + provider.FileExtension;
            }
            else
            {
                sourceFile = outFilename + "." + provider.FileExtension;
            }


            // Create a TextWriter to a StreamWriter to the output file.
            using (StreamWriter sw = new StreamWriter(sourceFile, false))
            {
                IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");

                // Generate source code using the code provider.
                provider.GenerateCodeFromCompileUnit(compileunit, tw,
                    new CodeGeneratorOptions());

                // Close the output file.
                tw.Close();
            }
        }


        private static CodeTypeDeclaration GetProduction(XmlNode productionNode)
        {
            XmlNode nonterminalNode = productionNode.FirstChild;
            string name = nonterminalNode.InnerText.Replace("-", "_");

            CodeTypeDeclaration productionType = new CodeTypeDeclaration(name);
            productionType.BaseTypes.Add(typeof (Nonterminal));

            var setProductionMethod = GetSetProductionMethod(productionNode);
            productionType.Members.Add(setProductionMethod);

            return productionType;
        }

        private static CodeMemberMethod GetSetProductionMethod(XmlNode productionNode)
        {
            CodeMemberMethod setProductionMethod = GetSetProductionMethod();

            CodeObjectCreateExpression newProduction =
                GetProductionAssignmentFromExpression(productionNode.FirstChild.NextSibling);

            var assignment = new CodeAssignStatement(
                new CodeVariableReferenceExpression("Production"), 
                newProduction);

            setProductionMethod.Statements.Add(assignment);

            return setProductionMethod;
        }

        private static CodeMemberMethod GetSetProductionMethod()
        {
            CodeMemberMethod setProductionMethod = new CodeMemberMethod();
            setProductionMethod.Name = "SetProduction";
            setProductionMethod.Attributes = MemberAttributes.Public | MemberAttributes.Override;

            return setProductionMethod;
        }

        private static CodeObjectCreateExpression GetProductionAssignmentFromExpression(
            XmlNode expressionNode)
        {
            CodeObjectCreateExpression newProduction = null;

            XmlNode expressionTypeNode = expressionNode.FirstChild;
            switch (expressionTypeNode.Name)
            {
                case "terminal":
                    newProduction = GetTerminal(expressionTypeNode);
                    break;
                case "nonterminal":
                    newProduction = GetNonterminal(expressionTypeNode);
                    break;
                case "sequence":
                    newProduction = GetSequence(expressionTypeNode);
                    break;
                case "choice":
                    newProduction = GetChoice(expressionTypeNode);
                    break;
                case "optional":
                    newProduction = GetOptional(expressionTypeNode);
                    break;
            }

            return newProduction;
        }

        private static CodeObjectCreateExpression GetTerminal(XmlNode terminalNode)
        {
            string terminal = terminalNode.InnerText;
            CodePrimitiveExpression terminalPrimitive = new CodePrimitiveExpression(terminal);
            return new CodeObjectCreateExpression(typeof(Terminal), terminalPrimitive);
        }

        private static CodeObjectCreateExpression GetNonterminal(XmlNode nonterminalNode)
        {
            string name = nonterminalNode.InnerText.Replace("-", "_");
            return new CodeObjectCreateExpression(name);
        }

        private static CodeObjectCreateExpression GetSequence(XmlNode sequenceNode)
        {
            var parameters = new List<CodeObjectCreateExpression>();
            foreach (XmlNode expressionNode in sequenceNode.ChildNodes)
            {
                parameters.Add(GetProductionAssignmentFromExpression(expressionNode));
            }

            return new CodeObjectCreateExpression(typeof(Sequence), parameters.ToArray());
        }

        private static CodeObjectCreateExpression GetChoice(XmlNode choiceNode)
        {
            var parameters = new List<CodeObjectCreateExpression>();
            foreach (XmlNode expressionNode in choiceNode.ChildNodes)
            {
                parameters.Add(GetProductionAssignmentFromExpression(expressionNode));
            }

            return new CodeObjectCreateExpression(typeof(Choice), parameters.ToArray());
        }

        private static CodeObjectCreateExpression GetOptional(XmlNode optionalNode)
        {
            XmlNode expressionNode = optionalNode.FirstChild;
            var parameter = GetProductionAssignmentFromExpression(expressionNode);

            return new CodeObjectCreateExpression(typeof(Optional), parameter);
        }
    }
}