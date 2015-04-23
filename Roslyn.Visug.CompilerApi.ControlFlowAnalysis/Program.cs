using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Roslyn.Visug.CompilerApi.ControlFlowAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = CSharpSyntaxTree.ParseText(@"
                public static List<Int32> SearchPrimes(Int32 max)
                {
                    var result = new List<Int32>();
                    for (var i = 2; i <= max; i++)
                    {
                        var isPrime = true;
                        for (var d = 2; d <= Math.Sqrt(i); d++)
                        {
                            if (i % d == 0)
                            {
                                isPrime = false;
                                break;
                            }
                        }
                        if (isPrime)
                        {
                            result.Add(i);
                        }
                    }
                    return result;
                }
                ");
            var root = tree.GetRoot();

            var mscorlib = MetadataReference.CreateFromAssembly(typeof(Object).Assembly);
            var compilation = CSharpCompilation.Create("Demo").AddSyntaxTrees(tree).AddReferences(mscorlib);
            var semanticModel = compilation.GetSemanticModel(tree);

            BreakWalker walker = new BreakWalker();
            walker.Visit(root);
            var controlFlow = semanticModel.AnalyzeControlFlow(walker.BlockSyntax);
        }


    }

    public class BreakWalker : CSharpSyntaxWalker
    {
        public BlockSyntax BlockSyntax { get; private set; }

        public override void VisitBlock(BlockSyntax node)
        {
            if (node.Parent is IfStatementSyntax && ((IfStatementSyntax)node.Parent).Condition.ToString().Contains("i % d"))
            {
                this.BlockSyntax = node;
            }
            base.VisitBlock(node);
        }
    }
}
