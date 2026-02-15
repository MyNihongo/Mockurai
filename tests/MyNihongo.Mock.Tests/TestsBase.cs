namespace MyNihongo.Mock.Tests;

public abstract class TestsBase
{
	protected static CSharpSourceGeneratorTest<SourceGenerator, DefaultVerifier> CreateFixture(string testCode, GeneratedSources expected)
	{
		return new CSharpSourceGeneratorTest<SourceGenerator, DefaultVerifier>
		{
			ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
			TestCode = testCode,
			TestState =
			{
				AdditionalReferences = { typeof(MockuraiGenerateAttribute).Assembly },
				GeneratedSources =
				{
					expected,
					(typeof(SourceGenerator), "_Usings.g.cs", "global using MyNihongo.Mock;"),
				},
			},
		};
	}

	protected static GeneratedSource CreateSetupCode(params string[] types)
	{
		var className = string.Join(null, types);
		var parameters = string.Join(", ", types.Select(static (x, i) => $"{x.GetTypeString()} param{i + 1}"));
		var parameterNames = string.Join(", ", types.Select(static (_, i) => $"param{i + 1}"));
		var setupParameters = string.Join(", ", types.Select(static (x, i) => $"in ItSetup<{x.GetTypeString()}>? param{i + 1}"));
		var invoke = string.Join(Environment.NewLine + "\t\t\t", types.Select(static (_, i) =>
		{
			var index = i + 1;

			return
				$"""
				 if (setup.Param{index}.HasValue && !setup.Param{index}.Value.Check(param{index}))
				 				continue;
				 """;
		}));
		var itemSetupFields = string.Join(Environment.NewLine + "\t\t", types.Select(static (type, i) =>
		{
			var index = i + 1;
			return $"public readonly ItSetup<{type.GetTypeString()}>? Param{index};";
		}));
		var itemSetupParameterAssign = string.Join(Environment.NewLine + "\t\t\t", types.Select(static (_, i) =>
		{
			var index = i + 1;
			return $"Param{index} = param{index};";
		}));
		var itemSetupComparer = (string item) =>
		{
			return string.Join(Environment.NewLine + "\t\t\t\t", types.Select((_, i) =>
			{
				var index = i + 1;

				return
					$"""
					 if ({item}.Param{index}.HasValue)
					 					{item}Sort += {item}.Param{index}.Value.Sort;
					 """;
			}));
		};

		var sourceCode =
			$$"""
			  namespace MyNihongo.Mock;

			  public sealed class Setup{{className}} : ISetupCallbackJoin<Setup{{className}}.CallbackDelegate>, ISetupCallbackReset<Setup{{className}}.CallbackDelegate>, ISetupThrowsJoin<Setup{{className}}.CallbackDelegate>, ISetupThrowsReset<Setup{{className}}.CallbackDelegate>
			  {
			  	private static readonly Comparer SortComparer = new();
			  	private SetupContainer<Item>? _setups;
			  	private Item? _currentSetup;

			  	public delegate void CallbackDelegate({{parameters}});

			  	public void Invoke({{parameters}})
			  	{
			  		if (_setups is null)
			  			return;

			  		foreach (var setup in _setups)
			  		{
			  			{{invoke}}

			  			var x = setup.GetSetup();
			  			x.Callback?.Invoke({{parameterNames}});

			  			if (x.Exception is not null)
			  				throw x.Exception;

			  			return;
			  		}
			  	}

			  	public void SetupParameters(in ItSetup<int> param1, in ItSetup<float> param2)
			  	{
			  		_currentSetup = new Item({{parameterNames}});

			  		_setups ??= new SetupContainer<Item>(SortComparer);
			  		_setups.Add(_currentSetup);
			  	}

			  	public void Callback(in CallbackDelegate callback)
			  	{
			  		if (_currentSetup is null)
			  			throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

			  		_currentSetup.Add(callback);
			  	}

			  	public void Throws(in System.Exception exception)
			  	{
			  		if (_currentSetup is null)
			  			throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

			  		_currentSetup.Add(exception);
			  	}

			  	ISetupCallbackJoin<Setup{{className}}.CallbackDelegate> ISetupCallbackStart<Setup{{className}}.CallbackDelegate>.Callback(in CallbackDelegate callback)
			  	{
			  		Callback(callback);
			  		return this;
			  	}

			  	ISetup<Setup{{className}}.CallbackDelegate> ISetupCallbackReset<Setup{{className}}.CallbackDelegate>.Callback(in CallbackDelegate callback)
			  	{
			  		Callback(callback);
			  		return this;
			  	}

			  	ISetupThrowsJoin<Setup{{className}}.CallbackDelegate> ISetupThrowsStart<Setup{{className}}.CallbackDelegate>.Throws(in System.Exception exception)
			  	{
			  		Throws(exception);
			  		return this;
			  	}

			  	ISetup<Setup{{className}}.CallbackDelegate> ISetupThrowsReset<Setup{{className}}.CallbackDelegate>.Throws(in System.Exception exception)
			  	{
			  		Throws(exception);
			  		return this;
			  	}

			  	ISetupCallbackReset<Setup{{className}}.CallbackDelegate> ISetupThrowsJoin<Setup{{className}}.CallbackDelegate>.And()
			  	{
			  		_currentSetup?.AndContinue = true;
			  		return this;
			  	}

			  	ISetupThrowsReset<Setup{{className}}.CallbackDelegate> ISetupCallbackJoin<Setup{{className}}.CallbackDelegate>.And()
			  	{
			  		_currentSetup?.AndContinue = true;
			  		return this;
			  	}

			  	private sealed class Item
			  	{
			  		private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
			  		private ItemSetup? _currentSetup;
			  		public bool AndContinue;

			  		{{itemSetupFields}}

			  		public Item({{setupParameters}})
			  		{
			  			{{itemSetupParameterAssign}}
			  		}

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

			  		public void Add(in System.Exception exception)
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

			  	private sealed class ItemSetup
			  	{
			  		public static readonly ItemSetup Default = new();

			  		public CallbackDelegate? Callback;
			  		public System.Exception? Exception;

			  		public ItemSetup(in CallbackDelegate? callback = null, in System.Exception? exception = null)
			  		{
			  			Callback = callback;
			  			Exception = exception;
			  		}
			  	}

			  	private sealed class Comparer: System.Collections.Generic.IComparer<Item>
			  	{
			  		public int Compare(Item? x, Item? y)
			  		{
			  			var xSort = 0;
			  			var ySort = 0;

			  			if (x is not null)
			  			{
			  				{{itemSetupComparer("x")}}
			  			}

			  			if (y is not null)
			  			{
			  				{{itemSetupComparer("y")}}
			  			}

			  			return xSort.CompareTo(ySort);
			  		}
			  	}
			  }
			  """;

		return (
			$"Setup{className}.g.cs",
			sourceCode
		);
	}

	protected static GeneratedSource CreateInvocationCode(params string[] types)
	{
		var className = string.Join(null, types);
		var prefixes = string.Join(", ", types.Select(static (_, i) => $"_prefixParam{i + 1}"));
		var jsonSnapshots = string.Join(", ", types.Select(static (_, i) => $"_jsonSnapshotParam{i + 1}"));
		var prefixParameters = string.Join(", ", types.Select(static (_, i) => $"string? prefixParam{i + 1} = null"));
		var parameters = string.Join(", ", types.Select(static (x, i) => $"{x.GetTypeString()} param{i + 1}"));
		var parameterNames = string.Join(", ", types.Select(static (_, i) => $"param{i + 1}"));
		var prefixAssignemnts = string.Join(Environment.NewLine + "\t\t", types.Select(static (_, i) => $"_prefixParam{i + 1} = prefixParam{i + 1};"));
		var setupParameters = string.Join(", ", types.Select(static (x, i) => $"in ItSetup<{x.GetTypeString()}> param{i + 1}"));
		var verifyParameters = string.Join(Environment.NewLine + "\t\t\t", types.Select(static (_, i) => $"var verifyParam{i + 1} = span[i].GetParam{i + 1}(param{i + 1}.Type);"));
		var parameterToString = string.Join(", ", types.Select(static (_, i) => $"param{i + 1}.ToString(_prefixParam{i + 1})"));
		var typeNameParameters = string.Join(Environment.NewLine + "\t\t", types.Select(static (x, i) => $"var typeNameParam{i + 1} = !string.IsNullOrEmpty(_prefixParam{i + 1}) ? $\"{{_prefixParam{i + 1}}} {x.GetTypeString()}\" : \"{x.GetTypeString()}\";"));
		var typeParameterNames = string.Join(", ", types.Select(static (_, i) => $"typeNameParam{i + 1}"));
		var parameterFields = string.Join(Environment.NewLine + "\t\t", types.Select(static (x, i) => $"private readonly {x.GetTypeString()} _param{i + 1};"));
		var verifyChecks = string.Join(Environment.NewLine + "\t\t\t", types.Skip(1).Select(static (_, i) =>
		{
			var index = i + 2;

			return
				$$"""
				  if (!param{{index}}.Check(verifyParam{{index}}, out result))
				  			{
				  				verifyResults = verifyResults is not null
				  					? [..verifyResults, ("param{{index}}", result)]
				  					: [("param{{index}}", result)];
				  			}
				  """;
		}));
		var verifyItemParameterAssignments = string.Join(Environment.NewLine + Environment.NewLine + "\t\t\t", types.Select(static (_, i) =>
		{
			var index = i + 1;

			return
				$$"""
				try
							{
								_param{{index}} = param{{index}};
								_jsonSnapshotParam{{index}} = System.Text.Json.JsonSerializer.Serialize(param{{index}});
							}
							catch
							{
								// Swallow
							}
				""";
		}));
		var verifyItemGetParameterFunctions = string.Join(Environment.NewLine + Environment.NewLine + "\t\t", types.Select(static (x ,i) =>
		{
			var index = i + 1;
			var typeString = x.GetTypeString();

			return
				$$"""
				public {{typeString}} GetParam{{index}}(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParam{{index}})
								? System.Text.Json.JsonSerializer.Deserialize<{{typeString}}>(_jsonSnapshotParam{{index}})
								: _param{{index}};
						}
				""";
		}));
		var verifyItemParameterToString = string.Join(Environment.NewLine + Environment.NewLine + "\t\t\t", types.Select(static (_, i) =>
		{
			var index = i + 1;
			var stringBuilderClear = i > 0
				? Environment.NewLine + "\t\t\tstringBuilder.Clear();"
				: string.Empty;

			return
				$$"""
				// param{{index}}{{stringBuilderClear}}
							if (!string.IsNullOrEmpty(_invocation._prefixParam{{index}}))
								stringBuilder.Append($"{_invocation._prefixParam{{index}}} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParam{{index}}))
								stringBuilder.Append(_jsonSnapshotParam{{index}});
							else
								stringBuilder.Append(_param{{index}});
							var param{{index}} = stringBuilder.ToString();
				""";
		}));

		var sourceCode =
			$$"""
			  namespace MyNihongo.Mock;

			  public sealed class Invocation{{className}} : IInvocationVerify
			  {
			  	private readonly string _name;
			  	private readonly string? {{prefixes}};
			  	private readonly InvocationContainer<Item> _invocations = [];

			  	public InvocationInt32Single(string name, {{prefixParameters}})
			  	{
			  		_name = name;
			  		{{prefixAssignemnts}}
			  	}

			  	public void Register(in InvocationIndex.Counter index, {{parameters}})
			  	{
			  		var invokedIndex = index.Increment();
			  		_invocations.Add(new Item(invokedIndex, {{parameterNames}}, invocation: this));
			  	}

			  	public void Verify({{setupParameters}}, in Times times, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
			  	{
			  		var span = _invocations.GetItemsSpan();

			  		var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
			  		System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

			  		var count = 0;
			  		for (var i = 0; i < span.Length; i++)
			  		{
			  			{{verifyParameters}}
			  			(string, ComparisonResult?)[]? verifyResults = null;

			  			if (!param1.Check(verifyParam1, out var result))
			  			{
			  				verifyResults = [("param1", result)];
			  			}
			  			{{verifyChecks}}

			  			if (verifyResults is not null)
			  			{
			  				verifyOutput[i] = (span[i], verifyResults);
			  				continue;
			  			}

			  			verifyOutput[i] = (span[i], null);
			  			span[i].IsVerified = true;
			  			count++;
			  		}

			  		if (times.Predicate(count))
			  			return;

			  		var invocations = verifyOutput.GetStrings(invocationProviders);
			  		var verifyName = string.Format(_name, {{parameterToString}});
			  		throw new MockVerifyCountException(verifyName, times, count, invocations);
			  	}

			  	public long Verify({{setupParameters}}, long index, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
			  	{
			  		var span = _invocations.GetItemsSpanFrom(index);

			  		var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
			  		System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

			  		for (var i = 0; i < span.Length; i++)
			  		{
			  			{{verifyParameters}}
			  			(string, ComparisonResult?)[]? verifyResults = null;

			  			if (!param1.Check(verifyParam1, out var result))
			  			{
			  				verifyResults = [("param1", result)];
			  			}
			  			{{verifyChecks}}

			  			if (verifyResults is not null)
			  			{
			  				verifyOutput[i] = (span[i], verifyResults);
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
			  		var verifyName = string.Format(_name, {{parameterToString}});
			  		throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
			  	}

			  	public void VerifyNoOtherCalls(System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
			  	{
			  		var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
			  		if (unverifiedItems is null)
			  			return;

			  		{{typeNameParameters}}
			  		var verifyName = string.Format(_name, {{typeParameterNames}});
			  		throw new MockUnverifiedException(verifyName, unverifiedItems);
			  	}

			  	public System.Collections.Generic.IEnumerable<IInvocation> GetInvocations()
			  	{
			  		return _invocations;
			  	}

			  	private sealed class Item : IInvocation
			  	{
			  		{{parameterFields}}
			  		private readonly string? {{jsonSnapshots}};
			  		private readonly Invocation{{className}} _invocation;

			  		public Item(long index, {{parameters}}, Invocation{{className}} invocation)
			  		{
			  			_invocation = invocation;
			  			Index = index;

			  			{{verifyItemParameterAssignments}}
			  		}

			  		public long Index { get; }

			  		public bool IsVerified { get; set; }

			  		{{verifyItemGetParameterFunctions}}

			  		public override string ToString()
			  		{
			  			var stringBuilder = new System.Text.StringBuilder();

			  			{{verifyItemParameterToString}}

			  			var value = string.Format(_invocation._name, {{parameterNames}});
			  			return $"{Index}: {value}";
			  		}
			  	}
			  }
			  """;

		return (
			"InvocationInt32Single.g.cs",
			sourceCode
		);
	}
}

file static class SourceFileCollectionEx
{
	public static void Add(this SourceFileCollection @this, GeneratedSources expected)
	{
		foreach (var item in expected)
		{
			@this.Add((typeof(SourceGenerator), item.FileName, item.SourceCode));
		}
	}

	public static string GetTypeString(this string type)
	{
		return type switch
		{
			"Int32" => "int",
			"Single" => "float",
			_ => throw new NotImplementedException($"Unsupported type: `{type}`"),
		};
	}
}
