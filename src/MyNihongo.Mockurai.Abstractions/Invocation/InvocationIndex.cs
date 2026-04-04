namespace MyNihongo.Mockurai;

public static class InvocationIndex
{
	private static readonly AsyncLocal<Counter> Value = new();
	public static Counter CounterValue => Value.Value ??= new Counter();

	public sealed class Counter(in long value = 0L)
	{
		private long _value = value;

		public long Increment()
		{
			return Interlocked.Increment(ref _value);
		}
	}
}
