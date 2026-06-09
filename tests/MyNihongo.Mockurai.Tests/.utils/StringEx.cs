using System.Text;

namespace MyNihongo.Mockurai.Tests;

public static class StringEx
{
	extension(string @this)
	{
		public string NewLineIndent(int newLine, int indent)
		{
			var value = @this.IndentBuilder(indent);

			if (!string.IsNullOrEmpty(@this))
			{
				for (var i = 0; i < newLine; i++)
					value.Insert(index: 0, Environment.NewLine);
			}

			return value.ToString();
		}

		public string Indent(int indent)
		{
			return @this.IndentBuilder(indent)
				.ToString();
		}

		private StringBuilder IndentBuilder(int indent)
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

			return stringBuilder;
		}
	}
}
