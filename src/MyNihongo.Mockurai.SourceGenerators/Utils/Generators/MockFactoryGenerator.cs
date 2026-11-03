using Microsoft.CodeAnalysis.CSharp;

namespace MyNihongo.Mockurai.Utils;

internal static class MockFactoryGenerator
{
	public static string GenerateMockFactoryInterceptors(this Dictionary<ITypeSymbol, List<InterceptableLocation>> invocations, CompilationCombinedResult result)
	{
		var stringBuilder = new StringBuilder()
			.AppendLine("#nullable enable")
			.AppendLine("namespace System.Runtime.CompilerServices")
			.AppendLine("{")
			.Indent(1).AppendLine("[System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = true)]")
			.Indent(1).AppendLine("file sealed class InterceptsLocationAttribute : System.Attribute")
			.Indent(1).AppendLine("{")
			.Indent(2).AppendLine("public InterceptsLocationAttribute(int version, string data)")
			.Indent(2).AppendLine("{")
			.Indent(2).AppendLine("}")
			.Indent(1).AppendLine("}")
			.AppendLine("}")
			.AppendLine()
			// Interceptors must be declared in a namespace listed in InterceptorsNamespaces, therefore the namespace is constant
			.Append("namespace ").AppendLine(MockGeneratorConst.Namespace)
			.AppendLine("{")
			.Indent(1).AppendLine("file static class MockFactoryInterceptors")
			.Indent(1).AppendLine("{");

		var index = 0;
		foreach (var invocation in invocations)
		{
			if (index > 0)
				stringBuilder.AppendLine();

			foreach (var location in invocation.Value)
				stringBuilder.Indent(2).AppendLine(location.GetInterceptsLocationAttributeSyntax());

			stringBuilder
				.Indent(2)
				.Append("public static IMock<").Append(invocation.Key).Append("> ")
				.Append(MockGeneratorConst.FactoryMethodName).Append(index++).AppendLine("() =>")
				.Indent(3)
				.Append("new global::").Append(result.Options.RootNamespace).Append('.')
				.AppendMockClassName(invocation.Key)
				.AppendLine("(InvocationIndex.CounterValue);");
		}

		return stringBuilder
			.Indent(1).AppendLine("}")
			.Append('}')
			.ToString();
	}
}
