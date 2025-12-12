using System.Text;

namespace MyNihongo.Mock.Tests;

public static class StringEx
{
	public static string Indent(this string @this, int indent)
	{
		var stringBuilder = new StringBuilder();
		using var reader = new StringReader(@this);

		var hasLines = false;
		while (reader.ReadLine() is { } line)
		{
			if (hasLines)
				stringBuilder.AppendLine();

			if (line.Length > 0)
			{
				for (var i = 0; i < indent; i++)
					stringBuilder.Append('\t');
			}

			stringBuilder.Append(line);
			hasLines = true;
		}

		return stringBuilder.ToString();
	}
}
