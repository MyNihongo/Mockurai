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
			  {{CreateGetInvocations(stringBuilder, mockableMembers, indent: 2)}}
			  	}

			  	private sealed class Proxy : {{typeString}}
			  	{
			  		private readonly {{mockClassName}} _mock;

			  		public Proxy({{mockClassName}} mock)
			  		{
			  			_mock = mock;
			  		}

			  {{CreateProxyMethods(stringBuilder, mockedTypeSymbol, mockableMembers, indent: 2)}}
			  	}
			  }

			  public static partial class MockExtensions
			  {
			  	extension{{genericTypes}}(IMock<{{typeString}}> @this)
			  	{
			  		public void VerifyNoOtherCalls() =>
			  			(({{mockClassName}})@this).VerifyNoOtherCalls();

			  {{CreateMockExtensions(stringBuilder, mockableMembers, mockClassName, indent: 2)}}
			  	}
			  }

			  public static partial class MockSequenceExtensions
			  {
			  	extension{{genericTypes}}(IMockSequence<{{typeString}}> @this)
			  	{
			  {{CreateMockSequenceExtensions(stringBuilder, mockableMembers, mockClassName, indent: 2)}}
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

	private static string CreateGetInvocations(StringBuilder stringBuilder, IReadOnlyList<MockedMemberSymbol> members, int indent)
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

	private static string CreateProxyMethods(StringBuilder stringBuilder, MockedTypeSymbol typeSymbol, IReadOnlyList<MockedMemberSymbol> members, int indent)
	{
		stringBuilder.Clear();

		var generatedCount = 0;
		foreach (var member in members)
		{
			Action<StringBuilder, MockedTypeSymbol, MockedMemberSymbol, int>? handler = member.Symbol.Kind switch
			{
				SymbolKind.Event => MockImplementationEventGenerator.AppendProxyEventImplementation,
				SymbolKind.Property => MockImplementationPropertyGenerator.AppendProxyPropertyImplementation,
				SymbolKind.Method => MockImplementationMethodGenerator.AppendProxyMethodImplementation,
				_ => null,
			};

			if (handler is null)
				continue;

			if (generatedCount > 0)
			{
				stringBuilder
					.AppendLine()
					.AppendLine();
			}

			handler(stringBuilder, typeSymbol, member, indent);
			generatedCount++;
		}

		foreach (var member in typeSymbol.TypeSymbol.GetIrrelevantOverridableMembers().FilterMockableSymbols())
		{
			Action<StringBuilder, ISymbol, int>? handler = member.Kind switch
			{
				SymbolKind.Event => MockImplementationEventGenerator.AppendProxyEventDummyImplementation,
				SymbolKind.Property => MockImplementationPropertyGenerator.AppendProxyPropertyDummyImplementation,
				SymbolKind.Method => MockImplementationMethodGenerator.AppendProxyMethodDummyImplementation,
				_ => null,
			};

			if (handler is null)
				continue;

			if (generatedCount > 0)
			{
				stringBuilder
					.AppendLine()
					.AppendLine();
			}

			handler(stringBuilder, member, indent);
			generatedCount++;
		}

		return stringBuilder.ToString();
	}

	private static string CreateMockExtensions(StringBuilder stringBuilder, IReadOnlyList<MockedMemberSymbol> members, string mockClassName, int indent)
	{
		stringBuilder.Clear();

		var generatedCount = 0;
		foreach (var member in members)
		{
			Action<StringBuilder, MockedMemberSymbol, string, int>? handler = member.Symbol.Kind switch
			{
				SymbolKind.Event => MockImplementationEventGenerator.AppendEventMockExtensions,
				SymbolKind.Property => MockImplementationPropertyGenerator.AppendPropertyMockExtensions,
				SymbolKind.Method => MockImplementationMethodGenerator.AppendMethodMockExtensions,
				_ => null,
			};

			if (handler is null)
				continue;

			if (generatedCount > 0)
			{
				stringBuilder
					.AppendLine()
					.AppendLine();
			}

			stringBuilder.AppendNameComment(member, indent);
			handler(stringBuilder, member, mockClassName, indent);
			generatedCount++;
		}

		return stringBuilder.ToString();
	}

	private static string CreateMockSequenceExtensions(StringBuilder stringBuilder, IReadOnlyList<MockedMemberSymbol> members, string mockClassName, int indent)
	{
		stringBuilder.Clear();

		var generatedCount = 0;
		foreach (var member in members)
		{
			Action<StringBuilder, MockedMemberSymbol, string, int>? handler = member.Symbol.Kind switch
			{
				SymbolKind.Event => MockImplementationEventGenerator.AppendEventMockSequenceExtensions,
				_ => null,
			};

			if (handler is null)
				continue;

			if (generatedCount > 0)
			{
				stringBuilder
					.AppendLine()
					.AppendLine();
			}

			stringBuilder.AppendNameComment(member, indent);
			handler(stringBuilder, member, mockClassName, indent);
			generatedCount++;
		}

		return stringBuilder.ToString();
	}
}
