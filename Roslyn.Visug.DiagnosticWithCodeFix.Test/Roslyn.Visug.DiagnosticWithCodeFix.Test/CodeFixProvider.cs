using System;
using System.Collections.Generic;
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
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;

namespace Roslyn.Visug.DiagnosticWithCodeFix.Test
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(RoslynVisugDiagnosticWithCodeFixTestCodeFixProvider)), Shared]
    public class RoslynVisugDiagnosticWithCodeFixTestCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(RoslynVisugDiagnosticWithCodeFixTestAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            // TODO: Replace the following code with your own analysis, generating a CodeAction for each fix to suggest
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            // Find the type declaration identified by the diagnostic.
            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<InvocationExpressionSyntax>().First();

            // Register a code action that will invoke the fix.
            context.RegisterCodeFix(
                CodeAction.Create("Use newer version", c => UseNewerVersionAsync(context.Document, declaration, c)),
                diagnostic);
        }

        private async Task<Document> UseNewerVersionAsync(Document document, InvocationExpressionSyntax invocationExpression, CancellationToken cancellationToken)
        {
            try
            {
                var argumentList = invocationExpression.ArgumentList;
                if (argumentList.Arguments.Count > 1)
                {
                    var syntaxNodeOrTokens = new List<SyntaxNodeOrToken>();
                    foreach (var argument in argumentList.Arguments)
                    {
                        syntaxNodeOrTokens.Add(argument.Expression);
                        if (argumentList.Arguments.IndexOf(argument) < argumentList.Arguments.Count - 1)
                        {
                            syntaxNodeOrTokens.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
                        }
                    }

                    var root = await document.GetSyntaxRootAsync(cancellationToken);
                    var newArgumentList = SyntaxFactory.ArgumentList(
                        SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
                            SyntaxFactory.Argument(SyntaxFactory.ArrayCreationExpression(
                                SyntaxFactory.ArrayType(
                                    SyntaxFactory.PredefinedType(
                                        SyntaxFactory.Token(SyntaxKind.DecimalKeyword)))
                                            .WithRankSpecifiers(
                                                SyntaxFactory.SingletonList<ArrayRankSpecifierSyntax>(
                                                    SyntaxFactory.ArrayRankSpecifier(
                                                        SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(
                                                            SyntaxFactory.OmittedArraySizeExpression())))))
                                                                .WithInitializer(
                                                                    SyntaxFactory.InitializerExpression(
                                                                        SyntaxKind.ArrayInitializerExpression, SyntaxFactory.SeparatedList<ExpressionSyntax>(syntaxNodeOrTokens))))));

                    var newRoot = root.ReplaceNode(argumentList, newArgumentList);

                    //// Return document with transformed tree.
                    return document.WithSyntaxRoot(newRoot);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return document;
        }
    }
}