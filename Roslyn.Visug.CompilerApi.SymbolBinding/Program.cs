using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslyn.Visug.CompilerApi.SymbolBinding
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = CSharpSyntaxTree.ParseText(@"
                using System;

                class Foo {
                  private int y;

                  void Bar( int x ) {
                    Console.WriteLine(x);
                    Console.WriteLine(y);

                    int z = 42;
                    Console.WriteLine(z);

                    Console.WriteLine(a);
                  }
                }
                ");
            var root = tree.GetRoot();

            var mscorlib = MetadataReference.CreateFromAssembly(typeof(Object).Assembly);
            var compilation = CSharpCompilation.Create("Demo")
                .AddSyntaxTrees(tree)
                .AddReferences(mscorlib);
            var semanticModel = compilation.GetSemanticModel(tree);

            var walker = new ConsoleWriteLineWalker();
            walker.Visit(root);

            foreach (var identifier in walker.Identifiers)
            {

                var symbolInfo = semanticModel.GetSymbolInfo(identifier);
                Console.WriteLine($"Symbol: {identifier.GetText()}, {symbolInfo.Symbol?.Kind.ToString() ?? "null"}");
            }

            Console.ReadKey();

        }
    }

    public class ConsoleWriteLineWalker : CSharpSyntaxWalker
    {
        public List<ExpressionSyntax> Identifiers { get; }

        public ConsoleWriteLineWalker()
        {
            this.Identifiers = new List<ExpressionSyntax>();
        }

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (node.Expression.ToString() == "Console.WriteLine")
            {
                this.Identifiers.Add(node.ArgumentList.Arguments[0].Expression);
            }
        }
    }
}
