using System.Text;

namespace MyNihongo.Mock;

public static class TupleEx
{
	public static string GetString<T>(this (long, T) @this)
	{
		return $"{@this.Item1}: {@this.Item2}";
	}

	public static IEnumerable<string> GetStrings<T>(this List<(long, T, ComparisonResult?)> @this)
	{
		var stringBuilder = new StringBuilder();

		foreach (var item in @this)
		{
			var message = $"{item.Item1}: {item.Item2}";
			if (item.Item3 is null || item.Item3.Entries.Count == 0)
			{
				yield return message;
				continue;
			}

			stringBuilder.AppendLine(message);

			for (var i = 0; i < item.Item3.Entries.Count; i++)
			{
				if (i > 0)
					stringBuilder.AppendLine();

				stringBuilder
					.AppendLine($"  - {item.Item3.Entries[i].Path}:")
					.AppendLine($"    expected: {item.Item3.Entries[i].ExpectedValue}")
					.Append($"    actual: {item.Item3.Entries[i].ActualValue}");
			}

			yield return stringBuilder.ToString();
			stringBuilder.Clear();
		}
	}
}
