namespace MyNihongo.Mockurai.Tests;

public abstract class TestsGenericBase : TestsBase
{
	protected static string CreateInterfaceTestCode(string members, string customCode)
	{
		return
			$$"""
			  using System.Threading.Tasks;

			  namespace MyNihongo.Example.Tests;

			  {{customCode}}

			  public interface IInterface<T>
			  {
			  	{{members}}
			  }

			  [MockuraiGenerate]
			  public abstract partial class TestsBase
			  {
			  	protected partial IMock<IInterface<string>> InterfaceMock { get; }
			  }
			  """;
	}

	protected static GeneratedSource GetInterfaceTestsBase()
	{
		const string testsBase =
			"""
			#nullable enable
			namespace MyNihongo.Example.Tests;

			public partial class TestsBase
			{
				// InterfaceMock
				private readonly InterfaceMock<string> _interfaceMock = new(InvocationIndex.CounterValue);
				protected partial IMock<MyNihongo.Example.Tests.IInterface<string>> InterfaceMock => _interfaceMock;

				protected void VerifyNoOtherCalls()
				{
					InterfaceMock.VerifyNoOtherCalls();
				}

				protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
				{
					var ctx = new VerifySequenceContext(
						interfaceMock: InterfaceMock
					);

					verify(ctx);
				}

				protected sealed class VerifySequenceContext
				{
					private readonly VerifyIndex _verifyIndex = new();
					public readonly IMockSequence<MyNihongo.Example.Tests.IInterface<string>> InterfaceMock;

					public VerifySequenceContext(IMock<MyNihongo.Example.Tests.IInterface<string>> interfaceMock)
					{
						InterfaceMock = new MockSequence<MyNihongo.Example.Tests.IInterface<string>>
						{
							VerifyIndex = _verifyIndex,
							Mock = interfaceMock,
						};
					}
				}
			}
			""";

		return ("TestsBase.g.cs", testsBase);
	}

	protected static GeneratedSource GetInterfaceMock(string methods, string proxy, string verifyNoOtherCalls, string invocations, string extensions, string sequenceExtensions)
	{
		var mock =
			$$"""
			  #nullable enable
			  namespace MyNihongo.Mockurai;

			  public sealed class InterfaceMock<T> : IMock<MyNihongo.Example.Tests.IInterface<T>>
			  {
			  	private readonly InvocationIndex.Counter _invocationIndex;
			  	private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
			  	private Proxy? _proxy;

			  	public InterfaceMock(InvocationIndex.Counter invocationIndex)
			  	{
			  		_invocationIndex = invocationIndex;
			  		_invocationProviders = GetInvocations;
			  	}

			  	public MyNihongo.Example.Tests.IInterface<T> Object => _proxy ??= new Proxy(this);

			  {{methods.Indent(1)}}

			  	public void VerifyNoOtherCalls()
			  	{
			  {{verifyNoOtherCalls.Indent(2)}}
			  	}

			  	private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
			  	{
			  {{invocations.Indent(2)}}
			  	}

			  	private sealed class Proxy : MyNihongo.Example.Tests.IInterface<T>
			  	{
			  		private readonly InterfaceMock<T> _mock;

			  		public Proxy(InterfaceMock<T> mock)
			  		{
			  			_mock = mock;
			  		}

			  {{proxy.Indent(2)}}
			  	}
			  }

			  public static partial class MockExtensions
			  {
			  	extension<T>(IMock<MyNihongo.Example.Tests.IInterface<T>> @this)
			  	{
			  		public void VerifyNoOtherCalls() =>
			  			((InterfaceMock<T>)@this).VerifyNoOtherCalls();

			  {{extensions.Indent(2)}}
			  	}
			  }

			  public static partial class MockSequenceExtensions
			  {
			  	extension<T>(IMockSequence<MyNihongo.Example.Tests.IInterface<T>> @this)
			  	{
			  {{sequenceExtensions.Indent(2)}}
			  	}
			  }
			  """;

		return ("InterfaceMock_T_.g.cs", mock);
	}

	protected static string CreateClassTestCode(string members, string customCode, bool isAbstract)
	{
		return
			$$"""
			  namespace MyNihongo.Example.Tests;

			  {{customCode}}

			  public {{(isAbstract ? "abstract " : "")}}class Class<T>
			  {
			  	{{members}}
			  }

			  [MockuraiGenerate]
			  public abstract partial class TestsBase
			  {
			  	protected partial IMock<Class<string>> ClassMock { get; }
			  }
			  """;
	}

	protected static GeneratedSource GetClassTestsBase()
	{
		const string testsBase =
			"""
			#nullable enable
			namespace MyNihongo.Example.Tests;

			public partial class TestsBase
			{
				// ClassMock
				private readonly ClassMock<string> _classMock = new(InvocationIndex.CounterValue);
				protected partial IMock<MyNihongo.Example.Tests.Class<string>> ClassMock => _classMock;

				protected void VerifyNoOtherCalls()
				{
					ClassMock.VerifyNoOtherCalls();
				}

				protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
				{
					var ctx = new VerifySequenceContext(
						classMock: ClassMock
					);

					verify(ctx);
				}

				protected sealed class VerifySequenceContext
				{
					private readonly VerifyIndex _verifyIndex = new();
					public readonly IMockSequence<MyNihongo.Example.Tests.Class<string>> ClassMock;

					public VerifySequenceContext(IMock<MyNihongo.Example.Tests.Class<string>> classMock)
					{
						ClassMock = new MockSequence<MyNihongo.Example.Tests.Class<string>>
						{
							VerifyIndex = _verifyIndex,
							Mock = classMock,
						};
					}
				}
			}
			""";

		return ("TestsBase.g.cs", testsBase);
	}

	protected static GeneratedSource GetClassMock(string methods, string proxy, string verifyNoOtherCalls, string invocations, string extensions, string sequenceExtensions)
	{
		var mock =
			$$"""
			  #nullable enable
			  namespace MyNihongo.Mockurai;

			  public sealed class ClassMock<T> : IMock<MyNihongo.Example.Tests.Class<T>>
			  {
			  	private readonly InvocationIndex.Counter _invocationIndex;
			  	private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
			  	private Proxy? _proxy;

			  	public ClassMock(InvocationIndex.Counter invocationIndex)
			  	{
			  		_invocationIndex = invocationIndex;
			  		_invocationProviders = GetInvocations;
			  	}

			  	public MyNihongo.Example.Tests.Class<T> Object => _proxy ??= new Proxy(this);

			  {{methods.Indent(1)}}

			  	public void VerifyNoOtherCalls()
			  	{
			  {{verifyNoOtherCalls.Indent(2)}}
			  	}

			  	private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
			  	{
			  {{invocations.Indent(2)}}
			  	}

			  	private sealed class Proxy : MyNihongo.Example.Tests.Class<T>
			  	{
			  		private readonly ClassMock<T> _mock;

			  		public Proxy(ClassMock<T> mock)
			  		{
			  			_mock = mock;
			  		}

			  {{proxy.Indent(2)}}
			  	}
			  }

			  public static partial class MockExtensions
			  {
			  	extension<T>(IMock<MyNihongo.Example.Tests.Class<T>> @this)
			  	{
			  		public void VerifyNoOtherCalls() =>
			  			((ClassMock<T>)@this).VerifyNoOtherCalls();

			  {{extensions.Indent(2)}}
			  	}
			  }

			  public static partial class MockSequenceExtensions
			  {
			  	extension<T>(IMockSequence<MyNihongo.Example.Tests.Class<T>> @this)
			  	{
			  {{sequenceExtensions.Indent(2)}}
			  	}
			  }
			  """;

		return ("ClassMock_T_.g.cs", mock);
	}
}
