using System.Data;
using System.Resources;

namespace MyNihongo.Mock.Sample._Generated.SetupWithSeveralParametersTests;

public sealed class InvokePrimitiveShould : SetupWithSeveralParametersTestsBase
{
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

		const int input2 = 324343242;
		var actual = () => fixture.Invoke(setupValue1, input2);

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

		const int input1 = 324343242;
		var actual = () => fixture.Invoke(input1, setupValue2);

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

		const int input1 = 324343242, input2 = 837483252;
		var actual = () => fixture.Invoke(input1, input2);

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

		const int input1 = 25;
		var actual = () => fixture.Invoke(input1, setupValue2);

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

		const int input1 = -345332543;
		var actual = () => fixture.Invoke(input1, setupValue2);

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

		const int input2 = 21;
		var actual = () => fixture.Invoke(setupValue1, input2);

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

		const int input2 = -21;
		var actual = () => fixture.Invoke(setupValue1, input2);

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

		const int input1 = 14, input2 = 21;
		var actual = () => fixture.Invoke(input1, input2);

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

		const int input1 = -14, input2 = 21;
		var actual = () => fixture.Invoke(input1, input2);

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

		const int input1 = 14, input2 = -21;
		var actual = () => fixture.Invoke(input1, input2);

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

		const int input1 = -14, input2 = -21;
		var actual = () => fixture.Invoke(input1, input2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}
}
