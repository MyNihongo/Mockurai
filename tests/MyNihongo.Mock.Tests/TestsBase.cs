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

	protected static GeneratedSource CreateSetupCode(string[] types)
	{
		var typeModels = types.ToTypeModels();
		return CreateSetupCode(useReturns: false, typeModels);
	}

	protected static GeneratedSource CreateSetupCode(TypeModel[] types)
	{
		return CreateSetupCode(useReturns: false, types);
	}

	protected static GeneratedSource CreateSetupReturnsCode(string[] types, string? returnValue = null)
	{
		var typeModels = types.ToTypeModels();
		return CreateSetupCode(useReturns: true, typeModels, returnValue);
	}

	protected static GeneratedSource CreateSetupReturnsCode(TypeModel[] types, string? returnValue = null)
	{
		return CreateSetupCode(useReturns: true, types, returnValue);
	}

	private static GeneratedSource CreateSetupCode(bool useReturns, TypeModel[] types, string? returnValue = null)
	{
		if (string.IsNullOrEmpty(returnValue))
			returnValue = "returnValue";

		const string returns = "TReturns";
		var genericTypes = types.Where(x => x.IsGeneric).Select(x => x.Type).ToList();
		var outTypes = types.Where(x => x.RefType == "out").ToArray();
		var inputParameters = types.Where(static x => x.IsInputParameter).ToArray();
		if (useReturns) genericTypes.Add(returns);

		var returnsGenericType = genericTypes.Count > 0 ? $"<{string.Join(", ", genericTypes)}>" : string.Empty;
		var classNameReturns = string.Join(null, types) + returnsGenericType;
		var parameters = string.Join(", ", types.Select(static x => x.GetParameterDeclarationString()));
		var discardedParameters = string.Join(", ", types.Select(static x => x.GetParameterDeclarationString(typeNameOverride: "_")));
		var parameterNamesWithRef = string.Join(", ", types.Select(static x => x.GetParameterNameString(appendRefKind: true)));
		var inputParameterNames = string.Join(", ", inputParameters.Select(static x => x.GetParameterNameString()));
		var setupParametersName = inputParameters.Length > 1 ? "SetupParameters" : "SetupParameter";
		var setupParameters = (bool isNullable) => string.Join(", ", inputParameters.Select(x => $"in ItSetup<{x.GetTypeString()}>{(isNullable ? "?" : string.Empty)} {x.GetParameterNameString()}"));
		var invoke = string.Join(Environment.NewLine + "\t\t\t", inputParameters.Select(static x =>
		{
			return
				$"""
				 if (setup.{x.GetCamelCaseNameString()}.HasValue && !setup.{x.GetCamelCaseNameString()}.Value.Check({x.GetParameterNameString()}))
				 				continue;
				 """;
		}));
		var itemSetupFields = string.Join(Environment.NewLine + "\t\t", inputParameters.Select(static x => { return $"public readonly ItSetup<{x.GetTypeString()}>? {x.GetCamelCaseNameString()};"; }));
		var itemSetupParameterAssign = string.Join(Environment.NewLine + "\t\t\t", inputParameters.Select(static x => { return $"{x.GetCamelCaseNameString()} = {x.GetParameterNameString()};"; }));
		var itemSetupComparer = (string item) =>
		{
			return string.Join(Environment.NewLine + "\t\t\t\t", inputParameters.Select(x =>
			{
				return
					$"""
					 if ({item}.{x.GetCamelCaseNameString()}.HasValue)
					 					{item}Sort += {item}.{x.GetCamelCaseNameString()}.Value.Sort;
					 """;
			}));
		};
		var interfaceGeneric = useReturns
			? $"Setup{classNameReturns}.CallbackDelegate, TReturns, Setup{classNameReturns}.ReturnsCallbackDelegate"
			: $"Setup{classNameReturns}.CallbackDelegate";
		var iSetupThrowsJoin = useReturns ? "ISetupReturnsThrowsJoin" : "ISetupThrowsJoin";
		var iSetupThrowsReset = useReturns ? "ISetupReturnsThrowsReset" : "ISetupThrowsReset";
		var iSetupThrowsStart = useReturns ? "ISetupReturnsThrowsStart" : "ISetupThrowsStart";
		var returnsDelegate = useReturns ? Environment.NewLine + $"\tpublic delegate {returns}? ReturnsCallbackDelegate({parameters});" : string.Empty;
		var invokeFunction = useReturns ? $"public bool Execute({parameters}, out {returns}? {returnValue})" : $"public void Invoke({parameters})";
		var checkReturns = useReturns
			? $$"""
			    if (x.Returns is not null)
			    			{
			    				{{returnValue}} = x.Returns({{parameterNamesWithRef}});
			    				return true;
			    			}


			    """ + "\t\t\t"
			: string.Empty;
		var defaultReturns = useReturns
			? $"""


			   		Default:
			   		{returnValue} = default;{(outTypes.Length > 0 ? string.Concat(outTypes.Select(static x => Environment.NewLine + $"\t\t{x.GetParameterNameString()} = default;")) : string.Empty)}
			   		return false;
			   """
			: string.Empty;

		var returnsMethods = useReturns
			? $$"""


			    	public void Returns(TReturns? returns)
			    	{
			    		Returns(({{discardedParameters}}) => returns);
			    	}

			    	public void Returns(in ReturnsCallbackDelegate returns)
			    	{
			    		if (_currentSetup is null)
			    			throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

			    		_currentSetup.Add(returns);
			    	}
			    """
			: string.Empty;
		var returnsInterfaceMethods = useReturns
			? $$"""


			    	{{iSetupThrowsJoin}}<{{interfaceGeneric}}> ISetupReturnsThrowsStart<{{interfaceGeneric}}>.Returns(in TReturns returns)
			    	{
			    		Returns(returns);
			    		return this;
			    	}

			    	ISetup<{{interfaceGeneric}}> ISetupReturnsThrowsReset<{{interfaceGeneric}}>.Returns(in TReturns returns)
			    	{
			    		Returns(returns);
			    		return this;
			    	}

			    	{{iSetupThrowsJoin}}<{{interfaceGeneric}}> ISetupReturnsThrowsStart<{{interfaceGeneric}}>.Returns(in ReturnsCallbackDelegate returns)
			    	{
			    		Returns(returns);
			    		return this;
			    	}

			    	ISetup<{{interfaceGeneric}}> ISetupReturnsThrowsReset<{{interfaceGeneric}}>.Returns(in ReturnsCallbackDelegate returns)
			    	{
			    		Returns(returns);
			    		return this;
			    	}
			    """
			: string.Empty;
		var returnsItemAdd = useReturns
			? """


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
			  """
			: string.Empty;
		var returnsItemSetup = useReturns ? Environment.NewLine + "\t\tpublic ReturnsCallbackDelegate? Returns;" : string.Empty;
		var returnsItemSetupConstructorParameter = useReturns ? "in ReturnsCallbackDelegate? returns = null, " : string.Empty;
		var returnsItemSetupConstructorAssign = useReturns ? Environment.NewLine + "\t\t\tReturns = returns;" : string.Empty;

		var sourceCode =
			$$"""
			  namespace MyNihongo.Mock;

			  public sealed class Setup{{classNameReturns}} : ISetupCallbackJoin<{{interfaceGeneric}}>, ISetupCallbackReset<{{interfaceGeneric}}>, {{iSetupThrowsJoin}}<{{interfaceGeneric}}>, {{iSetupThrowsReset}}<{{interfaceGeneric}}>
			  {
			  	private static readonly Comparer SortComparer = new();
			  	private SetupContainer<Item>? _setups;
			  	private Item? _currentSetup;

			  	public delegate void CallbackDelegate({{parameters}});{{returnsDelegate}}

			  	{{invokeFunction}}
			  	{
			  		if (_setups is null)
			  			{{(useReturns ? "goto Default" : "return")}};

			  		foreach (var setup in _setups)
			  		{
			  			{{invoke}}

			  			var x = setup.GetSetup();
			  			x.Callback?.Invoke({{parameterNamesWithRef}});

			  			if (x.Exception is not null)
			  				throw x.Exception;

			  			{{(useReturns ? $"{checkReturns}goto Default" : "return")}};
			  		}{{defaultReturns}}
			  	}

			  	public void {{setupParametersName}}({{setupParameters(false)}})
			  	{
			  		_currentSetup = new Item({{inputParameterNames}});

			  		_setups ??= new SetupContainer<Item>(SortComparer);
			  		_setups.Add(_currentSetup);
			  	}{{returnsMethods}}

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
			  	}{{returnsInterfaceMethods}}

			  	ISetupCallbackJoin<{{interfaceGeneric}}> ISetupCallbackStart<{{interfaceGeneric}}>.Callback(in CallbackDelegate callback)
			  	{
			  		Callback(callback);
			  		return this;
			  	}

			  	ISetup<{{interfaceGeneric}}> ISetupCallbackReset<{{interfaceGeneric}}>.Callback(in CallbackDelegate callback)
			  	{
			  		Callback(callback);
			  		return this;
			  	}

			  	{{iSetupThrowsJoin}}<{{interfaceGeneric}}> {{iSetupThrowsStart}}<{{interfaceGeneric}}>.Throws(in System.Exception exception)
			  	{
			  		Throws(exception);
			  		return this;
			  	}

			  	ISetup<{{interfaceGeneric}}> {{iSetupThrowsReset}}<{{interfaceGeneric}}>.Throws(in System.Exception exception)
			  	{
			  		Throws(exception);
			  		return this;
			  	}

			  	ISetupCallbackReset<{{interfaceGeneric}}> {{iSetupThrowsJoin}}<{{interfaceGeneric}}>.And()
			  	{
			  		_currentSetup?.AndContinue = true;
			  		return this;
			  	}

			  	{{iSetupThrowsReset}}<{{interfaceGeneric}}> ISetupCallbackJoin<{{interfaceGeneric}}>.And()
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

			  		public Item({{setupParameters(true)}})
			  		{
			  			{{itemSetupParameterAssign}}
			  		}{{returnsItemAdd}}

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
			  				_currentSetup = new ItemSetup(callback: callback);
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
			  		public System.Exception? Exception;{{returnsItemSetup}}

			  		public ItemSetup({{returnsItemSetupConstructorParameter}}in CallbackDelegate? callback = null, in System.Exception? exception = null)
			  		{{{returnsItemSetupConstructorAssign}}
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

		var sanitizedClassName = classNameReturns.Replace('<', '_').Replace('>', '_').Replace(", ", "_");

		return (
			$"Setup{sanitizedClassName}.g.cs",
			sourceCode
		);
	}

	protected static GeneratedSource CreateInvocationCode(params string[] types)
	{
		var typeModels = types.ToTypeModels();
		return CreateInvocationCode(typeModels);
	}

	protected static GeneratedSource CreateInvocationCode(params TypeModel[] types)
	{
		var genericTypes = types.Where(x => x.IsGeneric).Select(x => x.Type).ToList();
		var genericType = genericTypes.Count > 0 ? $"<{string.Join(", ", genericTypes)}>" : string.Empty;
		var className = string.Join(null, types);
		var classNameGenerics = className + genericType;
		var prefixes = string.Join(", ", types.Select(static x => $"_prefix{x.GetCamelCaseNameString()}"));
		var jsonSnapshots = string.Join(", ", types.Select(static x => $"_jsonSnapshot{x.GetCamelCaseNameString()}"));
		var prefixParameters = string.Join(", ", types.Select(static x => $"string? prefix{x.GetCamelCaseNameString()} = null"));
		var parameters = string.Join(", ", types.Select(static x => x.GetParameterDeclarationString()));
		var parametersWithoutRef = string.Join(", ", types.Select(static x => x.GetParameterDeclarationString(appendRefKind: false)));
		var parameterNames = string.Join(", ", types.Select(static x => x.GetParameterNameString()));
		var prefixAssignemnts = string.Join(Environment.NewLine + "\t\t", types.Select(static x => $"_prefix{x.GetCamelCaseNameString()} = prefix{x.GetCamelCaseNameString()};"));
		var setupParameters = string.Join(", ", types.Select(static x => $"in ItSetup<{x.GetTypeString()}> {x.GetParameterNameString()}"));
		var verifyParameters = string.Join(Environment.NewLine + "\t\t\t", types.Select(static x => $"var verify{x.GetCamelCaseNameString()} = span[i].Get{x.GetCamelCaseNameString()}({x.GetParameterNameString()}.Type);"));
		var parameterToString = string.Join(", ", types.Select(static x => $"{x.GetParameterNameString()}.ToString(_prefix{x.GetCamelCaseNameString()})"));
		var typeNameParameters = string.Join(Environment.NewLine + "\t\t", types.Select(static x => $"var typeName{x.GetCamelCaseNameString()} = !string.IsNullOrEmpty(_prefix{x.GetCamelCaseNameString()}) ? $\"{{_prefix{x.GetCamelCaseNameString()}}} {x.GetTypeString()}\" : \"{x.GetTypeString()}\";"));
		var typeParameterNames = string.Join(", ", types.Select(static x => $"typeName{x.GetCamelCaseNameString()}"));
		var parameterFields = string.Join(Environment.NewLine + "\t\t", types.Select(static x => $"private readonly {x.GetTypeString()} _{x.GetParameterNameString()};"));
		var verifyChecks = string.Join(Environment.NewLine + "\t\t\t", types.Select(static (x, i) =>
		{
			return i == 0
				? $$"""
				    if (!{{x.GetParameterNameString()}}.Check(verify{{x.GetCamelCaseNameString()}}, out var result))
				    			{
				    				verifyResults = [("{{x.GetParameterNameString()}}", result)];
				    			}
				    """
				: $$"""
				    if (!{{x.GetParameterNameString()}}.Check(verify{{x.GetCamelCaseNameString()}}, out result))
				    			{
				    				verifyResults = verifyResults is not null
				    					? [..verifyResults, ("{{x.GetParameterNameString()}}", result)]
				    					: [("{{x.GetParameterNameString()}}", result)];
				    			}
				    """;
		}));
		var verifyItemParameterAssignments = string.Join(Environment.NewLine + Environment.NewLine + "\t\t\t", types.Select(static x =>
		{
			var name = x.GetParameterNameString();
			var camelCase = x.GetCamelCaseNameString();

			return
				$$"""
				  try
				  			{
				  				_{{name}} = {{name}};
				  				_jsonSnapshot{{camelCase}} = System.Text.Json.JsonSerializer.Serialize({{name}});
				  			}
				  			catch
				  			{
				  				// Swallow
				  			}
				  """;
		}));
		var verifyItemGetParameterFunctions = string.Join(Environment.NewLine + Environment.NewLine + "\t\t", types.Select(static x =>
		{
			var typeString = x.GetTypeString();

			return
				$$"""
				  public {{typeString}} Get{{x.GetCamelCaseNameString()}}(SetupType setupType)
				  		{
				  			return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshot{{x.GetCamelCaseNameString()}})
				  				? System.Text.Json.JsonSerializer.Deserialize<{{typeString}}>(_jsonSnapshot{{x.GetCamelCaseNameString()}})
				  				: _{{x.GetParameterNameString()}};
				  		}
				  """;
		}));
		var verifyItemParameterToString = string.Join(Environment.NewLine + Environment.NewLine + "\t\t\t", types.Select(static (x, i) =>
		{
			var stringBuilderClear = i > 0
				? Environment.NewLine + "\t\t\tstringBuilder.Clear();"
				: string.Empty;

			return
				$$"""
				  // {{x.GetParameterNameString()}}{{stringBuilderClear}}
				  			if (!string.IsNullOrEmpty(_invocation._prefix{{x.GetCamelCaseNameString()}}))
				  				stringBuilder.Append($"{_invocation._prefix{{x.GetCamelCaseNameString()}}} ");
				  			if (!string.IsNullOrEmpty(_jsonSnapshot{{x.GetCamelCaseNameString()}}))
				  				stringBuilder.Append(_jsonSnapshot{{x.GetCamelCaseNameString()}});
				  			else
				  				stringBuilder.Append(_{{x.GetParameterNameString()}});
				  			var {{x.GetParameterNameString()}} = stringBuilder.ToString();
				  """;
		}));

		var sourceCode =
			$$"""
			  namespace MyNihongo.Mock;

			  public sealed class Invocation{{classNameGenerics}} : IInvocationVerify
			  {
			  	private readonly string _name;
			  	private readonly string? {{prefixes}};
			  	private readonly InvocationContainer<Item> _invocations = [];

			  	public Invocation{{className}}(string name, {{prefixParameters}})
			  	{
			  		_name = name;
			  		{{prefixAssignemnts}}
			  	}

			  	public void Register(in InvocationIndex.Counter index, {{parametersWithoutRef}})
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
			  		private readonly Invocation{{classNameGenerics}} _invocation;

			  		public Item(long index, {{parametersWithoutRef}}, Invocation{{classNameGenerics}} invocation)
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

		var sanitizedClassName = classNameGenerics.Replace('<', '_').Replace('>', '_').Replace(", ", "_");

		return (
			$"Invocation{sanitizedClassName}.g.cs",
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

	public static TypeModel[] ToTypeModels(this string[] @this)
	{
		return @this
			.Select(static (x, i) => new TypeModel(x, i + 1))
			.ToArray();
	}
}
