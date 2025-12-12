using System.Text;

namespace MyNihongo.Mock.Utils;

internal static class MockImplementationEventGenerator
{
	public static void AppendEventMockMethod(StringBuilder stringBuilder, ITypeSymbol mockedTypeSymbol, MemberSymbol member, int indent)
	{
		if (member.Symbol is not IEventSymbol eventSymbol)
			return;

		var eventTypeString = eventSymbol.Type.ToString();
		var parameterSymbol = eventSymbol.GetDelegateParameter();

		stringBuilder
			.Indent(indent)
			.Append("// ")
			.AppendLine(member.Symbol.Name);

		// Fields
		stringBuilder
			.Indent(indent)
			.Append("private ")
			.Append(eventTypeString)
			.Append(' ')
			.AppendFieldName(member.MemberName)
			.AppendLine(";");

		stringBuilder
			.Indent(indent)
			.Append("private Invocation<")
			.Append(eventTypeString)
			.Append(">? ")
			.AppendFieldName(member.MemberName)
			.AppendLine("AddInvocation;");

		stringBuilder
			.Indent(indent)
			.Append("private Invocation<")
			.Append(eventTypeString)
			.Append(">? ")
			.AppendFieldName(member.MemberName)
			.AppendLine("RemoveInvocation;");

		// Raise method
		stringBuilder
			.AppendLine()
			.Indent(indent)
			.Append("public void Raise")
			.AppendPropertyName(member.Symbol.Name)
			.Append('(')
			.AppendParameter(parameterSymbol)
			.AppendLine(")");

		stringBuilder
			.Indent(indent++).AppendLine("{")
			.Indent(indent)
			.AppendFieldName(member.MemberName)
			.Append("?.Invoke(Object, ")
			.Append(parameterSymbol?.Name)
			.AppendLine(");");

		stringBuilder
			.Indent(--indent).AppendLine("}");

		stringBuilder
			.AppendVerifyMethods(mockedTypeSymbol, member, type: "add", eventTypeString, indent)
			.AppendLine()
			.AppendVerifyMethods(mockedTypeSymbol, member, type: "remove", eventTypeString, indent);
	}

	extension(StringBuilder stringBuilder)
	{
		private StringBuilder AppendVerifyMethods(ITypeSymbol mockedTypeSymbol, MemberSymbol member, string type, string eventTypeString, int indent)
		{
			// Verify method - times
			stringBuilder
				.AppendLine()
				.Indent(indent)
				.Append("public void Verify")
				.AppendPropertyName(type)
				.AppendPropertyName(member.Symbol.Name)
				.Append("(in ")
				.Append(eventTypeString)
				.AppendLine(" handler, in Times times)");

			stringBuilder
				.Indent(indent++).AppendLine("{")
				.Indent(indent).AppendInvocationDeclaration(mockedTypeSymbol, member, type, eventTypeString)
				.Indent(indent)
				.AppendFieldName(member.MemberName)
				.AppendPropertyName(type)
				.AppendLine("Invocation.Verify(handler, times, _invocationProviders);")
				.Indent(--indent).AppendLine("}");

			// Verify method - index
			stringBuilder
				.AppendLine()
				.Indent(indent)
				.Append("public long Verify")
				.AppendPropertyName(type)
				.AppendPropertyName(member.Symbol.Name)
				.Append("(in ")
				.Append(eventTypeString)
				.AppendLine(" handler, long index)");

			return stringBuilder
				.Indent(indent++).AppendLine("{")
				.Indent(indent).AppendInvocationDeclaration(mockedTypeSymbol, member, type, eventTypeString)
				.Indent(indent)
				.Append("return ")
				.AppendFieldName(member.MemberName)
				.AppendPropertyName(type)
				.AppendLine("Invocation.Verify(handler, index, _invocationProviders);")
				.Indent(--indent).Append('}');
		}

		private StringBuilder AppendInvocationDeclaration(ITypeSymbol mockedTypeSymbol, MemberSymbol member, string type, string eventTypeString)
		{
			return stringBuilder
				.AppendFieldName(member.MemberName)
				.AppendPropertyName(type)
				.Append("Invocation ??= new Invocation<")
				.Append(eventTypeString)
				.Append(">(\"")
				.Append(mockedTypeSymbol.Name)
				.AppendGenericTypes(mockedTypeSymbol)
				.Append('.')
				.AppendPropertyName(member.Symbol.Name)
				.Append('.')
				.Append(type)
				.AppendLine("\");");
		}
	}
}
