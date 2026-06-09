namespace MyNihongo.Mockurai.Utils;

internal static class MockSetupGenerator
{
	public static MockSetupResult GenerateMockSetup(this IMethodSymbol methodSymbol, CompilationCombinedResult result)
	{
		var stringBuilder = new StringBuilder();
		var className = CreateSetupClassName(stringBuilder, methodSymbol, out var genericTypeOverride);
		var returnType = methodSymbol.TryGetReturnType();
		var parameterSplit = methodSymbol.Parameters.SplitParameters();

		var source = stringBuilder
			.AppendLine("#nullable enable")
			.Append("namespace ").Append(result.Options.RootNamespace).AppendLine(";")
			.AppendLine()
			.Append("public sealed class ").Append(className).Append(" : ").CreateInterfaceDerivedFrom(methodSymbol, returnType).AppendLine()
			.AppendLine("{")
			.CreateFields(parameterSplit, indent: 1).AppendLine()
			.AppendLine()
			.CreateDelegates(methodSymbol, returnType, genericTypeOverride, indent: 1).AppendLine()
			.CreateMethodImplementations(methodSymbol, returnType, parameterSplit, genericTypeOverride, indent: 1).AppendLine()
			.CreateInterfaceMethodImplementations(methodSymbol, returnType, parameterSplit.InputParameters, indent: 1).AppendLine()
			.AppendLine()
			.Indent(1).AppendLine("private sealed class Item")
			.Indent(1).AppendLine("{")
			.Indent(2).AppendLine("private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];")
			.Indent(2).AppendLine("private ItemSetup? _currentSetup;")
			.Indent(2).AppendLine("public bool AndContinue;")
			.CreateItemDeclaration(parameterSplit.InputParameters, genericTypeOverride, indent: 2).AppendLine()
			.Indent(2).AppendLine("public void Add(in CallbackDelegate callback)")
			.Indent(2).AppendLine("{")
			.Indent(3).AppendLine("if (AndContinue && _currentSetup is not null)")
			.Indent(3).AppendLine("{")
			.Indent(4).AppendLine("_currentSetup.Callback = callback;")
			.Indent(4).AppendLine("AndContinue = false;")
			.Indent(4).AppendLine("_currentSetup = null;")
			.Indent(3).AppendLine("}")
			.Indent(3).AppendLine("else")
			.Indent(3).AppendLine("{")
			.Indent(4).AppendLine("_currentSetup = new ItemSetup(callback: callback);")
			.Indent(4).AppendLine("_queue.Enqueue(_currentSetup);")
			.Indent(3).AppendLine("}")
			.Indent(2).AppendLine("}")
			.CreateItemReturnsMethod(returnType, indent: 2).AppendLine()
			.Indent(2).AppendLine("public void Add(in System.Exception exception)")
			.Indent(2).AppendLine("{")
			.Indent(3).AppendLine("if (AndContinue && _currentSetup is not null)")
			.Indent(3).AppendLine("{")
			.Indent(4).AppendLine("_currentSetup.Exception = exception;")
			.Indent(4).AppendLine("AndContinue = false;")
			.Indent(4).AppendLine("_currentSetup = null;")
			.Indent(3).AppendLine("}")
			.Indent(3).AppendLine("else")
			.Indent(3).AppendLine("{")
			.Indent(4).AppendLine("_currentSetup = new ItemSetup(exception: exception);")
			.Indent(4).AppendLine("_queue.Enqueue(_currentSetup);")
			.Indent(3).AppendLine("}")
			.Indent(2).AppendLine("}")
			.AppendLine()
			.Indent(2).AppendLine("public ItemSetup GetSetup()")
			.Indent(2).AppendLine("{")
			.Indent(3).AppendLine("return _queue.Count switch")
			.Indent(3).AppendLine("{")
			.Indent(4).AppendLine("0 => ItemSetup.Default,")
			.Indent(4).AppendLine("1 => _queue.Peek(),")
			.Indent(4).AppendLine("_ => _queue.Dequeue(),")
			.Indent(3).AppendLine("};")
			.Indent(2).AppendLine("}")
			.Indent(1).AppendLine("}")
			.AppendLine()
			.Indent(1).AppendLine("private sealed class ItemSetup")
			.Indent(1).AppendLine("{")
			.Indent(2).AppendLine("public static readonly ItemSetup Default = new();")
			.AppendLine()
			.Indent(2).AppendLine("public CallbackDelegate? Callback;")
			.Indent(2).AppendLine("public System.Exception? Exception;")
			.CreateItemSetupDeclaration(returnType, indent: 2).AppendLine()
			.Indent(1).AppendLine("}")
			.CreateComparer(parameterSplit.InputParameters, indent: 1).AppendLine()
			.Append('}')
			.ToString();

		return new MockSetupResult(
			name: className,
			source: source
		);
	}

	extension(StringBuilder stringBuilder)
	{
		private StringBuilder CreateInterfaceDerivedFrom(IMethodSymbol methodSymbol, ITypeSymbol? returnType)
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

			return stringBuilder
				.AppendInterface("ISetupCallbackJoin", methodSymbol, returnType)
				.Append(", ").AppendInterface("ISetupCallbackReset", methodSymbol, returnType)
				.Append(", ").AppendInterface(setupThrowsJoin, methodSymbol, returnType)
				.Append(", ").AppendInterface(setupThrowsReset, methodSymbol, returnType);
		}

		private StringBuilder CreateFields(ParameterSplit parameterSplit, int indent)
		{
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

			return stringBuilder;
		}

		private StringBuilder CreateDelegates(IMethodSymbol methodSymbol, ITypeSymbol? returnType, ImmutableDictionary<IParameterSymbol, StringTemplate> genericTypeOverride, int indent)
		{
			stringBuilder
				.Indent(indent).Append("public delegate void CallbackDelegate(")
				.AppendParameters(methodSymbol.Parameters, parameterTypeOverride: genericTypeOverride, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
				.AppendLine(");");

			if (returnType is not null)
			{
				stringBuilder
					.Indent(indent).Append($"public delegate {MockGeneratorConst.Suffixes.GenericReturnParameter}? ReturnsCallbackDelegate(")
					.AppendParameters(methodSymbol.Parameters, parameterTypeOverride: genericTypeOverride, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
					.AppendLine(");");
			}

			return stringBuilder;
		}

		private StringBuilder CreateMethodImplementations(IMethodSymbol methodSymbol,
			ITypeSymbol? returnType,
			ParameterSplit parameterSplit,
			ImmutableDictionary<IParameterSymbol, StringTemplate> genericTypeOverride,
			int indent)
		{
			stringBuilder.AppendInvokeExecuteMethod(methodSymbol, returnType, parameterSplit, genericTypeOverride, indent);

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
				.AppendThrowsMethod(parameterSplit.InputParameters, indent);
		}

		private StringBuilder CreateInterfaceMethodImplementations(IMethodSymbol methodSymbol,
			ITypeSymbol? returnType,
			ImmutableArray<ParameterSplit.Item> inputParameters,
			int indent)
		{
			if (returnType is not null)
				stringBuilder.AppendReturnsInterfaceImplementation(methodSymbol, returnType, indent);

			return stringBuilder
				.AppendCallbackInterfaceImplementation(methodSymbol, returnType, indent)
				.AppendThrowsInterfaceImplementation(methodSymbol, returnType, indent)
				.AppendAndInterfaceImplementation(methodSymbol, returnType, inputParameters, indent);
		}

		private StringBuilder CreateItemDeclaration(ImmutableArray<ParameterSplit.Item> inputParameters,
			ImmutableDictionary<IParameterSymbol, StringTemplate> genericTypeOverride,
			int indent)
		{
			if (inputParameters.IsDefaultOrEmpty)
				return stringBuilder;

			stringBuilder.AppendLine();

			// fields
			foreach (var parameterItem in inputParameters)
			{
				var typeOverride = genericTypeOverride
					.GetValueOrDefault(parameterItem.Parameter)
					.Build();

				stringBuilder
					.Indent(indent)
					.Append("public readonly ")
					.AppendItSetupType(parameterItem.Parameter.Type, isNullable: true, typeOverride)
					.Append(' ')
					.AppendParameterPropertyName(parameterItem.Index)
					.AppendLine(";");
			}

			// constructor
			stringBuilder
				.AppendLine()
				.Indent(indent).Append("public Item(")
				.AppendItSetupParameters(inputParameters, isNullable: true, parameterTypeOverride: genericTypeOverride, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
				.AppendLine(")")
				.Indent(indent++).AppendLine("{");

			foreach (var parameterItem in inputParameters)
			{
				stringBuilder
					.Indent(indent)
					.AppendParameterPropertyName(parameterItem.Index)
					.Append(" = ")
					.AppendParameterVariableName(parameterItem.Index)
					.AppendLine(";");
			}

			return stringBuilder
				.Indent(--indent).AppendLine("}");
		}

		private StringBuilder CreateItemReturnsMethod(ITypeSymbol? returnType, int indent)
		{
			if (returnType is null)
				return stringBuilder;

			return stringBuilder
				.AppendLine()
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
				.Indent(--indent).AppendLine("}");
		}

		private StringBuilder CreateItemSetupDeclaration(ITypeSymbol? returnType, int indent)
		{
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
				.Indent(--indent).Append('}');
		}

		private StringBuilder CreateComparer(ImmutableArray<ParameterSplit.Item> inputParameters, int indent)
		{
			if (inputParameters.IsDefaultOrEmpty)
				return stringBuilder;

			stringBuilder.AppendLine();

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
				.Indent(--indent).Append('}');
		}
	}

	private static string CreateSetupClassName(StringBuilder stringBuilder, IMethodSymbol methodSymbol, out ImmutableDictionary<IParameterSymbol, StringTemplate> genericTypeOverride)
	{
		var className = stringBuilder
			.AppendSetupClassName(methodSymbol, useOverriddenGenericNames: true, out genericTypeOverride)
			.ToString();

		stringBuilder.Clear();
		return className;
	}

	extension(StringBuilder @this)
	{
		private StringBuilder AppendInterface(string interfaceName, IMethodSymbol methodSymbol, ITypeSymbol? returnTypeSymbol)
		{
			return @this.AppendInterface(interfaceName, methodSymbol, returnTypeSymbol, useOverriddenGenericNames: true);
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

		private StringBuilder AppendAndInterfaceImplementation(IMethodSymbol methodSymbol, ITypeSymbol? returnTypeSymbol, ImmutableArray<ParameterSplit.Item> inputParameters, int indent)
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

		private StringBuilder AppendComparerDeclaration(ImmutableArray<ParameterSplit.Item> inputParameters, string item, int indent)
		{
			foreach (var parameterItem in inputParameters)
			{
				@this
					.Indent(indent)
					.Append("if (")
					.Append(item)
					.Append('.')
					.AppendParameterPropertyName(parameterItem.Index)
					.AppendLine(".HasValue)");

				@this
					.Indent(indent + 1)
					.Append(item)
					.Append("Sort += ")
					.Append(item)
					.Append('.')
					.AppendParameterPropertyName(parameterItem.Index)
					.AppendLine(".Value.Sort;");
			}

			return @this;
		}
	}
}

file static class Extensions
{
	private const string DefaultAssign = MockGeneratorConst.Suffixes.DefaultAssign;
	private const string ReturnValueName = MockGeneratorConst.Variables.ReturnValue;

	extension(StringBuilder stringBuilder)
	{
		public void AppendInvokeExecuteMethod(IMethodSymbol methodSymbol,
			ITypeSymbol? returnType,
			ParameterSplit parameterSplit,
			ImmutableDictionary<IParameterSymbol, StringTemplate> genericTypeOverride,
			int indent)
		{
			stringBuilder.Indent(indent);

			// Method name
			if (returnType is null)
			{
				stringBuilder
					.Append("public void Invoke(")
					.AppendParameters(methodSymbol.Parameters, parameterTypeOverride: genericTypeOverride, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
					.AppendLine(")");
			}
			else
			{
				stringBuilder
					.Append("public bool Execute(")
					.AppendParameters(methodSymbol.Parameters, parameterTypeOverride: genericTypeOverride, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
					.Append($", out {MockGeneratorConst.Suffixes.GenericReturnParameter}? ")
					.Append(ReturnValueName)
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

				foreach (var parameterItem in parameterSplit.InputParameters)
				{
					stringBuilder
						.Indent(indent)
						.Append("if (setup.")
						.AppendParameterPropertyName(parameterItem.Index)
						.Append(".HasValue && !setup.")
						.AppendParameterPropertyName(parameterItem.Index)
						.Append(".Value.Check(")
						.AppendParameterVariableName(parameterItem.Index)
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
					.AppendParameterNames(methodSymbol.Parameters, appendRefModifier: true, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
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
					.AppendParameterNames(methodSymbol.Parameters, appendRefModifier: true, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
					.AppendLine(");");

				stringBuilder
					.Indent(indent).AppendLine("}")
					.Indent(indent).AppendLine("else")
					.Indent(indent).AppendLine("{");

				foreach (var parameterItem in parameterSplit.OutputParameters)
				{
					stringBuilder
						.Indent(indent + 1)
						.AppendParameterVariableName(parameterItem.Index)
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
					.Append(ReturnValueName)
					.Append(" = x.Returns(")
					.AppendParameterNames(methodSymbol.Parameters, appendRefModifier: true, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
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
						.Indent(indent).Append(ReturnValueName).AppendLine(DefaultAssign)
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
						.Append(ReturnValueName)
						.AppendLine(DefaultAssign);
				}

				foreach (var parameterItem in parameterSplit.OutputParameters)
				{
					stringBuilder
						.Indent(indent)
						.AppendParameterVariableName(parameterItem.Index)
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

		public void AppendSetupParametersMethod(ImmutableArray<ParameterSplit.Item> inputParameters, ImmutableDictionary<IParameterSymbol, StringTemplate> genericTypeOverride, int indent)
		{
			stringBuilder
				.Indent(indent)
				.Append("public void ")
				.AppendSetupParametersMethodName(inputParameters)
				.Append('(')
				.AppendItSetupParameters(inputParameters, parameterTypeOverride: genericTypeOverride, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
				.AppendLine(")");

			stringBuilder
				.Indent(indent++).AppendLine("{");

			stringBuilder
				.Indent(indent).Append("_currentSetup = new Item(")
				.AppendParameterNames(inputParameters, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
				.AppendLine(");").AppendLine();

			stringBuilder
				.Indent(indent).AppendLine("_setups ??= new SetupContainer<Item>(SortComparer);")
				.Indent(indent).AppendLine("_setups.Add(_currentSetup);");

			stringBuilder
				.Indent(--indent).AppendLine("}");
		}

		public StringBuilder AppendCallbackMethod(ImmutableArray<ParameterSplit.Item> inputParameters, int indent)
		{
			stringBuilder
				.Indent(indent).AppendLine("public void Callback(in CallbackDelegate callback)")
				.Indent(indent++).AppendLine("{")
				.TryAppendCurrentSetupCheck(inputParameters, indent)
				.Indent(indent).AppendLine("_currentSetup.Add(callback);");

			return stringBuilder
				.Indent(--indent).AppendLine("}");
		}

		public StringBuilder AppendThrowsMethod(ImmutableArray<ParameterSplit.Item> inputParameters, int indent)
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
				.Indent(++indent).Append("Returns((").AppendDiscardParameterNames(methodSymbol.Parameters, appendParameterName: MethodSymbolEx.AppendParameterVariableName).Append(") =>");

			if (parameterSplit.OutputParameters.IsDefaultOrEmpty)
			{
				stringBuilder.Append(" returns");
			}
			else
			{
				stringBuilder
					.AppendLine()
					.Indent(indent++).AppendLine("{");

				foreach (var parameterItem in parameterSplit.OutputParameters)
				{
					stringBuilder
						.Indent(indent)
						.AppendParameterVariableName(parameterItem.Index)
						.AppendLine(DefaultAssign);
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

		private StringBuilder TryAppendCurrentSetupCheck(ImmutableArray<ParameterSplit.Item> inputParameters, int indent)
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
