using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;

namespace Roslyn.Visug.DiagnosticWithCodeFix.If
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(RoslynVisugDiagnosticWithCodeFixIfCodeFixProvider)), Shared]
    public class RoslynVisugDiagnosticWithCodeFixIfCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(RoslynVisugDiagnosticWithCodeFixIfAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return null;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);

            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            // Find the local declaration identified by the diagnostic.

            var eleClauseSyntax = root.FindToken(diagnosticSpan.Start).Parent as ElseClauseSyntax;
            if (eleClauseSyntax != null)
            {
                context.RegisterCodeFix(CodeAction.Create("Make better else", c => MakeBetterElseAsync(context.Document, eleClauseSyntax, c)), diagnostic);
            }
            var ifStatementSyntax = root.FindToken(diagnosticSpan.Start).Parent as IfStatementSyntax;
            if (ifStatementSyntax != null)
            {
                context.RegisterCodeFix(CodeAction.Create("Make better if", c => MakeBetterIfAsync(context.Document, ifStatementSyntax, c)), diagnostic);
            }
            var forStatementSyntax = root.FindToken(diagnosticSpan.Start).Parent as ForStatementSyntax;
            if (forStatementSyntax != null)
            {
                context.RegisterCodeFix(CodeAction.Create("Make better for", c => MakeBetterForAsync(context.Document, forStatementSyntax, c)), diagnostic);
            }
            var foreachStatementSyntax = root.FindToken(diagnosticSpan.Start).Parent as ForEachStatementSyntax;
            if (foreachStatementSyntax != null)
            {
                context.RegisterCodeFix(CodeAction.Create("Make better foreach", c => MakeBetterForEachAsync(context.Document, foreachStatementSyntax, c)), diagnostic);
            }
            // Register a code action that will invoke the fix.

        }

        private async Task<Document> MakeBetterIfAsync(Document document, IfStatementSyntax localDeclaration, CancellationToken cancellationToken)
        {
            var root = await document.GetSyntaxRootAsync(cancellationToken);
            var newIfBlock = SyntaxFactory.Block(localDeclaration.Statement);
            var totalIf = localDeclaration.WithStatement(newIfBlock);

            var newRoot = root.ReplaceNode(localDeclaration, totalIf);

            //// Return document with transformed tree.
            return document.WithSyntaxRoot(newRoot);
        }

        private async Task<Document> MakeBetterElseAsync(Document document, ElseClauseSyntax localDeclaration, CancellationToken cancellationToken)
        {
            var root = await document.GetSyntaxRootAsync(cancellationToken);
            var newIfBlock = SyntaxFactory.Block(localDeclaration.Statement);
            var totalIf = localDeclaration.WithStatement(newIfBlock);

            var newRoot = root.ReplaceNode(localDeclaration, totalIf);

            //// Return document with transformed tree.
            return document.WithSyntaxRoot(newRoot);
        }

        private async Task<Document> MakeBetterForAsync(Document document, ForStatementSyntax localDeclaration, CancellationToken cancellationToken)
        {
            StatementSyntax x;
            var root = await document.GetSyntaxRootAsync(cancellationToken);
            var newIfBlock = SyntaxFactory.Block(localDeclaration.Statement);
            var totalIf = localDeclaration.WithStatement(newIfBlock);

            var newRoot = root.ReplaceNode(localDeclaration, totalIf);

            //// Return document with transformed tree.
            return document.WithSyntaxRoot(newRoot);
        }

        private async Task<Document> MakeBetterForEachAsync(Document document, ForEachStatementSyntax localDeclaration, CancellationToken cancellationToken)
        {
            var root = await document.GetSyntaxRootAsync(cancellationToken);
            var newIfBlock = SyntaxFactory.Block(localDeclaration.Statement);
            var totalIf = localDeclaration.WithStatement(newIfBlock);

            var newRoot = root.ReplaceNode(localDeclaration, totalIf);

            //// Return document with transformed tree.
            return document.WithSyntaxRoot(newRoot);
        }
    }
}