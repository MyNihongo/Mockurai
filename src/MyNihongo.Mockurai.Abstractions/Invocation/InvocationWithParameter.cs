using System.Runtime.InteropServices;

namespace MyNihongo.Mockurai;

/// <summary>
/// Tracks invocations against a mocked member that takes a single argument of type <typeparamref name="TParameter"/>,
/// and exposes verification helpers that match arguments against an <see cref="ItSetup{TParameter}"/>.
/// </summary>
/// <typeparam name="TParameter">The captured argument type.</typeparam>
public sealed class Invocation<TParameter> : IInvocationVerify, IInvocationProvider<TParameter>
{
	private readonly string _name;
	private readonly string? _prefix;
	private readonly InvocationContainer<Item> _invocations = [];

	/// <summary>
	/// Initializes a new instance of the <see cref="Invocation{TParameter}"/> class.
	/// </summary>
	/// <param name="name">The display name of the mocked member used in failure messages, expected to contain a single format placeholder for the argument representation.</param>
	/// <param name="prefix">An optional prefix prepended to the argument representation in failure messages.</param>
	public Invocation(in string name, in string? prefix = null)
	{
		_name = name;
		_prefix = prefix;
	}

	/// <summary>
	/// Records a new invocation along with the argument that was passed to the mocked member.
	/// </summary>
	/// <param name="index">The shared invocation counter used to order calls.</param>
	/// <param name="parameter">The argument captured for this invocation.</param>
	public void Register(in InvocationIndex.Counter index, in TParameter parameter)
	{
		var invokedIndex = index.Increment();
		_invocations.Add(new Item(invokedIndex, parameter, invocation: this));
	}

	/// <summary>
	/// Verifies that the number of invocations matching the supplied <paramref name="parameter"/> setup satisfies <paramref name="times"/>,
	/// and marks each matching invocation as verified.
	/// </summary>
	/// <param name="parameter">The argument-matching setup used to filter invocations.</param>
	/// <param name="times">The invocation count constraint to enforce.</param>
	/// <param name="invocationProviders">An optional accessor that supplies additional invocation providers used to enrich the failure message.</param>
	/// <exception cref="MockVerifyCountException">Thrown when the matched count does not satisfy <paramref name="times"/>.</exception>
	public void Verify(in ItSetup<TParameter> parameter, in Times times, Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var span = _invocations.GetItemsSpan();

		var verifyOutput = new List<(Item, ComparisonResult?)>();
		CollectionsMarshal.SetCount(verifyOutput, span.Length);

		var count = 0;
		for (var i = 0; i < span.Length; i++)
		{
			var verifyParameter = span[i].GetParameter(parameter.Type);
			if (!parameter.Check(verifyParameter, out var result))
			{
				verifyOutput[i] = (span[i], result);
				continue;
			}

			verifyOutput[i] = (span[i], null);
			span[i].IsVerified = true;
			count++;
		}

		if (times.Predicate(count))
			return;

		var invocations = verifyOutput.GetStrings(invocationProviders);
		var verifyName = string.Format(_name, parameter.ToString(_prefix));
		throw new MockVerifyCountException(verifyName, times, count, invocations);
	}

	/// <summary>
	/// Verifies that an invocation matching the supplied <paramref name="parameter"/> setup occurred at or after the specified sequence index,
	/// marks it as verified, and returns the next index to use for sequential verification.
	/// </summary>
	/// <param name="parameter">The argument-matching setup used to filter invocations.</param>
	/// <param name="index">The minimum sequence index expected for the next matching invocation.</param>
	/// <param name="invocationProviders">An optional accessor that supplies additional invocation providers used to enrich the failure message.</param>
	/// <returns>The index immediately after the matched invocation, suitable for chaining further sequence verifications.</returns>
	/// <exception cref="MockVerifySequenceOutOfRangeException">Thrown when no matching invocation exists at or after <paramref name="index"/>.</exception>
	public long Verify(in ItSetup<TParameter> parameter, in long index, Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var span = _invocations.GetItemsSpanFrom(index);

		var verifyOutput = new List<(Item, ComparisonResult?)>();
		CollectionsMarshal.SetCount(verifyOutput, span.Length);

		for (var i = 0; i < span.Length; i++)
		{
			var verifyParameter = span[i].GetParameter(parameter.Type);

			if (!parameter.Check(verifyParameter, out var result))
			{
				verifyOutput[i] = (span[i], result);
				continue;
			}

			verifyOutput[i] = (span[i], null);
			span[i].IsVerified = true;
			return span[i].Index + 1;
		}

		if (invocationProviders is null)
		{
			span = _invocations.GetItemsSpanBefore(index);
			for (var i = 0; i < span.Length; i++)
				verifyOutput.Insert(i, (span[i], null));
		}

		var invocations = verifyOutput.GetStrings(invocationProviders);
		var verifyName = string.Format(_name, parameter.ToString(_prefix));
		throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
	}

	/// <inheritdoc/>
	public void VerifyNoOtherCalls(Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
		if (unverifiedItems is null)
			return;

		var typeName = !string.IsNullOrEmpty(_prefix)
			? $"{_prefix} {typeof(TParameter).Name}"
			: typeof(TParameter).Name;

		var verifyName = string.Format(_name, typeName);
		throw new MockUnverifiedException(verifyName, unverifiedItems);
	}

	/// <inheritdoc/>
	public IEnumerable<IInvocation> GetInvocations()
	{
		return _invocations;
	}

	/// <inheritdoc/>
	public IEnumerable<IInvocation<TParameter>> GetInvocationsWithArguments()
	{
		return _invocations;
	}

	private sealed class Item : IInvocation<TParameter>
	{
		private readonly TParameter _parameter;
		private readonly string? _jsonSnapshot;
		private readonly Invocation<TParameter> _invocation;

		public Item(in long index, in TParameter parameter, in Invocation<TParameter> invocation)
		{
			_parameter = parameter;
			_invocation = invocation;
			Index = index;

			try
			{
				_jsonSnapshot = parameter.SerializeToJson();
			}
			catch
			{
				// Swallow
			}
		}

		public long Index { get; }

		public bool IsVerified { get; set; }

		public TParameter Arguments => _parameter;

		public TParameter GetParameter(SetupType? setupType)
		{
			return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshot)
				? _jsonSnapshot.DeserializeFromJson(_parameter)
				: _parameter;
		}

		public override string ToString()
		{
			var stringValue = string.IsNullOrEmpty(_jsonSnapshot)
				? _parameter?.ToString() ?? string.Empty
				: _jsonSnapshot;

			stringValue = !string.IsNullOrEmpty(_invocation._prefix)
				? $"{_invocation._prefix} {stringValue}"
				: stringValue;

			stringValue = string.Format(_invocation._name, stringValue);
			return $"{Index}: {stringValue}";
		}
	}
}
