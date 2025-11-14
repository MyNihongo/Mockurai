namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class SetupIntInt
	: ISetupCallbackJoin<Action<int, int>>, ISetupCallbackReset<Action<int, int>>,
		ISetupThrowsJoin<Action<int, int>>, ISetupThrowsReset<Action<int, int>>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

	public void Invoke(in int parameter1, in int parameter2)
	{
		if (_setups is null)
			return;

		foreach (var setup in _setups)
		{
			if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
				continue;
			if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(parameter1, parameter2);

			if (x.Exception is not null)
				throw x.Exception;
		}
	}

	public void SetupParameters(in It<int> setup1, in It<int> setup2)
	{
		_currentSetup = new Item(setup1.ValueSetup, setup2.ValueSetup);

		_setups ??= new SetupContainer<Item>(SortComparer);
		_setups.Add(_currentSetup);
	}

	public void Callback(in Action<int, int> callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(callback);
	}

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(exception);
	}

	ISetupCallbackJoin<Action<int, int>> ISetupCallbackStart<Action<int, int>>.Callback(in Action<int, int> callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<Action<int, int>> ISetupCallbackReset<Action<int, int>>.Callback(in Action<int, int> callback)
	{
		Callback(callback);
		return this;
	}

	ISetupThrowsJoin<Action<int, int>> ISetupThrowsStart<Action<int, int>>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<Action<int, int>> ISetupThrowsReset<Action<int, int>>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupThrowsReset<Action<int, int>> ISetupCallbackJoin<Action<int, int>>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	ISetupCallbackReset<Action<int, int>> ISetupThrowsJoin<Action<int, int>>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		private readonly Queue<ItemSetup> _queue = [];
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private ItemSetup? _currentSetup;
		public bool AndContinue;

		public void Add(in Action<int, int> callback)
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

	private sealed class ItemSetup(in Action<int, int>? callback = null, in Exception? exception = null)
	{
		public static readonly ItemSetup Default = new();

		public Action<int, int>? Callback = callback;
		public Exception? Exception = exception;
	}

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x is not null)
			{
				if (x.Parameter1.HasValue)
					xSort += x.Parameter1.Value.Sort;
				if (x.Parameter2.HasValue)
					xSort += x.Parameter2.Value.Sort;
			}

			if (y is not null)
			{
				if (y.Parameter1.HasValue)
					ySort += y.Parameter1.Value.Sort;
				if (y.Parameter2.HasValue)
					ySort += y.Parameter2.Value.Sort;
			}

			return xSort.CompareTo(ySort);
		}
	}
}

[Obsolete("Will be generated")]
public sealed class SetupIntInt<TReturns>
	: ISetupCallbackJoin<Action<int, int>, TReturns, Func<int, int, TReturns?>>, ISetupCallbackReset<Action<int, int>, TReturns, Func<int, int, TReturns?>>,
		ISetupReturnsThrowsJoin<Action<int, int>, TReturns, Func<int, int, TReturns?>>, ISetupReturnsThrowsReset<Action<int, int>, TReturns, Func<int, int, TReturns?>>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

	public bool Execute(in int parameter1, in int parameter2, out TReturns? returnValue)
	{
		if (_setups is null)
			goto Default;

		foreach (var setup in _setups)
		{
			if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
				continue;
			if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(parameter1, parameter2);

			if (x.Exception is not null)
				throw x.Exception;

			if (x.Returns is not null)
			{
				returnValue = x.Returns(parameter1, parameter2);
				return true;
			}

			returnValue = default;
			return false;
		}

		Default:
		returnValue = default;
		return false;
	}

	public void SetupParameters(in It<int> setup1, in It<int> setup2)
	{
		_currentSetup = new Item(setup1.ValueSetup, setup2.ValueSetup);

		_setups ??= new SetupContainer<Item>(SortComparer);
		_setups.Add(_currentSetup);
	}

	public void Callback(in Action<int, int> callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(callback);
	}

	public void Returns(TReturns? returns)
	{
		Returns((_, _) => returns);
	}

	public void Returns(in Func<int, int, TReturns?> returns)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(returns);
	}

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(exception);
	}

	ISetupCallbackJoin<Action<int, int>, TReturns, Func<int, int, TReturns?>> ISetupCallbackStart<Action<int, int>, TReturns, Func<int, int, TReturns?>>.Callback(in Action<int, int> callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<Action<int, int>, TReturns, Func<int, int, TReturns?>> ISetupCallbackReset<Action<int, int>, TReturns, Func<int, int, TReturns?>>.Callback(in Action<int, int> callback)
	{
		Callback(callback);
		return this;
	}

	ISetupReturnsThrowsJoin<Action<int, int>, TReturns, Func<int, int, TReturns?>> ISetupReturnsThrowsStart<Action<int, int>, TReturns, Func<int, int, TReturns?>>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<Action<int, int>, TReturns, Func<int, int, TReturns?>> ISetupReturnsThrowsReset<Action<int, int>, TReturns, Func<int, int, TReturns?>>.Returns(in Func<int, int, TReturns?> returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<Action<int, int>, TReturns, Func<int, int, TReturns?>> ISetupReturnsThrowsStart<Action<int, int>, TReturns, Func<int, int, TReturns?>>.Returns(in Func<int, int, TReturns?> returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<Action<int, int>, TReturns, Func<int, int, TReturns?>> ISetupReturnsThrowsReset<Action<int, int>, TReturns, Func<int, int, TReturns?>>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<Action<int, int>, TReturns, Func<int, int, TReturns?>> ISetupReturnsThrowsReset<Action<int, int>, TReturns, Func<int, int, TReturns?>>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupReturnsThrowsJoin<Action<int, int>, TReturns, Func<int, int, TReturns?>> ISetupReturnsThrowsStart<Action<int, int>, TReturns, Func<int, int, TReturns?>>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupReturnsThrowsReset<Action<int, int>, TReturns, Func<int, int, TReturns?>> ISetupCallbackJoin<Action<int, int>, TReturns, Func<int, int, TReturns?>>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	ISetupCallbackReset<Action<int, int>, TReturns, Func<int, int, TReturns?>> ISetupReturnsThrowsJoin<Action<int, int>, TReturns, Func<int, int, TReturns?>>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;

		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;
		public bool AndContinue;

		public void Add(in Action<int, int> callback)
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

		public void Add(in Func<int, int, TReturns?> returns)
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

	private sealed class ItemSetup(in Action<int, int>? callback = null, in Func<int, int, TReturns?>? returns = null, in Exception? exception = null)
	{
		public static readonly ItemSetup Default = new();

		public Action<int, int>? Callback = callback;
		public Func<int, int, TReturns?>? Returns = returns;
		public Exception? Exception = exception;
	}

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x is not null)
			{
				if (x.Parameter1.HasValue)
					xSort += x.Parameter1.Value.Sort;
				if (x.Parameter2.HasValue)
					xSort += x.Parameter2.Value.Sort;
			}

			if (y is not null)
			{
				if (y.Parameter1.HasValue)
					ySort += y.Parameter1.Value.Sort;
				if (y.Parameter2.HasValue)
					ySort += y.Parameter2.Value.Sort;
			}

			return xSort.CompareTo(ySort);
		}
	}
}

[Obsolete("Will be generated")]
public sealed class SetupRefIntInt
	: ISetupCallbackJoin<SetupRefIntInt.CallbackDelegate>, ISetupCallbackReset<SetupRefIntInt.CallbackDelegate>,
		ISetupThrowsJoin<SetupRefIntInt.CallbackDelegate>, ISetupThrowsReset<SetupRefIntInt.CallbackDelegate>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

	public delegate void CallbackDelegate(ref int parameter1, int parameter2);

	public void Invoke(ref int parameter1, in int parameter2)
	{
		if (_setups is null)
			return;

		foreach (var setup in _setups)
		{
			if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
				continue;
			if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(ref parameter1, parameter2);

			if (x.Exception is not null)
				throw x.Exception;
		}
	}

	public void SetupParameters(in It<int> setup1, in It<int> setup2)
	{
		_currentSetup = new Item(setup1.ValueSetup, setup2.ValueSetup);

		_setups ??= new SetupContainer<Item>(SortComparer);
		_setups.Add(_currentSetup);
	}

	public void Callback(in CallbackDelegate callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(callback);
	}

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(exception);
	}

	ISetupCallbackJoin<CallbackDelegate> ISetupCallbackStart<CallbackDelegate>.Callback(in CallbackDelegate callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<CallbackDelegate> ISetupCallbackReset<CallbackDelegate>.Callback(in CallbackDelegate callback)
	{
		Callback(callback);
		return this;
	}

	ISetupThrowsJoin<CallbackDelegate> ISetupThrowsStart<CallbackDelegate>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<CallbackDelegate> ISetupThrowsReset<CallbackDelegate>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupThrowsReset<CallbackDelegate> ISetupCallbackJoin<CallbackDelegate>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	ISetupCallbackReset<CallbackDelegate> ISetupThrowsJoin<CallbackDelegate>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;
		public bool AndContinue;

		public void Add(in CallbackDelegate callback)
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

	private sealed class ItemSetup(in CallbackDelegate? callback = null, in Exception? exception = null)
	{
		public static readonly ItemSetup Default = new();

		public CallbackDelegate? Callback = callback;
		public Exception? Exception = exception;
	}

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x is not null)
			{
				if (x.Parameter1.HasValue)
					xSort += x.Parameter1.Value.Sort;
				if (x.Parameter2.HasValue)
					xSort += x.Parameter2.Value.Sort;
			}

			if (y is not null)
			{
				if (y.Parameter1.HasValue)
					ySort += y.Parameter1.Value.Sort;
				if (y.Parameter2.HasValue)
					ySort += y.Parameter2.Value.Sort;
			}

			return xSort.CompareTo(ySort);
		}
	}
}

[Obsolete("Will be generated")]
public sealed class SetupRefIntInt<TReturns>
	: ISetupCallbackJoin<SetupRefIntInt<TReturns>.CallbackDelegate, TReturns, SetupRefIntInt<TReturns>.ReturnsCallbackDelegate>, ISetupCallbackReset<SetupRefIntInt<TReturns>.CallbackDelegate, TReturns, SetupRefIntInt<TReturns>.ReturnsCallbackDelegate>,
		ISetupReturnsThrowsJoin<SetupRefIntInt<TReturns>.CallbackDelegate, TReturns, SetupRefIntInt<TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsReset<SetupRefIntInt<TReturns>.CallbackDelegate, TReturns, SetupRefIntInt<TReturns>.ReturnsCallbackDelegate>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

	public delegate void CallbackDelegate(ref int parameter1, int parameter2);

	public delegate TReturns? ReturnsCallbackDelegate(ref int parameter1, int parameter2);

	public bool Execute(ref int parameter1, in int parameter2, out TReturns? returnValue)
	{
		if (_setups is null)
			goto Default;

		foreach (var setup in _setups)
		{
			if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
				continue;
			if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(ref parameter1, parameter2);

			if (x.Exception is not null)
				throw x.Exception;

			if (x.Returns is not null)
			{
				returnValue = x.Returns(ref parameter1, parameter2);
				return true;
			}

			returnValue = default;
			return false;
		}

		Default:
		returnValue = default;
		return false;
	}

	public void SetupParameters(in It<int> setup1, in It<int> setup2)
	{
		_currentSetup = new Item(setup1.ValueSetup, setup2.ValueSetup);

		_setups ??= new SetupContainer<Item>(SortComparer);
		_setups.Add(_currentSetup);
	}

	public void Callback(in CallbackDelegate callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(callback);
	}

	public void Returns(TReturns? returns)
	{
		Returns((ref _, _) => returns);
	}

	public void Returns(in ReturnsCallbackDelegate returns)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(returns);
	}

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(exception);
	}

	ISetupCallbackJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupCallbackStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupCallbackReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
	{
		Callback(callback);
		return this;
	}

	ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupCallbackJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	ISetupCallbackReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;
		public bool AndContinue;

		public void Add(in CallbackDelegate callback)
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

		public void Add(in ReturnsCallbackDelegate returns)
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

	private sealed class ItemSetup(in CallbackDelegate? callback = null, in ReturnsCallbackDelegate? returns = null, in Exception? exception = null)
	{
		public static readonly ItemSetup Default = new();

		public CallbackDelegate? Callback = callback;
		public ReturnsCallbackDelegate? Returns = returns;
		public Exception? Exception = exception;
	}

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x is not null)
			{
				if (x.Parameter1.HasValue)
					xSort += x.Parameter1.Value.Sort;
				if (x.Parameter2.HasValue)
					xSort += x.Parameter2.Value.Sort;
			}

			if (y is not null)
			{
				if (y.Parameter1.HasValue)
					ySort += y.Parameter1.Value.Sort;
				if (y.Parameter2.HasValue)
					ySort += y.Parameter2.Value.Sort;
			}

			return xSort.CompareTo(ySort);
		}
	}
}

[Obsolete("Will be generated")]
public sealed class SetupRefT1T2<T1, T2, TReturns>
	: ISetupCallbackJoin<SetupRefT1T2<T1, T2, TReturns>.CallbackDelegate, TReturns, SetupRefT1T2<T1, T2, TReturns>.ReturnsCallbackDelegate>, ISetupCallbackReset<SetupRefT1T2<T1, T2, TReturns>.CallbackDelegate, TReturns, SetupRefT1T2<T1, T2, TReturns>.ReturnsCallbackDelegate>,
		ISetupReturnsThrowsJoin<SetupRefT1T2<T1, T2, TReturns>.CallbackDelegate, TReturns, SetupRefT1T2<T1, T2, TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsReset<SetupRefT1T2<T1, T2, TReturns>.CallbackDelegate, TReturns, SetupRefT1T2<T1, T2, TReturns>.ReturnsCallbackDelegate>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

	public delegate void CallbackDelegate(ref T1 parameter1, T2 parameter2);

	public delegate TReturns? ReturnsCallbackDelegate(ref T1 parameter1, T2 parameter2);

	public bool Execute(ref T1 parameter1, in T2 parameter2, out TReturns? returnValue)
	{
		if (_setups is null)
			goto Default;

		foreach (var setup in _setups)
		{
			if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
				continue;
			if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(ref parameter1, parameter2);

			if (x.Exception is not null)
				throw x.Exception;

			if (x.Returns is not null)
			{
				returnValue = x.Returns(ref parameter1, parameter2);
				return true;
			}

			returnValue = default;
			return false;
		}

		Default:
		returnValue = default;
		return false;
	}

	public void SetupParameters(in It<T1> setup1, in It<T2> setup2)
	{
		_currentSetup = new Item(setup1.ValueSetup, setup2.ValueSetup);

		_setups ??= new SetupContainer<Item>(SortComparer);
		_setups.Add(_currentSetup);
	}

	public void Callback(in CallbackDelegate callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(callback);
	}

	public void Returns(TReturns? returns)
	{
		Returns((ref _, _) => returns);
	}

	public void Returns(in ReturnsCallbackDelegate returns)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(returns);
	}

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(exception);
	}

	ISetupCallbackJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupCallbackStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupCallbackReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
	{
		Callback(callback);
		return this;
	}

	ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupCallbackJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	ISetupCallbackReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	private sealed class Item(in It<T1>.Setup? parameter1, in It<T2>.Setup? parameter2)
	{
		public readonly It<T1>.Setup? Parameter1 = parameter1;
		public readonly It<T2>.Setup? Parameter2 = parameter2;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;
		public bool AndContinue;

		public void Add(in CallbackDelegate callback)
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

		public void Add(in ReturnsCallbackDelegate returns)
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

	private sealed class ItemSetup(in CallbackDelegate? callback = null, in ReturnsCallbackDelegate? returns = null, in Exception? exception = null)
	{
		public static readonly ItemSetup Default = new();

		public CallbackDelegate? Callback = callback;
		public ReturnsCallbackDelegate? Returns = returns;
		public Exception? Exception = exception;
	}

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x is not null)
			{
				if (x.Parameter1.HasValue)
					xSort += x.Parameter1.Value.Sort;
				if (x.Parameter2.HasValue)
					xSort += x.Parameter2.Value.Sort;
			}

			if (y is not null)
			{
				if (y.Parameter1.HasValue)
					ySort += y.Parameter1.Value.Sort;
				if (y.Parameter2.HasValue)
					ySort += y.Parameter2.Value.Sort;
			}

			return xSort.CompareTo(ySort);
		}
	}
}

[Obsolete("Will be generated")]
public sealed class SetupIntRefInt
	: ISetupCallbackJoin<SetupIntRefInt.CallbackDelegate>, ISetupCallbackReset<SetupIntRefInt.CallbackDelegate>,
		ISetupThrowsJoin<SetupIntRefInt.CallbackDelegate>, ISetupThrowsReset<SetupIntRefInt.CallbackDelegate>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

	public delegate void CallbackDelegate(int parameter1, ref int parameter2);

	public void Invoke(in int parameter1, ref int parameter2)
	{
		if (_setups is null)
			return;

		foreach (var setup in _setups)
		{
			if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
				continue;
			if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(parameter1, ref parameter2);

			if (x.Exception is not null)
				throw x.Exception;
		}
	}

	public void SetupParameters(in It<int> setup1, in It<int> setup2)
	{
		_currentSetup = new Item(setup1.ValueSetup, setup2.ValueSetup);

		_setups ??= new SetupContainer<Item>(SortComparer);
		_setups.Add(_currentSetup);
	}

	public void Callback(in CallbackDelegate callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(callback);
	}

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(exception);
	}

	ISetupCallbackJoin<CallbackDelegate> ISetupCallbackStart<CallbackDelegate>.Callback(in CallbackDelegate callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<CallbackDelegate> ISetupCallbackReset<CallbackDelegate>.Callback(in CallbackDelegate callback)
	{
		Callback(callback);
		return this;
	}

	ISetupThrowsJoin<CallbackDelegate> ISetupThrowsStart<CallbackDelegate>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<CallbackDelegate> ISetupThrowsReset<CallbackDelegate>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupThrowsReset<CallbackDelegate> ISetupCallbackJoin<CallbackDelegate>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	ISetupCallbackReset<CallbackDelegate> ISetupThrowsJoin<CallbackDelegate>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;
		public bool AndContinue;

		public void Add(in CallbackDelegate callback)
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

	private sealed class ItemSetup(in CallbackDelegate? callback = null, in Exception? exception = null)
	{
		public static readonly ItemSetup Default = new();

		public CallbackDelegate? Callback = callback;
		public Exception? Exception = exception;
	}

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x is not null)
			{
				if (x.Parameter1.HasValue)
					xSort += x.Parameter1.Value.Sort;
				if (x.Parameter2.HasValue)
					xSort += x.Parameter2.Value.Sort;
			}

			if (y is not null)
			{
				if (y.Parameter1.HasValue)
					ySort += y.Parameter1.Value.Sort;
				if (y.Parameter2.HasValue)
					ySort += y.Parameter2.Value.Sort;
			}

			return xSort.CompareTo(ySort);
		}
	}
}

[Obsolete("Will be generated")]
public sealed class SetupIntRefInt<TReturns>
	: ISetupCallbackJoin<SetupIntRefInt<TReturns>.CallbackDelegate, TReturns, SetupIntRefInt<TReturns>.ReturnsCallbackDelegate>, ISetupCallbackReset<SetupIntRefInt<TReturns>.CallbackDelegate, TReturns, SetupIntRefInt<TReturns>.ReturnsCallbackDelegate>,
		ISetupReturnsThrowsJoin<SetupIntRefInt<TReturns>.CallbackDelegate, TReturns, SetupIntRefInt<TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsReset<SetupIntRefInt<TReturns>.CallbackDelegate, TReturns, SetupIntRefInt<TReturns>.ReturnsCallbackDelegate>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

	public delegate void CallbackDelegate(int parameter1, ref int parameter2);

	public delegate TReturns? ReturnsCallbackDelegate(int parameter1, ref int parameter2);

	public bool Execute(in int parameter1, ref int parameter2, out TReturns? returnValue)
	{
		if (_setups is null)
			goto Default;

		foreach (var setup in _setups)
		{
			if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
				continue;
			if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(parameter1, ref parameter2);

			if (x.Exception is not null)
				throw x.Exception;

			if (x.Returns is not null)
			{
				returnValue = x.Returns(parameter1, ref parameter2);
				return true;
			}

			returnValue = default;
			return false;
		}

		Default:
		returnValue = default;
		return false;
	}

	public void SetupParameters(in It<int> setup1, in It<int> setup2)
	{
		_currentSetup = new Item(setup1.ValueSetup, setup2.ValueSetup);

		_setups ??= new SetupContainer<Item>(SortComparer);
		_setups.Add(_currentSetup);
	}

	public void Callback(in CallbackDelegate callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(callback);
	}

	public void Returns(TReturns? returns)
	{
		Returns((_, ref _) => returns);
	}

	public void Returns(in ReturnsCallbackDelegate value)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(value);
	}

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(exception);
	}

	ISetupCallbackJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupCallbackStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupCallbackReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
	{
		Callback(callback);
		return this;
	}

	ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupCallbackJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	ISetupCallbackReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;
		public bool AndContinue;

		public void Add(in CallbackDelegate callback)
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

		public void Add(in ReturnsCallbackDelegate returns)
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

	private sealed class ItemSetup(in CallbackDelegate? callback = null, in ReturnsCallbackDelegate? returns = null, in Exception? exception = null)
	{
		public static readonly ItemSetup Default = new();

		public CallbackDelegate? Callback = callback;
		public ReturnsCallbackDelegate? Returns = returns;
		public Exception? Exception = exception;
	}

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x is not null)
			{
				if (x.Parameter1.HasValue)
					xSort += x.Parameter1.Value.Sort;
				if (x.Parameter2.HasValue)
					xSort += x.Parameter2.Value.Sort;
			}

			if (y is not null)
			{
				if (y.Parameter1.HasValue)
					ySort += y.Parameter1.Value.Sort;
				if (y.Parameter2.HasValue)
					ySort += y.Parameter2.Value.Sort;
			}

			return xSort.CompareTo(ySort);
		}
	}
}

[Obsolete("Will be generated")]
public sealed class SetupRefIntRefInt
	: ISetupCallbackJoin<SetupRefIntRefInt.CallbackDelegate>, ISetupCallbackReset<SetupRefIntRefInt.CallbackDelegate>,
		ISetupThrowsJoin<SetupRefIntRefInt.CallbackDelegate>, ISetupThrowsReset<SetupRefIntRefInt.CallbackDelegate>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

	public delegate void CallbackDelegate(ref int parameter1, ref int parameter2);

	public void Invoke(ref int parameter1, ref int parameter2)
	{
		if (_setups is null)
			return;

		foreach (var setup in _setups)
		{
			if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
				continue;
			if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(ref parameter1, ref parameter2);

			if (x.Exception is not null)
				throw x.Exception;
		}
	}

	public void SetupParameters(in It<int> setup1, in It<int> setup2)
	{
		_currentSetup = new Item(setup1.ValueSetup, setup2.ValueSetup);

		_setups ??= new SetupContainer<Item>(SortComparer);
		_setups.Add(_currentSetup);
	}

	public void Callback(in CallbackDelegate callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(callback);
	}

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(exception);
	}

	ISetupCallbackJoin<CallbackDelegate> ISetupCallbackStart<CallbackDelegate>.Callback(in CallbackDelegate callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<CallbackDelegate> ISetupCallbackReset<CallbackDelegate>.Callback(in CallbackDelegate callback)
	{
		Callback(callback);
		return this;
	}

	ISetupThrowsJoin<CallbackDelegate> ISetupThrowsStart<CallbackDelegate>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<CallbackDelegate> ISetupThrowsReset<CallbackDelegate>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupThrowsReset<CallbackDelegate> ISetupCallbackJoin<CallbackDelegate>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	ISetupCallbackReset<CallbackDelegate> ISetupThrowsJoin<CallbackDelegate>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;
		public bool AndContinue;

		public void Add(in CallbackDelegate callback)
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

	private sealed class ItemSetup(in CallbackDelegate? callback = null, in Exception? exception = null)
	{
		public static readonly ItemSetup Default = new();

		public CallbackDelegate? Callback = callback;
		public Exception? Exception = exception;
	}

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x is not null)
			{
				if (x.Parameter1.HasValue)
					xSort += x.Parameter1.Value.Sort;
				if (x.Parameter2.HasValue)
					xSort += x.Parameter2.Value.Sort;
			}

			if (y is not null)
			{
				if (y.Parameter1.HasValue)
					ySort += y.Parameter1.Value.Sort;
				if (y.Parameter2.HasValue)
					ySort += y.Parameter2.Value.Sort;
			}

			return xSort.CompareTo(ySort);
		}
	}
}

[Obsolete("Will be generated")]
public sealed class SetupRefIntRefInt<TReturns>
	: ISetupCallbackJoin<SetupRefIntRefInt<TReturns>.CallbackDelegate, TReturns, SetupRefIntRefInt<TReturns>.ReturnsCallbackDelegate>, ISetupCallbackReset<SetupRefIntRefInt<TReturns>.CallbackDelegate, TReturns, SetupRefIntRefInt<TReturns>.ReturnsCallbackDelegate>,
		ISetupReturnsThrowsJoin<SetupRefIntRefInt<TReturns>.CallbackDelegate, TReturns, SetupRefIntRefInt<TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsReset<SetupRefIntRefInt<TReturns>.CallbackDelegate, TReturns, SetupRefIntRefInt<TReturns>.ReturnsCallbackDelegate>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

	public delegate void CallbackDelegate(ref int parameter1, ref int parameter2);

	public delegate TReturns? ReturnsCallbackDelegate(ref int parameter1, ref int parameter2);

	public bool Execute(ref int parameter1, ref int parameter2, out TReturns? returnValue)
	{
		if (_setups is null)
			goto Default;

		foreach (var setup in _setups)
		{
			if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
				continue;
			if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(ref parameter1, ref parameter2);

			if (x.Exception is not null)
				throw x.Exception;

			if (x.Returns is not null)
			{
				returnValue = x.Returns(ref parameter1, ref parameter2);
				return true;
			}

			returnValue = default;
			return false;
		}

		Default:
		returnValue = default;
		return false;
	}

	public void SetupParameters(in It<int> setup1, in It<int> setup2)
	{
		_currentSetup = new Item(setup1.ValueSetup, setup2.ValueSetup);

		_setups ??= new SetupContainer<Item>(SortComparer);
		_setups.Add(_currentSetup);
	}

	public void Callback(in CallbackDelegate callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(callback);
	}

	public void Returns(TReturns? returns)
	{
		Returns((ref _, ref _) => returns);
	}

	public void Returns(in ReturnsCallbackDelegate value)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(value);
	}

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(exception);
	}

	ISetupCallbackJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupCallbackStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupCallbackReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
	{
		Callback(callback);
		return this;
	}

	ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsStart<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupReturnsThrowsReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupCallbackJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	ISetupCallbackReset<CallbackDelegate, TReturns, ReturnsCallbackDelegate> ISetupReturnsThrowsJoin<CallbackDelegate, TReturns, ReturnsCallbackDelegate>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;
		public bool AndContinue;

		public void Add(in CallbackDelegate callback)
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

		public void Add(in ReturnsCallbackDelegate returns)
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

	private sealed class ItemSetup(in CallbackDelegate? callback = null, in ReturnsCallbackDelegate? returns = null, in Exception? exception = null)
	{
		public static readonly ItemSetup Default = new();

		public CallbackDelegate? Callback = callback;
		public ReturnsCallbackDelegate? Returns = returns;
		public Exception? Exception = exception;
	}

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x is not null)
			{
				if (x.Parameter1.HasValue)
					xSort += x.Parameter1.Value.Sort;
				if (x.Parameter2.HasValue)
					xSort += x.Parameter2.Value.Sort;
			}

			if (y is not null)
			{
				if (y.Parameter1.HasValue)
					ySort += y.Parameter1.Value.Sort;
				if (y.Parameter2.HasValue)
					ySort += y.Parameter2.Value.Sort;
			}

			return xSort.CompareTo(ySort);
		}
	}
}

[Obsolete("Will be generated")]
public sealed class SetupT1Int<T1>
	: ISetupCallbackJoin<Action<T1, int>>, ISetupCallbackReset<Action<T1, int>>,
		ISetupThrowsJoin<Action<T1, int>>, ISetupThrowsReset<Action<T1, int>>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

	public void Invoke(in T1 parameter1, in int parameter2)
	{
		if (_setups is null)
			return;

		foreach (var setup in _setups)
		{
			if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
				continue;
			if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(parameter1, parameter2);

			if (x.Exception is not null)
				throw x.Exception;
		}
	}

	public void SetupParameters(in It<T1> setup1, in It<int> setup2)
	{
		_currentSetup = new Item(setup1.ValueSetup, setup2.ValueSetup);

		_setups ??= new SetupContainer<Item>(SortComparer);
		_setups.Add(_currentSetup);
	}

	public void Callback(in Action<T1, int> callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(callback);
	}

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(exception);
	}

	ISetupCallbackJoin<Action<T1, int>> ISetupCallbackStart<Action<T1, int>>.Callback(in Action<T1, int> callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<Action<T1, int>> ISetupCallbackReset<Action<T1, int>>.Callback(in Action<T1, int> callback)
	{
		Callback(callback);
		return this;
	}

	ISetupThrowsJoin<Action<T1, int>> ISetupThrowsStart<Action<T1, int>>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<Action<T1, int>> ISetupThrowsReset<Action<T1, int>>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupThrowsReset<Action<T1, int>> ISetupCallbackJoin<Action<T1, int>>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	ISetupCallbackReset<Action<T1, int>> ISetupThrowsJoin<Action<T1, int>>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	private sealed class Item(in It<T1>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		private readonly Queue<ItemSetup> _queue = [];
		public readonly It<T1>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private ItemSetup? _currentSetup;
		public bool AndContinue;

		public void Add(in Action<T1, int> callback)
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

	private sealed class ItemSetup(in Action<T1, int>? callback = null, in Exception? exception = null)
	{
		public static readonly ItemSetup Default = new();

		public Action<T1, int>? Callback = callback;
		public Exception? Exception = exception;
	}

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x is not null)
			{
				if (x.Parameter1.HasValue)
					xSort += x.Parameter1.Value.Sort;
				if (x.Parameter2.HasValue)
					xSort += x.Parameter2.Value.Sort;
			}

			if (y is not null)
			{
				if (y.Parameter1.HasValue)
					ySort += y.Parameter1.Value.Sort;
				if (y.Parameter2.HasValue)
					ySort += y.Parameter2.Value.Sort;
			}

			return xSort.CompareTo(ySort);
		}
	}
}
