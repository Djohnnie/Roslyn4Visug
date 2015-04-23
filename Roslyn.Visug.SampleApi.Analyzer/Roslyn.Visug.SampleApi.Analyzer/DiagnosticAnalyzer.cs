using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Roslyn.Visug.SampleApi.Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class RoslynVisugSampleApiAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "Roslyn.Visug.SampleApi D001";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
        internal static readonly LocalizableString Title = "Obsolete Average.Calculate";
        internal static readonly LocalizableString MessageFormat = "Use the overload that takes an array of the Average.Calculate method instead!";
        internal static readonly LocalizableString Description = "DESCRIPTION";
        internal const string Category = "Obsoletes";

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
            context.RegisterSyntaxNodeAction(AnalyzeSymbol, SyntaxKind.InvocationExpression);
        }

        private static void AnalyzeSymbol(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = context.Node as InvocationExpressionSyntax;
            if (invocationExpression != null
                && invocationExpression.Expression.ToString().Equals("Average.Calculate")
                && invocationExpression.ArgumentList.Arguments.Count > 1)
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, invocationExpression.GetLocation()));
            }
        }
    }
}
