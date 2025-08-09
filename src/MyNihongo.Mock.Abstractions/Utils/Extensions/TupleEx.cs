using System.Text;

namespace MyNihongo.Mock;

public static class TupleEx
{
	public static string GetString<T>(this (long, T) @this)
	{
		return $"{@this.Item1}: {@this.Item2}";
	}

	public static IEnumerable<string>? GetStrings<T>(this List<(long, T, ComparisonResult?)> @this)
	{
		return @this.Count > 0
			? EnumerateStrings(@this)
			: null;

		static IEnumerable<string> EnumerateStrings(List<(long, T, ComparisonResult?)> @this)
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

					if (item.Item3.Entries[i].Path == ComparisonResult.RootPath)
					{
						stringBuilder
							.AppendLine($"  expected: {item.Item3.Entries[i].ExpectedValue}")
							.Append($"  actual: {item.Item3.Entries[i].ActualValue}");
					}
					else
					{
						stringBuilder
							.AppendLine($"  - {item.Item3.Entries[i].Path}:")
							.AppendLine($"    expected: {item.Item3.Entries[i].ExpectedValue}")
							.Append($"    actual: {item.Item3.Entries[i].ActualValue}");
					}
				}

				yield return stringBuilder.ToString();
				stringBuilder.Clear();
			}
		}
	}

	public static IEnumerable<string>? GetStrings<T>(this List<(long, T, (string, ComparisonResult?)[]?)> @this)
	{
		return @this.Count > 0
			? EnumerateStrings(@this)
			: null;

		static IEnumerable<string> EnumerateStrings(List<(long, T, (string, ComparisonResult?)[]?)> @this)
		{
			var stringBuilder = new StringBuilder();

			foreach (var item in @this)
			{
				var message = $"{item.Item1}: {item.Item2}";
				if (item.Item3 is null || item.Item3.Length == 0)
				{
					yield return message;
					continue;
				}

				stringBuilder.Append(message);

				foreach (var resultItem in item.Item3)
				{
					if (resultItem.Item2 is null || resultItem.Item2.Entries.Count == 0)
						continue;

					stringBuilder.AppendLine();
					stringBuilder.AppendLine($"  - {resultItem.Item1}:");

					for (var i = 0; i < resultItem.Item2.Entries.Count; i++)
					{
						if (i > 0)
							stringBuilder.AppendLine();

						if (resultItem.Item2.Entries[i].Path == ComparisonResult.RootPath)
						{
							stringBuilder
								.AppendLine($"    expected: {resultItem.Item2.Entries[i].ExpectedValue}")
								.Append($"    actual: {resultItem.Item2.Entries[i].ActualValue}");
						}
						else
						{
							stringBuilder
								.AppendLine($"    - {resultItem.Item2.Entries[i].Path}:")
								.AppendLine($"      expected: {resultItem.Item2.Entries[i].ExpectedValue}")
								.Append($"      actual: {resultItem.Item2.Entries[i].ActualValue}");
						}
					}
				}

				yield return stringBuilder.ToString();
				stringBuilder.Clear();
			}
		}
	}
}
