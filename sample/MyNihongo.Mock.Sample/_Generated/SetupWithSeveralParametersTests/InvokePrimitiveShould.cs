using System.Data;
using System.Resources;

namespace MyNihongo.Mock.Sample._Generated.SetupWithSeveralParametersTests;

public sealed class InvokePrimitiveShould : SetupWithSeveralParametersTestsBase
{
	[Fact]
	public void ThrowForAnySetup()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 12345678, inputValue2 = 987654321;
		var actual = () => fixture.Invoke(inputValue1, inputValue2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowForValueSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const int inputValue1 = 12345678, inputValue2 = 987654321;
		It<int> setup1 = inputValue1, setup2 = inputValue2;

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var actual = () => fixture.Invoke(inputValue1, inputValue2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void NotThrowForValueSetup1()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 747474;
		fixture.Invoke(inputValue1, setupValue2);
	}

	[Fact]
	public void NotThrowForValueSetup2()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue2 = 747474;
		fixture.Invoke(setupValue1, inputValue2);
	}

	[Fact]
	public void NotThrowForValueSetup1And2()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 3243253, inputValue2 = 747474;
		fixture.Invoke(inputValue1, inputValue2);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ThrowForWhereSetup1(int inputValue1)
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var actual = () => fixture.Invoke(inputValue1, setupValue2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void NotThrowForWhereSetup1(int inputValue1)
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		fixture.Invoke(inputValue1, setupValue2);
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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var actual = () => fixture.Invoke(setupValue1, inputValue2);

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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		fixture.Invoke(setupValue1, inputValue2);
	}

	[Fact]
	public void ThrowForWhereSetups()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 101, inputValue2 = 9;
		var actual = () => fixture.Invoke(inputValue1, inputValue2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void NotThrowForWhereSetups1()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 99, inputValue2 = 9;
		fixture.Invoke(inputValue1, inputValue2);
	}

	[Fact]
	public void NotThrowForWhereSetups2()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 101, inputValue2 = 11;
		fixture.Invoke(inputValue1, inputValue2);
	}

	[Fact]
	public void NotThrowForWhereSetups1And2()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 99, inputValue2 = 11;
		fixture.Invoke(inputValue1, inputValue2);
	}

	[Fact]
	public void PrioritiseValueOverAnyThrow2()
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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		const int inputValue2 = 324343242;
		var actual = () => fixture.Invoke(setupValue1, inputValue2);

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverAnyThrow3()
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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		const int inputValue1 = 324343242;
		var actual = () => fixture.Invoke(inputValue1, setupValue2);

		var exception = Assert.Throws<InvalidCastException>(actual);
		Assert.Equal(errorMessage3, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverAnyThrow4()
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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		var actual = () => fixture.Invoke(setupValue1, setupValue2);

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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		const int inputValue1 = 324343242, inputValue2 = 837483252;
		var actual = () => fixture.Invoke(inputValue1, inputValue2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow9()
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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		var actual = () => fixture.Invoke(setupValue1, setupValue2);

		var exception = Assert.Throws<InvalidExpressionException>(actual);
		Assert.Equal(errorMessage9, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow8()
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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = 25;
		var actual = () => fixture.Invoke(inputValue1, setupValue2);

		var exception = Assert.Throws<ArithmeticException>(actual);
		Assert.Equal(errorMessage8, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow7()
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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = -345332543;
		var actual = () => fixture.Invoke(inputValue1, setupValue2);

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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue2 = 21;
		var actual = () => fixture.Invoke(setupValue1, inputValue2);

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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue2 = -21;
		var actual = () => fixture.Invoke(setupValue1, inputValue2);

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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = 14, inputValue2 = 21;
		var actual = () => fixture.Invoke(inputValue1, inputValue2);

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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = -14, inputValue2 = 21;
		var actual = () => fixture.Invoke(inputValue1, inputValue2);

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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = 14, inputValue2 = -21;
		var actual = () => fixture.Invoke(inputValue1, inputValue2);

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

		var fixture = CreateFixture<SetupIntInt>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = -14, inputValue2 = -21;
		var actual = () => fixture.Invoke(inputValue1, inputValue2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}
}
