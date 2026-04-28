namespace MyNihongo.Mockurai;

/// <summary>
/// Base class for void mock setups, maintaining a FIFO queue of configured actions and supporting
/// <c>Callback</c>/<c>Throws</c> pairing via the <c>And</c> operator.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type invoked when the mocked member is called.</typeparam>
public abstract class SetupBase<TCallback>
	: ISetupCallbackJoin<TCallback>, ISetupCallbackReset<TCallback>,
		ISetupThrowsJoin<TCallback>, ISetupThrowsReset<TCallback>
{
	private readonly Queue<ItemSetup> _queue = [];
	private ItemSetup? _currentSetup;
	private bool _andContinue;

	/// <summary>
	/// Configures a callback to run when the mocked member is invoked, either as a new step
	/// or merged with the previous step when following an <c>And</c>.
	/// </summary>
	/// <param name="callback">The callback to invoke.</param>
	public void Callback(in TCallback callback)
	{
		if (_andContinue && _currentSetup is not null)
		{
			_currentSetup.Callback = callback;
			_andContinue = false;
			_currentSetup = null;
		}
		else
		{
			_currentSetup = new ItemSetup(callback);
			_queue.Enqueue(_currentSetup);
		}
	}

	/// <summary>
	/// Configures an exception to be thrown when the mocked member is invoked, either as a new step
	/// or merged with the previous step when following an <c>And</c>.
	/// </summary>
	/// <param name="exception">The exception to throw.</param>
	public void Throws(in Exception exception)
	{
		if (_andContinue && _currentSetup is not null)
		{
			_currentSetup.Exception = exception;
			_andContinue = false;
			_currentSetup = null;
		}
		else
		{
			_currentSetup = new ItemSetup(exception: exception);
			_queue.Enqueue(_currentSetup);
		}
	}

	/// <summary>
	/// Returns the next queued setup action; if only one remains it is preserved as the default for subsequent calls.
	/// </summary>
	protected ItemSetup GetSetup()
	{
		return _queue.Count switch
		{
			0 => ItemSetup.Default,
			1 => _queue.Peek(),
			_ => _queue.Dequeue(),
		};
	}

	ISetupCallbackJoin<TCallback> ISetupCallbackStart<TCallback>.Callback(in TCallback callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<TCallback> ISetupCallbackReset<TCallback>.Callback(in TCallback callback)
	{
		Callback(callback);
		return this;
	}

	ISetupThrowsJoin<TCallback> ISetupThrowsStart<TCallback>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<TCallback> ISetupThrowsReset<TCallback>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupThrowsReset<TCallback> ISetupCallbackJoin<TCallback>.And()
	{
		_andContinue = true;
		return this;
	}

	ISetupCallbackReset<TCallback> ISetupThrowsJoin<TCallback>.And()
	{
		_andContinue = true;
		return this;
	}

	/// <summary>
	/// A single configured setup step combining an optional callback and an optional exception.
	/// </summary>
	/// <param name="callback">The callback to invoke, if any.</param>
	/// <param name="exception">The exception to throw, if any.</param>
	protected sealed class ItemSetup(in TCallback? callback = default, in Exception? exception = null)
	{
		/// <summary>
		/// The default no-op setup used when no setups have been configured.
		/// </summary>
		public static readonly ItemSetup Default = new();

		/// <summary>
		/// The callback to invoke, or <see langword="default"/> if none was configured.
		/// </summary>
		public TCallback? Callback = callback;

		/// <summary>
		/// The exception to throw, or <see langword="null"/> if none was configured.
		/// </summary>
		public Exception? Exception = exception;
	}
}
