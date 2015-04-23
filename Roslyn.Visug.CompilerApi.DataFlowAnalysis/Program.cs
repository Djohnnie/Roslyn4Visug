using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslyn.Visug.CompilerApi.DataFlowAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {

            var tree = CSharpSyntaxTree.ParseText("void Foo() { int x = 42; int i = 0; int y; y = x + i++; }");
            var root = tree.GetRoot();

            var mscorlib = MetadataReference.CreateFromAssembly(typeof(Object).Assembly);
            var compilation = CSharpCompilation.Create("Demo").AddSyntaxTrees(tree).AddReferences(mscorlib);
            var semanticModel = compilation.GetSemanticModel(tree);

            AssignmentWalker walker = new AssignmentWalker();
            walker.Visit(root);
            var dataFlow = semanticModel.AnalyzeDataFlow(walker.AssignmentExpression);

        }

    }

    public class AssignmentWalker : CSharpSyntaxWalker
    {
        public ExpressionSyntax AssignmentExpression { get; private set; }
        public override void VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            if (node.Left.ToString() == "y")
            {
                this.AssignmentExpression = node;
            }
            base.VisitAssignmentExpression(node);
        }
    }
}