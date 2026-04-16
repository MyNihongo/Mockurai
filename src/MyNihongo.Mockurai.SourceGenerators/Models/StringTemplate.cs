namespace MyNihongo.Mockurai.Models;

internal readonly struct StringTemplate(string template, object[]? parameters) : IEquatable<StringTemplate>
{
	private readonly string _template = template;
	private readonly object[]? _parameters = parameters;

	public bool HasParameters => _parameters?.Length > 0;

	public string Build()
	{
		return _parameters is not null
			? string.Format(_template, _parameters)
			: _template;
	}

	public string Build(Func<object, object> parameterMapping)
	{
		if (_parameters is null)
			return _template;

		var args = _parameters.Select(parameterMapping).ToArray();
		return string.Format(_template, args);
	}

	public static implicit operator StringTemplate(string template)
	{
		return new StringTemplate(template, parameters: null);
	}

	public bool Equals(StringTemplate other) =>
		_template == other._template;

	public override bool Equals(object? obj) =>
		obj is StringTemplate other && Equals(other);

	public override int GetHashCode() =>
		_template.GetHashCode();
}
