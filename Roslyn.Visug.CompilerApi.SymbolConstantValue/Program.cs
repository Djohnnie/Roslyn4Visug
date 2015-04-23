using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Roslyn.Visug.CompilerApi.SymbolConstantValue
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
                    Console.WriteLine(3.14);
                    Console.WriteLine(""hello"");
                    Console.WriteLine('c');
                    Console.WriteLine(null);
                    Console.WriteLine(x * 2 + 1);
                  }
                }
                ");
            var root = tree.GetRoot();

            var mscorlib = MetadataReference.CreateFromAssembly(typeof(Object).Assembly);
            var compilation = CSharpCompilation.Create("Demo").AddSyntaxTrees(tree).AddReferences(mscorlib);
            var semanticModel = compilation.GetSemanticModel(tree);

            var walker = new ConsoleWriteLineWalker();
            walker.Visit(root);

            foreach (var identifier in walker.Identifiers)
            {
                var value = semanticModel.GetConstantValue(identifier);
                if (value.HasValue)
                {
                    Console.WriteLine($"{identifier} has constant value {value.Value ?? "null"} of type {value.Value?.GetType() ?? typeof(Object)}");
                }
                else
                {
                    Console.WriteLine($"{identifier} does not have a constant value");
                }
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
