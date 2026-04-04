using System.Text;

namespace MyNihongo.Mockurai;

public static class InvocationProviderEx
{
	public static IEnumerable<string>? GetStrings(this Func<IEnumerable<IInvocationProvider?>> @this)
	{
		return @this
			.GetInvocations()
			.NullIfEmpty();
	}

	public static IEnumerable<string>? GetUnverifiedInvocations<T>(this InvocationContainer<T> @this, Func<IEnumerable<IInvocationProvider?>>? invocationProviders)
		where T : class, IInvocation
	{
		if (!@this.Any(static x => !x.IsVerified))
			return null;

		var invocations = invocationProviders is not null
			? invocationProviders.GetInvocations()
			: @this;

		return invocations
			.Where(static x => !x.IsVerified)
			.NullIfEmpty();
	}

	public static IEnumerable<string>? GetStrings<T>(this List<(T, ComparisonResult?)> @this, Func<IEnumerable<IInvocationProvider?>>? invocationProviders)
		where T : IInvocation
	{
		if (@this.Count == 0)
			return invocationProviders?.GetStrings();

		var result = invocationProviders is not null
			? EnumerateInvocationStrings(@this, invocationProviders)
			: EnumerateStrings(@this);

		return result.NullIfEmpty();

		static IEnumerable<string> EnumerateStrings(List<(T, ComparisonResult?)> @this)
		{
			var stringBuilder = new StringBuilder();

			foreach (var item in @this)
			{
				Append(stringBuilder, item);

				yield return stringBuilder.ToString();
				stringBuilder.Clear();
			}
		}

		static IEnumerable<string> EnumerateInvocationStrings(List<(T, ComparisonResult?)> @this, Func<IEnumerable<IInvocationProvider?>> invocationProviders)
		{
			var stringBuilder = new StringBuilder();

			var i = 0;
			foreach (var invocation in invocationProviders.GetInvocations())
			{
				if (i < @this.Count && @this[i].Item1.Index == invocation.Index)
				{
					Append(stringBuilder, @this[i++]);
					yield return stringBuilder.ToString();
					stringBuilder.Clear();
				}
				else
				{
					yield return invocation.ToString();
				}
			}
		}

		static void Append(in StringBuilder stringBuilder, in (T, ComparisonResult?) item)
		{
			stringBuilder.Append(item.Item1.ToString());

			if (item.Item2 is null || item.Item2.Entries.Count <= 0)
				return;

			for (var i = 0; i < item.Item2.Entries.Count; i++)
			{
				stringBuilder.AppendLine();

				if (item.Item2.Entries[i].Path == ComparisonResult.RootPath)
				{
					stringBuilder
						.AppendLine($"  expected: {item.Item2.Entries[i].ExpectedValue}")
						.Append($"  actual: {item.Item2.Entries[i].ActualValue}");
				}
				else
				{
					stringBuilder
						.AppendLine($"  - {item.Item2.Entries[i].Path}:")
						.AppendLine($"    expected: {item.Item2.Entries[i].ExpectedValue}")
						.Append($"    actual: {item.Item2.Entries[i].ActualValue}");
				}
			}
		}
	}

	public static IEnumerable<string>? GetStrings<T>(this List<(T, (string, ComparisonResult?)[]?)> @this, Func<IEnumerable<IInvocationProvider?>>? invocationProviders)
		where T : IInvocation
	{
		if (@this.Count == 0)
			return invocationProviders?.GetStrings();

		var result = invocationProviders is not null
			? EnumerateInvocationStrings(@this, invocationProviders)
			: EnumerateStrings(@this);

		return result.NullIfEmpty();

		static IEnumerable<string> EnumerateStrings(List<(T, (string, ComparisonResult?)[]?)> @this)
		{
			var stringBuilder = new StringBuilder();

			foreach (var item in @this)
			{
				Append(stringBuilder, item);
				yield return stringBuilder.ToString();
				stringBuilder.Clear();
			}
		}

		static IEnumerable<string> EnumerateInvocationStrings(List<(T, (string, ComparisonResult?)[]?)> @this, Func<IEnumerable<IInvocationProvider?>> invocationProviders)
		{
			var stringBuilder = new StringBuilder();

			var i = 0;
			foreach (var invocation in invocationProviders.GetInvocations())
			{
				if (i < @this.Count && @this[i].Item1.Index == invocation.Index)
				{
					Append(stringBuilder, @this[i++]);
					yield return stringBuilder.ToString();
					stringBuilder.Clear();
				}
				else
				{
					yield return invocation.ToString();
				}
			}
		}

		static void Append(in StringBuilder stringBuilder, in (T, (string, ComparisonResult?)[]?) item)
		{
			stringBuilder.Append(item.Item1.ToString());

			if (item.Item2 is null || item.Item2.Length == 0)
				return;

			foreach (var resultItem in item.Item2)
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
		}
	}

	private static IEnumerable<IInvocation> GetInvocations(this Func<IEnumerable<IInvocationProvider?>> @this)
	{
		return @this.Invoke()
			.SelectMany(static x => x?.GetInvocations() ?? [])
			.OrderBy(static x => x.Index);
	}
}
