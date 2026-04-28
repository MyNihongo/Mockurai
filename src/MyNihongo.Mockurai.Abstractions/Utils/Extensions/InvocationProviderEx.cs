using System.Text;

namespace MyNihongo.Mockurai;

/// <summary>
/// Extension methods that aggregate and format invocations from one or more <see cref="IInvocationProvider"/> sources into
/// the string sequences used in verification failure messages.
/// </summary>
public static class InvocationProviderEx
{
	/// <summary>
	/// Materializes invocations from the supplied accessor, ordered by index, and projects them to their string representations.
	/// </summary>
	/// <param name="this">An accessor that yields the invocation providers to aggregate.</param>
	/// <returns>The combined invocation strings, or <see langword="null"/> when no invocations exist.</returns>
	public static IEnumerable<string>? GetStrings(this Func<IEnumerable<IInvocationProvider?>> @this)
	{
		return @this
			.GetInvocations()
			.NullIfEmpty();
	}

	/// <summary>
	/// Returns the string representations of any unverified invocations, optionally combining them with invocations from additional providers.
	/// </summary>
	/// <typeparam name="T">The invocation element type stored in the container.</typeparam>
	/// <param name="this">The container whose unverified invocations are inspected.</param>
	/// <param name="invocationProviders">An optional accessor that supplies additional invocation providers used to enrich the result.</param>
	/// <returns>The unverified invocation strings, or <see langword="null"/> when every invocation has been verified.</returns>
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

	/// <summary>
	/// Formats verification output paired with optional <see cref="ComparisonResult"/> diagnostics into the string sequence
	/// used in verification failure messages, optionally interleaving invocations from additional providers.
	/// </summary>
	/// <typeparam name="T">The invocation element type.</typeparam>
	/// <param name="this">The verification output where each entry pairs an invocation with the comparison result that caused it to be excluded (if any).</param>
	/// <param name="invocationProviders">An optional accessor that supplies additional invocation providers used to enrich the result.</param>
	/// <returns>The formatted invocation strings, or <see langword="null"/> when there is nothing to report.</returns>
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

	/// <summary>
	/// Formats verification output where each invocation may be paired with multiple named <see cref="ComparisonResult"/> diagnostics
	/// (one per parameter), optionally interleaving invocations from additional providers.
	/// </summary>
	/// <typeparam name="T">The invocation element type.</typeparam>
	/// <param name="this">The verification output where each entry pairs an invocation with an array of (parameter name, comparison result) pairs.</param>
	/// <param name="invocationProviders">An optional accessor that supplies additional invocation providers used to enrich the result.</param>
	/// <returns>The formatted invocation strings, or <see langword="null"/> when there is nothing to report.</returns>
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
