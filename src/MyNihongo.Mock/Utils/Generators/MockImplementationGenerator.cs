using System.Text;

namespace MyNihongo.Mock.Utils;

internal static class MockImplementationGenerator
{
	public static string GenerateMockImplementation(this ITypeSymbol typeSymbol, CompilationCombinedResult result)
	{
		var stringBuilder = new StringBuilder();
		var typeString = typeSymbol.ToString();
		var mockClassName = CreateMockClassName(stringBuilder, typeSymbol);
		var mockableMembers = GetMockableMembers(typeSymbol);

		return
			$$"""
			  namespace {{result.Options.RootNamespace}};

			  public sealed class {{mockClassName}} : IMock<{{typeString}}>
			  {
			  	private readonly InvocationIndex.Counter _invocationIndex;
			  	private readonly Func<IEnumerable<IInvocationProvider?>> _invocationProviders;
			  	private Proxy? _proxy;

			  	public {{mockClassName}}(InvocationIndex.Counter invocationIndex)
			  	{
			  		_invocationIndex = invocationIndex;
			  		_invocationProviders = GetInvocations;
			  	}

			  	public IPrimitiveDependencyService Object => _proxy ??= new Proxy(this);

			  {{CreateMockMethods(stringBuilder, mockableMembers, indent: 1)}}

			  	public void VerifyNoOtherCalls()
			  	{
			  	{{CreateVerifyNoOtherCalls(stringBuilder, mockableMembers, indent: 2)}}
			  	}

			  	private IEnumerable<IInvocationProvider?> GetInvocations()
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

			  {{CreateProxyMethods(stringBuilder, mockableMembers, indent: 2)}}
			  	}
			  }

			  public static partial class MockExtensions
			  {
			  	extension(IMock<{{typeString}}> @this)
			  	{
			  		public void VerifyNoOtherCalls() =>
			  			(({{mockClassName}})@this).VerifyNoOtherCalls();

			  		{{CreateMockExtensions(stringBuilder, mockableMembers, indent: 2)}}
			  	}
			  }

			  public static partial class MockSequenceExtensions
			  {
			  	extension(IMockSequence<{{typeString}}> @this)
			  	{
			  	{{CreateMockSequenceExtensions(stringBuilder, mockableMembers, indent: 2)}}
			  	}
			  }
			  """;
	}

	private static string CreateMockClassName(StringBuilder stringBuilder, ITypeSymbol typeSymbol)
	{
		stringBuilder.Clear();
		stringBuilder.AppendMockClassName(typeSymbol);

		return stringBuilder.ToString();
	}

	private static IReadOnlyList<MemberSymbol> GetMockableMembers(ITypeSymbol typeSymbol)
	{
		var symbols = typeSymbol.TypeKind switch
		{
			TypeKind.Class => typeSymbol.GetOverridableMembers(),
			_ => typeSymbol.GetMembers(),
		};

		return symbols
			.ToLookup(static x => x.Name)
			.SelectMany(static x => x.Select((y, i) => new MemberSymbol($"{x.Key}{i}", y)))
			.ToArray();
	}

	private static string CreateMockMethods(StringBuilder stringBuilder, IReadOnlyList<MemberSymbol> members, int indent)
	{
		stringBuilder.Clear();

		for (int i = 0, generateCount = 0; i < members.Count; i++)
		{
			var member = members[i];
			Action<StringBuilder, MemberSymbol, int>? handler = member.Symbol.Kind switch
			{
				SymbolKind.Event => MockImplementationEventGenerator.AppendEventMockMethod,
				_ => null,
			};

			if (handler is null)
				continue;

			if (generateCount > 0)
				stringBuilder.AppendLine();

			handler(stringBuilder, member, indent);
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

		return stringBuilder.ToString();
	}

	private static string CreateProxyMethods(StringBuilder stringBuilder, IReadOnlyList<MemberSymbol> members, int indent)
	{
		stringBuilder.Clear();
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
