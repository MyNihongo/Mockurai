namespace MyNihongo.Mock;

public static class StringEx
{
	public static string FormatParameters(this string @this, in string parameters)
	{
		var bracketIndex = @this.IndexOf('(');

		return bracketIndex >= 0
			? @this[..bracketIndex] + '(' + parameters + ')'
			: string.Format(@this, parameters);
	}
}
