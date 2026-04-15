namespace MyNihongo.Mockurai.Models;

internal readonly struct StringTemplate(string template, object[]? parameters) : IEquatable<StringTemplate>
{
	private readonly string _template = template;
	private readonly object[]? _parameters = parameters;

	public static implicit operator StringTemplate(string template)
	{
		return new StringTemplate(template, parameters: null);
	}

	public string Build()
	{
		return _parameters is not null
			? string.Format(_template, _parameters)
			: _template;
	}

	public bool Equals(StringTemplate other) =>
		_template == other._template;

	public override bool Equals(object? obj) =>
		obj is StringTemplate other && Equals(other);

	public override int GetHashCode() =>
		_template.GetHashCode();
}
