using MockClassName = (string Constructor, string Type, string GenericTypes);

namespace MyNihongo.Mock.Utils;

internal static class MockImplementationGenerator
{
	public static MockImplementationResult GenerateMockImplementation(this ITypeSymbol typeSymbol, CompilationCombinedResult result)
	{
		var stringBuilder = new StringBuilder();
		var typeString = typeSymbol.ToString();
		var (constructor, mockClassName, genericTypes) = CreateMockClassName(stringBuilder, typeSymbol);
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

			  {{CreateMockMethods(stringBuilder, typeSymbol, mockableMembers, indent: 1)}}

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
			source: source
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

	private static IReadOnlyList<MemberSymbol> GetMockableMembers(ITypeSymbol typeSymbol)
	{
		var symbols = typeSymbol.TypeKind switch
		{
			TypeKind.Class => typeSymbol.GetOverridableMembers(),
			_ => typeSymbol.GetMembers(),
		};

		return symbols
			.FilterMockableSymbols()
			.ToLookup(static x => x.Name)
			.SelectMany(static x => x.Select((y, i) => new MemberSymbol($"{x.Key}{i}", y)))
			.ToArray();
	}

	private static string CreateMockMethods(StringBuilder stringBuilder, ITypeSymbol typeSymbol, IReadOnlyList<MemberSymbol> members, int indent)
	{
		stringBuilder.Clear();

		for (int i = 0, generateCount = 0; i < members.Count; i++)
		{
			var member = members[i];
			Action<StringBuilder, ITypeSymbol, MemberSymbol, int>? handler = member.Symbol.Kind switch
			{
				SymbolKind.Event => MockImplementationEventGenerator.AppendEventMockMethod,
				SymbolKind.Property => MockImplementationPropertyGenerator.AppendPropertyMockMethod,
				SymbolKind.Method => MockImplementationMethodGenerator.AppendPropertyMockMethod,
				_ => null,
			};

			if (handler is null)
				continue;

			if (generateCount > 0)
				stringBuilder
					.AppendLine()
					.AppendLine();

			handler(stringBuilder, typeSymbol, member, indent);
			generateCount++;
		}

		return stringBuilder.ToString();
	}

	private static string CreateVerifyNoOtherCalls(StringBuilder stringBuilder, IReadOnlyList<MemberSymbol> members, int indent)
	{
		stringBuilder.Clear();
		return stringBuilder.ToString();
	}

	private static string CreateGetInvocations(StringBuilder stringBuilder, IReadOnlyList<MemberSymbol> members, int indent)
	{
		stringBuilder.Clear();

		if (members.Count == 0)
		{
			stringBuilder
				.Indent(indent)
				.Append("yield break;");

			return stringBuilder.ToString();
		}

		// TODO: implement
		return stringBuilder
			.Indent(indent)
			.Append("yield break;")
			.ToString();
	}

	private static string CreateProxyMethods(StringBuilder stringBuilder, ITypeSymbol typeSymbol, IReadOnlyList<MemberSymbol> members, int indent)
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
						.Append("(")
						.AppendParameters(((IMethodSymbol)member.Symbol).Parameters)
						.AppendLine(") {}");
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
						.Append("public ")
						.TryAppendOverride(member)
						.Append(((IMethodSymbol)member).ReturnType)
						.Append(' ')
						.Append(member.Name)
						.Append("(")
						.AppendParameters(((IMethodSymbol)member).Parameters)
						.AppendLine(") {}");
					break;
			}
		}

		return stringBuilder.ToString();
	}

	private static string CreateMockExtensions(StringBuilder stringBuilder, IReadOnlyList<MemberSymbol> members, int indent)
	{
		stringBuilder.Clear();
		return stringBuilder.ToString();
	}

	private static string CreateMockSequenceExtensions(StringBuilder stringBuilder, IReadOnlyList<MemberSymbol> members, int indent)
	{
		stringBuilder.Clear();
		return stringBuilder.ToString();
	}
}
