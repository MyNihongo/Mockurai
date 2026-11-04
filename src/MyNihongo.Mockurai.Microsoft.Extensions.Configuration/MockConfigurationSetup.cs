using System.Dynamic;

namespace MyNihongo.Mockurai;

internal sealed class MockConfigurationSetup<T> : IConfigurationSetup
	where T : class, IConfiguration
{
	private static readonly SectionInfoProvider SectionInfoProvider = new();
	private readonly IMock<T> _mock;

	public MockConfigurationSetup(IMock<T> mock)
	{
		_mock = mock;
	}

	public void Returns(object? configurationObject)
	{
		// TODO: reset


		if (configurationObject is null)
			return;

		var type = configurationObject.GetType();

		if (type.IsPrimitive() || configurationObject is IEnumerable and not ExpandoObject)
			throw new InvalidOperationException($"`{type.FullName}` must not be a primitive type or enumerable");

		_mock.SetupDefaultSection();
		
		var props = SectionInfoProvider.Resolve(type, configurationObject);
		if (props.Count == 0)
			return;
	}
}
