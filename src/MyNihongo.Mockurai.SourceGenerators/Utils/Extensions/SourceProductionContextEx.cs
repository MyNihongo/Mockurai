using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace MyNihongo.Mockurai.Utils;

internal static class SourceProductionContextEx
{
	extension(SourceProductionContext @this)
	{
		public void AddSanitisedSource(string fileName, string source)
		{
			fileName = !string.IsNullOrEmpty(fileName)
				? fileName.Replace('<', '_').Replace('>', '_').Replace(", ", "_")
				: fileName;

			@this.AddSourceEx(fileName, source);
		}

		public Compilation AddSourceToSyntaxTree(string fileName, string source, CompilationCombinedResult combinedResult)
		{
			@this.AddSourceEx(fileName, source);

			var options = (CSharpParseOptions)((CSharpCompilation)combinedResult.Compilation).SyntaxTrees[0].Options;
			return combinedResult.Compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(source, options));
		}

		private void AddSourceEx(string fileName, string source)
		{
			var sourceText = SourceText.From(source, Encoding.UTF8);
			@this.AddSource(fileName, sourceText);
		}
	}
}
