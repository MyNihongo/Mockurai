namespace MyNihongo.Mockurai.Tests;

public abstract class TestsNonGenericBase : TestsBase
{
	protected static string CreateInterfaceTestCode(string members, string customCode)
	{
		return
			$$"""
			  using System.Threading.Tasks;

			  namespace MyNihongo.Example.Tests;

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
			#nullable enable
			namespace MyNihongo.Example.Tests;

			public partial class TestsBase
			{
				// InterfaceMock
				private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
				protected partial IMock<MyNihongo.Example.Tests.IInterface> InterfaceMock => _interfaceMock;

				protected virtual void VerifyNoOtherCalls()
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

				protected class VerifySequenceContext
				{
					protected readonly VerifyIndex VerifyIndex;
					public readonly IMockSequence<MyNihongo.Example.Tests.IInterface> InterfaceMock;

					public VerifySequenceContext(IMock<MyNihongo.Example.Tests.IInterface> interfaceMock)
					{
						VerifyIndex = new VerifyIndex();
						InterfaceMock = new MockSequence<MyNihongo.Example.Tests.IInterface>
						{
							VerifyIndex = VerifyIndex,
							Mock = interfaceMock,
						};
					}

					protected VerifySequenceContext(VerifySequenceContext ctx)
					{
						VerifyIndex = ctx.VerifyIndex;
						InterfaceMock = ctx.InterfaceMock;
					}
				}
			}
			""";

		return ("TestsBase.g.cs", testsBase);
	}

	protected static GeneratedSource GetInterfaceMock(string methods, string proxy, string invocationContainer, string verifyNoOtherCalls, string invocations, string extensions, string sequenceExtensions)
	{
		var mock =
			$$"""
			  #nullable enable
			  namespace MyNihongo.Mockurai;

			  public sealed class InterfaceMock : IMock<MyNihongo.Example.Tests.IInterface>
			  {
			  	private readonly InvocationIndex.Counter _invocationIndex;
			  	private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
			  	private Proxy? _proxy;

			  	public InterfaceMock(InvocationIndex.Counter invocationIndex)
			  	{
			  		_invocationIndex = invocationIndex;
			  		_invocationProviders = GetInvocations;
			  	}

			  	public MyNihongo.Example.Tests.IInterface Object => _proxy ??= new Proxy(this);

			  	public InvocationContainer Invocations => field ??= new InvocationContainer(this);

			  {{methods.Indent(1)}}

			  	public void VerifyNoOtherCalls()
			  	{
			  {{verifyNoOtherCalls.Indent(2)}}
			  	}

			  	private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
			  	{
			  {{invocations.Indent(2)}}
			  	}

			  	private sealed class Proxy : MyNihongo.Example.Tests.IInterface
			  	{
			  		private readonly InterfaceMock _mock;

			  		public Proxy(InterfaceMock mock)
			  		{
			  			_mock = mock;
			  		}

			  {{proxy.Indent(2)}}
			  	}

			  	public sealed class InvocationContainer
			  	{
			  		private readonly InterfaceMock _mock;

			  		public InvocationContainer(InterfaceMock mock)
			  		{
			  			_mock = mock;
			  		}

			  {{invocationContainer.Indent(2)}}
			  	}
			  }

			  public static partial class MockExtensions
			  {
			  	extension(IMock<MyNihongo.Example.Tests.IInterface> @this)
			  	{
			  		public InterfaceMock.InvocationContainer Invocations => ((InterfaceMock)@this).Invocations;

			  		public void VerifyNoOtherCalls() =>
			  			((InterfaceMock)@this).VerifyNoOtherCalls();

			  {{extensions.Indent(2)}}
			  	}
			  }

			  public static partial class MockSequenceExtensions
			  {
			  	extension(IMockSequence<MyNihongo.Example.Tests.IInterface> @this)
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
			  namespace MyNihongo.Example.Tests;

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
			#nullable enable
			namespace MyNihongo.Example.Tests;

			public partial class TestsBase
			{
				// ClassMock
				private readonly ClassMock _classMock = new(InvocationIndex.CounterValue);
				protected partial IMock<MyNihongo.Example.Tests.Class> ClassMock => _classMock;

				protected virtual void VerifyNoOtherCalls()
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

				protected class VerifySequenceContext
				{
					protected readonly VerifyIndex VerifyIndex;
					public readonly IMockSequence<MyNihongo.Example.Tests.Class> ClassMock;

					public VerifySequenceContext(IMock<MyNihongo.Example.Tests.Class> classMock)
					{
						VerifyIndex = new VerifyIndex();
						ClassMock = new MockSequence<MyNihongo.Example.Tests.Class>
						{
							VerifyIndex = VerifyIndex,
							Mock = classMock,
						};
					}

					protected VerifySequenceContext(VerifySequenceContext ctx)
					{
						VerifyIndex = ctx.VerifyIndex;
						ClassMock = ctx.ClassMock;
					}
				}
			}
			""";

		return ("TestsBase.g.cs", testsBase);
	}

	protected static GeneratedSource GetClassMock(string methods, string proxy, string verifyNoOtherCalls, string invocations, string extensions, string extensionsSequence, string invocationContainer = "")
	{
		var mock =
			$$"""
			  #nullable enable
			  namespace MyNihongo.Mockurai;

			  public sealed class ClassMock : IMock<MyNihongo.Example.Tests.Class>
			  {
			  	private readonly InvocationIndex.Counter _invocationIndex;
			  	private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
			  	private Proxy? _proxy;

			  	public ClassMock(InvocationIndex.Counter invocationIndex)
			  	{
			  		_invocationIndex = invocationIndex;
			  		_invocationProviders = GetInvocations;
			  	}

			  	public MyNihongo.Example.Tests.Class Object => _proxy ??= new Proxy(this);

			  	public InvocationContainer Invocations => field ??= new InvocationContainer(this);

			  {{methods.Indent(1)}}

			  	public void VerifyNoOtherCalls()
			  	{
			  {{verifyNoOtherCalls.Indent(2)}}
			  	}

			  	private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
			  	{
			  {{invocations.Indent(2)}}
			  	}

			  	private sealed class Proxy : MyNihongo.Example.Tests.Class
			  	{
			  		private readonly ClassMock _mock;

			  		public Proxy(ClassMock mock)
			  		{
			  			_mock = mock;
			  		}

			  {{proxy.Indent(2)}}
			  	}

			  	public sealed class InvocationContainer
			  	{
			  		private readonly ClassMock _mock;

			  		public InvocationContainer(ClassMock mock)
			  		{
			  			_mock = mock;
			  		}

			  {{invocationContainer.Indent(2)}}
			  	}
			  }

			  public static partial class MockExtensions
			  {
			  	extension(IMock<MyNihongo.Example.Tests.Class> @this)
			  	{
			  		public ClassMock.InvocationContainer Invocations => ((ClassMock)@this).Invocations;

			  		public void VerifyNoOtherCalls() =>
			  			((ClassMock)@this).VerifyNoOtherCalls();

			  {{extensions.Indent(2)}}
			  	}
			  }

			  public static partial class MockSequenceExtensions
			  {
			  	extension(IMockSequence<MyNihongo.Example.Tests.Class> @this)
			  	{
			  {{extensionsSequence.Indent(2)}}
			  	}
			  }
			  """;

		return ("ClassMock.g.cs", mock);
	}
}
