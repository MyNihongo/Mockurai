namespace MyNihongo.Mock.Sample;

public class UnitTest1
{
	[Fact]
	public void Test1()
	{
		new MockTest
		{
			Number = 123,
		};
	}
}
