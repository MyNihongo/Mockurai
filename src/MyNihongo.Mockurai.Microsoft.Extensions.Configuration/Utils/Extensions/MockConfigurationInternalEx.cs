namespace MyNihongo.Mockurai;

internal static class MockConfigurationInternalEx
{
	public static void SetupDefaultSection<T>(this IMock<T> mock)
		where T : class, IConfiguration
	{
		mock
			.SetupGetSection()
			.Returns(static key =>
			{
				var sectionMock = Mock.Create<IConfigurationSection>();

				sectionMock
					.SetupGetKey()
					.Returns(key);

				sectionMock
					.SetupGetPath()
					.Returns(key);

				sectionMock
					.SetupGetChildren()
					.Returns([]);

				return sectionMock.Object;
			});
	}
}
