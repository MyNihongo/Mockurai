using Microsoft.CodeAnalysis.Text;

namespace MyNihongo.Mockurai.Utils;

internal static class SourceProductionContextEx
{
	public static void AddSanitisedSource(this SourceProductionContext @this, string fileName, string source)
	{
		fileName = !string.IsNullOrEmpty(fileName)
			? fileName.Replace('<', '_').Replace('>', '_').Replace(", ", "_")
			: fileName;

		var sourceText = SourceText.From(source, Encoding.UTF8);
		@this.AddSource(fileName, sourceText);
	}
}
