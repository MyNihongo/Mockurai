namespace MyNihongo.Mockurai;

/// <summary>
/// Provides access to an async-local <see cref="Counter"/> used to assign monotonic indices to invocations within a logical flow of execution.
/// </summary>
public static class InvocationIndex
{
	private static readonly AsyncLocal<Counter> Value = new();

	/// <summary>
	/// Gets the <see cref="Counter"/> associated with the current async flow, creating one on first access.
	/// </summary>
	public static Counter CounterValue => Value.Value ??= new Counter();

	/// <summary>
	/// A thread-safe, monotonically-increasing counter used to order invocations.
	/// </summary>
	/// <param name="value">The initial counter value.</param>
	public sealed class Counter(in long value = 0L)
	{
		private long _value = value;

		/// <summary>
		/// Atomically increments the counter and returns the new value.
		/// </summary>
		/// <returns>The incremented counter value.</returns>
		public long Increment()
		{
			return Interlocked.Increment(ref _value);
		}
	}
}
