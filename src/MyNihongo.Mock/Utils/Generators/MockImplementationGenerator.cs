using MockClassName = (string Constructor, string Type, string GenericTypes);

namespace MyNihongo.Mock.Utils;

internal static class MockImplementationGenerator
{
	public static MockImplementationResult GenerateMockImplementation(this ITypeSymbol typeSymbol, CompilationCombinedResult result)
	{
		var stringBuilder = new StringBuilder();
		var typeString = typeSymbol.ToString();
		var (constructor, mockClassName, genericTypes) = CreateMockClassName(stringBuilder, typeSymbol);
		var mockedTypeSymbol = new MockedTypeSymbol(typeSymbol);
		var mockableMembers = GetMockableMembers(typeSymbol);

		var source =
			$$"""
			  namespace {{result.Options.RootNamespace}};

			  public sealed class {{mockClassName}} : IMock<{{typeString}}>
			  {
			  	private readonly InvocationIndex.Counter _invocationIndex;
			  	private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
			  	private Proxy? _proxy;

			  	public {{constructor}}(InvocationIndex.Counter invocationIndex)
			  	{
			  		_invocationIndex = invocationIndex;
			  		_invocationProviders = GetInvocations;
			  	}

			  	public {{typeString}} Object => _proxy ??= new Proxy(this);

			  {{CreateMockMethods(stringBuilder, mockedTypeSymbol, mockableMembers, indent: 1, out var methodSymbols)}}

			  	public void VerifyNoOtherCalls()
			  	{
			  {{CreateVerifyNoOtherCalls(stringBuilder, mockableMembers, indent: 2)}}
			  	}

			  	private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
			  	{
			  {{CreateGetInvocations(stringBuilder, mockedTypeSymbol, mockableMembers, indent: 2)}}
			  	}

			  	private sealed class Proxy : {{typeString}}
			  	{
			  		private readonly {{mockClassName}} _mock;

			  		public Proxy({{mockClassName}} mock)
			  		{
			  			_mock = mock;
			  		}

			  {{CreateProxyMethods(stringBuilder, typeSymbol, mockableMembers, indent: 2)}}
			  	}
			  }

			  public static partial class MockExtensions
			  {
			  	extension{{genericTypes}}(IMock<{{typeString}}> @this)
			  	{
			  		public void VerifyNoOtherCalls() =>
			  			(({{mockClassName}})@this).VerifyNoOtherCalls();

			  		{{CreateMockExtensions(stringBuilder, mockableMembers, indent: 2)}}
			  	}
			  }

			  public static partial class MockSequenceExtensions
			  {
			  	extension{{genericTypes}}(IMockSequence<{{typeString}}> @this)
			  	{
			  	{{CreateMockSequenceExtensions(stringBuilder, mockableMembers, indent: 2)}}
			  	}
			  }
			  """;

		return new MockImplementationResult(
			name: mockClassName,
			source: source,
			methodSymbols: methodSymbols
		);
	}

	private static MockClassName CreateMockClassName(StringBuilder stringBuilder, ITypeSymbol typeSymbol)
	{
		stringBuilder.Clear();
		stringBuilder.AppendMockClassName(typeSymbol, appendGenericTypes: false);
		var constructorName = stringBuilder.ToString();

		stringBuilder.Clear();
		stringBuilder.AppendGenericTypes(typeSymbol);

		if (stringBuilder.Length == 0)
			return (constructorName, constructorName, string.Empty);

		var genericTypes = stringBuilder.ToString();
		return (constructorName, constructorName + genericTypes, genericTypes);
	}

	private static IReadOnlyList<MockedMemberSymbol> GetMockableMembers(ITypeSymbol typeSymbol)
	{
		var symbols = typeSymbol.TypeKind switch
		{
			TypeKind.Class => typeSymbol.GetOverridableMembers(),
			_ => typeSymbol.GetMembers(),
		};

		return symbols
			.FilterMockableSymbols()
			.ToLookup(static x => x.Name)
			.SelectMany(static x => x.Select((y, i) => new MockedMemberSymbol($"{x.Key}{i}", y)))
			.ToArray();
	}

	private static string CreateMockMethods(StringBuilder stringBuilder, MockedTypeSymbol typeSymbol, IReadOnlyList<MockedMemberSymbol> members, int indent, out IReadOnlyList<IMethodSymbol>? methodSymbols)
	{
		stringBuilder.Clear();
		List<IMethodSymbol>? methodSymbolList = null;

		for (int i = 0, generateCount = 0; i < members.Count; i++)
		{
			var member = members[i];
			Func<StringBuilder, MockedTypeSymbol, MockedMemberSymbol, int, IMethodSymbol?>? handler = member.Symbol.Kind switch
			{
				SymbolKind.Event => MockImplementationEventGenerator.AppendEventMockMethod,
				SymbolKind.Property => MockImplementationPropertyGenerator.AppendPropertyMockMethod,
				SymbolKind.Method => MockImplementationMethodGenerator.AppendMethodMockMethod,
				_ => null,
			};

			if (handler is null)
				continue;

			if (generateCount > 0)
				stringBuilder
					.AppendLine()
					.AppendLine();

			var methodSymbol = handler(stringBuilder, typeSymbol, member, indent);
			if (methodSymbol is not null)
			{
				methodSymbolList ??= [];
				methodSymbolList.Add(methodSymbol);
			}

			generateCount++;
		}

		methodSymbols = methodSymbolList;
		return stringBuilder.ToString();
	}

	private static string CreateVerifyNoOtherCalls(StringBuilder stringBuilder, IReadOnlyList<MockedMemberSymbol> members, int indent)
	{
		stringBuilder.Clear();

		var methodIndex = 0;
		foreach (var member in members)
		{
			Func<MockedMemberSymbol, IEnumerable<IMethodSymbol>>? methods = member.Symbol.Kind switch
			{
				SymbolKind.Event => MockImplementationEventGenerator.GetEventMethods,
				SymbolKind.Property => MockImplementationPropertyGenerator.GetPropertyMethods,
				SymbolKind.Method => MockImplementationMethodGenerator.GetMethodMethods,
				_ => null,
			};

			if (methods is null)
				continue;

			foreach (var method in methods(member))
			{
				if (methodIndex > 0)
					stringBuilder.AppendLine();

				stringBuilder
					.Indent(indent)
					.AppendInvocationFieldName(member.MemberName, method.MethodKind)
					.AppendVerifyNoOtherCallsInvocation()
					.Append(';');

				methodIndex++;
			}
		}

		return stringBuilder.ToString();
	}

	private static string CreateGetInvocations(StringBuilder stringBuilder, MockedTypeSymbol typeSymbol, IReadOnlyList<MockedMemberSymbol> members, int indent)
	{
		stringBuilder.Clear();

		if (members.Count == 0)
		{
			stringBuilder
				.Indent(indent)
				.Append("yield break;");

			return stringBuilder.ToString();
		}

		var methodIndex = 0;
		foreach (var member in members)
		{
			Func<MockedMemberSymbol, IEnumerable<IMethodSymbol>>? methods = member.Symbol.Kind switch
			{
				SymbolKind.Event => MockImplementationEventGenerator.GetEventMethods,
				SymbolKind.Property => MockImplementationPropertyGenerator.GetPropertyMethods,
				SymbolKind.Method => MockImplementationMethodGenerator.GetMethodMethods,
				_ => null,
			};

			if (methods is null)
				continue;

			foreach (var method in methods(member))
			{
				if (methodIndex > 0)
					stringBuilder.AppendLine();

				stringBuilder
					.Indent(indent)
					.Append("yield return ")
					.AppendInvocationFieldName(member.MemberName, method.MethodKind)
					.Append(';');

				methodIndex++;
			}
		}

		return stringBuilder.ToString();
	}

	private static string CreateProxyMethods(StringBuilder stringBuilder, ITypeSymbol typeSymbol, IReadOnlyList<MockedMemberSymbol> members, int indent)
	{
		stringBuilder.Clear();

		// TODO: implement
		foreach (var member in members)
		{
			switch (member.Symbol.Kind)
			{
				case SymbolKind.Event:
					stringBuilder
						.Indent(indent)
						.Append("public ")
						.TryAppendOverride(member.Symbol)
						.Append("event ")
						.Append(((IEventSymbol)member.Symbol).Type)
						.Append(' ')
						.Append(member.Symbol.Name)
						.AppendLine(";");
					break;
				case SymbolKind.Property:
					stringBuilder
						.Indent(indent)
						.Append("public ")
						.TryAppendOverride(member.Symbol)
						.Append(((IPropertySymbol)member.Symbol).Type)
						.Append(' ')
						.Append(member.Symbol.Name)
						.Append(" { get; ");

					if (((IPropertySymbol)member.Symbol).SetMethod is not null)
					{
						var name = ((IPropertySymbol)member.Symbol).SetMethod!.IsInitOnly ? "init; " : "set; ";
						stringBuilder.Append(name);
					}

					stringBuilder
						.AppendLine("}");
					break;
				case SymbolKind.Method:
					stringBuilder
						.Indent(indent)
						.Append("public ")
						.TryAppendOverride(member.Symbol)
						.Append(((IMethodSymbol)member.Symbol).ReturnType)
						.Append(' ')
						.Append(member.Symbol.Name)
						.AppendGenericTypes(((IMethodSymbol)member.Symbol).TypeArguments)
						.Append("(")
						.AppendParameters(((IMethodSymbol)member.Symbol).Parameters)
						.Append(") {");

					foreach (var parameter in ((IMethodSymbol)member.Symbol).Parameters)
					{
						if (parameter.RefKind != RefKind.Out)
							continue;

						stringBuilder
							.Append(parameter.Name)
							.Append(" = default;");
					}

					// TODO: appropriate check
					if (!((IMethodSymbol)member.Symbol).ReturnsVoid)
					{
						if (((IMethodSymbol)member.Symbol).ReturnType is INamedTypeSymbol { Name: "Task" or "ValueTask" } returnType)
						{
							stringBuilder
								.Append("return ")
								.Append(returnType.ContainingNamespace)
								.Append('.')
								.Append(returnType.Name);

							if (returnType.TypeArguments.IsDefaultOrEmpty)
								stringBuilder.Append(".CompletedTask;");
							else
								stringBuilder.Append(".FromResult").AppendGenericTypes(returnType.TypeArguments).Append("(default);");
						}
						else
						{
							stringBuilder.Append("return default;");
						}
					}

					stringBuilder
						.AppendLine("}");
					break;
			}
		}

		// TODO: extract
		foreach (var member in typeSymbol.GetIrrelevantOverridableMembers().FilterMockableSymbols())
		{
			switch (member.Kind)
			{
				case SymbolKind.Event:
					stringBuilder.Indent(indent)
						.Append(member.DeclaredAccessibility.GetString()).Append(' ')
						.TryAppendOverride(member)
						.Append("event ")
						.Append(((IEventSymbol)member).Type)
						.Append(' ')
						.Append(member.Name)
						.AppendLine(";");
					break;
				case SymbolKind.Property:
					stringBuilder
						.Indent(indent)
						.Append("public ")
						.TryAppendOverride(member)
						.Append(((IPropertySymbol)member).Type)
						.Append(' ')
						.Append(member.Name)
						.Append(" { get; ");

					if (((IPropertySymbol)member).SetMethod is not null)
					{
						var name = ((IPropertySymbol)member).SetMethod!.IsInitOnly ? "init; " : "set; ";
						stringBuilder.Append(name);
					}

					stringBuilder
						.AppendLine("}");
					break;
				case SymbolKind.Method:
					stringBuilder
						.Indent(indent)
						.Append(member.DeclaredAccessibility.GetString())
						.Append(" ")
						.TryAppendOverride(member)
						.Append(((IMethodSymbol)member).ReturnType)
						.Append(' ')
						.Append(member.Name)
						.AppendGenericTypes(((IMethodSymbol)member).TypeArguments)
						.Append("(")
						.AppendParameters(((IMethodSymbol)member).Parameters)
						.Append(") {");

					foreach (var parameter in ((IMethodSymbol)member).Parameters)
					{
						if (parameter.RefKind != RefKind.Out)
							continue;

						stringBuilder
							.Append(parameter.Name)
							.Append(" = default;");
					}

					// TODO: appropriate check
					if (!((IMethodSymbol)member).ReturnsVoid && ((IMethodSymbol)member).ReturnType is INamedTypeSymbol returnType)
					{
						if (returnType.Name is "Task" or "ValueTask")
						{
							stringBuilder
								.Append("return ")
								.Append(returnType.ContainingNamespace)
								.Append('.')
								.Append(returnType.Name);

							if (returnType.TypeArguments.IsDefaultOrEmpty)
								stringBuilder.Append(".CompletedTask;");
							else
								stringBuilder.Append(".FromResult").AppendGenericTypes(returnType.TypeArguments).Append("(default);");
						}
						else
						{
							stringBuilder.Append("return default;");
						}
					}

					stringBuilder
						.AppendLine("}");
					break;
			}
		}

		return stringBuilder.ToString();
	}

	private static string CreateMockExtensions(StringBuilder stringBuilder, IReadOnlyList<MockedMemberSymbol> members, int indent)
	{
		stringBuilder.Clear();
		return stringBuilder.ToString();
	}

	private static string CreateMockSequenceExtensions(StringBuilder stringBuilder, IReadOnlyList<MockedMemberSymbol> members, int indent)
	{
		stringBuilder.Clear();
		return stringBuilder.ToString();
	}
}
