using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslyn.Visug.DiagnosticWithCodeFix.If
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class RoslynVisugDiagnosticWithCodeFixIfAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "RoslynVisugDiagnosticWithCodeFixIf";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
        internal static readonly LocalizableString Title = "Title";
        internal static readonly LocalizableString MessageFormat = "Oh no you didn't?";
        internal static readonly LocalizableString Description = "Try not to use an if statement without curly braces!";
        internal const string Category = "Category";

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeSymbol, SyntaxKind.IfStatement, SyntaxKind.ElseClause, SyntaxKind.ForStatement, SyntaxKind.ForEachStatement);
        }
        private static void AnalyzeSymbol(SyntaxNodeAnalysisContext context)
        {
            var ifStatementSyntax = context.Node as IfStatementSyntax;
            if (ifStatementSyntax != null && !(ifStatementSyntax.Statement is BlockSyntax))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, ifStatementSyntax.IfKeyword.GetLocation()));
            }
            var elseClauseSyntax = context.Node as ElseClauseSyntax;
            if (elseClauseSyntax != null && !(elseClauseSyntax.Statement is BlockSyntax) && !(elseClauseSyntax.Statement is IfStatementSyntax))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, elseClauseSyntax.ElseKeyword.GetLocation()));
            }
            var forStatementSyntax = context.Node as ForStatementSyntax;
            if (forStatementSyntax != null && !(forStatementSyntax.Statement is BlockSyntax))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, forStatementSyntax.ForKeyword.GetLocation()));
            }
            var forEachStatementSyntax = context.Node as ForEachStatementSyntax;
            if (forEachStatementSyntax != null && !(forEachStatementSyntax.Statement is BlockSyntax))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, forEachStatementSyntax.ForEachKeyword.GetLocation()));
            }
        }
    }
}
