using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslyn.Visug.CompilerAPI.CompilationAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var mscorlib = MetadataReference.CreateFromAssembly(typeof(Object).Assembly);
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var tree = CSharpSyntaxTree.ParseText("class Foo { void Bar() {} }");
            var compilation = CSharpCompilation.Create("Demo")
                .WithOptions(options)
                .WithReferences(mscorlib)
                .AddSyntaxTrees(tree);
            var result = compilation.Emit("demo.exe");
            if (result.Success)
            {

            }

            var conversion = compilation.ClassifyConversion(
                compilation.GetSpecialType(SpecialType.System_Int32),
                compilation.GetSpecialType(SpecialType.System_Object)
                );
            var myConversion = compilation.ClassifyConversion(
                compilation.GetSpecialType(SpecialType.System_Object),
                compilation.GetTypeByMetadataName("Foo")
                );
            Console.WriteLine(conversion);
        }
    }
}
