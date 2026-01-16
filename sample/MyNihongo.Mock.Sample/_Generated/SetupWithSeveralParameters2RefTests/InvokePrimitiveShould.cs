namespace MyNihongo.Mock.Sample._Generated.SetupWithSeveralParameters2RefTests;

public sealed class InvokePrimitiveShould : SetupWithSeveralParametersTestsBase
{
	[Fact]
	public void ThrowForAnySetup()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 12345678;
		var inputValue2 = 987654321;
		var actual = () => fixture.Invoke(inputValue1, ref inputValue2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowForValueSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const int inputValue1 = 12345678;
		var inputValue2 = 987654321;
		It<int> setup1 = inputValue1, setup2 = inputValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var actual = () => fixture.Invoke(inputValue1, ref inputValue2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void NotThrowForValueSetup1()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue1 = 12345678;
		var setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 747474;
		fixture.Invoke(inputValue1, ref setupValue2);
	}

	[Fact]
	public void NotThrowForValueSetup2()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue2 = 987654321;
		var setupValue1 = 12345678;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var inputValue2 = 747474;
		fixture.Invoke(setupValue1, ref inputValue2);
	}

	[Fact]
	public void NotThrowForValueSetup1And2()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 3243253;
		var inputValue2 = 747474;
		fixture.Invoke(inputValue1, ref inputValue2);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ThrowForWhereSetup1(int inputValue1)
	{
		const string errorMessage = nameof(errorMessage);
		var setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var actual = () => fixture.Invoke(inputValue1, ref setupValue2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void NotThrowForWhereSetup1(int inputValue1)
	{
		const string errorMessage = nameof(errorMessage);
		var setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		fixture.Invoke(inputValue1, ref setupValue2);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ThrowForWhereSetup2(int inputValue2)
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var actual = () => fixture.Invoke(setupValue1, ref inputValue2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void NotThrowForWhereSetup2(int inputValue2)
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		fixture.Invoke(setupValue1, ref inputValue2);
	}

	[Fact]
	public void ThrowForWhereSetups()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 101;
		var inputValue2 = 9;
		var actual = () => fixture.Invoke(inputValue1, ref inputValue2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void NotThrowForWhereSetups1()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 99;
		var inputValue2 = 9;
		fixture.Invoke(inputValue1, ref inputValue2);
	}

	[Fact]
	public void NotThrowForWhereSetups2()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 101;
		var inputValue2 = 11;
		fixture.Invoke(inputValue1, ref inputValue2);
	}

	[Fact]
	public void NotThrowForWhereSetups1And2()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 99;
		var inputValue2 = 11;
		fixture.Invoke(inputValue1, ref inputValue2);
	}

	[Fact]
	public void PrioritiseValueOverAnyThrow2()
	{
		const int setupValue2 = 2;
		const int setupValue1 = 1;

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		var inputValue2 = 324343242;
		var actual = () => fixture.Invoke(setupValue1, ref inputValue2);

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverAnyThrow3()
	{
		const int setupValue1 = 1;
		var setupValue2 = 2;

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		const int inputValue1 = 324343242;
		var actual = () => fixture.Invoke(inputValue1, ref setupValue2);

		var exception = Assert.Throws<InvalidCastException>(actual);
		Assert.Equal(errorMessage3, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverAnyThrow4()
	{
		const int setupValue1 = 1;
		var setupValue2 = 2;

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		var actual = () => fixture.Invoke(setupValue1, ref setupValue2);

		var exception = Assert.Throws<OverflowException>(actual);
		Assert.Equal(errorMessage4, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverAnyThrow1()
	{
		const int setupValue1 = 1, setupValue2 = 2;

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		const int inputValue1 = 324343242;
		var inputValue2 = 837483252;
		var actual = () => fixture.Invoke(inputValue1, ref inputValue2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow9()
	{
		const int setupValue1 = 1;
		var setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		var actual = () => fixture.Invoke(setupValue1, ref setupValue2);

		var exception = Assert.Throws<InvalidExpressionException>(actual);
		Assert.Equal(errorMessage9, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow8()
	{
		const int setupValue1 = 1;
		var setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = 25;
		var actual = () => fixture.Invoke(inputValue1, ref setupValue2);

		var exception = Assert.Throws<ArithmeticException>(actual);
		Assert.Equal(errorMessage8, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow7()
	{
		const int setupValue1 = 1;
		var setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = -345332543;
		var actual = () => fixture.Invoke(inputValue1, ref setupValue2);

		var exception = Assert.Throws<AggregateException>(actual);
		Assert.Equal(errorMessage7, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow6()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		var inputValue2 = 21;
		var actual = () => fixture.Invoke(setupValue1, ref inputValue2);

		var exception = Assert.Throws<FileLoadException>(actual);
		Assert.Equal(errorMessage6, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow5()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		var inputValue2 = -21;
		var actual = () => fixture.Invoke(setupValue1, ref inputValue2);

		var exception = Assert.Throws<MissingSatelliteAssemblyException>(actual);
		Assert.Equal(errorMessage5, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow4()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = 14;
		var inputValue2 = 21;
		var actual = () => fixture.Invoke(inputValue1, ref inputValue2);

		var exception = Assert.Throws<OverflowException>(actual);
		Assert.Equal(errorMessage4, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow3()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = -14;
		var inputValue2 = 21;
		var actual = () => fixture.Invoke(inputValue1, ref inputValue2);

		var exception = Assert.Throws<InvalidCastException>(actual);
		Assert.Equal(errorMessage3, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow2()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = 14;
		var inputValue2 = -21;
		var actual = () => fixture.Invoke(inputValue1, ref inputValue2);

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow1()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = -14;
		var inputValue2 = -21;
		var actual = () => fixture.Invoke(inputValue1, ref inputValue2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public void InvokeCallbackForAny()
	{
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		int inputValue1 = 99, inputValue2 = 11;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected = 89;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void SetValueInCallbackForAny()
	{
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => y -= x);

		const int inputValue1 = 99;
		var inputValue2 = 11;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected = -88;
		Assert.Equal(expected, inputValue2);
	}

	[Fact]
	public void InvokeCallbackForAnyBeforeThrows()
	{
		const string expectedMessage = nameof(expectedMessage);
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);
		fixture.And();
		fixture.Throws(new Exception(expectedMessage));

		const int inputValue1 = 99;
		var inputValue2 = 11;
		var actual = () => fixture.Invoke(inputValue1, ref inputValue2);

		const int expected = 89;
		var exception = Assert.Throws<Exception>(actual);
		Assert.Equal(expectedMessage, exception.Message);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForValue()
	{
		const int setupValue1 = 12345678;
		var setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		fixture.Invoke(setupValue1, ref setupValue2);

		const int expected = -11111110;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void SetValueInCallbackForValue()
	{
		const int setupValue1 = 12345678;
		var setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => y -= x);

		fixture.Invoke(setupValue1, ref setupValue2);

		const int expected = 11111111;
		Assert.Equal(expected, setupValue2);
	}

	[Fact]
	public void InvokeCallbackForValueBeforeThrows()
	{
		const string expectedMessage = nameof(expectedMessage);
		const int setupValue1 = 12345678;
		var setupValue2 = 23456789;
		It<int> setup1 = setupValue1, setup2 = setupValue2;
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new Exception(expectedMessage));
		fixture.And();
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		var actual = () => fixture.Invoke(setupValue1, ref setupValue2);

		const int expected = -11111110;
		var exception = Assert.Throws<Exception>(actual);
		Assert.Equal(expectedMessage, exception.Message);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForWhere()
	{
		It<int> setup1 = It<int>.Where(x => x > 20), setup2 = It<int>.Where(x => x < 15);
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		const int inputValue1 = 99;
		var inputValue2 = 11;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected = 89;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void SetValueInCallbackForWhere()
	{
		It<int> setup1 = It<int>.Where(x => x > 20), setup2 = It<int>.Where(x => x < 15);

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => y -= x);

		const int inputValue1 = 99;
		var inputValue2 = 11;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected = -88;
		Assert.Equal(expected, inputValue2);
	}

	[Fact]
	public void InvokeCallbackForWhereBeforeThrows()
	{
		const string expectedMessage = nameof(expectedMessage);
		It<int> setup1 = It<int>.Where(x => x > 20), setup2 = It<int>.Where(x => x < 15);
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);
		fixture.And();
		fixture.Throws(new Exception(expectedMessage));

		const int inputValue1 = 99;
		var inputValue2 = 11;
		var actual = () => fixture.Invoke(inputValue1, ref inputValue2);

		const int expected = 89;
		var exception = Assert.Throws<Exception>(actual);
		Assert.Equal(expectedMessage, exception.Message);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotInvokeCallbackForValue1()
	{
		const int setupValue1 = 12345678;
		var setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		const int inputValue1 = -64713;
		fixture.Invoke(inputValue1, ref setupValue2);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForValue1()
	{
		const int setupValue1 = 12345678;
		var setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => y -= x);

		const int inputValue1 = -64713;
		fixture.Invoke(inputValue1, ref setupValue2);

		const int expected1 = -64713, expected2 = 23456789;
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, setupValue2);
	}

	[Fact]
	public void NotInvokeCallbackForValue2()
	{
		const int setupValue1 = 12345678, setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		var inputValue2 = -64713;
		fixture.Invoke(setupValue1, ref inputValue2);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForValue2()
	{
		const int setupValue1 = 12345678, setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => y -= x);

		var inputValue2 = -64713;
		fixture.Invoke(setupValue1, ref inputValue2);

		const int expected1 = 12345678, expected2 = -64713;
		Assert.Equal(expected1, setupValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForValue3()
	{
		const int setupValue1 = 12345678, setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		const int inputValue1 = -64713;
		var inputValue2 = -28257;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForValue3()
	{
		const int setupValue1 = 12345678, setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => y -= x);

		const int inputValue1 = -64713;
		var inputValue2 = -28257;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected1 = -64713, expected2 = -28257;
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForValue4()
	{
		const int setupValue2 = 23456789;
		var setupValue1 = 12345678;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		fixture.Invoke(setupValue2, ref setupValue1);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForValue4()
	{
		const int setupValue2 = 23456789;
		var setupValue1 = 12345678;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => y -= x);

		fixture.Invoke(setupValue2, ref setupValue1);

		const int expected1 = 23456789, expected2 = 12345678;
		Assert.Equal(expected1, setupValue2);
		Assert.Equal(expected2, setupValue1);
	}

	[Fact]
	public void NotInvokeCallbackForValueAny1()
	{
		const int setupValue1 = 12345678;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		const int inputValue1 = -2341414;
		var inputValue2 = 1234;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForValueAny1()
	{
		const int setupValue1 = 12345678;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = It<int>.Any();

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => y -= x);

		const int inputValue1 = -2341414;
		var inputValue2 = 1234;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected1 = -2341414, expected2 = 1234;
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForValueAny2()
	{
		const int setupValue2 = 12345678;
		It<int> setup1 = It<int>.Any(), setup2 = setupValue2;
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		const int inputValue1 = -2341414;
		var inputValue2 = 1234;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForValueAny2()
	{
		const int setupValue2 = 12345678;
		It<int> setup1 = It<int>.Any(), setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => y -= x);

		const int inputValue1 = -2341414;
		var inputValue2 = 1234;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected1 = -2341414, expected2 = 1234;
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForWhere1()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Where(x => x < 20);
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		const int inputValue1 = 10;
		var inputValue2 = 9;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForWhere1()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Where(x => x < 20);

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => y -= x);

		const int inputValue1 = 10;
		var inputValue2 = 9;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected1 = 10, expected2 = 9;
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForWhere2()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Where(x => x < 20);
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		const int inputValue1 = 11;
		var inputValue2 = 20;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForWhere2()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Where(x => x < 20);

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => y -= x);

		const int inputValue1 = 11;
		var inputValue2 = 20;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected1 = 11, expected2 = 20;
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForWhere3()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Where(x => x < 20);
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		const int inputValue1 = 10;
		var inputValue2 = 20;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForWhere3()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Where(x => x < 20);

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => y -= x);

		const int inputValue1 = 10;
		var inputValue2 = 20;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected1 = 10, expected2 = 20;
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForWhereAny1()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		const int inputValue1 = 10;
		var inputValue2 = 20;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForWhereAny1()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Any();

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => y -= x);

		const int inputValue1 = 10;
		var inputValue2 = 20;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected1 = 10, expected2 = 20;
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForWhereAny2()
	{
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Where(x => x < 20);
		var callbackValue = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => callbackValue = x - y + 1);

		const int inputValue1 = 10;
		var inputValue2 = 20;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForWhereAny2()
	{
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Where(x => x < 20);

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((x, ref y) => y -= x);

		const int inputValue1 = 10;
		var inputValue2 = 20;
		fixture.Invoke(inputValue1, ref inputValue2);

		const int expected1 = 10, expected2 = 20;
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotDuplicateSameSetup()
	{
		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((_, ref _) => { });
		fixture.Throws(new Exception());
		fixture.Callback((_, ref _) => { Debug.WriteLine("output"); });
		fixture.Throws(new Exception());

		const int expected = 1;
		var actual = GetSetupCount(fixture);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void InsertAllSetups()
	{
		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((_, ref _) => { });

		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Throws(new Exception());

		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((_, ref _) => { Debug.WriteLine("output"); });

		const int expected = 3;
		var actual = GetSetupCount(fixture);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowLastExceptionAny()
	{
		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		const int inputValue1 = 12345678;
		var inputValue2 = 23456;
		var actual = () => fixture.Invoke(inputValue1, ref inputValue2);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowLastExceptionAnyWhere()
	{
		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Where(x => x > 10).ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Where(x => x > 10).ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		const int inputValue1 = 12345678;
		var inputValue2 = 23456;
		var actual = () => fixture.Invoke(inputValue1, ref inputValue2);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowLastExceptionAnyValue()
	{
		var setupValue2 = 123;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		const int inputValue1 = 12345678;
		var actual = () => fixture.Invoke(inputValue1, ref setupValue2);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowLastExceptionWhere()
	{
		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(It<int>.Where(x => x > 100).ValueSetup, It<int>.Where(x => x < 20).ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameters(It<int>.Where(x => x > 100).ValueSetup, It<int>.Where(x => x < 20).ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		const int inputValue1 = 123;
		var inputValue2 = 15;
		var actual = () => fixture.Invoke(inputValue1, ref inputValue2);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowLastExceptionWhereValue()
	{
		var setupValue2 = 123;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(It<int>.Where(x => x > 100).ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameters(It<int>.Where(x => x > 100).ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		const int inputValue1 = 123;
		var actual = () => fixture.Invoke(inputValue1, ref setupValue2);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowLastExceptionValue()
	{
		const int setupValue1 = 11;
		var setupValue2 = 123;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(It<int>.Value(setupValue1).ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameters(It<int>.Value(setupValue1).ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		var actual = () => fixture.Invoke(setupValue1, ref setupValue2);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		const int parameter1 = 123;
		var parameter2 = 321;
		var actual1 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((_, ref _) => callback++);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		const int parameter1 = 123;
		var parameter2 = 321;
		fixture.Invoke(parameter1, ref parameter2);

		var actual2 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		const int expectedCallback = 1;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((_, ref _) => callback++);
		fixture.And();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		const int parameter1 = 123;
		var parameter2 = 321;
		var actual1 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback = 1;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Callback((_, ref _) => callback++);
		fixture.And();
		fixture.Throws(new NullReferenceException(errorMessage2));

		const int parameter1 = 123;
		var parameter2 = 321;
		var actual1 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback = 2;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		int callback1 = 10, callback2 = 0;

		var fixture = CreateFixture<SetupIntRefInt>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((_, ref _) => callback1++);
		fixture.And();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));
		fixture.And();
		fixture.Callback((_, ref _) => callback2++);

		const int parameter1 = 123;
		var parameter2 = 321;
		var actual1 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(parameter1, ref parameter2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback1 = 11, expectedCallback2 = 2;
		Assert.Equal(expectedCallback1, callback1);
		Assert.Equal(expectedCallback2, callback2);
	}
}
