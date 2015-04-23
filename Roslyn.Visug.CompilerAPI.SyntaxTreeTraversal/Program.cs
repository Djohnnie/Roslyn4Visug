using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Roslyn.Visug.CompilerAPI.SyntaxTreeTraversal
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = CSharpSyntaxTree.ParseText("class Foo { void Bar() {} }");
            var root = tree.GetRoot();

            var fooClass = root.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Single(c => c.Identifier.Text == "Foo");
            //Console.WriteLine(fooClass);

            new MyVisitor().Visit(root);

            Console.ReadKey();
            Int32 x = 5;

            Console.WriteLine( x );

        }
    }

    class MyVisitor : CSharpSyntaxWalker
    {

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if( node.Identifier.Text == "Bar")
            {
                Console.WriteLine(node);
            }

            base.VisitMethodDeclaration(node);
        }

    }
}
