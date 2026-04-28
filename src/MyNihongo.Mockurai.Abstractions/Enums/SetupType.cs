namespace MyNihongo.Mockurai;

/// <summary>
/// Identifies how a mock setup matches arguments, ordered so that more specific matchers take precedence over less specific ones.
/// </summary>
public enum SetupType
{
	/// <summary>
	/// Matches any argument value.
	/// </summary>
	Any = 0,

	/// <summary>
	/// Matches arguments that satisfy a user-supplied predicate.
	/// </summary>
	Where = 1,

	/// <summary>
	/// Matches arguments that are structurally equivalent to a reference value.
	/// </summary>
	Equivalent = 9,

	/// <summary>
	/// Matches arguments that are equal to a specific value.
	/// </summary>
	Value = 10,
}
