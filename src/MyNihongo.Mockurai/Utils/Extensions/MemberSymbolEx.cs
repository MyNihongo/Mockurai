namespace MyNihongo.Mockurai.Utils;

internal static class MemberSymbolEx
{
	extension(StringBuilder @this)
	{
		public void AppendNameComment(MockedMemberSymbol member, int indent)
		{
			@this
				.Indent(indent)
				.Append("// ")
				.AppendLine(member.Symbol.Name);
		}
	}
}
