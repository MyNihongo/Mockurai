namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class SetupIntInt : ISetup
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

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		private readonly Queue<ItemSetup> _queue = [];
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private ItemSetup? _currentSetup;

		public void Add(in Action<int, int> callback)
		{
			var currentSetup = new ItemSetup(callback);
			_queue.Enqueue(currentSetup);
			_currentSetup = currentSetup;
		}

		public void Add(in Exception exception)
		{
			if (_currentSetup is null)
			{
				_queue.Enqueue(new ItemSetup(exception: exception));
			}
			else
			{
				_currentSetup.Exception = exception;
				_currentSetup = null;
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

		public readonly Action<int, int>? Callback = callback;
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
public sealed class SetupIntInt<TReturns> : ISetup<TReturns>
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

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;

		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;

		public void Add(in Action<int, int> callback)
		{
			var currentSetup = new ItemSetup(callback);
			_queue.Enqueue(currentSetup);
			_currentSetup = currentSetup;
		}

		public void Add(in Func<int, int, TReturns?> returns)
		{
			if (_currentSetup is null)
			{
				_queue.Enqueue(new ItemSetup(returns: returns));
			}
			else
			{
				_currentSetup.Returns = returns;
				_currentSetup = null;
			}
		}

		public void Add(in Exception exception)
		{
			if (_currentSetup is null)
			{
				_queue.Enqueue(new ItemSetup(exception: exception));
			}
			else
			{
				_currentSetup.Exception = exception;
				_currentSetup = null;
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

		public readonly Action<int, int>? Callback = callback;
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
public sealed class SetupRefIntInt : ISetup
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

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

	public delegate void CallbackDelegate(ref int parameter1, int parameter2);

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;

		public void Add(in CallbackDelegate callback)
		{
			var currentSetup = new ItemSetup(callback);
			_queue.Enqueue(currentSetup);
			_currentSetup = currentSetup;
		}

		public void Add(in Exception exception)
		{
			if (_currentSetup is null)
			{
				_queue.Enqueue(new ItemSetup(exception: exception));
			}
			else
			{
				_currentSetup.Exception = exception;
				_currentSetup = null;
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

		public readonly CallbackDelegate? Callback = callback;
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
public sealed class SetupRefIntInt<TReturns> : ISetup<TReturns>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

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

	public delegate void CallbackDelegate(ref int parameter1, int parameter2);

	public delegate TReturns? ReturnsCallbackDelegate(ref int parameter1, int parameter2);

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;

		public void Add(in CallbackDelegate callback)
		{
			var currentSetup = new ItemSetup(callback);
			_queue.Enqueue(currentSetup);
			_currentSetup = currentSetup;
		}

		public void Add(in ReturnsCallbackDelegate returns)
		{
			if (_currentSetup is null)
			{
				_queue.Enqueue(new ItemSetup(returns: returns));
			}
			else
			{
				_currentSetup.Returns = returns;
				_currentSetup = null;
			}
		}

		public void Add(in Exception exception)
		{
			if (_currentSetup is null)
			{
				_queue.Enqueue(new ItemSetup(exception: exception));
			}
			else
			{
				_currentSetup.Exception = exception;
				_currentSetup = null;
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

		public readonly CallbackDelegate? Callback = callback;
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
public sealed class SetupIntRefInt : ISetup
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

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

	public delegate void CallbackDelegate(int parameter1, ref int parameter2);

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;

		public void Add(in CallbackDelegate callback)
		{
			var currentSetup = new ItemSetup(callback);
			_queue.Enqueue(currentSetup);
			_currentSetup = currentSetup;
		}

		public void Add(in Exception exception)
		{
			if (_currentSetup is null)
			{
				_queue.Enqueue(new ItemSetup(exception: exception));
			}
			else
			{
				_currentSetup.Exception = exception;
				_currentSetup = null;
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

		public readonly CallbackDelegate? Callback = callback;
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
public sealed class SetupIntRefInt<TReturns> : ISetup<TReturns>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

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

	public delegate void CallbackDelegate(int parameter1, ref int parameter2);

	public delegate TReturns? ReturnsCallbackDelegate(int parameter1, ref int parameter2);

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;

		public void Add(in CallbackDelegate callback)
		{
			var currentSetup = new ItemSetup(callback);
			_queue.Enqueue(currentSetup);
			_currentSetup = currentSetup;
		}

		public void Add(in ReturnsCallbackDelegate returns)
		{
			if (_currentSetup is null)
			{
				_queue.Enqueue(new ItemSetup(returns: returns));
			}
			else
			{
				_currentSetup.Returns = returns;
				_currentSetup = null;
			}
		}

		public void Add(in Exception exception)
		{
			if (_currentSetup is null)
			{
				_queue.Enqueue(new ItemSetup(exception: exception));
			}
			else
			{
				_currentSetup.Exception = exception;
				_currentSetup = null;
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

		public readonly CallbackDelegate? Callback = callback;
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
public sealed class SetupRefIntRefInt : ISetup
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

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

	public delegate void CallbackDelegate(ref int parameter1, ref int parameter2);

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;

		public void Add(in CallbackDelegate callback)
		{
			var currentSetup = new ItemSetup(callback);
			_queue.Enqueue(currentSetup);
			_currentSetup = currentSetup;
		}

		public void Add(in Exception exception)
		{
			if (_currentSetup is null)
			{
				_queue.Enqueue(new ItemSetup(exception: exception));
			}
			else
			{
				_currentSetup.Exception = exception;
				_currentSetup = null;
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

		public readonly CallbackDelegate? Callback = callback;
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
public sealed class SetupRefIntRefInt<TReturns> : ISetup<TReturns>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

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

	public delegate void CallbackDelegate(ref int parameter1, ref int parameter2);

	public delegate TReturns? ReturnsCallbackDelegate(ref int parameter1, ref int parameter2);

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;

		public void Add(in CallbackDelegate callback)
		{
			var currentSetup = new ItemSetup(callback);
			_queue.Enqueue(currentSetup);
			_currentSetup = currentSetup;
		}

		public void Add(in ReturnsCallbackDelegate returns)
		{
			if (_currentSetup is null)
			{
				_queue.Enqueue(new ItemSetup(returns: returns));
			}
			else
			{
				_currentSetup.Returns = returns;
				_currentSetup = null;
			}
		}

		public void Add(in Exception exception)
		{
			if (_currentSetup is null)
			{
				_queue.Enqueue(new ItemSetup(exception: exception));
			}
			else
			{
				_currentSetup.Exception = exception;
				_currentSetup = null;
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

		public readonly CallbackDelegate? Callback = callback;
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
