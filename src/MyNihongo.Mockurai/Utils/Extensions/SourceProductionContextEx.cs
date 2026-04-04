namespace MyNihongo.Mockurai.Utils;

internal static class SourceProductionContextEx
{
	public static void AddSanitisedSource(this SourceProductionContext @this, string fileName, string source)
	{
		fileName = !string.IsNullOrEmpty(fileName)
			? fileName.Replace('<', '_').Replace('>', '_').Replace(", ", "_")
			: fileName;

		@this.AddSource(fileName, source);
	}
}
