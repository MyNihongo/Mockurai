using Microsoft.Extensions.Primitives;

namespace MyNihongo.Mockurai;

internal sealed class NullChangeToken : IChangeToken
{
	public static readonly NullChangeToken Instance = new();

	private NullChangeToken()
	{
	}

	public bool HasChanged => false;

	public bool ActiveChangeCallbacks => false;

	public IDisposable RegisterChangeCallback(Action<object?> callback, object? state) =>
		EmptyDisposable.EmptyInstance;

	private sealed class EmptyDisposable : IDisposable
	{
		public static readonly EmptyDisposable EmptyInstance = new();

		public void Dispose()
		{
		}
	}
}
