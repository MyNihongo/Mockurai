namespace MyNihongo.Mockurai;

/// <summary>
/// Base class for returning mock setups, maintaining a FIFO queue of configured actions and supporting
/// <c>Callback</c>/<c>Returns</c>/<c>Throws</c> pairing via the <c>And</c> operator.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type invoked when the mocked member is called.</typeparam>
/// <typeparam name="TReturns">The return value type of the mocked member.</typeparam>
/// <typeparam name="TReturnsCallback">The delegate type used to compute a return value dynamically.</typeparam>
public abstract class SetupBaseReturns<TCallback, TReturns, TReturnsCallback>
	: ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback>, ISetupCallbackReset<TCallback, TReturns, TReturnsCallback>,
		ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback>, ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback>
{
	private readonly Queue<ItemSetup> _queue = [];
	private ItemSetup? _currentSetup;
	private bool _andContinue;

	/// <summary>
	/// Configures the mocked member to return the specified constant value.
	/// </summary>
	/// <param name="returns">The value to return.</param>
	public abstract void Returns(TReturns returns);

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
	/// Configures a delegate that computes the return value when the mocked member is invoked, either as a new step
	/// or merged with the previous step when following an <c>And</c>.
	/// </summary>
	/// <param name="returns">The delegate that produces the return value.</param>
	public void Returns(in TReturnsCallback returns)
	{
		if (_andContinue && _currentSetup is not null)
		{
			_currentSetup.Returns = returns;
			_andContinue = false;
			_currentSetup = null;
		}
		else
		{
			_currentSetup = new ItemSetup(returns: returns);
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

	ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback> ISetupCallbackStart<TCallback, TReturns, TReturnsCallback>.Callback(in TCallback callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<TCallback, TReturns, TReturnsCallback> ISetupCallbackReset<TCallback, TReturns, TReturnsCallback>.Callback(in TCallback callback)
	{
		Callback(callback);
		return this;
	}

	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsStart<TCallback, TReturns, TReturnsCallback>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsStart<TCallback, TReturns, TReturnsCallback>.Returns(in TReturnsCallback returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback>.Returns(in TReturnsCallback returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsStart<TCallback, TReturns, TReturnsCallback>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback> ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback>.And()
	{
		_andContinue = true;
		return this;
	}

	ISetupCallbackReset<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback>.And()
	{
		_andContinue = true;
		return this;
	}

	/// <summary>
	/// A single configured setup step combining an optional callback, an optional return-value delegate, and an optional exception.
	/// </summary>
	/// <param name="callback">The callback to invoke, if any.</param>
	/// <param name="returns">The delegate that produces the return value, if any.</param>
	/// <param name="exception">The exception to throw, if any.</param>
	protected sealed class ItemSetup(in TCallback? callback = default, in TReturnsCallback? returns = default, in Exception? exception = null)
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
		/// The delegate that produces the return value, or <see langword="default"/> if none was configured.
		/// </summary>
		public TReturnsCallback? Returns = returns;

		/// <summary>
		/// The exception to throw, or <see langword="null"/> if none was configured.
		/// </summary>
		public Exception? Exception = exception;
	}
}
