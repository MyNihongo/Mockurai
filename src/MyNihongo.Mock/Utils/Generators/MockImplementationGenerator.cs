using System.Text;

namespace MyNihongo.Mock.Utils;

internal static class MockImplementationGenerator
{
	public static string GenerateMockImplementation(this ITypeSymbol typeSymbol, CompilationCombinedResult result)
	{
		var stringBuilder = new StringBuilder();
		var typeString = typeSymbol.ToString();
		var mockClassName = CreateMockClassName(stringBuilder, typeSymbol);

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

			  {{CreateMockMethods(stringBuilder, typeSymbol, indent: 1)}}

			  	public void VerifyNoOtherCalls()
			  	{
			  	}

			  	private IEnumerable<IInvocationProvider?> GetInvocations()
			  	{
			  	}

			  	private sealed class Proxy : {{typeString}}
			  	{
			  		private readonly {{mockClassName}} _mock;

			  		public Proxy({{mockClassName}} mock)
			  		{
			  			_mock = mock;
			  		}

			  {{CreateProxyMethods(stringBuilder, typeSymbol, indent: 2)}}
			  	}
			  }

			  public static partial class MockExtensions
			  {
			  	extension(IMock<{{typeString}}> @this)
			  	{
			  		public void VerifyNoOtherCalls() =>
			  			(({{mockClassName}})@this).VerifyNoOtherCalls();

			  		{{CreateMockExtensions(stringBuilder, typeSymbol, indent: 2)}}
			  	}
			  }

			  public static partial class MockSequenceExtensions
			  {
			  	extension(IMockSequence<{{typeString}}> @this)
			  	{
			  	{{CreateMockSequenceExtensions(stringBuilder, typeSymbol, indent: 2)}}
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

	private static string CreateMockMethods(StringBuilder stringBuilder, ITypeSymbol typeSymbol, int indent)
	{
		stringBuilder.Clear();
		return stringBuilder.ToString();
	}

	private static string CreateProxyMethods(StringBuilder stringBuilder, ITypeSymbol typeSymbol, int indent)
	{
		stringBuilder.Clear();
		return stringBuilder.ToString();
	}

	private static string CreateMockExtensions(StringBuilder stringBuilder, ITypeSymbol typeSymbol, int indent)
	{
		stringBuilder.Clear();
		return stringBuilder.ToString();
	}

	private static string CreateMockSequenceExtensions(StringBuilder stringBuilder, ITypeSymbol typeSymbol, int indent)
	{
		stringBuilder.Clear();
		return stringBuilder.ToString();
	}
}
