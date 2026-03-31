namespace MyNihongo.Mock.Sample._Generated.SetupReturnsWithSeveralParameters1Ref2RefTests;

public sealed class ExecutePrimitiveShould : SetupReturnsTestsBase
{
	[Fact]
	public void ReturnForAnySetup()
	{
		const string returnValue = nameof(returnValue);
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		int inputValue1 = 12345678, inputValue2 = 987654321;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ReturnForValueSetup()
	{
		const string returnValue = nameof(returnValue);
		int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(ref setupValue1, ref setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ReturnNullForValueSetup1()
	{
		const string returnValue = nameof(returnValue);
		const int setupValue1 = 12345678;
		var setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		var inputValue1 = 84837621;
		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void ReturnDefaultForValueSetup1()
	{
		const int returnValue = -731473432;
		const int setupValue1 = 12345678;
		var setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		var inputValue1 = 84837621;
		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullForValueSetup2()
	{
		const string returnValue = nameof(returnValue);
		var setupValue1 = 12345678;
		const int setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		var inputValue2 = 84837621;
		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void ReturnDefaultForValueSetup2()
	{
		const int returnValue = -731473432;
		var setupValue1 = 12345678;
		const int setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		var inputValue2 = 84837621;
		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullForValueSetup1And2()
	{
		const string returnValue = nameof(returnValue);
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		int inputValue1 = -771245, inputValue2 = 84837621;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void ReturnDefaultForValueSetup1And2()
	{
		const int returnValue = -731473432;
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		int inputValue1 = -771245, inputValue2 = 84837621;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ReturnForWhereSetup1(int inputValue1)
	{
		const string returnValue = nameof(returnValue);
		var setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnNullForWhereSetup1(int inputValue2)
	{
		const string returnValue = nameof(returnValue);
		var setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(ref inputValue2, ref setupValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnDefaultForWhereSetup1(int inputValue1)
	{
		const int returnValue = 12345;
		var setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ReturnForWhereSetup2(int inputValue2)
	{
		const string returnValue = nameof(returnValue);
		var setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnNullForWhereSetup2(int inputValue2)
	{
		const string returnValue = nameof(returnValue);
		var setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnDefaultForWhereSetup2(int inputValue2)
	{
		const int returnValue = 12345;
		var setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnForWhereSetups()
	{
		const string returnValue = nameof(returnValue);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		int inputValue1 = 101, inputValue2 = 9;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ReturnNullForWhereSetups1()
	{
		const string returnValue = nameof(returnValue);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		int inputValue1 = 99, inputValue2 = 9;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void ReturnDefaultForWhereSetups1()
	{
		const int returnValue = 374452;
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		int inputValue1 = 99, inputValue2 = 9;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullForWhereSetups2()
	{
		const string returnValue = nameof(returnValue);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		int inputValue1 = 101, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void ReturnDefaultForWhereSetups2()
	{
		const int returnValue = 374452;
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		int inputValue1 = 101, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullForWhereSetups1And2()
	{
		const string returnValue = nameof(returnValue);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		int inputValue1 = 99, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void ReturnDefaultForWhereSetups1And2()
	{
		const int returnValue = 374452;
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns(returnValue);

		int inputValue1 = 99, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowForAnySetup()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		int inputValue1 = 12345678, inputValue2 = 987654321;
		Action actual = () => fixture.Execute(ref inputValue1, ref inputValue2, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowForValueSetup()
	{
		const string errorMessage = nameof(errorMessage);
		int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => fixture.Execute(ref setupValue1, ref setupValue2, out _);

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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var inputValue1 = 84837621;
		fixture.Execute(ref inputValue1, ref setupValue2, out _);
	}

	[Fact]
	public void NotThrowForValueSetup2()
	{
		const string errorMessage = nameof(errorMessage);
		var setupValue1 = 12345678;
		const int setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var inputValue2 = 84837621;
		fixture.Execute(ref setupValue1, ref inputValue2, out _);
	}

	[Fact]
	public void NotThrowForValueSetupFor1And2()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		int inputValue1 = -771245, inputValue2 = 84837621;
		fixture.Execute(ref inputValue1, ref inputValue2, out _);
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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => fixture.Execute(ref inputValue1, ref setupValue2, out _);

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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		fixture.Execute(ref inputValue1, ref setupValue2, out _);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ThrowForWhereSetup2(int inputValue2)
	{
		const string errorMessage = nameof(errorMessage);
		var setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => fixture.Execute(ref setupValue1, ref inputValue2, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void NotThrowForWhereSetup2(int inputValue2)
	{
		const string errorMessage = nameof(errorMessage);
		var setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		fixture.Execute(ref setupValue1, ref inputValue2, out _);
	}

	[Fact]
	public void ThrowForWhereSetups()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		int inputValue1 = 101, inputValue2 = 9;
		Action actual = () => fixture.Execute(ref inputValue1, ref inputValue2, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowForWhereSetups1()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		int inputValue1 = 99, inputValue2 = 9;
		fixture.Execute(ref inputValue1, ref inputValue2, out _);
	}

	[Fact]
	public void ThrowForWhereSetups2()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		int inputValue1 = 101, inputValue2 = 11;
		fixture.Execute(ref inputValue1, ref inputValue2, out _);
	}

	[Fact]
	public void ThrowForWhereSetups1And2()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		int inputValue1 = 99, inputValue2 = 11;
		fixture.Execute(ref inputValue1, ref inputValue2, out _);
	}

	[Fact]
	public void PrioritiseValueOverAnyReturn4()
	{
		int setupValue1 = 1, setupValue2 = 2;

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Returns(returnValue4);

		var hasValue = fixture.Execute(ref setupValue1, ref setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue4, actual);
	}

	[Fact]
	public void PrioritiseValueOverAnyReturn3()
	{
		const int setupValue1 = 1;
		var setupValue2 = 2;

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Returns(returnValue4);

		var inputValue1 = 324343242;
		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue3, actual);
	}

	[Fact]
	public void PrioritiseValueOverAnyReturn2()
	{
		var setupValue1 = 1;
		const int setupValue2 = 2;

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Returns(returnValue4);

		var inputValue2 = 324343242;
		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue2, actual);
	}

	[Fact]
	public void PrioritiseValueOverAnyReturn1()
	{
		const int setupValue1 = 1, setupValue2 = 2;

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Returns(returnValue4);

		int inputValue1 = 324343242, inputValue2 = 837483252;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue1, actual);
	}

	[Fact]
	public void PrioritiseValueOverAnyThrow4()
	{
		int setupValue1 = 1, setupValue2 = 2;

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		Action actual = () => fixture.Execute(ref setupValue1, ref setupValue2, out _);

		var exception = Assert.Throws<OverflowException>(actual);
		Assert.Equal(errorMessage4, exception.Message);
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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		var inputValue1 = 324343242;
		Action actual = () => fixture.Execute(ref inputValue1, ref setupValue2, out _);

		var exception = Assert.Throws<InvalidCastException>(actual);
		Assert.Equal(errorMessage3, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverAnyThrow2()
	{
		var setupValue1 = 1;
		const int setupValue2 = 2;

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		var inputValue2 = 324343242;
		Action actual = () => fixture.Execute(ref setupValue1, ref inputValue2, out _);

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Throws(new OverflowException(errorMessage4));

		int inputValue1 = 324343242, inputValue2 = 837483252;
		Action actual = () => fixture.Execute(ref inputValue1, ref inputValue2, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns9()
	{
		int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Returns(returnValue9);

		var hasValue = fixture.Execute(ref setupValue1, ref setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue9, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns8()
	{
		const int setupValue1 = 1;
		var setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Returns(returnValue9);

		var inputValue1 = 25;
		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue8, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns7()
	{
		const int setupValue1 = 1;
		var setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Returns(returnValue9);

		var inputValue1 = -345332543;
		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue7, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns6()
	{
		var setupValue1 = 1;
		const int setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Returns(returnValue9);

		var inputValue2 = 21;
		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue6, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns5()
	{
		var setupValue1 = 1;
		const int setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Returns(returnValue9);

		var inputValue2 = -21;
		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue5, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns4()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Returns(returnValue9);

		int inputValue1 = 14, inputValue2 = 21;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue4, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns3()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Returns(returnValue9);

		int inputValue1 = -14, inputValue2 = 21;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue3, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns2()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Returns(returnValue9);

		int inputValue1 = 14, inputValue2 = -21;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue2, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns1()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup11.ValueSetup, setup12.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21.ValueSetup, setup22.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31.ValueSetup, setup32.ValueSetup);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41.ValueSetup, setup42.ValueSetup);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51.ValueSetup, setup52.ValueSetup);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61.ValueSetup, setup62.ValueSetup);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71.ValueSetup, setup72.ValueSetup);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81.ValueSetup, setup82.ValueSetup);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91.ValueSetup, setup92.ValueSetup);
		fixture.Returns(returnValue9);

		int inputValue1 = -14, inputValue2 = -21;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue1, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow9()
	{
		int setupValue1 = 1, setupValue2 = 2;
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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
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

		Action actual = () => fixture.Execute(ref setupValue1, ref setupValue2, out _);

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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
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

		var inputValue1 = 25;
		Action actual = () => fixture.Execute(ref inputValue1, ref setupValue2, out _);

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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
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

		var inputValue1 = -345332543;
		Action actual = () => fixture.Execute(ref inputValue1, ref setupValue2, out _);

		var exception = Assert.Throws<AggregateException>(actual);
		Assert.Equal(errorMessage7, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow6()
	{
		var setupValue1 = 1;
		const int setupValue2 = 2;
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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
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
		Action actual = () => fixture.Execute(ref setupValue1, ref inputValue2, out _);

		var exception = Assert.Throws<FileLoadException>(actual);
		Assert.Equal(errorMessage6, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow5()
	{
		var setupValue1 = 1;
		const int setupValue2 = 2;
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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
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
		Action actual = () => fixture.Execute(ref setupValue1, ref inputValue2, out _);

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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
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

		int inputValue1 = 14, inputValue2 = 21;
		Action actual = () => fixture.Execute(ref inputValue1, ref inputValue2, out _);

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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
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

		int inputValue1 = -14, inputValue2 = 21;
		Action actual = () => fixture.Execute(ref inputValue1, ref inputValue2, out _);

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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
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

		int inputValue1 = 14, inputValue2 = -21;
		Action actual = () => fixture.Execute(ref inputValue1, ref inputValue2, out _);

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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
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

		int inputValue1 = -14, inputValue2 = -21;
		Action actual = () => fixture.Execute(ref inputValue1, ref inputValue2, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public void ReturnForAnySetupFunc()
	{
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => (x + y - 1).ToString());

		int inputValue1 = 12345678, inputValue2 = 987654321;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const string returnValue = "999999998";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void SetValueInReturnForAnySetupFunc()
	{
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => ((x -= y) - (y -= x)).ToString());

		int inputValue1 = 12345678, inputValue2 = 987654321;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected1 = -975308643, expected2 = 1962962964;
		const string returnValue = "1356695689";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void ReturnForValueSetupFunc()
	{
		int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => (x + y - 1).ToString());

		var hasValue = fixture.Execute(ref setupValue1, ref setupValue2, out var actual);

		const string returnValue = "999999998";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void SetValueInReturnForValueSetupFunc()
	{
		int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => ((x -= y) - (y -= x)).ToString());

		var hasValue = fixture.Execute(ref setupValue1, ref setupValue2, out var actual);

		const int expected1 = -975308643, expected2 = 1962962964;
		const string returnValue = "1356695689";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
		Assert.Equal(expected1, setupValue1);
		Assert.Equal(expected2, setupValue2);
	}

	[Fact]
	public void ReturnNullForValueSetupFunc()
	{
		const int setupValue1 = 12345678;
		var setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => (x + y - 1).ToString());

		var inputValue1 = 84837621;
		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void NotSetValueInReturnForValueSetupFunc()
	{
		const int setupValue1 = 12345678;
		var setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => ((x -= y) - (y -= x)).ToString());

		var inputValue1 = 84837621;
		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		const int expected1 = 84837621, expected2 = 987654321;
		Assert.False(hasValue);
		Assert.Null(actual);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, setupValue2);
	}

	[Fact]
	public void ReturnDefaultForValueSetupFunc1()
	{
		const int setupValue1 = 12345678;
		var setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => x + y - 1);

		var inputValue1 = 84837621;
		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullForValueSetupFunc2()
	{
		var setupValue1 = 12345678;
		const int setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => (x + y - 1).ToString());

		var inputValue2 = 84837621;
		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void NotSetValueInReturnForValueSetupFunc2()
	{
		var setupValue1 = 12345678;
		const int setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => ((x -= y) - (y -= x)).ToString());

		var inputValue2 = 84837621;
		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		const int expected1 = 12345678, expected2 = 84837621;
		Assert.False(hasValue);
		Assert.Null(actual);
		Assert.Equal(expected1, setupValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void ReturnDefaultForValueSetupFunc2()
	{
		var setupValue1 = 12345678;
		const int setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => x + y - 1);

		var inputValue2 = 84837621;
		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullForValueSetupFunc1And2()
	{
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => (x + y - 1).ToString());

		int inputValue1 = -771245, inputValue2 = 84837621;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void NotSetValueInReturnForValueSetupFunc1And2()
	{
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => ((x -= y) - (y -= x)).ToString());

		int inputValue1 = -771245, inputValue2 = 84837621;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected1 = -771245, expected2 = 84837621;
		Assert.False(hasValue);
		Assert.Null(actual);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void ReturnDefaultForValueSetupFunc1And2()
	{
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => x + y - 1);

		int inputValue1 = -771245, inputValue2 = 84837621;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ReturnForWhereSetupFunc1(int inputValue1)
	{
		var setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => (x + y - 1).ToString());

		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		var returnValue = $"{inputValue1 + setupValue2 - 1}";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void SetValueInReturnForWhereSetupFunc1(int inputValue1)
	{
		var setupValue2 = 987654321;
		int expected1 = inputValue1 - setupValue2, expected2 = setupValue2 - expected1;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => ((x -= y) - (y -= x)).ToString());

		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		var returnValue = $"{inputValue1 - setupValue2}";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, setupValue2);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnNullForWhereSetupFunc1(int inputValue1)
	{
		var setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => (x + y - 1).ToString());

		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void NotSetValueInReturnForWhereSetupFunc1(int inputValue1)
	{
		var setupValue2 = 987654321;
		int expected1 = inputValue1, expected2 = setupValue2;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => ((x -= y) - (y -= x)).ToString());

		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, setupValue2);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnDefaultForWhereSetupFunc1(int inputValue1)
	{
		var setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => x + y - 1);

		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ReturnForWhereSetupFunc2(int inputValue2)
	{
		var setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => (x + y - 1).ToString());

		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		var returnValue = $"{setupValue1 + inputValue2 - 1}";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void SetValueInReturnForWhereSetupFunc2(int inputValue2)
	{
		var setupValue1 = 987654321;
		int expected1 = setupValue1 - inputValue2, expected2 = inputValue2 - expected1;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => ((x -= y) - (y -= x)).ToString());

		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		var returnValue = $"{setupValue1 - inputValue2}";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
		Assert.Equal(expected1, setupValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnNullForWhereSetupFunc2(int inputValue2)
	{
		var setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => (x + y - 1).ToString());

		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void NotSetValueInReturnForWhereSetupFunc2(int inputValue2)
	{
		var setupValue1 = 987654321;
		int expected1 = setupValue1, expected2 = inputValue2;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => ((x -= y) - (y -= x)).ToString());

		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
		Assert.Equal(expected1, setupValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnDefaultForWhereSetupFunc2(int inputValue2)
	{
		var setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => x + y - 1);

		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnForWhereSetupsFunc()
	{
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => (x + y - 1).ToString());

		int inputValue1 = 101, inputValue2 = 9;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const string returnValue = "109";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void SetValueInReturnForWhereSetupsFunc()
	{
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => ((x -= y) - (y -= x)).ToString());

		int inputValue1 = 101, inputValue2 = 9;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected1 = 92, expected2 = -83;
		const string returnValue = "175";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void ReturnNullForWhereSetupsFunc1()
	{
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => (x + y - 1).ToString());

		int inputValue1 = 99, inputValue2 = 9;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void NotSetValueInReturnForWhereSetupsFunc1()
	{
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => ((x -= y) - (y -= x)).ToString());

		int inputValue1 = 99, inputValue2 = 9;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected1 = 99, expected2 = 9;
		Assert.False(hasValue);
		Assert.Null(actual);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void ReturnDefaultForWhereSetupsFunc1()
	{
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => x + y - 1);

		int inputValue1 = 99, inputValue2 = 9;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullForWhereSetupsFunc2()
	{
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => (x + y - 1).ToString());

		int inputValue1 = 101, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void NotSetValueInReturnForWhereSetupsFunc2()
	{
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => ((x -= y) - (y -= x)).ToString());

		int inputValue1 = 101, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected1 = 101, expected2 = 11;
		Assert.False(hasValue);
		Assert.Null(actual);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void ReturnDefaultForWhereSetupsFunc2()
	{
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => x + y - 1);

		int inputValue1 = 101, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullForWhereSetupsFunc1And2()
	{
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => (x + y - 1).ToString());

		int inputValue1 = 99, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void NotSetValueInReturnForWhereSetupsFunc1And2()
	{
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => ((x -= y) - (y -= x)).ToString());

		int inputValue1 = 99, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected1 = 99, expected2 = 11;
		Assert.False(hasValue);
		Assert.Null(actual);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void ReturnDefaultForWhereSetupsFunc1And2()
	{
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixtureInt<SetupRefInt32RefInt32<int>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Returns((ref x, ref y) => x + y - 1);

		int inputValue1 = 99, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void InvokeCallbackForAny()
	{
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		int inputValue1 = 99, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected = 89;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void SetValueInCallbackForAny()
	{
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x += 2;
			y += 3;
		});

		int inputValue1 = 99, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected1 = 101, expected2 = 14;
		Assert.False(hasValue);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void InvokeCallbackForAnyBeforeReturns()
	{
		const string returnValue = nameof(returnValue);
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);
		fixture.And();
		fixture.Returns(returnValue);

		int inputValue1 = 99, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected = 89;
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForAnyBeforeThrows()
	{
		const string expectedMessage = nameof(expectedMessage);
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Throws(new Exception(expectedMessage));
		fixture.And();
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		int inputValue1 = 99, inputValue2 = 11;
		Action actual = () => fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected = 89;
		var exception = Assert.Throws<Exception>(actual);
		Assert.Equal(expectedMessage, exception.Message);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForValue()
	{
		int setupValue1 = 12345678, setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		var hasValue = fixture.Execute(ref setupValue1, ref setupValue2, out _);

		const int expected = -11111110;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void SetValueInCallbackForValue()
	{
		int setupValue1 = 12345678, setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x += 2;
			y += 3;
		});

		var hasValue = fixture.Execute(ref setupValue1, ref setupValue2, out _);

		const int expected1 = 12345680, expected2 = 23456792;
		Assert.False(hasValue);
		Assert.Equal(expected1, setupValue1);
		Assert.Equal(expected2, setupValue2);
	}

	[Fact]
	public void NotSetValueInCallbackForValue()
	{
		int setupValue1 = 12345678, setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue2), setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x += 2;
			y += 3;
		});

		var hasValue = fixture.Execute(ref setupValue1, ref setupValue2, out _);

		const int expected1 = 12345678, expected2 = 23456789;
		Assert.False(hasValue);
		Assert.Equal(expected1, setupValue1);
		Assert.Equal(expected2, setupValue2);
	}

	[Fact]
	public void InvokeCallbackForValueBeforeReturns()
	{
		const string returnValue = nameof(returnValue);
		int setupValue1 = 12345678, setupValue2 = 23456789;
		It<int> setup1 = setupValue1, setup2 = setupValue2;
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);
		fixture.And();
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(ref setupValue1, ref setupValue2, out var actual);

		const int expected = -11111110;
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForValueBeforeThrows()
	{
		const string expectedMessage = nameof(expectedMessage);
		int setupValue1 = 12345678, setupValue2 = 23456789;
		It<int> setup1 = setupValue1, setup2 = setupValue2;
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);
		fixture.And();
		fixture.Throws(new Exception(expectedMessage));

		Action actual = () => fixture.Execute(ref setupValue1, ref setupValue2, out _);

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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		int inputValue1 = 99, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected = 89;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void SetValueInCallbackForWhere()
	{
		It<int> setup1 = It<int>.Where(x => x > 20), setup2 = It<int>.Where(x => x < 15);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x += 2;
			y += 3;
		});

		int inputValue1 = 99, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected1 = 101, expected2 = 14;
		Assert.False(hasValue);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotSetValueInCallbackForWhere()
	{
		It<int> setup1 = It<int>.Where(x => x > 20), setup2 = It<int>.Where(x => x < 15);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x += 2;
			y += 3;
		});

		int inputValue1 = 19, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected1 = 19, expected2 = 11;
		Assert.False(hasValue);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void InvokeCallbackForWhereBeforeReturns()
	{
		const string returnValue = nameof(returnValue);
		It<int> setup1 = It<int>.Where(x => x > 20), setup2 = It<int>.Where(x => x < 15);
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);
		fixture.And();
		fixture.Returns(returnValue);

		int inputValue1 = 99, inputValue2 = 11;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		const int expected = 89;
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForWhereBeforeThrows()
	{
		const string expectedMessage = nameof(expectedMessage);
		It<int> setup1 = It<int>.Where(x => x > 20), setup2 = It<int>.Where(x => x < 15);
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);
		fixture.And();
		fixture.Throws(new Exception(expectedMessage));

		int inputValue1 = 99, inputValue2 = 11;
		Action actual = () => fixture.Execute(ref inputValue1, ref inputValue2, out _);

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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		var inputValue1 = -64713;
		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForValue1()
	{
		const int setupValue1 = 12345678;
		var setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => x = +y);

		var inputValue1 = -64713;
		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out _);

		const int expected = -64713;
		Assert.False(hasValue);
		Assert.Equal(expected, inputValue1);
	}

	[Fact]
	public void NotInvokeCallbackForValue2()
	{
		var setupValue1 = 12345678;
		const int setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		var inputValue2 = -64713;
		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForValue2()
	{
		var setupValue1 = 12345678;
		const int setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x -= y;
			y -= x;
		});

		var inputValue2 = -64713;
		var hasValue = fixture.Execute(ref setupValue1, ref inputValue2, out _);

		const int expected1 = 12345678, expected2 = -64713;
		Assert.False(hasValue);
		Assert.Equal(expected1, setupValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForValue3()
	{
		const int setupValue1 = 12345678, setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		int inputValue1 = -64713, inputValue2 = -28257;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForValue3()
	{
		const int setupValue1 = 12345678, setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x -= y;
			y -= x;
		});

		int inputValue1 = -64713, inputValue2 = -28257;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected = -64713;
		Assert.False(hasValue);
		Assert.Equal(expected, inputValue1);
	}

	[Fact]
	public void NotInvokeCallbackForValue4()
	{
		int setupValue1 = 12345678, setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		var hasValue = fixture.Execute(ref setupValue2, ref setupValue1, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForValue4()
	{
		int setupValue1 = 12345678, setupValue2 = 23456789;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x -= y;
			y -= x;
		});

		var hasValue = fixture.Execute(ref setupValue2, ref setupValue1, out _);

		const int expected1 = 12345678, expected2 = 23456789;
		Assert.False(hasValue);
		Assert.Equal(expected1, setupValue1);
		Assert.Equal(expected2, setupValue2);
	}

	[Fact]
	public void NotInvokeCallbackForValueAny1()
	{
		const int setupValue1 = 12345678;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		int inputValue1 = -2341414, inputValue2 = 1234;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForValueAny1()
	{
		const int setupValue1 = 12345678;
		It<int> setup1 = It<int>.Value(setupValue1), setup2 = It<int>.Any();

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x -= y;
			y -= x;
		});

		int inputValue1 = -2341414, inputValue2 = 1234;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected1 = -2341414, expected2 = 1234;
		Assert.False(hasValue);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForValueAny2()
	{
		const int setupValue2 = 12345678;
		It<int> setup1 = It<int>.Any(), setup2 = setupValue2;
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		int inputValue1 = -2341414, inputValue2 = 1234;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForValueAny2()
	{
		const int setupValue2 = 12345678;
		It<int> setup1 = It<int>.Any(), setup2 = setupValue2;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x -= y;
			y -= x;
		});

		int inputValue1 = -2341414, inputValue2 = 1234;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected1 = -2341414, expected2 = 1234;
		Assert.False(hasValue);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForWhere1()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Where(x => x < 20);
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		int inputValue1 = 10, inputValue2 = 9;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForWhere1()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Where(x => x < 20);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x -= y;
			y -= x;
		});

		int inputValue1 = 10, inputValue2 = 9;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected1 = 10, expected2 = 9;
		Assert.False(hasValue);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForWhere2()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Where(x => x < 20);
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		int inputValue1 = 11, inputValue2 = 20;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForWhere2()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Where(x => x < 20);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x -= y;
			y -= x;
		});

		int inputValue1 = 11, inputValue2 = 20;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected1 = 11, expected2 = 20;
		Assert.False(hasValue);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForWhere3()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Where(x => x < 20);
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		int inputValue1 = 10, inputValue2 = 20;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForWhere3()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Where(x => x < 20);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x -= y;
			y -= x;
		});

		int inputValue1 = 10, inputValue2 = 20;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected1 = 10, expected2 = 20;
		Assert.False(hasValue);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForWhereAny1()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		int inputValue1 = 10, inputValue2 = 20;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForWhereAny1()
	{
		It<int> setup1 = It<int>.Where(x => x > 10), setup2 = It<int>.Any();

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x -= y;
			y -= x;
		});

		int inputValue1 = 10, inputValue2 = 20;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected1 = 10, expected2 = 20;
		Assert.False(hasValue);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotInvokeCallbackForWhereAny2()
	{
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Where(x => x < 20);
		var callbackValue = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) => callbackValue = x - y + 1);

		int inputValue1 = 10, inputValue2 = 20;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForWhereAny2()
	{
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Where(x => x < 20);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(setup1.ValueSetup, setup2.ValueSetup);
		fixture.Callback((ref x, ref y) =>
		{
			x -= y;
			y -= x;
		});

		int inputValue1 = 10, inputValue2 = 20;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out _);

		const int expected1 = 10, expected2 = 20;
		Assert.False(hasValue);
		Assert.Equal(expected1, inputValue1);
		Assert.Equal(expected2, inputValue2);
	}

	[Fact]
	public void NotDuplicateSameSetup()
	{
		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((ref _, ref _) => { });
		fixture.Throws(new Exception());
		fixture.Callback((ref _, ref _) => { Debug.WriteLine("output"); });
		fixture.Throws(new Exception());

		const int expected = 1;
		var actual = GetSetupCount(fixture);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void InsertAllSetups()
	{
		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((ref _, ref _) => { });

		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Throws(new Exception());

		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((ref _, ref _) => { Debug.WriteLine("output"); });

		const int expected = 3;
		var actual = GetSetupCount(fixture);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnLastResultAny()
	{
		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Returns("random text");

		const string returnValue = nameof(returnValue);
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Returns(returnValue);

		int inputValue1 = 12345678, inputValue2 = 23456;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ThrowLastExceptionAny()
	{
		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		int inputValue1 = 12345678, inputValue2 = 23456;
		Action actual = () => fixture.Execute(ref inputValue1, ref inputValue2, out _);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnLastResultAnyWhere()
	{
		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Where(x => x > 10).ValueSetup);
		fixture.Returns("random text");

		const string returnValue = nameof(returnValue);
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Where(x => x > 10).ValueSetup);
		fixture.Returns(returnValue);

		int inputValue1 = 12345678, inputValue2 = 23456;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ThrowLastExceptionAnyWhere()
	{
		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Where(x => x > 10).ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Where(x => x > 10).ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		int inputValue1 = 12345678, inputValue2 = 23456;
		Action actual = () => fixture.Execute(ref inputValue1, ref inputValue2, out _);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnLastResultAnyValue()
	{
		var setupValue2 = 123;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Returns("random text");

		const string returnValue = nameof(returnValue);
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Returns(returnValue);

		var inputValue1 = 12345678;
		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ThrowLastExceptionAnyValue()
	{
		var setupValue2 = 123;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		var inputValue1 = 12345678;
		Action actual = () => fixture.Execute(ref inputValue1, ref setupValue2, out _);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnLastResultWhere()
	{
		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Where(x => x > 100).ValueSetup, It<int>.Where(x => x < 20).ValueSetup);
		fixture.Returns("random text");

		const string returnValue = nameof(returnValue);
		fixture.SetupParameters(It<int>.Where(x => x > 100).ValueSetup, It<int>.Where(x => x < 20).ValueSetup);
		fixture.Returns(returnValue);

		int inputValue1 = 123, inputValue2 = 15;
		var hasValue = fixture.Execute(ref inputValue1, ref inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ThrowLastExceptionWhere()
	{
		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Where(x => x > 100).ValueSetup, It<int>.Where(x => x < 20).ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameters(It<int>.Where(x => x > 100).ValueSetup, It<int>.Where(x => x < 20).ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		int inputValue1 = 123, inputValue2 = 15;
		Action actual = () => fixture.Execute(ref inputValue1, ref inputValue2, out _);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnLastResultWhereValue()
	{
		var setupValue2 = 123;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Where(x => x > 100).ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Returns("random text");

		const string returnValue = nameof(returnValue);
		fixture.SetupParameters(It<int>.Where(x => x > 100).ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Returns(returnValue);

		var inputValue1 = 123;
		var hasValue = fixture.Execute(ref inputValue1, ref setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ThrowLastExceptionWhereValue()
	{
		var setupValue2 = 123;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Where(x => x > 100).ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameters(It<int>.Where(x => x > 100).ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		var inputValue1 = 123;
		Action actual = () => fixture.Execute(ref inputValue1, ref setupValue2, out _);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnLastResultValue()
	{
		int setupValue1 = 11, setupValue2 = 123;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Value(setupValue1).ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Returns("random text");

		const string returnValue = nameof(returnValue);
		fixture.SetupParameters(It<int>.Value(setupValue1).ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(ref setupValue1, ref setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ThrowLastExceptionValue()
	{
		int setupValue1 = 11, setupValue2 = 123;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Value(setupValue1).ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameters(It<int>.Value(setupValue1).ValueSetup, It<int>.Value(setupValue2).ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		Action actual = () => fixture.Execute(ref setupValue1, ref setupValue2, out _);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnDifferentValues()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Returns(setupValue1);
		fixture.Returns(setupValue2);

		int parameter1 = 123, parameter2 = 234;
		var actual1 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback1()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((ref _, ref _) => callback++);
		fixture.Returns(setupValue1);
		fixture.Returns(setupValue2);

		int parameter1 = 123, parameter2 = 234;
		var actual1 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue1);
		Assert.False(actual1);
		Assert.Null(returnValue1);

		var actual2 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue1, returnValue2);

		var actual3 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);

		var actual4 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue4);
		Assert.True(actual4);
		Assert.Equal(setupValue2, returnValue4);

		const int expectedCallback = 1;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback2()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((ref _, ref _) => callback++);
		fixture.And();
		fixture.Returns(setupValue1);
		fixture.Returns(setupValue2);

		int parameter1 = 123, parameter2 = 234;
		var actual1 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);

		const int expectedCallback = 1;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback3()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Returns(setupValue1);
		fixture.Callback((ref _, ref _) => callback++);
		fixture.And();
		fixture.Returns(setupValue2);

		int parameter1 = 123, parameter2 = 234;
		var actual1 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);

		const int expectedCallback = 2;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback4()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		int callback1 = 10, callback2 = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((ref _, ref _) => callback1++);
		fixture.And();
		fixture.Returns(setupValue1);
		fixture.Returns(setupValue2);
		fixture.And();
		fixture.Callback((ref _, ref _) => callback2++);

		int parameter1 = 123, parameter2 = 234;
		var actual1 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);

		const int expectedCallback1 = 11, expectedCallback2 = 2;
		Assert.Equal(expectedCallback1, callback1);
		Assert.Equal(expectedCallback2, callback2);
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		int parameter1 = 123, parameter2 = 234;
		Action actual1 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((ref _, ref _) => callback++);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		int parameter1 = 123, parameter2 = 234;
		fixture.Execute(ref parameter1, ref parameter2, out _);

		Action actual2 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		Action actual3 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Action actual4 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((ref _, ref _) => callback++);
		fixture.And();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		int parameter1 = 123, parameter2 = 234;
		Action actual1 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Callback((ref _, ref _) => callback++);
		fixture.And();
		fixture.Throws(new NullReferenceException(errorMessage2));

		int parameter1 = 123, parameter2 = 234;
		Action actual1 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
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

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Callback((ref _, ref _) => callback1++);
		fixture.And();
		fixture.Throws(new COMException(errorMessage1));
		fixture.And();
		fixture.Callback((ref _, ref _) => callback2++);
		fixture.Throws(new NullReferenceException(errorMessage2));

		int parameter1 = 123, parameter2 = 234;
		Action actual1 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback1 = 11, expectedCallback2 = 2;
		Assert.Equal(expectedCallback1, callback1);
		Assert.Equal(expectedCallback2, callback2);
	}

	[Fact]
	public void ReturnWithThrowException()
	{
		const string errorMessage = nameof(errorMessage);
		const string expected = nameof(expected);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Returns(expected);
		fixture.Throws(new IndexOutOfRangeException(errorMessage));

		int parameter1 = 123, parameter2 = 234;
		var actual1 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(expected, returnValue1);

		Action actual2 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
		var exception2 = Assert.Throws<IndexOutOfRangeException>(actual2);
		Assert.Equal(errorMessage, exception2.Message);

		Action actual3 = () => fixture.Execute(ref parameter1, ref parameter2, out _);
		var exception3 = Assert.Throws<IndexOutOfRangeException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);
	}

	[Fact]
	public void ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage);
		const string expected = nameof(expected);

		var fixture = CreateFixture<SetupRefInt32RefInt32<string>>();
		fixture.SetupParameters(It<int>.Any().ValueSetup, It<int>.Any().ValueSetup);
		fixture.Throws(new IndexOutOfRangeException(errorMessage));
		fixture.Returns(expected);

		var actual1 = () =>
		{
			int parameter1 = 123, parameter2 = 234;
			fixture.Execute(ref parameter1, ref parameter2, out _);
		};
		var exception1 = Assert.Throws<IndexOutOfRangeException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		int parameter1 = 123, parameter2 = 234;
		var actual2 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(expected, returnValue2);

		var actual3 = fixture.Execute(ref parameter1, ref parameter2, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(expected, returnValue3);
	}
}
