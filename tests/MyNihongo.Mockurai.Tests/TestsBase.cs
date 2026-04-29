namespace MyNihongo.Mockurai.Tests;

public abstract class TestsBase
{
	protected static CSharpSourceGeneratorTest<SourceGenerator, DefaultVerifier> CreateFixture(string testCode, GeneratedSources expected)
	{
		return new CSharpSourceGeneratorTest<SourceGenerator, DefaultVerifier>
		{
			ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
			TestCode = testCode,
			TestState =
			{
				AdditionalReferences = { typeof(MockuraiGenerateAttribute).Assembly },
				GeneratedSources =
				{
					(typeof(SourceGenerator), "_Usings.g.cs", "global using MyNihongo.Mockurai;"),
					expected,
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

	protected static GeneratedSource CreateSetupReturnsCode(string[] types)
	{
		var typeModels = types.ToTypeModels();
		return CreateSetupCode(useReturns: true, typeModels);
	}

	protected static GeneratedSource CreateSetupReturnsCode(TypeModel[] types)
	{
		return CreateSetupCode(useReturns: true, types);
	}

	private static GeneratedSource CreateSetupCode(bool useReturns, TypeModel[] types)
	{
		const string returns = "TReturns", returnValue = "returnValue";
		var genericTypes = types.SelectMany(x => x.GenericTypes).ToList();
		var outTypes = types.Where(x => x.RefType == "out").ToArray();
		var inputParameters = types.Where(static x => x.IsInputParameter).ToArray();
		if (useReturns) genericTypes.Add(returns);

		var returnsGenericType = genericTypes.Count > 0 ? $"<{string.Join(", ", genericTypes.Distinct())}>" : string.Empty;
		var classNameReturns = string.Join(null, types) + returnsGenericType;
		var parameters = string.Join(", ", types.Select(static x => x.GetParameterDeclarationString()));
		var discardedParameters = string.Join(", ", types.Select(static x => x.GetParameterDeclarationString(typeNameOverride: "_")));
		var parameterNamesWithRef = string.Join(", ", types.Select(static x => x.RefType == "out" ? $"{x.GetParameterNameString(appendRefKind: true)}!" : x.GetParameterNameString(appendRefKind: true)));
		var returnsValue = outTypes.Length > 0 ? Environment.NewLine + "\t\t{" + string.Concat(outTypes.Select(static x => $"{Environment.NewLine}\t\t\t{x.GetParameterNameString()} = default!;")) + $"{Environment.NewLine}\t\t\treturn returns;{Environment.NewLine}" + "\t\t}" : " returns";
		var inputParameterNames = string.Join(", ", inputParameters.Select(static x => x.GetParameterNameString()));
		var setupParametersName = inputParameters.Length > 1 ? "SetupParameters" : "SetupParameter";
		var setupParameters = (bool isNullable) => string.Join(", ", inputParameters.Select(x => $"in ItSetup<{x.GetTypeString()}>{(isNullable ? "?" : string.Empty)} {x.GetParameterNameString()}"));
		var invoke = inputParameters.Length > 0
			? string.Concat(inputParameters.Select(static x =>
			{
				return
					$"""

					 			if (setup.{x.GetCamelCaseNameString()}.HasValue && !setup.{x.GetCamelCaseNameString()}.Value.Check({x.GetParameterNameString()}))
					 				continue;
					 """;
			})) + Environment.NewLine
			: string.Empty;
		var itemSetupFields = string.Join(Environment.NewLine + "\t\t", inputParameters.Select(static x => { return $"public readonly ItSetup<{x.GetTypeString()}>? {x.GetCamelCaseNameString()};"; }));
		var itemSetupParameterAssign = string.Join(Environment.NewLine + "\t\t\t", inputParameters.Select(static x => { return $"{x.GetCamelCaseNameString()} = {x.GetParameterNameString()};"; }));

		var itemFieldAndConstructor = inputParameters.Length > 0
			? $$"""


			    		{{itemSetupFields}}

			    		public Item({{setupParameters(true)}})
			    		{
			    			{{itemSetupParameterAssign}}
			    		}
			    """
			: string.Empty;

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
			? inputParameters.Length > 0
				? $$"""
				    if (x.Returns is not null)
				    			{
				    				{{returnValue}} = x.Returns({{parameterNamesWithRef}});
				    				return true;
				    			}


				    """ + "\t\t\t"
				: $$"""
				    if (x.Returns is not null)
				    		{
				    			{{returnValue}} = x.Returns({{parameterNamesWithRef}});
				    			return true;
				    		}


				    """ + "\t\t"
			: string.Empty;
		var defaultReturns = useReturns || outTypes.Length > 0
			? $"""


			   		Default:{(useReturns ? Environment.NewLine + $"\t\t{returnValue} = default!;" : string.Empty)}{(outTypes.Length > 0 ? string.Concat(outTypes.Select(static x => Environment.NewLine + $"\t\t{x.GetParameterNameString()} = default!;")) : string.Empty)}
			   		return{(useReturns ? " false" : string.Empty)};
			   """
			: string.Empty;

		var currentSetupCheck = inputParameters.Length > 0
			? """

			  		if (_currentSetup is null)
			  			throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

			  """
			: string.Empty;

		var returnsMethods = useReturns
			? $$"""


			    	public void Returns(TReturns? returns)
			    	{
			    		Returns(({{discardedParameters}}) =>{{returnsValue}});
			    	}

			    	public void Returns(in ReturnsCallbackDelegate returns)
			    	{{{currentSetupCheck}}
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

		var callbackInvoke = outTypes.Length == 0
			? $"x.Callback?.Invoke({parameterNamesWithRef});"
			: inputParameters.Length > 0
				? $$"""
				    if (x.Callback is not null)
				    			{
				    				x.Callback.Invoke({{parameterNamesWithRef}});
				    			}
				    			else
				    			{
				    				{{string.Join(Environment.NewLine + "\t\t\t\t", outTypes.Select(x => $"{x.GetParameterNameString()} = default!;"))}}
				    			}
				    """
				: $$"""
				    if (x.Callback is not null)
				    		{
				    			x.Callback.Invoke({{parameterNamesWithRef}});
				    		}
				    		else
				    		{
				    			{{string.Join(Environment.NewLine + "\t\t\t", outTypes.Select(x => $"{x.GetParameterNameString()} = default!;"))}}
				    		}
				    """;

		var setupFields = inputParameters.Length > 0
			? """
			  private static readonly Comparer SortComparer = new();
			  	private SetupContainer<Item>? _setups;
			  	private Item? _currentSetup;
			  """
			: "private readonly Item _currentSetup = new();";

		var setupMethod = inputParameters.Length > 0
			? $$"""


			    	public void {{setupParametersName}}({{setupParameters(false)}})
			    	{
			    		_currentSetup = new Item({{inputParameterNames}});

			    		_setups ??= new SetupContainer<Item>(SortComparer);
			    		_setups.Add(_currentSetup);
			    	}
			    """
			: string.Empty;

		var comparer = inputParameters.Length > 0
			? $$"""


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
			    """
			: string.Empty;

		var invokeFunctionBody = inputParameters.Length > 0
			? $$"""
			    {
			    		if (_setups is null)
			    			{{(useReturns || outTypes.Length > 0 ? "goto Default" : "return")}};

			    		foreach (var setup in _setups)
			    		{{{invoke}}
			    			var x = setup.GetSetup();
			    			{{callbackInvoke}}

			    			if (x.Exception is not null)
			    				throw x.Exception;

			    			{{(useReturns ? checkReturns + (outTypes.Length > 0 ? $"returnValue = default!;{Environment.NewLine}\t\t\treturn false" : "goto Default") : "return")}};
			    		}{{defaultReturns}}
			    	}
			    """
			: $$"""
			    {{{invoke}}
			    		var x = _currentSetup.GetSetup();
			    		{{callbackInvoke}}

			    		if (x.Exception is not null)
			    			throw x.Exception;{{(useReturns ? $"{Environment.NewLine + Environment.NewLine}\t\t" + checkReturns + (outTypes.Length > 0 ? $"returnValue = default!;{Environment.NewLine}\t\treturn false;" : "goto Default;") : string.Empty)}}
			    	}
			    """;

		var sourceCode =
			$$"""
			  #nullable enable
			  namespace MyNihongo.Mockurai;

			  public sealed class Setup{{classNameReturns}} : ISetupCallbackJoin<{{interfaceGeneric}}>, ISetupCallbackReset<{{interfaceGeneric}}>, {{iSetupThrowsJoin}}<{{interfaceGeneric}}>, {{iSetupThrowsReset}}<{{interfaceGeneric}}>
			  {
			  	{{setupFields}}

			  	public delegate void CallbackDelegate({{parameters}});{{returnsDelegate}}

			  	{{invokeFunction}}
			  	{{invokeFunctionBody}}{{setupMethod}}{{returnsMethods}}

			  	public void Callback(in CallbackDelegate callback)
			  	{{{currentSetupCheck}}
			  		_currentSetup.Add(callback);
			  	}

			  	public void Throws(in System.Exception exception)
			  	{{{currentSetupCheck}}
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
			  		_currentSetup{{(inputParameters.Length > 0 ? "?" : string.Empty)}}.AndContinue = true;
			  		return this;
			  	}

			  	{{iSetupThrowsReset}}<{{interfaceGeneric}}> ISetupCallbackJoin<{{interfaceGeneric}}>.And()
			  	{
			  		_currentSetup{{(inputParameters.Length > 0 ? "?" : string.Empty)}}.AndContinue = true;
			  		return this;
			  	}

			  	private sealed class Item
			  	{
			  		private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
			  		private ItemSetup? _currentSetup;
			  		public bool AndContinue;{{itemFieldAndConstructor}}

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
			  		}{{returnsItemAdd}}

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
			  	}{{comparer}}
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
		var genericTypes = types.SelectMany(x => x.GenericTypes).ToList();
		var genericType = genericTypes.Count > 0 ? $"<{string.Join(", ", genericTypes.Distinct())}>" : string.Empty;
		var className = string.Join(null, types);
		var classNameGenerics = className + genericType;
		var prefixes = string.Join(", ", types.Select(static x => $"_{x.GetParameterNameString()}Prefix"));
		var jsonSnapshots = string.Join(", ", types.Select(static x => $"_jsonSnapshot{x.GetCamelCaseNameString()}"));
		var prefixParameters = string.Join(", ", types.Select(static x => $"string? {x.GetParameterNameString()}Prefix = null"));
		var parametersWithoutRef = string.Join(", ", types.Select(static x => x.GetParameterDeclarationString(appendRefKind: false)));
		var parameterNames = string.Join(", ", types.Select(static x => x.GetParameterNameString()));
		var prefixAssignments = string.Join(Environment.NewLine + "\t\t", types.Select(static x => $"_{x.GetParameterNameString()}Prefix = {x.GetParameterNameString()}Prefix;"));
		var setupParameters = string.Join(", ", types.Select(static x => $"in ItSetup<{x.GetTypeString()}> {x.GetParameterNameString()}"));
		var verifyParameters = string.Join(Environment.NewLine + "\t\t\t", types.Select(static x => $"var verify{x.GetCamelCaseNameString()} = span[i].Get{x.GetCamelCaseNameString()}({x.GetParameterNameString()}.Type);"));
		var parameterToString = string.Join(", ", types.Select(static x => $"{x.GetParameterNameString()}.ToString(_{x.GetParameterNameString()}Prefix)"));
		var typeNameParameters = string.Join(Environment.NewLine + "\t\t", types.Select(static x => { return $"var typeName{x.GetCamelCaseNameString()} = !string.IsNullOrEmpty(_{x.GetParameterNameString()}Prefix) ? $\"{{_{x.GetParameterNameString()}Prefix}} {(x.HasGenericTypes ? $"{x.GetTypeofTypeString()}" : x.GetTypeString())}\" : {(x.HasGenericTypes ? x.IsGeneric ? x.GetTypeofTypeString(appendBrackets: false) : $"$\"{x.GetTypeofTypeString()}\"" : $"\"{x.GetTypeString()}\"")};"; }));
		var typeParameterNames = string.Join(", ", types.Select(static x => $"typeName{x.GetCamelCaseNameString()}"));
		var tupleType = '(' + string.Join(", ", types.Select(static x => $"{x.GetTypeString()} {x.GetParameterNameString()}")) + ')';
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
				  				_jsonSnapshot{{camelCase}} = {{name}}.SerializeToJson();
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
				  				? _jsonSnapshot{{x.GetCamelCaseNameString()}}.DeserializeFromJson(_argument.{{x.GetParameterNameString()}})
				  				: _argument.{{x.GetParameterNameString()}};
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
				  			if (!string.IsNullOrEmpty(_invocation._{{x.GetParameterNameString()}}Prefix))
				  				stringBuilder.Append($"{_invocation._{{x.GetParameterNameString()}}Prefix} ");
				  			if (!string.IsNullOrEmpty(_jsonSnapshot{{x.GetCamelCaseNameString()}}))
				  				stringBuilder.Append(_jsonSnapshot{{x.GetCamelCaseNameString()}});
				  			else
				  				stringBuilder.Append(_argument.{{x.GetParameterNameString()}});
				  			var {{x.GetParameterNameString()}} = stringBuilder.ToString();
				  """;
		}));

		var sourceCode =
			$$"""
			  #nullable enable
			  namespace MyNihongo.Mockurai;

			  public sealed class Invocation{{classNameGenerics}} : IInvocationVerify
			  {
			  	private readonly string _name;
			  	private readonly string? {{prefixes}};
			  	private readonly InvocationContainer<Item> _invocations = [];

			  	public Invocation{{className}}(string name, {{prefixParameters}})
			  	{
			  		_name = name;
			  		{{prefixAssignments}}
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

			  	public System.Collections.Generic.IEnumerable<IInvocation<{{tupleType}}>> GetInvocationsWithArguments()
			  	{
			  		return _invocations;
			  	}

			  	private sealed class Item : IInvocation<{{tupleType}}>
			  	{
			  		private readonly {{tupleType}} _argument;
			  		private readonly string? {{jsonSnapshots}};
			  		private readonly Invocation{{classNameGenerics}} _invocation;

			  		public Item(long index, {{parametersWithoutRef}}, Invocation{{classNameGenerics}} invocation)
			  		{
			  			_argument = ({{parameterNames}});
			  			_invocation = invocation;
			  			Index = index;

			  			{{verifyItemParameterAssignments}}
			  		}

			  		public long Index { get; }

			  		public bool IsVerified { get; set; }
			  
			  		public {{tupleType}} Arguments => _argument;

			  		{{verifyItemGetParameterFunctions}}

			  		public override string ToString()
			  		{
			  			var stringBuilder = new System.Text.StringBuilder();

			  			{{verifyItemParameterToString}}

			  			var stringValue = string.Format(_invocation._name, {{parameterNames}});
			  			return $"{Index}: {stringValue}";
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
