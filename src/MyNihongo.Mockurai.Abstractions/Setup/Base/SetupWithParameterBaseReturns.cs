namespace MyNihongo.Mockurai;

/// <summary>
/// Base class for returning mock setups that match invocations against a single parameter, ordered so that
/// more specific matchers run before less specific ones.
/// </summary>
/// <typeparam name="TParameter">The argument type captured by the setup.</typeparam>
/// <typeparam name="TCallback">The callback delegate type invoked when the mocked member is called.</typeparam>
/// <typeparam name="TReturns">The return value type of the mocked member.</typeparam>
/// <typeparam name="TReturnsCallback">The delegate type used to compute a return value dynamically.</typeparam>
public abstract class SetupWithParameterBase<TParameter, TCallback, TReturns, TReturnsCallback>
	: ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback>, ISetupCallbackReset<TCallback, TReturns, TReturnsCallback>,
		ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback>, ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback>
{
	private static readonly Comparer SortComparer = new();

	/// <summary>
	/// The lazily-initialized container of per-argument setups, ordered by matcher specificity.
	/// </summary>
	protected SetupContainer<Item>? Setups;
	private Item? _currentSetup;

	/// <summary>
	/// Configures the mocked member to return the specified constant value.
	/// </summary>
	/// <param name="returns">The value to return.</param>
	public abstract void Returns(TReturns returns);

	/// <summary>
	/// Begins a new per-argument setup that subsequent <c>Callback</c>/<c>Returns</c>/<c>Throws</c> calls will configure.
	/// </summary>
	/// <param name="parameter">The argument matcher used to filter invocations.</param>
	public void SetupParameter(in ItSetup<TParameter> parameter)
	{
		_currentSetup = new Item(parameter);

		Setups ??= new SetupContainer<Item>(SortComparer);
		Setups.Add(_currentSetup);
	}

	/// <summary>
	/// Configures a callback for the current per-argument setup.
	/// </summary>
	/// <param name="callback">The callback to invoke.</param>
	/// <exception cref="InvalidOperationException">Thrown when called before <see cref="SetupParameter"/>.</exception>
	public void Callback(in TCallback callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(callback);
	}

	/// <summary>
	/// Configures a delegate that computes the return value for the current per-argument setup.
	/// </summary>
	/// <param name="value">The delegate that produces the return value.</param>
	/// <exception cref="InvalidOperationException">Thrown when called before <see cref="SetupParameter"/>.</exception>
	public void Returns(in TReturnsCallback value)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(value);
	}

	/// <summary>
	/// Configures an exception to be thrown by the current per-argument setup.
	/// </summary>
	/// <param name="exception">The exception to throw.</param>
	/// <exception cref="InvalidOperationException">Thrown when called before <see cref="SetupParameter"/>.</exception>
	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(exception);
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
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	ISetupCallbackReset<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	/// <summary>
	/// A grouping of configured actions for invocations that match a particular argument matcher.
	/// </summary>
	/// <param name="parameter">The argument matcher associated with this setup, or <see langword="null"/> for the catch-all.</param>
	protected sealed class Item(in ItSetup<TParameter>? parameter)
	{
		/// <summary>
		/// The argument matcher associated with this setup.
		/// </summary>
		public readonly ItSetup<TParameter>? Parameter = parameter;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;

		/// <summary>
		/// Indicates that the next configured action should be merged into the current setup step rather than appended as a new one.
		/// </summary>
		public bool AndContinue;

		/// <summary>
		/// Adds a callback to this group, either as a new step or merged with the previous step when <see cref="AndContinue"/> is set.
		/// </summary>
		/// <param name="callback">The callback to invoke.</param>
		public void Add(in TCallback callback)
		{
			if (AndContinue && _currentSetup is not null)
			{
				_currentSetup.Callback = callback;
				AndContinue = false;
				_currentSetup = null;
			}
			else
			{
				_currentSetup = new ItemSetup(callback);
				_queue.Enqueue(_currentSetup);
			}
		}

		/// <summary>
		/// Adds a return-value delegate to this group, either as a new step or merged with the previous step when <see cref="AndContinue"/> is set.
		/// </summary>
		/// <param name="returns">The delegate that produces the return value.</param>
		public void Add(in TReturnsCallback returns)
		{
			if (AndContinue && _currentSetup is not null)
			{
				_currentSetup.Returns = returns;
				AndContinue = false;
				_currentSetup = null;
			}
			else
			{
				_currentSetup = new ItemSetup(returns: returns);
				_queue.Enqueue(_currentSetup);
			}
		}

		/// <summary>
		/// Adds an exception to this group, either as a new step or merged with the previous step when <see cref="AndContinue"/> is set.
		/// </summary>
		/// <param name="exception">The exception to throw.</param>
		public void Add(in Exception exception)
		{
			if (AndContinue && _currentSetup is not null)
			{
				_currentSetup.Exception = exception;
				AndContinue = false;
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
		public ItemSetup GetSetup()
		{
			return _queue.Count switch
			{
				0 => ItemSetup.Default,
				1 => _queue.Peek(),
				_ => _queue.Dequeue(),
			};
		}
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

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x?.Parameter.HasValue == true)
				xSort += x.Parameter.Value.Sort;

			if (y?.Parameter.HasValue == true)
				ySort += y.Parameter.Value.Sort;

			return xSort.CompareTo(ySort);
		}
	}
}
