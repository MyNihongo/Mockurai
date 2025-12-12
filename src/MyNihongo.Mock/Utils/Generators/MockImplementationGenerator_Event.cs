using System.Text;

namespace MyNihongo.Mock.Utils;

internal static class MockImplementationEventGenerator
{
	public static void AppendEventMockMethod(StringBuilder stringBuilder, ITypeSymbol mockedTypeSymbol, MemberSymbol member, int indent)
	{
		if (member.Symbol is not IEventSymbol eventSymbol)
			return;

		const string addType = "add", removeType = "remove";

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

		// VerifyAdd method - times
		stringBuilder
			.AppendLine()
			.Indent(indent)
			.Append("public void VerifyAdd")
			.AppendPropertyName(member.Symbol.Name)
			.Append("(in ")
			.Append(eventTypeString)
			.AppendLine(" handler, in Times times)");

		stringBuilder
			.Indent(indent++).AppendLine("{")
			.Indent(indent).AppendInvocationDeclaration(mockedTypeSymbol, member, type: addType, eventTypeString)
			.Indent(indent)
			.AppendFieldName(member.MemberName)
			.AppendLine("AddInvocation.Verify(handler, times, _invocationProviders);")
			.Indent(--indent).AppendLine("}");

		// VerifyAdd method - index
		stringBuilder
			.AppendLine()
			.Indent(indent)
			.Append("public long VerifyAdd")
			.AppendPropertyName(member.Symbol.Name)
			.Append("(in ")
			.Append(eventTypeString)
			.AppendLine(" handler, long index)");

		stringBuilder
			.Indent(indent++).AppendLine("{")
			.Indent(indent).AppendInvocationDeclaration(mockedTypeSymbol, member, type: addType, eventTypeString)
			.Indent(indent)
			.Append("return ")
			.AppendFieldName(member.MemberName)
			.AppendLine("AddInvocation.Verify(handler, index, _invocationProviders);")
			.Indent(--indent).AppendLine("}");
	}

	private static StringBuilder AppendInvocationDeclaration(this StringBuilder stringBuilder, ITypeSymbol mockedTypeSymbol, MemberSymbol member, string type, string eventTypeString)
	{
		return stringBuilder
			.AppendFieldName(member.MemberName)
			.AppendPropertyName(type)
			.Append("Invocation ??= new Invocation<")
			.Append(eventTypeString)
			.Append(">(\"")
			.Append(mockedTypeSymbol.Name)
			.Append('.')
			.AppendPropertyName(member.Symbol.Name)
			.Append('.')
			.Append(type)
			.AppendLine("\");");
	}
}
