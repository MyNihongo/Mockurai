using System.Dynamic;

namespace MyNihongo.Mockurai;

internal sealed class MockConfigurationSetup<T> : IConfigurationSetup
	where T : class, IConfiguration
{
	private readonly SectionInfoProvider _sectionInfoProvider = new();
	private readonly IMock<T> _mock;

	public MockConfigurationSetup(IMock<T> mock)
	{
		_mock = mock;
	}

	public void Returns(object? configurationObject)
	{
		// TODO: reset
		
		// TODO: generate if exported
		Mock.Create<IConfiguration>();

		if (configurationObject is null)
			return;

		var type = configurationObject.GetType();

		if (type.IsPrimitive() || configurationObject is IEnumerable and not ExpandoObject)
			throw new InvalidOperationException($"`{type.FullName}` must not be a primitive type or enumerable");

		_mock.SetupDefaultSection();
		
		var props = _sectionInfoProvider.Resolve(type, configurationObject);
		if (props.Count == 0)
			return;
	}
}
