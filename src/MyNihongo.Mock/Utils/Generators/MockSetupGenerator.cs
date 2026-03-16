namespace MyNihongo.Mock.Utils;

internal static class MockSetupGenerator
{
	public static MockSetupResult GenerateMockSetup(this IMethodSymbol methodSymbol, CompilationCombinedResult result)
	{
		var stringBuilder = new StringBuilder();
		var className = CreateSetupClassName(stringBuilder, methodSymbol, out var genericTypeOverride);
		var returnType = methodSymbol.TryGetReturnType();
		var returnValueName = GetReturnValueName(methodSymbol.Parameters);
		var parameterSplit = methodSymbol.Parameters.SplitParameters();

		var source =
			$$"""
			  namespace {{result.Options.RootNamespace}};

			  public sealed class {{className}} : {{CreateInterfaceDerivedFrom(stringBuilder, methodSymbol, returnType)}}
			  {
			  {{CreateFields(stringBuilder, parameterSplit, indent: 1)}}

			  {{CreateDelegates(stringBuilder, methodSymbol, returnType, genericTypeOverride, indent: 1)}}
			  {{CreateMethodImplementations(stringBuilder, methodSymbol, returnType, returnValueName, parameterSplit, genericTypeOverride, indent: 1)}}
			  {{CreateInterfaceMethodImplementations(stringBuilder, methodSymbol, returnType, parameterSplit.InputParameters, indent: 1)}}

			  	private sealed class Item
			  	{
			  		private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
			  		private ItemSetup? _currentSetup;
			  		public bool AndContinue;
			  {{CreateItemDeclaration(stringBuilder, returnType, parameterSplit.InputParameters, genericTypeOverride, indent: 2)}}
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
			  		public System.Exception? Exception;
			  {{CreateItemSetupDeclaration(stringBuilder, returnType, indent: 2)}}
			  	}{{CreateComparer(stringBuilder, parameterSplit.InputParameters, indent: 1)}}
			  }
			  """;

		return new MockSetupResult(
			name: className,
			source: source
		);
	}

	private static string CreateInterfaceDerivedFrom(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ITypeSymbol? returnType)
	{
		string setupThrowsJoin, setupThrowsReset;
		if (returnType is null)
		{
			setupThrowsJoin = "ISetupThrowsJoin";
			setupThrowsReset = "ISetupThrowsReset";
		}
		else
		{
			setupThrowsJoin = "ISetupReturnsThrowsJoin";
			setupThrowsReset = "ISetupReturnsThrowsReset";
		}

		return stringBuilder.Clear()
			.AppendInterface("ISetupCallbackJoin", methodSymbol, returnType)
			.Append(", ").AppendInterface("ISetupCallbackReset", methodSymbol, returnType)
			.Append(", ").AppendInterface(setupThrowsJoin, methodSymbol, returnType)
			.Append(", ").AppendInterface(setupThrowsReset, methodSymbol, returnType)
			.ToString();
	}

	private static string CreateFields(StringBuilder stringBuilder, ParameterSplit parameterSplit, int indent)
	{
		stringBuilder.Clear();

		if (parameterSplit.InputParameters.IsDefaultOrEmpty)
		{
			stringBuilder
				.Indent(indent)
				.Append("private readonly Item _currentSetup = new();");
		}
		else
		{
			stringBuilder
				.Indent(indent).AppendLine("private static readonly Comparer SortComparer = new();")
				.Indent(indent).AppendLine("private SetupContainer<Item>? _setups;")
				.Indent(indent).Append("private Item? _currentSetup;");
		}

		return stringBuilder.ToString();
	}

	private static string CreateDelegates(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ITypeSymbol? returnType, ImmutableDictionary<IParameterSymbol, string> genericTypeOverride, int indent)
	{
		stringBuilder.Clear();

		stringBuilder
			.Indent(indent).Append("public delegate void CallbackDelegate(")
			.AppendParameters(methodSymbol.Parameters, parameterTypeOverride: genericTypeOverride)
			.AppendLine(");");

		if (returnType is not null)
		{
			stringBuilder
				.Indent(indent).Append($"public delegate {MockGeneratorConst.Suffixes.GenericReturnParameter}? ReturnsCallbackDelegate(")
				.AppendParameters(methodSymbol.Parameters, parameterTypeOverride: genericTypeOverride)
				.AppendLine(");");
		}

		return stringBuilder.ToString();
	}

	private static string CreateMethodImplementations(
		StringBuilder stringBuilder,
		IMethodSymbol methodSymbol,
		ITypeSymbol? returnType,
		string returnValueName,
		ParameterSplit parameterSplit,
		ImmutableDictionary<IParameterSymbol, string> genericTypeOverride,
		int indent)
	{
		stringBuilder.Clear();
		stringBuilder.AppendInvokeExecuteMethod(methodSymbol, returnType, returnValueName, parameterSplit, genericTypeOverride, indent);

		if (!parameterSplit.InputParameters.IsDefaultOrEmpty)
		{
			stringBuilder
				.AppendLine()
				.AppendSetupParametersMethod(parameterSplit.InputParameters, genericTypeOverride, indent);
		}

		if (returnType is not null)
		{
			stringBuilder
				.AppendLine()
				.AppendReturnsMethods(methodSymbol, parameterSplit, indent);
		}

		return stringBuilder
			.AppendLine()
			.AppendCallbackMethod(parameterSplit.InputParameters, indent).AppendLine()
			.AppendThrowsMethod(parameterSplit.InputParameters, indent)
			.ToString();
	}

	private static string CreateInterfaceMethodImplementations(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ITypeSymbol? returnType, ImmutableArray<IParameterSymbol> inputParameters, int indent)
	{
		stringBuilder.Clear();

		if (returnType is not null)
			stringBuilder.AppendReturnsInterfaceImplementation(methodSymbol, returnType, indent);

		return stringBuilder
			.AppendCallbackInterfaceImplementation(methodSymbol, returnType, indent)
			.AppendThrowsInterfaceImplementation(methodSymbol, returnType, indent)
			.AppendAndInterfaceImplementation(methodSymbol, returnType, inputParameters, indent)
			.ToString();
	}

	private static string CreateItemDeclaration(
		StringBuilder stringBuilder,
		ITypeSymbol? returnType,
		ImmutableArray<IParameterSymbol> inputParameters,
		ImmutableDictionary<IParameterSymbol, string> genericTypeOverride,
		int indent)
	{
		if (inputParameters.IsDefaultOrEmpty)
			return string.Empty;

		stringBuilder.Clear();
		stringBuilder.AppendLine();

		// fields
		foreach (var parameter in inputParameters)
		{
			var typeOverride = genericTypeOverride.GetValueOrDefault(parameter);

			stringBuilder
				.Indent(indent)
				.Append("public readonly ")
				.AppendItSetupType(parameter.Type, isNullable: true, typeOverride)
				.Append(' ')
				.AppendPropertyName(parameter.Name)
				.AppendLine(";");
		}

		// constructor
		stringBuilder
			.AppendLine()
			.Indent(indent).Append("public Item(")
			.AppendItSetupParameters(inputParameters, isNullable: true, parameterTypeOverride: genericTypeOverride)
			.AppendLine(")")
			.Indent(indent++).AppendLine("{");

		foreach (var parameter in inputParameters)
		{
			stringBuilder
				.Indent(indent)
				.AppendPropertyName(parameter.Name)
				.Append(" = ")
				.AppendParameterName(parameter.Name)
				.AppendLine(";");
		}

		stringBuilder
			.Indent(--indent).Append('}');

		// add return
		if (returnType is not null)
		{
			stringBuilder
				.AppendLine()
				.AppendLine();

			stringBuilder
				.Indent(indent).AppendLine("public void Add(in ReturnsCallbackDelegate returns)")
				.Indent(indent++).AppendLine("{")
				.Indent(indent).AppendLine("if (AndContinue && _currentSetup is not null)")
				.Indent(indent++).AppendLine("{")
				.Indent(indent).AppendLine("_currentSetup.Returns = returns;")
				.Indent(indent).AppendLine("AndContinue = false;")
				.Indent(indent).AppendLine("_currentSetup = null;")
				.Indent(--indent).AppendLine("}")
				.Indent(indent).AppendLine("else")
				.Indent(indent++).AppendLine("{")
				.Indent(indent).AppendLine("_currentSetup = new ItemSetup(returns: returns);")
				.Indent(indent).AppendLine("_queue.Enqueue(_currentSetup);")
				.Indent(--indent).AppendLine("}")
				.Indent(--indent).Append('}');
		}

		return stringBuilder
			.AppendLine()
			.ToString();
	}

	private static string CreateItemSetupDeclaration(StringBuilder stringBuilder, ITypeSymbol? returnType, int indent)
	{
		stringBuilder.Clear();

		// fields
		if (returnType is not null)
		{
			stringBuilder
				.Indent(indent)
				.AppendLine("public ReturnsCallbackDelegate? Returns;");
		}

		// constructor
		stringBuilder
			.AppendLine()
			.Indent(indent)
			.Append("public ItemSetup(");

		if (returnType is not null)
			stringBuilder.Append("in ReturnsCallbackDelegate? returns = null, ");

		stringBuilder
			.AppendLine("in CallbackDelegate? callback = null, in System.Exception? exception = null)")
			.Indent(indent++).AppendLine("{");

		if (returnType is not null)
			stringBuilder.Indent(indent).AppendLine("Returns = returns;");

		return stringBuilder
			.Indent(indent).AppendLine("Callback = callback;")
			.Indent(indent).AppendLine("Exception = exception;")
			.Indent(--indent).Append('}')
			.ToString();
	}

	private static string CreateComparer(StringBuilder stringBuilder, ImmutableArray<IParameterSymbol> inputParameters, int indent)
	{
		if (inputParameters.IsDefaultOrEmpty)
			return string.Empty;

		stringBuilder
			.Clear()
			.AppendLine()
			.AppendLine();

		return stringBuilder
			.Indent(indent).AppendLine("private sealed class Comparer: System.Collections.Generic.IComparer<Item>")
			.Indent(indent++).AppendLine("{")
			.Indent(indent).AppendLine("public int Compare(Item? x, Item? y)")
			.Indent(indent++).AppendLine("{")
			.Indent(indent).AppendLine("var xSort = 0;")
			.Indent(indent).AppendLine("var ySort = 0;")
			.AppendLine()
			.Indent(indent).AppendLine("if (x is not null)")
			.Indent(indent++).AppendLine("{")
			.AppendComparerDeclaration(inputParameters, item: "x", indent)
			.Indent(--indent).AppendLine("}")
			.AppendLine()
			.Indent(indent).AppendLine("if (y is not null)")
			.Indent(indent++).AppendLine("{")
			.AppendComparerDeclaration(inputParameters, item: "y", indent)
			.Indent(--indent).AppendLine("}")
			.AppendLine()
			.Indent(indent).AppendLine("return xSort.CompareTo(ySort);")
			.Indent(--indent).AppendLine("}")
			.Indent(--indent).Append('}')
			.ToString();
	}

	private static string CreateSetupClassName(StringBuilder stringBuilder, IMethodSymbol methodSymbol, out ImmutableDictionary<IParameterSymbol, string> genericTypeOverride)
	{
		stringBuilder.Clear();

		return stringBuilder
			.AppendSetupClassName(methodSymbol, out genericTypeOverride)
			.ToString();
	}

	private static string GetReturnValueName(ImmutableArray<IParameterSymbol> parameterSymbols)
	{
		var parameterNames = parameterSymbols
			.Select(static x => x.Name)
			.ToImmutableHashSet();

		var returnValue = "returnValue";

		for (var i = 0; i < 100; i++)
		{
			if (!parameterNames.Contains(returnValue))
				break;

			returnValue = '_' + returnValue;
		}

		return returnValue;
	}

	extension(StringBuilder @this)
	{
		private StringBuilder AppendSetupClassName(IMethodSymbol methodSymbol)
		{
			return @this.AppendSetupClassName(methodSymbol, useOverriddenGenericNames: true);
		}

		private StringBuilder AppendSetupClassName(IMethodSymbol methodSymbol, out ImmutableDictionary<IParameterSymbol, string> genericTypeOverride)
		{
			return @this.AppendSetupClassName(methodSymbol, useOverriddenGenericNames: true, out genericTypeOverride);
		}

		private StringBuilder AppendInterface(string interfaceName, IMethodSymbol methodSymbol, ITypeSymbol? returnTypeSymbol)
		{
			@this
				.Append(interfaceName)
				.Append('<');

			if (returnTypeSymbol is null)
			{
				@this
					.AppendSetupClassName(methodSymbol)
					.Append(".CallbackDelegate");
			}
			else
			{
				@this
					.AppendSetupClassName(methodSymbol)
					.Append($".CallbackDelegate, {MockGeneratorConst.Suffixes.GenericReturnParameter}, ")
					.AppendSetupClassName(methodSymbol)
					.Append(".ReturnsCallbackDelegate");
			}

			return @this.Append('>');
		}

		private StringBuilder AppendCallbackInterfaceImplementation(IMethodSymbol methodSymbol, ITypeSymbol? returnTypeSymbol, int indent)
		{
			const string methodDeclaration = ".Callback(in CallbackDelegate callback)", methodCall = "Callback(callback);";

			@this
				.Indent(indent).AppendInterface("ISetupCallbackJoin", methodSymbol, returnTypeSymbol)
				.Append(' ')
				.AppendInterface("ISetupCallbackStart", methodSymbol, returnTypeSymbol)
				.AppendLine(methodDeclaration)
				.AppendInterfaceImplementationBody(methodCall, ref indent);

			return @this
				.Indent(indent).AppendInterface("ISetup", methodSymbol, returnTypeSymbol)
				.Append(' ')
				.AppendInterface("ISetupCallbackReset", methodSymbol, returnTypeSymbol)
				.AppendLine(methodDeclaration)
				.AppendInterfaceImplementationBody(methodCall, ref indent);
		}

		private StringBuilder AppendThrowsInterfaceImplementation(IMethodSymbol methodSymbol, ITypeSymbol? returnTypeSymbol, int indent)
		{
			const string methodDeclaration = ".Throws(in System.Exception exception)", methodCall = "Throws(exception);";

			string setupThrowsJoin, setupThrowsStart, setupThrowsReset;
			if (returnTypeSymbol is null)
			{
				setupThrowsJoin = "ISetupThrowsJoin";
				setupThrowsStart = "ISetupThrowsStart";
				setupThrowsReset = "ISetupThrowsReset";
			}
			else
			{
				setupThrowsJoin = "ISetupReturnsThrowsJoin";
				setupThrowsStart = "ISetupReturnsThrowsStart";
				setupThrowsReset = "ISetupReturnsThrowsReset";
			}

			@this
				.Indent(indent).AppendInterface(setupThrowsJoin, methodSymbol, returnTypeSymbol)
				.Append(' ')
				.AppendInterface(setupThrowsStart, methodSymbol, returnTypeSymbol)
				.AppendLine(methodDeclaration)
				.AppendInterfaceImplementationBody(methodCall, ref indent);

			return @this
				.Indent(indent).AppendInterface("ISetup", methodSymbol, returnTypeSymbol)
				.Append(' ')
				.AppendInterface(setupThrowsReset, methodSymbol, returnTypeSymbol)
				.AppendLine(methodDeclaration)
				.AppendInterfaceImplementationBody(methodCall, ref indent);
		}

		private void AppendReturnsInterfaceImplementation(IMethodSymbol methodSymbol, ITypeSymbol? returnTypeSymbol, int indent)
		{
			const string methodCall = "Returns(returns);";

			foreach (var methodDeclaration in new[] { $".Returns(in {MockGeneratorConst.Suffixes.GenericReturnParameter} returns)", ".Returns(in ReturnsCallbackDelegate returns)" })
			{
				@this
					.Indent(indent).AppendInterface("ISetupReturnsThrowsJoin", methodSymbol, returnTypeSymbol)
					.Append(' ')
					.AppendInterface("ISetupReturnsThrowsStart", methodSymbol, returnTypeSymbol)
					.AppendLine(methodDeclaration)
					.AppendInterfaceImplementationBody(methodCall, ref indent);

				@this
					.Indent(indent).AppendInterface("ISetup", methodSymbol, returnTypeSymbol)
					.Append(' ')
					.AppendInterface("ISetupReturnsThrowsReset", methodSymbol, returnTypeSymbol)
					.AppendLine(methodDeclaration)
					.AppendInterfaceImplementationBody(methodCall, ref indent);
			}
		}

		private StringBuilder AppendAndInterfaceImplementation(IMethodSymbol methodSymbol, ITypeSymbol? returnTypeSymbol, ImmutableArray<IParameterSymbol> inputParameters, int indent)
		{
			const string methodDeclaration = ".And()";
			var methodCall = inputParameters.IsDefaultOrEmpty
				? "_currentSetup.AndContinue = true;"
				: "_currentSetup?.AndContinue = true;";

			string setupThrowsJoin, setupThrowsReset;
			if (returnTypeSymbol is null)
			{
				setupThrowsJoin = "ISetupThrowsJoin";
				setupThrowsReset = "ISetupThrowsReset";
			}
			else
			{
				setupThrowsJoin = "ISetupReturnsThrowsJoin";
				setupThrowsReset = "ISetupReturnsThrowsReset";
			}

			@this
				.Indent(indent).AppendInterface("ISetupCallbackReset", methodSymbol, returnTypeSymbol)
				.Append(' ')
				.AppendInterface(setupThrowsJoin, methodSymbol, returnTypeSymbol)
				.AppendLine(methodDeclaration)
				.AppendInterfaceImplementationBody(methodCall, ref indent);

			return @this
				.Indent(indent).AppendInterface(setupThrowsReset, methodSymbol, returnTypeSymbol)
				.Append(' ')
				.AppendInterface("ISetupCallbackJoin", methodSymbol, returnTypeSymbol)
				.AppendLine(methodDeclaration)
				.AppendInterfaceImplementationBody(methodCall, ref indent, appendFinalLines: false);
		}

		private StringBuilder AppendInterfaceImplementationBody(string methodCall, ref int indent, bool appendFinalLines = true)
		{
			@this
				.Indent(indent++).AppendLine("{")
				.Indent(indent).AppendLine(methodCall)
				.Indent(indent).AppendLine("return this;")
				.Indent(--indent);

			return appendFinalLines
				? @this.AppendLine("}").AppendLine()
				: @this.Append('}');
		}

		private StringBuilder AppendComparerDeclaration(ImmutableArray<IParameterSymbol> inputParameters, string item, int indent)
		{
			foreach (var parameter in inputParameters)
			{
				var parameterName = parameter.Name;

				@this
					.Indent(indent)
					.Append("if (")
					.Append(item)
					.Append('.')
					.AppendPropertyName(parameterName)
					.AppendLine(".HasValue)");

				@this
					.Indent(indent + 1)
					.Append(item)
					.Append("Sort += ")
					.Append(item)
					.Append('.')
					.AppendPropertyName(parameterName)
					.AppendLine(".Value.Sort;");
			}

			return @this;
		}
	}
}

file static class Extensions
{
	private const string DefaultAssign = " = default;";

	extension(StringBuilder stringBuilder)
	{
		public void AppendInvokeExecuteMethod(IMethodSymbol methodSymbol,
			ITypeSymbol? returnType,
			string returnValueName,
			ParameterSplit parameterSplit,
			ImmutableDictionary<IParameterSymbol, string> genericTypeOverride,
			int indent)
		{
			stringBuilder.Indent(indent);

			// Method name
			if (returnType is null)
			{
				stringBuilder
					.Append("public void Invoke(")
					.AppendParameters(methodSymbol.Parameters, parameterTypeOverride: genericTypeOverride)
					.AppendLine(")");
			}
			else
			{
				stringBuilder
					.Append("public bool Execute(")
					.AppendParameters(methodSymbol.Parameters, parameterTypeOverride: genericTypeOverride)
					.Append($", out {MockGeneratorConst.Suffixes.GenericReturnParameter}? ")
					.Append(returnValueName)
					.AppendLine(")");
			}

			stringBuilder
				.Indent(indent++).AppendLine("{");

			var hasMultipleSetups = !parameterSplit.InputParameters.IsDefaultOrEmpty;

			if (hasMultipleSetups)
			{
				// Method body - early return
				stringBuilder
					.Indent(indent).AppendLine("if (_setups is null)")
					.Indent(indent + 1).AppendLine(returnType is not null || !parameterSplit.OutputParameters.IsDefaultOrEmpty ? "goto Default;" : "return;").AppendLine();

				// Method body - setup check
				stringBuilder
					.Indent(indent).AppendLine("foreach (var setup in _setups)")
					.Indent(indent++).AppendLine("{");

				foreach (var parameter in parameterSplit.InputParameters)
				{
					stringBuilder
						.Indent(indent)
						.Append("if (setup.")
						.AppendPropertyName(parameter.Name)
						.Append(".HasValue && !setup.")
						.AppendPropertyName(parameter.Name)
						.Append(".Value.Check(")
						.Append(parameter.Name)
						.AppendLine("))");

					stringBuilder
						.Indent(indent + 1)
						.AppendLine("continue;");
				}

				stringBuilder.AppendLine();
			}

			stringBuilder
				.Indent(indent)
				.Append("var x = ")
				.Append(hasMultipleSetups ? "setup" : "_currentSetup")
				.AppendLine(".GetSetup();");

			if (parameterSplit.OutputParameters.IsDefaultOrEmpty)
			{
				stringBuilder
					.Indent(indent)
					.Append("x.Callback?.Invoke(")
					.AppendParameterNames(methodSymbol.Parameters, appendRefModifier: true)
					.AppendLine(");");
			}
			else
			{
				stringBuilder
					.Indent(indent).AppendLine("if (x.Callback is not null)")
					.Indent(indent).AppendLine("{");

				stringBuilder
					.Indent(indent + 1)
					.Append("x.Callback.Invoke(")
					.AppendParameterNames(methodSymbol.Parameters, appendRefModifier: true)
					.AppendLine(");");

				stringBuilder
					.Indent(indent).AppendLine("}")
					.Indent(indent).AppendLine("else")
					.Indent(indent).AppendLine("{");

				foreach (var outputParameter in parameterSplit.OutputParameters)
				{
					stringBuilder
						.Indent(indent + 1)
						.Append(outputParameter.Name)
						.AppendLine(DefaultAssign);
				}

				stringBuilder
					.Indent(indent).AppendLine("}");
			}

			stringBuilder
				.AppendLine()
				.Indent(indent)
				.AppendLine("if (x.Exception is not null)")
				.Indent(indent + 1).AppendLine("throw x.Exception;");

			if (returnType is not null)
			{
				stringBuilder
					.AppendLine()
					.Indent(indent)
					.AppendLine("if (x.Returns is not null)")
					.Indent(indent++).AppendLine("{");

				stringBuilder
					.Indent(indent)
					.Append(returnValueName)
					.Append(" = x.Returns(")
					.AppendParameterNames(methodSymbol.Parameters, appendRefModifier: true)
					.AppendLine(");");

				stringBuilder
					.Indent(indent)
					.AppendLine("return true;");

				stringBuilder
					.Indent(--indent).AppendLine("}")
					.AppendLine();

				if (parameterSplit.OutputParameters.IsDefaultOrEmpty)
				{
					stringBuilder
						.Indent(indent).AppendLine("goto Default;");
				}
				else
				{
					// Here we want to keep values assigned by the callback
					stringBuilder
						.Indent(indent).Append(returnValueName).AppendLine(DefaultAssign)
						.Indent(indent).AppendLine("return false;");
				}
			}
			else if (hasMultipleSetups)
			{
				stringBuilder
					.AppendLine()
					.Indent(indent).AppendLine("return;");
			}

			if (hasMultipleSetups)
			{
				stringBuilder
					.Indent(--indent)
					.AppendLine("}");
			}

			// Method body - return value
			if (hasMultipleSetups && (returnType is not null || !parameterSplit.OutputParameters.IsDefaultOrEmpty))
			{
				stringBuilder
					.AppendLine()
					.Indent(indent).AppendLine("Default:");

				if (returnType is not null)
				{
					stringBuilder
						.Indent(indent)
						.Append(returnValueName)
						.AppendLine(DefaultAssign);
				}

				foreach (var parameter in parameterSplit.OutputParameters)
				{
					stringBuilder
						.Indent(indent)
						.Append(parameter.Name)
						.AppendLine(DefaultAssign);
				}

				stringBuilder
					.Indent(indent)
					.Append("return");

				if (returnType is not null)
					stringBuilder.Append(" false");

				stringBuilder.AppendLine(";");
			}

			stringBuilder
				.Indent(--indent)
				.AppendLine("}");
		}

		public void AppendSetupParametersMethod(ImmutableArray<IParameterSymbol> inputParameters, ImmutableDictionary<IParameterSymbol, string> genericTypeOverride, int indent)
		{
			stringBuilder
				.Indent(indent)
				.Append("public void ")
				.AppendSetupParametersMethodName(inputParameters)
				.Append('(')
				.AppendItSetupParameters(inputParameters, parameterTypeOverride: genericTypeOverride)
				.AppendLine(")");

			stringBuilder
				.Indent(indent++).AppendLine("{");

			stringBuilder
				.Indent(indent).Append("_currentSetup = new Item(")
				.AppendParameterNames(inputParameters)
				.AppendLine(");").AppendLine();

			stringBuilder
				.Indent(indent).AppendLine("_setups ??= new SetupContainer<Item>(SortComparer);")
				.Indent(indent).AppendLine("_setups.Add(_currentSetup);");

			stringBuilder
				.Indent(--indent).AppendLine("}");
		}

		public StringBuilder AppendCallbackMethod(ImmutableArray<IParameterSymbol> inputParameters, int indent)
		{
			stringBuilder
				.Indent(indent).AppendLine("public void Callback(in CallbackDelegate callback)")
				.Indent(indent++).AppendLine("{")
				.TryAppendCurrentSetupCheck(inputParameters, indent)
				.Indent(indent).AppendLine("_currentSetup.Add(callback);");

			return stringBuilder
				.Indent(--indent).AppendLine("}");
		}

		public StringBuilder AppendThrowsMethod(ImmutableArray<IParameterSymbol> inputParameters, int indent)
		{
			return stringBuilder
				.Indent(indent).AppendLine("public void Throws(in System.Exception exception)")
				.Indent(indent++).AppendLine("{")
				.TryAppendCurrentSetupCheck(inputParameters, indent)
				.Indent(indent).AppendLine("_currentSetup.Add(exception);")
				.Indent(--indent).AppendLine("}");
		}

		public void AppendReturnsMethods(IMethodSymbol methodSymbol, ParameterSplit parameterSplit, int indent)
		{
			// Value method
			stringBuilder
				.Indent(indent).AppendLine($"public void Returns({MockGeneratorConst.Suffixes.GenericReturnParameter}? returns)")
				.Indent(indent).AppendLine("{")
				.Indent(++indent).Append("Returns((").AppendDiscardParameterNames(methodSymbol.Parameters).Append(") =>");

			if (parameterSplit.OutputParameters.IsDefaultOrEmpty)
			{
				stringBuilder.Append(" returns");
			}
			else
			{
				stringBuilder
					.AppendLine()
					.Indent(indent++).AppendLine("{");

				foreach (var outParameter in parameterSplit.OutputParameters)
				{
					stringBuilder
						.Indent(indent)
						.Append(outParameter.Name)
						.AppendLine(" = default;");
				}

				stringBuilder
					.Indent(indent).AppendLine("return returns;")
					.Indent(--indent).Append('}');
			}

			stringBuilder
				.AppendLine(");")
				.Indent(--indent).AppendLine("}").AppendLine();

			// Delegate method
			stringBuilder
				.Indent(indent).AppendLine("public void Returns(in ReturnsCallbackDelegate returns)")
				.Indent(indent++).AppendLine("{")
				.TryAppendCurrentSetupCheck(parameterSplit.InputParameters, indent)
				.Indent(indent).AppendLine("_currentSetup.Add(returns);")
				.Indent(--indent).AppendLine("}");
		}

		private StringBuilder TryAppendCurrentSetupCheck(ImmutableArray<IParameterSymbol> inputParameters, int indent)
		{
			if (!inputParameters.IsDefaultOrEmpty)
			{
				stringBuilder
					.Indent(indent).AppendLine("if (_currentSetup is null)")
					.Indent(indent + 1).AppendLine("""throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");""").AppendLine();
			}

			return stringBuilder;
		}
	}
}
