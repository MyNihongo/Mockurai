namespace MyNihongo.Mock.Tests;

public abstract class TestsNonGenericBase : TestsBase
{
	protected static string CreateInterfaceTestCode(string members, string customCode)
	{
		return
			$$"""
			  using System.Threading.Tasks;

			  namespace MyNihongo.Mock.Tests;

			  {{customCode}}

			  public interface IInterface
			  {
			  	{{members}}
			  }

			  [MockuraiGenerate]
			  public abstract partial class TestsBase
			  {
			  	protected partial IMock<IInterface> InterfaceMock { get; }
			  }
			  """;
	}

	protected static GeneratedSource GetInterfaceTestsBase()
	{
		const string testsBase =
			"""
			namespace MyNihongo.Mock.Tests;

			public partial class TestsBase
			{
				// InterfaceMock
				private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
				protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock => _interfaceMock;

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
					public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface> InterfaceMock;

					public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> interfaceMock)
					{
						InterfaceMock = new MockSequence<MyNihongo.Mock.Tests.IInterface>
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
			  namespace MyNihongo.Mock;

			  public sealed class InterfaceMock : IMock<MyNihongo.Mock.Tests.IInterface>
			  {
			  	private readonly InvocationIndex.Counter _invocationIndex;
			  	private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
			  	private Proxy? _proxy;

			  	public InterfaceMock(InvocationIndex.Counter invocationIndex)
			  	{
			  		_invocationIndex = invocationIndex;
			  		_invocationProviders = GetInvocations;
			  	}

			  	public MyNihongo.Mock.Tests.IInterface Object => _proxy ??= new Proxy(this);

			  {{methods.Indent(1)}}

			  	public void VerifyNoOtherCalls()
			  	{
			  {{verifyNoOtherCalls.Indent(2)}}
			  	}

			  	private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
			  	{
			  {{invocations.Indent(2)}}
			  	}

			  	private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
			  	{
			  		private readonly InterfaceMock _mock;

			  		public Proxy(InterfaceMock mock)
			  		{
			  			_mock = mock;
			  		}

			  {{proxy.Indent(2)}}
			  	}
			  }

			  public static partial class MockExtensions
			  {
			  	extension(IMock<MyNihongo.Mock.Tests.IInterface> @this)
			  	{
			  		public void VerifyNoOtherCalls() =>
			  			((InterfaceMock)@this).VerifyNoOtherCalls();

			  {{extensions.Indent(2)}}
			  	}
			  }

			  public static partial class MockSequenceExtensions
			  {
			  	extension(IMockSequence<MyNihongo.Mock.Tests.IInterface> @this)
			  	{
			  {{sequenceExtensions.Indent(2)}}
			  	}
			  }
			  """;

		return ("InterfaceMock.g.cs", mock);
	}

	protected static string CreateClassTestCode(string members, string customCode, bool isAbstract)
	{
		return
			$$"""
			  namespace MyNihongo.Mock.Tests;

			  {{customCode}}

			  public {{(isAbstract ? "abstract " : "")}}class Class
			  {
			  	{{members}}
			  }

			  [MockuraiGenerate]
			  public abstract partial class TestsBase
			  {
			  	protected partial IMock<Class> ClassMock { get; }
			  }
			  """;
	}

	protected static GeneratedSource GetClassTestsBase()
	{
		const string testsBase =
			"""
			namespace MyNihongo.Mock.Tests;

			public partial class TestsBase
			{
				// ClassMock
				private readonly ClassMock _classMock = new(InvocationIndex.CounterValue);
				protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.Class> ClassMock => _classMock;

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
					public readonly IMockSequence<MyNihongo.Mock.Tests.Class> ClassMock;

					public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.Class> classMock)
					{
						ClassMock = new MockSequence<MyNihongo.Mock.Tests.Class>
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
			  namespace MyNihongo.Mock;

			  public sealed class ClassMock : IMock<MyNihongo.Mock.Tests.Class>
			  {
			  	private readonly InvocationIndex.Counter _invocationIndex;
			  	private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
			  	private Proxy? _proxy;

			  	public ClassMock(InvocationIndex.Counter invocationIndex)
			  	{
			  		_invocationIndex = invocationIndex;
			  		_invocationProviders = GetInvocations;
			  	}

			  	public MyNihongo.Mock.Tests.Class Object => _proxy ??= new Proxy(this);

			  {{methods.Indent(1)}}

			  	public void VerifyNoOtherCalls()
			  	{
			  {{verifyNoOtherCalls.Indent(2)}}
			  	}

			  	private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
			  	{
			  {{invocations.Indent(2)}}
			  	}

			  	private sealed class Proxy : MyNihongo.Mock.Tests.Class
			  	{
			  		private readonly ClassMock _mock;

			  		public Proxy(ClassMock mock)
			  		{
			  			_mock = mock;
			  		}

			  {{proxy.Indent(2)}}
			  	}
			  }

			  public static partial class MockExtensions
			  {
			  	extension(IMock<MyNihongo.Mock.Tests.Class> @this)
			  	{
			  		public void VerifyNoOtherCalls() =>
			  			((ClassMock)@this).VerifyNoOtherCalls();

			  {{extensions.Indent(2)}}
			  	}
			  }

			  public static partial class MockSequenceExtensions
			  {
			  	extension(IMockSequence<MyNihongo.Mock.Tests.Class> @this)
			  	{
			  {{sequenceExtensions.Indent(2)}}
			  	}
			  }
			  """;

		return ("ClassMock.g.cs", mock);
	}
}
