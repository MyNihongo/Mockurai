namespace MyNihongo.Mock.Utils;

internal static class MockSetupGenerator
{
	public static MockSetupResult GenerateMockSetup(this IMethodSymbol methodSymbol, CompilationCombinedResult result)
	{
		var stringBuilder = new StringBuilder();
		var className = CreateSetupClassName(stringBuilder, methodSymbol);
		var returnType = methodSymbol.TryGetReturnType();

		var source =
			$$"""
			  namespace {{result.Options.RootNamespace}};

			  public sealed class {{className}} : {{CreateInterfaceDerivedFrom(stringBuilder, methodSymbol, returnType)}}
			  {
			  	private static readonly Comparer SortComparer = new();
			  	private SetupContainer<Item>? _setups;
			  	private Item? _currentSetup;

			  {{CreateDelegates(stringBuilder, methodSymbol, returnType, indent: 1)}}
			  {{CreateMethodImplementations(stringBuilder, methodSymbol, returnType, indent: 1)}}
			  {{CreateInterfaceMethodImplementations(stringBuilder, methodSymbol, returnType, indent: 1)}}

			  	private sealed class Item
			  	{
			  		private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
			  		private ItemSetup? _currentSetup;
			  		public bool AndContinue;

			  {{CreateItemDeclaration(stringBuilder, methodSymbol, returnType, indent: 2)}}

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
			  {{CreateItemSetupDeclaration(stringBuilder, returnType, indent: 2)}}
			  	}

			  	private sealed class Comparer: System.Collections.Generic.IComparer<Item>
			  	{
			  		public int Compare(Item? x, Item? y)
			  		{
			  			var xSort = 0;
			  			var ySort = 0;

			  			if (x is not null)
			  			{
			  {{CreateCompareDeclaration(stringBuilder, methodSymbol, item: "x", indent: 4)}}
			  			}

			  			if (y is not null)
			  			{
			  {{CreateCompareDeclaration(stringBuilder, methodSymbol, item: "y", indent: 4)}}
			  			}

			  			return xSort.CompareTo(ySort);
			  		}
			  	}
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

	private static string CreateDelegates(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ITypeSymbol? returnType, int indent)
	{
		stringBuilder.Clear();

		stringBuilder
			.Indent(indent).Append("public delegate void CallbackDelegate(")
			.AppendParameters(methodSymbol.Parameters)
			.AppendLine(");");

		if (returnType is not null)
		{
			stringBuilder
				.Indent(indent).Append("public delegate TReturns? ReturnsCallbackDelegate(")
				.AppendParameters(methodSymbol.Parameters)
				.AppendLine(");");
		}

		return stringBuilder.ToString();
	}

	private static string CreateMethodImplementations(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ITypeSymbol? returnType, int indent)
	{
		stringBuilder.Clear();

		stringBuilder
			.AppendInvokeExecuteMethod(methodSymbol, returnType, indent).AppendLine()
			.AppendSetupParametersMethod(methodSymbol, indent).AppendLine();

		if (returnType is not null)
			stringBuilder.AppendReturnsMethods(methodSymbol, indent).AppendLine();

		return stringBuilder
			.AppendCallbackMethod(indent).AppendLine()
			.AppendThrowsMethod(indent)
			.ToString();
	}

	private static string CreateInterfaceMethodImplementations(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ITypeSymbol? returnType, int indent)
	{
		stringBuilder.Clear();

		if (returnType is not null)
			stringBuilder.AppendReturnsInterfaceImplementation(methodSymbol, returnType, indent);

		return stringBuilder
			.AppendCallbackInterfaceImplementation(methodSymbol, returnType, indent)
			.AppendThrowsInterfaceImplementation(methodSymbol, returnType, indent)
			.AppendAndInterfaceImplementation(methodSymbol, returnType, indent)
			.ToString();
	}

	private static string CreateItemDeclaration(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ITypeSymbol? returnType, int indent)
	{
		stringBuilder.Clear();

		// fields
		foreach (var parameter in methodSymbol.Parameters)
		{
			stringBuilder
				.Indent(indent)
				.Append("public readonly ")
				.AppendItSetupType(parameter.Type, isNullable: true)
				.Append(' ')
				.AppendPropertyName(parameter.Name)
				.AppendLine(";");
		}

		// constructor
		stringBuilder
			.AppendLine()
			.Indent(indent).Append("public Item(")
			.AppendItSetupParameters(methodSymbol.Parameters, isNullable: true)
			.AppendLine(")")
			.Indent(indent++).AppendLine("{");

		foreach (var parameter in methodSymbol.Parameters)
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

		return stringBuilder.ToString();
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

	private static string CreateCompareDeclaration(StringBuilder stringBuilder, IMethodSymbol methodSymbol, string item, int indent)
	{
		stringBuilder.Clear();

		for (int i = 0, lastIndex = methodSymbol.Parameters.Length - 1; i < methodSymbol.Parameters.Length; i++)
		{
			var parameterName = methodSymbol.Parameters[i].Name;

			stringBuilder
				.Indent(indent)
				.Append("if (")
				.Append(item)
				.Append('.')
				.AppendPropertyName(parameterName)
				.AppendLine(".HasValue)");

			stringBuilder
				.Indent(indent + 1)
				.Append(item)
				.Append("Sort += ")
				.Append(item)
				.Append('.')
				.AppendPropertyName(parameterName)
				.Append(".Value.Sort;");

			if (i < lastIndex)
				stringBuilder.AppendLine();
		}

		return stringBuilder.ToString();
	}

	private static string CreateSetupClassName(StringBuilder stringBuilder, IMethodSymbol methodSymbol)
	{
		stringBuilder.Clear();

		return stringBuilder
			.AppendSetupClassName(methodSymbol, OverrideReturnType)
			.ToString();
	}

	private static void OverrideReturnType(StringBuilder stringBuilder, ITypeSymbol typeSymbol)
	{
		stringBuilder.Append("TReturns");
	}

	extension(StringBuilder @this)
	{
		private StringBuilder AppendInterface(string interfaceName, IMethodSymbol methodSymbol, ITypeSymbol? returnTypeSymbol)
		{
			@this
				.Append(interfaceName)
				.Append('<');

			if (returnTypeSymbol is null)
			{
				@this
					.AppendSetupClassName(methodSymbol, OverrideReturnType)
					.Append(".CallbackDelegate");
			}
			else
			{
				@this
					.AppendSetupClassName(methodSymbol, OverrideReturnType)
					.Append(".CallbackDelegate, TReturns, ")
					.AppendSetupClassName(methodSymbol, OverrideReturnType)
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

			foreach (var methodDeclaration in new[] { ".Returns(in TReturns returns)", ".Returns(in ReturnsCallbackDelegate returns)" })
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

		private StringBuilder AppendAndInterfaceImplementation(IMethodSymbol methodSymbol, ITypeSymbol? returnTypeSymbol, int indent)
		{
			const string methodDeclaration = ".And()", methodCall = "_currentSetup?.AndContinue = true;";

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
	}
}

file static class Extensions
{
	extension(StringBuilder stringBuilder)
	{
		public StringBuilder AppendInvokeExecuteMethod(IMethodSymbol methodSymbol, ITypeSymbol? returnType, int indent)
		{
			stringBuilder.Indent(indent);

			// Method name
			if (returnType is null)
			{
				stringBuilder
					.Append("public void Invoke(")
					.AppendParameters(methodSymbol.Parameters)
					.AppendLine(")");
			}
			else
			{
				stringBuilder
					.Append("public bool Execute(")
					.AppendParameters(methodSymbol.Parameters)
					.AppendLine(", out TReturns? returnValue)");
			}

			stringBuilder
				.Indent(indent++).AppendLine("{");

			// Method body - early return
			stringBuilder
				.Indent(indent).AppendLine("if (_setups is null)")
				.Indent(indent + 1).AppendLine(returnType is null ? "return;" : "goto Default;").AppendLine();

			// Method body - setup check
			stringBuilder
				.Indent(indent).AppendLine("foreach (var setup in _setups)")
				.Indent(indent++).AppendLine("{");

			foreach (var parameter in methodSymbol.Parameters)
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

			stringBuilder
				.AppendLine()
				.Indent(indent)
				.AppendLine("var x = setup.GetSetup();");

			stringBuilder
				.Indent(indent)
				.Append("x.Callback?.Invoke(")
				.AppendParameterNames(methodSymbol.Parameters)
				.AppendLine(");").AppendLine();

			stringBuilder
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
					.Append("returnValue = x.Returns(")
					.AppendParameterNames(methodSymbol.Parameters)
					.AppendLine(");");

				stringBuilder
					.Indent(indent)
					.AppendLine("return true;");

				stringBuilder
					.Indent(--indent).AppendLine("}");

				stringBuilder
					.AppendLine()
					.Indent(indent).AppendLine("goto Default;");
			}
			else
			{
				stringBuilder
					.AppendLine()
					.Indent(indent).AppendLine("return;");
			}

			stringBuilder
				.Indent(--indent)
				.AppendLine("}");

			// Method body - return value
			if (returnType is not null)
			{
				stringBuilder
					.AppendLine()
					.Indent(indent).AppendLine("Default:")
					.Indent(indent).AppendLine("returnValue = default;")
					.Indent(indent).AppendLine("return false;");
			}

			return stringBuilder
				.Indent(--indent)
				.AppendLine("}");
		}

		public StringBuilder AppendSetupParametersMethod(IMethodSymbol methodSymbol, int indent)
		{
			stringBuilder
				.Indent(indent)
				.Append("public void SetupParameters(")
				.AppendItSetupParameters(methodSymbol.Parameters)
				.AppendLine(")");

			stringBuilder
				.Indent(indent++).AppendLine("{");

			stringBuilder
				.Indent(indent).Append("_currentSetup = new Item(")
				.AppendParameterNames(methodSymbol.Parameters)
				.AppendLine(");").AppendLine();

			stringBuilder
				.Indent(indent).AppendLine("_setups ??= new SetupContainer<Item>(SortComparer);")
				.Indent(indent).AppendLine("_setups.Add(_currentSetup);");

			return stringBuilder
				.Indent(--indent).AppendLine("}");
		}

		public StringBuilder AppendCallbackMethod(int indent)
		{
			stringBuilder
				.Indent(indent).AppendLine("public void Callback(in CallbackDelegate callback)")
				.Indent(indent++).AppendLine("{")
				.Indent(indent).AppendLine("if (_currentSetup is null)")
				.Indent(indent + 1).AppendLine("""throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");""").AppendLine()
				.Indent(indent).AppendLine("_currentSetup.Add(callback);");

			return stringBuilder
				.Indent(--indent).AppendLine("}");
		}

		public StringBuilder AppendThrowsMethod(int indent)
		{
			return stringBuilder
				.Indent(indent).AppendLine("public void Throws(in System.Exception exception)")
				.Indent(indent++).AppendLine("{")
				.Indent(indent).AppendLine("if (_currentSetup is null)")
				.Indent(indent + 1).AppendLine("""throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");""").AppendLine()
				.Indent(indent).AppendLine("_currentSetup.Add(exception);")
				.Indent(--indent).AppendLine("}");
		}

		public StringBuilder AppendReturnsMethods(IMethodSymbol methodSymbol, int indent)
		{
			// Value method
			stringBuilder
				.Indent(indent).AppendLine("public void Returns(TReturns? returns)")
				.Indent(indent).AppendLine("{")
				.Indent(indent + 1).Append("Returns((").AppendDiscardParameterNames(methodSymbol.Parameters).AppendLine(") => returns);")
				.Indent(indent).AppendLine("}").AppendLine();

			// Delegate method
			return stringBuilder
				.Indent(indent).AppendLine("public void Returns(in ReturnsCallbackDelegate returns)")
				.Indent(indent++).AppendLine("{")
				.Indent(indent).AppendLine("if (_currentSetup is null)")
				.Indent(indent + 1).AppendLine("""throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");""").AppendLine()
				.Indent(indent).AppendLine("_currentSetup.Add(returns);")
				.Indent(--indent).AppendLine("}");
		}
	}
}
