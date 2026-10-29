namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class SpecialCases : IssuesTestsBase
{
	[Fact]
	public async Task StreamMock()
	{
		const string testCode =
			"""
			using System.IO;

			namespace Issues.Tests;

			public interface IInterface
			{
				Stream GetStream();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
				protected partial IMock<Stream> StreamMock { get; }

				protected TestsBase()
				{
					InterfaceMock
						.SetupGetStream()
						.Returns(StreamMock.Object);
				}
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace Issues.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<Issues.Tests.IInterface> InterfaceMock => _interfaceMock;

					// StreamMock
					private readonly StreamMock _streamMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<System.IO.Stream> StreamMock => _streamMock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls()
					{
						InterfaceMock.VerifyNoOtherCalls();
						StreamMock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interfaceMock: InterfaceMock,
							streamMock: StreamMock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<Issues.Tests.IInterface> InterfaceMock;
						public readonly IMockSequence<System.IO.Stream> StreamMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<Issues.Tests.IInterface> interfaceMock, MyNihongo.Mockurai.IMock<System.IO.Stream> streamMock)
						{
							VerifyIndex = new VerifyIndex();
							InterfaceMock = new MockSequence<Issues.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interfaceMock,
							};
							StreamMock = new MockSequence<System.IO.Stream>
							{
								VerifyIndex = VerifyIndex,
								Mock = streamMock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							InterfaceMock = ctx.InterfaceMock;
							StreamMock = ctx.StreamMock;
						}
					}
				}
				"""
			),
			(
				"InterfaceMock.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InterfaceMock : IMock<Issues.Tests.IInterface>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public InterfaceMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public Issues.Tests.IInterface Object => _proxy ??= new Proxy(this);

					public InvocationContainer Invocations => field ??= new InvocationContainer(this);

					// GetStream
					private Setup<System.IO.Stream>? _getStream0;
					private Invocation? _getStream0Invocation;

					public Setup<System.IO.Stream> SetupGetStream()
					{
						_getStream0 ??= new Setup<System.IO.Stream>();
						_getStream0.Reset();
						return _getStream0;
					}

					public void VerifyGetStream(in Times times)
					{
						_getStream0Invocation ??= new Invocation("IInterface.GetStream()");
						_getStream0Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyGetStream(long index)
					{
						_getStream0Invocation ??= new Invocation("IInterface.GetStream()");
						return _getStream0Invocation.Verify(index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_getStream0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _getStream0Invocation;
					}

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.IO.Stream GetStream()
						{
							_mock._getStream0Invocation ??= new Invocation("IInterface.GetStream()");
							_mock._getStream0Invocation.Register(_mock._invocationIndex);
							return _mock._getStream0?.Execute(out var returnValue) == true ? returnValue! : default!;
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation> GetStream => _mock._getStream0Invocation?.GetInvocations() ?? [];
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Issues.Tests.IInterface> @this)
					{
						public InterfaceMock.InvocationContainer Invocations => ((InterfaceMock)@this).Invocations;

						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// GetStream
						public ISetup<System.Action, System.IO.Stream, System.Func<System.IO.Stream>> SetupGetStream() =>
							((InterfaceMock)@this).SetupGetStream();

						public void VerifyGetStream(in Times times) =>
							((InterfaceMock)@this).VerifyGetStream(times);

						public void VerifyGetStream(System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyGetStream(times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// GetStream
						public void GetStream()
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyGetStream(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			(
				"StreamMock.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class StreamMock : IMock<System.IO.Stream>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public StreamMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public System.IO.Stream Object => _proxy ??= new Proxy(this);

					public InvocationContainer Invocations => field ??= new InvocationContainer(this);

					// BeginRead
					private SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<System.IAsyncResult>? _beginRead0;
					private InvocationArray1ByteInt32Int32AsyncCallbackNullableObjectNullable? _beginRead0Invocation;

					public SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<System.IAsyncResult> SetupBeginRead(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.AsyncCallback?> callback, in It<object?> state)
					{
						_beginRead0 ??= new SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<System.IAsyncResult>();
						_beginRead0.SetupParameters(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, callback.ValueSetup, state.ValueSetup);
						return _beginRead0;
					}

					public void VerifyBeginRead(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.AsyncCallback?> callback, in It<object?> state, in Times times)
					{
						_beginRead0Invocation ??= new InvocationArray1ByteInt32Int32AsyncCallbackNullableObjectNullable("Stream.BeginRead({0}, {1}, {2}, {3}, {4})");
						_beginRead0Invocation.Verify(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, callback.ValueSetup, state.ValueSetup, times, _invocationProviders);
					}

					public long VerifyBeginRead(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.AsyncCallback?> callback, in It<object?> state, long index)
					{
						_beginRead0Invocation ??= new InvocationArray1ByteInt32Int32AsyncCallbackNullableObjectNullable("Stream.BeginRead({0}, {1}, {2}, {3}, {4})");
						return _beginRead0Invocation.Verify(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, callback.ValueSetup, state.ValueSetup, index, _invocationProviders);
					}

					// BeginWrite
					private SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<System.IAsyncResult>? _beginWrite0;
					private InvocationArray1ByteInt32Int32AsyncCallbackNullableObjectNullable? _beginWrite0Invocation;

					public SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<System.IAsyncResult> SetupBeginWrite(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.AsyncCallback?> callback, in It<object?> state)
					{
						_beginWrite0 ??= new SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<System.IAsyncResult>();
						_beginWrite0.SetupParameters(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, callback.ValueSetup, state.ValueSetup);
						return _beginWrite0;
					}

					public void VerifyBeginWrite(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.AsyncCallback?> callback, in It<object?> state, in Times times)
					{
						_beginWrite0Invocation ??= new InvocationArray1ByteInt32Int32AsyncCallbackNullableObjectNullable("Stream.BeginWrite({0}, {1}, {2}, {3}, {4})");
						_beginWrite0Invocation.Verify(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, callback.ValueSetup, state.ValueSetup, times, _invocationProviders);
					}

					public long VerifyBeginWrite(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.AsyncCallback?> callback, in It<object?> state, long index)
					{
						_beginWrite0Invocation ??= new InvocationArray1ByteInt32Int32AsyncCallbackNullableObjectNullable("Stream.BeginWrite({0}, {1}, {2}, {3}, {4})");
						return _beginWrite0Invocation.Verify(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, callback.ValueSetup, state.ValueSetup, index, _invocationProviders);
					}

					// Close
					private Setup? _close0;
					private Invocation? _close0Invocation;

					public Setup SetupClose()
					{
						_close0 ??= new Setup();
						_close0.Reset();
						return _close0;
					}

					public void VerifyClose(in Times times)
					{
						_close0Invocation ??= new Invocation("Stream.Close()");
						_close0Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyClose(long index)
					{
						_close0Invocation ??= new Invocation("Stream.Close()");
						return _close0Invocation.Verify(index, _invocationProviders);
					}

					// CopyTo
					private SetupStreamInt32? _copyTo0;
					private InvocationStreamInt32? _copyTo0Invocation;

					public SetupStreamInt32 SetupCopyTo(in It<System.IO.Stream> destination, in It<int> bufferSize)
					{
						_copyTo0 ??= new SetupStreamInt32();
						_copyTo0.SetupParameters(destination.ValueSetup, bufferSize.ValueSetup);
						return _copyTo0;
					}

					public void VerifyCopyTo(in It<System.IO.Stream> destination, in It<int> bufferSize, in Times times)
					{
						_copyTo0Invocation ??= new InvocationStreamInt32("Stream.CopyTo({0}, {1})");
						_copyTo0Invocation.Verify(destination.ValueSetup, bufferSize.ValueSetup, times, _invocationProviders);
					}

					public long VerifyCopyTo(in It<System.IO.Stream> destination, in It<int> bufferSize, long index)
					{
						_copyTo0Invocation ??= new InvocationStreamInt32("Stream.CopyTo({0}, {1})");
						return _copyTo0Invocation.Verify(destination.ValueSetup, bufferSize.ValueSetup, index, _invocationProviders);
					}

					// CopyToAsync
					private SetupStreamInt32CancellationToken? _copyToAsync0;
					private InvocationStreamInt32CancellationToken? _copyToAsync0Invocation;

					public SetupStreamInt32CancellationToken SetupCopyToAsync(in It<System.IO.Stream> destination, in It<int> bufferSize, in It<System.Threading.CancellationToken> cancellationToken)
					{
						_copyToAsync0 ??= new SetupStreamInt32CancellationToken();
						_copyToAsync0.SetupParameters(destination.ValueSetup, bufferSize.ValueSetup, cancellationToken.ValueSetup);
						return _copyToAsync0;
					}

					public void VerifyCopyToAsync(in It<System.IO.Stream> destination, in It<int> bufferSize, in It<System.Threading.CancellationToken> cancellationToken, in Times times)
					{
						_copyToAsync0Invocation ??= new InvocationStreamInt32CancellationToken("Stream.CopyToAsync({0}, {1}, {2})");
						_copyToAsync0Invocation.Verify(destination.ValueSetup, bufferSize.ValueSetup, cancellationToken.ValueSetup, times, _invocationProviders);
					}

					public long VerifyCopyToAsync(in It<System.IO.Stream> destination, in It<int> bufferSize, in It<System.Threading.CancellationToken> cancellationToken, long index)
					{
						_copyToAsync0Invocation ??= new InvocationStreamInt32CancellationToken("Stream.CopyToAsync({0}, {1}, {2})");
						return _copyToAsync0Invocation.Verify(destination.ValueSetup, bufferSize.ValueSetup, cancellationToken.ValueSetup, index, _invocationProviders);
					}

					// DisposeAsync
					private Setup? _disposeAsync0;
					private Invocation? _disposeAsync0Invocation;

					public Setup SetupDisposeAsync()
					{
						_disposeAsync0 ??= new Setup();
						_disposeAsync0.Reset();
						return _disposeAsync0;
					}

					public void VerifyDisposeAsync(in Times times)
					{
						_disposeAsync0Invocation ??= new Invocation("Stream.DisposeAsync()");
						_disposeAsync0Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyDisposeAsync(long index)
					{
						_disposeAsync0Invocation ??= new Invocation("Stream.DisposeAsync()");
						return _disposeAsync0Invocation.Verify(index, _invocationProviders);
					}

					// EndRead
					private SetupWithParameter<System.IAsyncResult, int>? _endRead0;
					private Invocation<System.IAsyncResult>? _endRead0Invocation;

					public SetupWithParameter<System.IAsyncResult, int> SetupEndRead(in It<System.IAsyncResult> asyncResult)
					{
						_endRead0 ??= new SetupWithParameter<System.IAsyncResult, int>();
						_endRead0.SetupParameter(asyncResult.ValueSetup);
						return _endRead0;
					}

					public void VerifyEndRead(in It<System.IAsyncResult> asyncResult, in Times times)
					{
						_endRead0Invocation ??= new Invocation<System.IAsyncResult>("Stream.EndRead({0})");
						_endRead0Invocation.Verify(asyncResult.ValueSetup, times, _invocationProviders);
					}

					public long VerifyEndRead(in It<System.IAsyncResult> asyncResult, long index)
					{
						_endRead0Invocation ??= new Invocation<System.IAsyncResult>("Stream.EndRead({0})");
						return _endRead0Invocation.Verify(asyncResult.ValueSetup, index, _invocationProviders);
					}

					// EndWrite
					private SetupWithParameter<System.IAsyncResult>? _endWrite0;
					private Invocation<System.IAsyncResult>? _endWrite0Invocation;

					public SetupWithParameter<System.IAsyncResult> SetupEndWrite(in It<System.IAsyncResult> asyncResult)
					{
						_endWrite0 ??= new SetupWithParameter<System.IAsyncResult>();
						_endWrite0.SetupParameter(asyncResult.ValueSetup);
						return _endWrite0;
					}

					public void VerifyEndWrite(in It<System.IAsyncResult> asyncResult, in Times times)
					{
						_endWrite0Invocation ??= new Invocation<System.IAsyncResult>("Stream.EndWrite({0})");
						_endWrite0Invocation.Verify(asyncResult.ValueSetup, times, _invocationProviders);
					}

					public long VerifyEndWrite(in It<System.IAsyncResult> asyncResult, long index)
					{
						_endWrite0Invocation ??= new Invocation<System.IAsyncResult>("Stream.EndWrite({0})");
						return _endWrite0Invocation.Verify(asyncResult.ValueSetup, index, _invocationProviders);
					}

					// Flush
					private Setup? _flush0;
					private Invocation? _flush0Invocation;

					public Setup SetupFlush()
					{
						_flush0 ??= new Setup();
						_flush0.Reset();
						return _flush0;
					}

					public void VerifyFlush(in Times times)
					{
						_flush0Invocation ??= new Invocation("Stream.Flush()");
						_flush0Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyFlush(long index)
					{
						_flush0Invocation ??= new Invocation("Stream.Flush()");
						return _flush0Invocation.Verify(index, _invocationProviders);
					}

					// FlushAsync
					private SetupWithParameter<System.Threading.CancellationToken>? _flushAsync0;
					private Invocation<System.Threading.CancellationToken>? _flushAsync0Invocation;

					public SetupWithParameter<System.Threading.CancellationToken> SetupFlushAsync(in It<System.Threading.CancellationToken> cancellationToken)
					{
						_flushAsync0 ??= new SetupWithParameter<System.Threading.CancellationToken>();
						_flushAsync0.SetupParameter(cancellationToken.ValueSetup);
						return _flushAsync0;
					}

					public void VerifyFlushAsync(in It<System.Threading.CancellationToken> cancellationToken, in Times times)
					{
						_flushAsync0Invocation ??= new Invocation<System.Threading.CancellationToken>("Stream.FlushAsync({0})");
						_flushAsync0Invocation.Verify(cancellationToken.ValueSetup, times, _invocationProviders);
					}

					public long VerifyFlushAsync(in It<System.Threading.CancellationToken> cancellationToken, long index)
					{
						_flushAsync0Invocation ??= new Invocation<System.Threading.CancellationToken>("Stream.FlushAsync({0})");
						return _flushAsync0Invocation.Verify(cancellationToken.ValueSetup, index, _invocationProviders);
					}

					// Read
					private SetupArray1ByteInt32Int32<int>? _read0;
					private InvocationArray1ByteInt32Int32? _read0Invocation;

					public SetupArray1ByteInt32Int32<int> SetupRead(in It<byte[]> buffer, in It<int> offset, in It<int> count)
					{
						_read0 ??= new SetupArray1ByteInt32Int32<int>();
						_read0.SetupParameters(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup);
						return _read0;
					}

					public void VerifyRead(in It<byte[]> buffer, in It<int> offset, in It<int> count, in Times times)
					{
						_read0Invocation ??= new InvocationArray1ByteInt32Int32("Stream.Read({0}, {1}, {2})");
						_read0Invocation.Verify(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, times, _invocationProviders);
					}

					public long VerifyRead(in It<byte[]> buffer, in It<int> offset, in It<int> count, long index)
					{
						_read0Invocation ??= new InvocationArray1ByteInt32Int32("Stream.Read({0}, {1}, {2})");
						return _read0Invocation.Verify(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, index, _invocationProviders);
					}

					// ReadAsync
					private SetupArray1ByteInt32Int32CancellationToken<int>? _readAsync0;
					private InvocationArray1ByteInt32Int32CancellationToken? _readAsync0Invocation;

					public SetupArray1ByteInt32Int32CancellationToken<int> SetupReadAsync(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.Threading.CancellationToken> cancellationToken)
					{
						_readAsync0 ??= new SetupArray1ByteInt32Int32CancellationToken<int>();
						_readAsync0.SetupParameters(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, cancellationToken.ValueSetup);
						return _readAsync0;
					}

					public void VerifyReadAsync(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.Threading.CancellationToken> cancellationToken, in Times times)
					{
						_readAsync0Invocation ??= new InvocationArray1ByteInt32Int32CancellationToken("Stream.ReadAsync({0}, {1}, {2}, {3})");
						_readAsync0Invocation.Verify(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, cancellationToken.ValueSetup, times, _invocationProviders);
					}

					public long VerifyReadAsync(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.Threading.CancellationToken> cancellationToken, long index)
					{
						_readAsync0Invocation ??= new InvocationArray1ByteInt32Int32CancellationToken("Stream.ReadAsync({0}, {1}, {2}, {3})");
						return _readAsync0Invocation.Verify(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, cancellationToken.ValueSetup, index, _invocationProviders);
					}

					// ReadAsync
					private SetupMemoryByteCancellationToken<int>? _readAsync1;
					private InvocationMemoryByteCancellationToken? _readAsync1Invocation;

					public SetupMemoryByteCancellationToken<int> SetupReadAsync(in It<System.Memory<byte>> buffer, in It<System.Threading.CancellationToken> cancellationToken)
					{
						_readAsync1 ??= new SetupMemoryByteCancellationToken<int>();
						_readAsync1.SetupParameters(buffer.ValueSetup, cancellationToken.ValueSetup);
						return _readAsync1;
					}

					public void VerifyReadAsync(in It<System.Memory<byte>> buffer, in It<System.Threading.CancellationToken> cancellationToken, in Times times)
					{
						_readAsync1Invocation ??= new InvocationMemoryByteCancellationToken("Stream.ReadAsync({0}, {1})");
						_readAsync1Invocation.Verify(buffer.ValueSetup, cancellationToken.ValueSetup, times, _invocationProviders);
					}

					public long VerifyReadAsync(in It<System.Memory<byte>> buffer, in It<System.Threading.CancellationToken> cancellationToken, long index)
					{
						_readAsync1Invocation ??= new InvocationMemoryByteCancellationToken("Stream.ReadAsync({0}, {1})");
						return _readAsync1Invocation.Verify(buffer.ValueSetup, cancellationToken.ValueSetup, index, _invocationProviders);
					}

					// ReadByte
					private Setup<int>? _readByte0;
					private Invocation? _readByte0Invocation;

					public Setup<int> SetupReadByte()
					{
						_readByte0 ??= new Setup<int>();
						_readByte0.Reset();
						return _readByte0;
					}

					public void VerifyReadByte(in Times times)
					{
						_readByte0Invocation ??= new Invocation("Stream.ReadByte()");
						_readByte0Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyReadByte(long index)
					{
						_readByte0Invocation ??= new Invocation("Stream.ReadByte()");
						return _readByte0Invocation.Verify(index, _invocationProviders);
					}

					// Seek
					private SetupInt64SeekOrigin<long>? _seek0;
					private InvocationInt64SeekOrigin? _seek0Invocation;

					public SetupInt64SeekOrigin<long> SetupSeek(in It<long> offset, in It<System.IO.SeekOrigin> origin)
					{
						_seek0 ??= new SetupInt64SeekOrigin<long>();
						_seek0.SetupParameters(offset.ValueSetup, origin.ValueSetup);
						return _seek0;
					}

					public void VerifySeek(in It<long> offset, in It<System.IO.SeekOrigin> origin, in Times times)
					{
						_seek0Invocation ??= new InvocationInt64SeekOrigin("Stream.Seek({0}, {1})");
						_seek0Invocation.Verify(offset.ValueSetup, origin.ValueSetup, times, _invocationProviders);
					}

					public long VerifySeek(in It<long> offset, in It<System.IO.SeekOrigin> origin, long index)
					{
						_seek0Invocation ??= new InvocationInt64SeekOrigin("Stream.Seek({0}, {1})");
						return _seek0Invocation.Verify(offset.ValueSetup, origin.ValueSetup, index, _invocationProviders);
					}

					// SetLength
					private SetupWithParameter<long>? _setLength0;
					private Invocation<long>? _setLength0Invocation;

					public SetupWithParameter<long> SetupSetLength(in It<long> value)
					{
						_setLength0 ??= new SetupWithParameter<long>();
						_setLength0.SetupParameter(value.ValueSetup);
						return _setLength0;
					}

					public void VerifySetLength(in It<long> value, in Times times)
					{
						_setLength0Invocation ??= new Invocation<long>("Stream.SetLength({0})");
						_setLength0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
					}

					public long VerifySetLength(in It<long> value, long index)
					{
						_setLength0Invocation ??= new Invocation<long>("Stream.SetLength({0})");
						return _setLength0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
					}

					// Write
					private SetupArray1ByteInt32Int32? _write0;
					private InvocationArray1ByteInt32Int32? _write0Invocation;

					public SetupArray1ByteInt32Int32 SetupWrite(in It<byte[]> buffer, in It<int> offset, in It<int> count)
					{
						_write0 ??= new SetupArray1ByteInt32Int32();
						_write0.SetupParameters(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup);
						return _write0;
					}

					public void VerifyWrite(in It<byte[]> buffer, in It<int> offset, in It<int> count, in Times times)
					{
						_write0Invocation ??= new InvocationArray1ByteInt32Int32("Stream.Write({0}, {1}, {2})");
						_write0Invocation.Verify(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, times, _invocationProviders);
					}

					public long VerifyWrite(in It<byte[]> buffer, in It<int> offset, in It<int> count, long index)
					{
						_write0Invocation ??= new InvocationArray1ByteInt32Int32("Stream.Write({0}, {1}, {2})");
						return _write0Invocation.Verify(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, index, _invocationProviders);
					}

					// WriteAsync
					private SetupArray1ByteInt32Int32CancellationToken? _writeAsync0;
					private InvocationArray1ByteInt32Int32CancellationToken? _writeAsync0Invocation;

					public SetupArray1ByteInt32Int32CancellationToken SetupWriteAsync(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.Threading.CancellationToken> cancellationToken)
					{
						_writeAsync0 ??= new SetupArray1ByteInt32Int32CancellationToken();
						_writeAsync0.SetupParameters(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, cancellationToken.ValueSetup);
						return _writeAsync0;
					}

					public void VerifyWriteAsync(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.Threading.CancellationToken> cancellationToken, in Times times)
					{
						_writeAsync0Invocation ??= new InvocationArray1ByteInt32Int32CancellationToken("Stream.WriteAsync({0}, {1}, {2}, {3})");
						_writeAsync0Invocation.Verify(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, cancellationToken.ValueSetup, times, _invocationProviders);
					}

					public long VerifyWriteAsync(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.Threading.CancellationToken> cancellationToken, long index)
					{
						_writeAsync0Invocation ??= new InvocationArray1ByteInt32Int32CancellationToken("Stream.WriteAsync({0}, {1}, {2}, {3})");
						return _writeAsync0Invocation.Verify(buffer.ValueSetup, offset.ValueSetup, count.ValueSetup, cancellationToken.ValueSetup, index, _invocationProviders);
					}

					// WriteAsync
					private SetupReadOnlyMemoryByteCancellationToken? _writeAsync1;
					private InvocationReadOnlyMemoryByteCancellationToken? _writeAsync1Invocation;

					public SetupReadOnlyMemoryByteCancellationToken SetupWriteAsync(in It<System.ReadOnlyMemory<byte>> buffer, in It<System.Threading.CancellationToken> cancellationToken)
					{
						_writeAsync1 ??= new SetupReadOnlyMemoryByteCancellationToken();
						_writeAsync1.SetupParameters(buffer.ValueSetup, cancellationToken.ValueSetup);
						return _writeAsync1;
					}

					public void VerifyWriteAsync(in It<System.ReadOnlyMemory<byte>> buffer, in It<System.Threading.CancellationToken> cancellationToken, in Times times)
					{
						_writeAsync1Invocation ??= new InvocationReadOnlyMemoryByteCancellationToken("Stream.WriteAsync({0}, {1})");
						_writeAsync1Invocation.Verify(buffer.ValueSetup, cancellationToken.ValueSetup, times, _invocationProviders);
					}

					public long VerifyWriteAsync(in It<System.ReadOnlyMemory<byte>> buffer, in It<System.Threading.CancellationToken> cancellationToken, long index)
					{
						_writeAsync1Invocation ??= new InvocationReadOnlyMemoryByteCancellationToken("Stream.WriteAsync({0}, {1})");
						return _writeAsync1Invocation.Verify(buffer.ValueSetup, cancellationToken.ValueSetup, index, _invocationProviders);
					}

					// WriteByte
					private SetupWithParameter<byte>? _writeByte0;
					private Invocation<byte>? _writeByte0Invocation;

					public SetupWithParameter<byte> SetupWriteByte(in It<byte> value)
					{
						_writeByte0 ??= new SetupWithParameter<byte>();
						_writeByte0.SetupParameter(value.ValueSetup);
						return _writeByte0;
					}

					public void VerifyWriteByte(in It<byte> value, in Times times)
					{
						_writeByte0Invocation ??= new Invocation<byte>("Stream.WriteByte({0})");
						_writeByte0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
					}

					public long VerifyWriteByte(in It<byte> value, long index)
					{
						_writeByte0Invocation ??= new Invocation<byte>("Stream.WriteByte({0})");
						return _writeByte0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
					}

					// CanRead
					private Setup<bool>? _canRead0Get;
					private Invocation? _canRead0GetInvocation;

					public Setup<bool> SetupGetCanRead()
					{
						_canRead0Get ??= new Setup<bool>();
						_canRead0Get.Reset();
						return _canRead0Get;
					}

					public void VerifyGetCanRead(in Times times)
					{
						_canRead0GetInvocation ??= new Invocation("Stream.CanRead.get");
						_canRead0GetInvocation.Verify(times, _invocationProviders);
					}

					public long VerifyGetCanRead(long index)
					{
						_canRead0GetInvocation ??= new Invocation("Stream.CanRead.get");
						return _canRead0GetInvocation.Verify(index, _invocationProviders);
					}

					// CanSeek
					private Setup<bool>? _canSeek0Get;
					private Invocation? _canSeek0GetInvocation;

					public Setup<bool> SetupGetCanSeek()
					{
						_canSeek0Get ??= new Setup<bool>();
						_canSeek0Get.Reset();
						return _canSeek0Get;
					}

					public void VerifyGetCanSeek(in Times times)
					{
						_canSeek0GetInvocation ??= new Invocation("Stream.CanSeek.get");
						_canSeek0GetInvocation.Verify(times, _invocationProviders);
					}

					public long VerifyGetCanSeek(long index)
					{
						_canSeek0GetInvocation ??= new Invocation("Stream.CanSeek.get");
						return _canSeek0GetInvocation.Verify(index, _invocationProviders);
					}

					// CanTimeout
					private Setup<bool>? _canTimeout0Get;
					private Invocation? _canTimeout0GetInvocation;

					public Setup<bool> SetupGetCanTimeout()
					{
						_canTimeout0Get ??= new Setup<bool>();
						_canTimeout0Get.Reset();
						return _canTimeout0Get;
					}

					public void VerifyGetCanTimeout(in Times times)
					{
						_canTimeout0GetInvocation ??= new Invocation("Stream.CanTimeout.get");
						_canTimeout0GetInvocation.Verify(times, _invocationProviders);
					}

					public long VerifyGetCanTimeout(long index)
					{
						_canTimeout0GetInvocation ??= new Invocation("Stream.CanTimeout.get");
						return _canTimeout0GetInvocation.Verify(index, _invocationProviders);
					}

					// CanWrite
					private Setup<bool>? _canWrite0Get;
					private Invocation? _canWrite0GetInvocation;

					public Setup<bool> SetupGetCanWrite()
					{
						_canWrite0Get ??= new Setup<bool>();
						_canWrite0Get.Reset();
						return _canWrite0Get;
					}

					public void VerifyGetCanWrite(in Times times)
					{
						_canWrite0GetInvocation ??= new Invocation("Stream.CanWrite.get");
						_canWrite0GetInvocation.Verify(times, _invocationProviders);
					}

					public long VerifyGetCanWrite(long index)
					{
						_canWrite0GetInvocation ??= new Invocation("Stream.CanWrite.get");
						return _canWrite0GetInvocation.Verify(index, _invocationProviders);
					}

					// Length
					private Setup<long>? _length0Get;
					private Invocation? _length0GetInvocation;

					public Setup<long> SetupGetLength()
					{
						_length0Get ??= new Setup<long>();
						_length0Get.Reset();
						return _length0Get;
					}

					public void VerifyGetLength(in Times times)
					{
						_length0GetInvocation ??= new Invocation("Stream.Length.get");
						_length0GetInvocation.Verify(times, _invocationProviders);
					}

					public long VerifyGetLength(long index)
					{
						_length0GetInvocation ??= new Invocation("Stream.Length.get");
						return _length0GetInvocation.Verify(index, _invocationProviders);
					}

					// Position
					private Setup<long>? _position0Get;
					private Invocation? _position0GetInvocation;
					private SetupWithParameter<long>? _position0Set;
					private Invocation<long>? _position0SetInvocation;

					public Setup<long> SetupGetPosition()
					{
						_position0Get ??= new Setup<long>();
						_position0Get.Reset();
						return _position0Get;
					}

					public void VerifyGetPosition(in Times times)
					{
						_position0GetInvocation ??= new Invocation("Stream.Position.get");
						_position0GetInvocation.Verify(times, _invocationProviders);
					}

					public long VerifyGetPosition(long index)
					{
						_position0GetInvocation ??= new Invocation("Stream.Position.get");
						return _position0GetInvocation.Verify(index, _invocationProviders);
					}

					public SetupWithParameter<long> SetupSetPosition(in It<long> value)
					{
						_position0Set ??= new SetupWithParameter<long>();
						_position0Set.SetupParameter(value.ValueSetup);
						return _position0Set;
					}

					public void VerifySetPosition(in It<long> value, in Times times)
					{
						_position0SetInvocation ??= new Invocation<long>("Stream.Position.set = {0}");
						_position0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
					}

					public long VerifySetPosition(in It<long> value, long index)
					{
						_position0SetInvocation ??= new Invocation<long>("Stream.Position.set = {0}");
						return _position0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
					}

					// ReadTimeout
					private Setup<int>? _readTimeout0Get;
					private Invocation? _readTimeout0GetInvocation;
					private SetupWithParameter<int>? _readTimeout0Set;
					private Invocation<int>? _readTimeout0SetInvocation;

					public Setup<int> SetupGetReadTimeout()
					{
						_readTimeout0Get ??= new Setup<int>();
						_readTimeout0Get.Reset();
						return _readTimeout0Get;
					}

					public void VerifyGetReadTimeout(in Times times)
					{
						_readTimeout0GetInvocation ??= new Invocation("Stream.ReadTimeout.get");
						_readTimeout0GetInvocation.Verify(times, _invocationProviders);
					}

					public long VerifyGetReadTimeout(long index)
					{
						_readTimeout0GetInvocation ??= new Invocation("Stream.ReadTimeout.get");
						return _readTimeout0GetInvocation.Verify(index, _invocationProviders);
					}

					public SetupWithParameter<int> SetupSetReadTimeout(in It<int> value)
					{
						_readTimeout0Set ??= new SetupWithParameter<int>();
						_readTimeout0Set.SetupParameter(value.ValueSetup);
						return _readTimeout0Set;
					}

					public void VerifySetReadTimeout(in It<int> value, in Times times)
					{
						_readTimeout0SetInvocation ??= new Invocation<int>("Stream.ReadTimeout.set = {0}");
						_readTimeout0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
					}

					public long VerifySetReadTimeout(in It<int> value, long index)
					{
						_readTimeout0SetInvocation ??= new Invocation<int>("Stream.ReadTimeout.set = {0}");
						return _readTimeout0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
					}

					// WriteTimeout
					private Setup<int>? _writeTimeout0Get;
					private Invocation? _writeTimeout0GetInvocation;
					private SetupWithParameter<int>? _writeTimeout0Set;
					private Invocation<int>? _writeTimeout0SetInvocation;

					public Setup<int> SetupGetWriteTimeout()
					{
						_writeTimeout0Get ??= new Setup<int>();
						_writeTimeout0Get.Reset();
						return _writeTimeout0Get;
					}

					public void VerifyGetWriteTimeout(in Times times)
					{
						_writeTimeout0GetInvocation ??= new Invocation("Stream.WriteTimeout.get");
						_writeTimeout0GetInvocation.Verify(times, _invocationProviders);
					}

					public long VerifyGetWriteTimeout(long index)
					{
						_writeTimeout0GetInvocation ??= new Invocation("Stream.WriteTimeout.get");
						return _writeTimeout0GetInvocation.Verify(index, _invocationProviders);
					}

					public SetupWithParameter<int> SetupSetWriteTimeout(in It<int> value)
					{
						_writeTimeout0Set ??= new SetupWithParameter<int>();
						_writeTimeout0Set.SetupParameter(value.ValueSetup);
						return _writeTimeout0Set;
					}

					public void VerifySetWriteTimeout(in It<int> value, in Times times)
					{
						_writeTimeout0SetInvocation ??= new Invocation<int>("Stream.WriteTimeout.set = {0}");
						_writeTimeout0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
					}

					public long VerifySetWriteTimeout(in It<int> value, long index)
					{
						_writeTimeout0SetInvocation ??= new Invocation<int>("Stream.WriteTimeout.set = {0}");
						return _writeTimeout0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
					}

					// InitializeLifetimeService
					private Setup<object>? _initializeLifetimeService0;
					private Invocation? _initializeLifetimeService0Invocation;

					public Setup<object> SetupInitializeLifetimeService()
					{
						_initializeLifetimeService0 ??= new Setup<object>();
						_initializeLifetimeService0.Reset();
						return _initializeLifetimeService0;
					}

					public void VerifyInitializeLifetimeService(in Times times)
					{
						_initializeLifetimeService0Invocation ??= new Invocation("Stream.InitializeLifetimeService()");
						_initializeLifetimeService0Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyInitializeLifetimeService(long index)
					{
						_initializeLifetimeService0Invocation ??= new Invocation("Stream.InitializeLifetimeService()");
						return _initializeLifetimeService0Invocation.Verify(index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_beginRead0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_beginWrite0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_close0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_copyTo0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_copyToAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_disposeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_endRead0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_endWrite0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_flush0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_flushAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_read0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_readAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_readAsync1Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_readByte0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_seek0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_setLength0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_write0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_writeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_writeAsync1Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_writeByte0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_canRead0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_canSeek0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_canTimeout0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_canWrite0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_length0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_position0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_position0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_readTimeout0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_readTimeout0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_writeTimeout0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_writeTimeout0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_initializeLifetimeService0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _beginRead0Invocation;
						yield return _beginWrite0Invocation;
						yield return _close0Invocation;
						yield return _copyTo0Invocation;
						yield return _copyToAsync0Invocation;
						yield return _disposeAsync0Invocation;
						yield return _endRead0Invocation;
						yield return _endWrite0Invocation;
						yield return _flush0Invocation;
						yield return _flushAsync0Invocation;
						yield return _read0Invocation;
						yield return _readAsync0Invocation;
						yield return _readAsync1Invocation;
						yield return _readByte0Invocation;
						yield return _seek0Invocation;
						yield return _setLength0Invocation;
						yield return _write0Invocation;
						yield return _writeAsync0Invocation;
						yield return _writeAsync1Invocation;
						yield return _writeByte0Invocation;
						yield return _canRead0GetInvocation;
						yield return _canSeek0GetInvocation;
						yield return _canTimeout0GetInvocation;
						yield return _canWrite0GetInvocation;
						yield return _length0GetInvocation;
						yield return _position0GetInvocation;
						yield return _position0SetInvocation;
						yield return _readTimeout0GetInvocation;
						yield return _readTimeout0SetInvocation;
						yield return _writeTimeout0GetInvocation;
						yield return _writeTimeout0SetInvocation;
						yield return _initializeLifetimeService0Invocation;
					}

					private sealed class Proxy : System.IO.Stream
					{
						private readonly StreamMock _mock;

						public Proxy(StreamMock mock)
						{
							_mock = mock;
						}

						public override System.IAsyncResult BeginRead(byte[] buffer, int offset, int count, System.AsyncCallback? callback, object? state)
						{
							_mock._beginRead0Invocation ??= new InvocationArray1ByteInt32Int32AsyncCallbackNullableObjectNullable("Stream.BeginRead({0}, {1}, {2}, {3}, {4})");
							_mock._beginRead0Invocation.Register(_mock._invocationIndex, buffer, offset, count, callback, state);
							return _mock._beginRead0?.Execute(buffer, offset, count, callback, state, out var returnValue) == true ? returnValue! : default!;
						}

						public override System.IAsyncResult BeginWrite(byte[] buffer, int offset, int count, System.AsyncCallback? callback, object? state)
						{
							_mock._beginWrite0Invocation ??= new InvocationArray1ByteInt32Int32AsyncCallbackNullableObjectNullable("Stream.BeginWrite({0}, {1}, {2}, {3}, {4})");
							_mock._beginWrite0Invocation.Register(_mock._invocationIndex, buffer, offset, count, callback, state);
							return _mock._beginWrite0?.Execute(buffer, offset, count, callback, state, out var returnValue) == true ? returnValue! : default!;
						}

						public override void Close()
						{
							_mock._close0Invocation ??= new Invocation("Stream.Close()");
							_mock._close0Invocation.Register(_mock._invocationIndex);
							_mock._close0?.Invoke();
						}

						public override void CopyTo(System.IO.Stream destination, int bufferSize)
						{
							_mock._copyTo0Invocation ??= new InvocationStreamInt32("Stream.CopyTo({0}, {1})");
							_mock._copyTo0Invocation.Register(_mock._invocationIndex, destination, bufferSize);
							_mock._copyTo0?.Invoke(destination, bufferSize);
						}

						public override System.Threading.Tasks.Task CopyToAsync(System.IO.Stream destination, int bufferSize, System.Threading.CancellationToken cancellationToken)
						{
							_mock._copyToAsync0Invocation ??= new InvocationStreamInt32CancellationToken("Stream.CopyToAsync({0}, {1}, {2})");
							_mock._copyToAsync0Invocation.Register(_mock._invocationIndex, destination, bufferSize, cancellationToken);
							_mock._copyToAsync0?.Invoke(destination, bufferSize, cancellationToken);
							return System.Threading.Tasks.Task.CompletedTask;
						}

						public override System.Threading.Tasks.ValueTask DisposeAsync()
						{
							_mock._disposeAsync0Invocation ??= new Invocation("Stream.DisposeAsync()");
							_mock._disposeAsync0Invocation.Register(_mock._invocationIndex);
							_mock._disposeAsync0?.Invoke();
							return System.Threading.Tasks.ValueTask.CompletedTask;
						}

						public override int EndRead(System.IAsyncResult asyncResult)
						{
							_mock._endRead0Invocation ??= new Invocation<System.IAsyncResult>("Stream.EndRead({0})");
							_mock._endRead0Invocation.Register(_mock._invocationIndex, asyncResult);
							return _mock._endRead0?.Execute(asyncResult, out var returnValue) == true ? returnValue! : default!;
						}

						public override void EndWrite(System.IAsyncResult asyncResult)
						{
							_mock._endWrite0Invocation ??= new Invocation<System.IAsyncResult>("Stream.EndWrite({0})");
							_mock._endWrite0Invocation.Register(_mock._invocationIndex, asyncResult);
							_mock._endWrite0?.Invoke(asyncResult);
						}

						public override void Flush()
						{
							_mock._flush0Invocation ??= new Invocation("Stream.Flush()");
							_mock._flush0Invocation.Register(_mock._invocationIndex);
							_mock._flush0?.Invoke();
						}

						public override System.Threading.Tasks.Task FlushAsync(System.Threading.CancellationToken cancellationToken)
						{
							_mock._flushAsync0Invocation ??= new Invocation<System.Threading.CancellationToken>("Stream.FlushAsync({0})");
							_mock._flushAsync0Invocation.Register(_mock._invocationIndex, cancellationToken);
							_mock._flushAsync0?.Invoke(cancellationToken);
							return System.Threading.Tasks.Task.CompletedTask;
						}

						public override int Read(byte[] buffer, int offset, int count)
						{
							_mock._read0Invocation ??= new InvocationArray1ByteInt32Int32("Stream.Read({0}, {1}, {2})");
							_mock._read0Invocation.Register(_mock._invocationIndex, buffer, offset, count);
							return _mock._read0?.Execute(buffer, offset, count, out var returnValue) == true ? returnValue! : default!;
						}

						public override System.Threading.Tasks.Task<int> ReadAsync(byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken)
						{
							_mock._readAsync0Invocation ??= new InvocationArray1ByteInt32Int32CancellationToken("Stream.ReadAsync({0}, {1}, {2}, {3})");
							_mock._readAsync0Invocation.Register(_mock._invocationIndex, buffer, offset, count, cancellationToken);
							return System.Threading.Tasks.Task.FromResult<int>(_mock._readAsync0?.Execute(buffer, offset, count, cancellationToken, out var returnValue) == true ? returnValue! : default!);
						}

						public override System.Threading.Tasks.ValueTask<int> ReadAsync(System.Memory<byte> buffer, System.Threading.CancellationToken cancellationToken)
						{
							_mock._readAsync1Invocation ??= new InvocationMemoryByteCancellationToken("Stream.ReadAsync({0}, {1})");
							_mock._readAsync1Invocation.Register(_mock._invocationIndex, buffer, cancellationToken);
							return System.Threading.Tasks.ValueTask.FromResult<int>(_mock._readAsync1?.Execute(buffer, cancellationToken, out var returnValue) == true ? returnValue! : default!);
						}

						public override int ReadByte()
						{
							_mock._readByte0Invocation ??= new Invocation("Stream.ReadByte()");
							_mock._readByte0Invocation.Register(_mock._invocationIndex);
							return _mock._readByte0?.Execute(out var returnValue) == true ? returnValue! : default!;
						}

						public override long Seek(long offset, System.IO.SeekOrigin origin)
						{
							_mock._seek0Invocation ??= new InvocationInt64SeekOrigin("Stream.Seek({0}, {1})");
							_mock._seek0Invocation.Register(_mock._invocationIndex, offset, origin);
							return _mock._seek0?.Execute(offset, origin, out var returnValue) == true ? returnValue! : default!;
						}

						public override void SetLength(long value)
						{
							_mock._setLength0Invocation ??= new Invocation<long>("Stream.SetLength({0})");
							_mock._setLength0Invocation.Register(_mock._invocationIndex, value);
							_mock._setLength0?.Invoke(value);
						}

						public override void Write(byte[] buffer, int offset, int count)
						{
							_mock._write0Invocation ??= new InvocationArray1ByteInt32Int32("Stream.Write({0}, {1}, {2})");
							_mock._write0Invocation.Register(_mock._invocationIndex, buffer, offset, count);
							_mock._write0?.Invoke(buffer, offset, count);
						}

						public override System.Threading.Tasks.Task WriteAsync(byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken)
						{
							_mock._writeAsync0Invocation ??= new InvocationArray1ByteInt32Int32CancellationToken("Stream.WriteAsync({0}, {1}, {2}, {3})");
							_mock._writeAsync0Invocation.Register(_mock._invocationIndex, buffer, offset, count, cancellationToken);
							_mock._writeAsync0?.Invoke(buffer, offset, count, cancellationToken);
							return System.Threading.Tasks.Task.CompletedTask;
						}

						public override System.Threading.Tasks.ValueTask WriteAsync(System.ReadOnlyMemory<byte> buffer, System.Threading.CancellationToken cancellationToken)
						{
							_mock._writeAsync1Invocation ??= new InvocationReadOnlyMemoryByteCancellationToken("Stream.WriteAsync({0}, {1})");
							_mock._writeAsync1Invocation.Register(_mock._invocationIndex, buffer, cancellationToken);
							_mock._writeAsync1?.Invoke(buffer, cancellationToken);
							return System.Threading.Tasks.ValueTask.CompletedTask;
						}

						public override void WriteByte(byte value)
						{
							_mock._writeByte0Invocation ??= new Invocation<byte>("Stream.WriteByte({0})");
							_mock._writeByte0Invocation.Register(_mock._invocationIndex, value);
							_mock._writeByte0?.Invoke(value);
						}

						public override bool CanRead
						{
							get
							{
								_mock._canRead0GetInvocation ??= new Invocation("Stream.CanRead.get");
								_mock._canRead0GetInvocation.Register(_mock._invocationIndex);
								return _mock._canRead0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
							}
						}

						public override bool CanSeek
						{
							get
							{
								_mock._canSeek0GetInvocation ??= new Invocation("Stream.CanSeek.get");
								_mock._canSeek0GetInvocation.Register(_mock._invocationIndex);
								return _mock._canSeek0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
							}
						}

						public override bool CanTimeout
						{
							get
							{
								_mock._canTimeout0GetInvocation ??= new Invocation("Stream.CanTimeout.get");
								_mock._canTimeout0GetInvocation.Register(_mock._invocationIndex);
								return _mock._canTimeout0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
							}
						}

						public override bool CanWrite
						{
							get
							{
								_mock._canWrite0GetInvocation ??= new Invocation("Stream.CanWrite.get");
								_mock._canWrite0GetInvocation.Register(_mock._invocationIndex);
								return _mock._canWrite0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
							}
						}

						public override long Length
						{
							get
							{
								_mock._length0GetInvocation ??= new Invocation("Stream.Length.get");
								_mock._length0GetInvocation.Register(_mock._invocationIndex);
								return _mock._length0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
							}
						}

						public override long Position
						{
							get
							{
								_mock._position0GetInvocation ??= new Invocation("Stream.Position.get");
								_mock._position0GetInvocation.Register(_mock._invocationIndex);
								return _mock._position0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
							}
							set
							{
								_mock._position0SetInvocation ??= new Invocation<long>("Stream.Position.set = {0}");
								_mock._position0SetInvocation.Register(_mock._invocationIndex, value);
								_mock._position0Set?.Invoke(value);
							}
						}

						public override int ReadTimeout
						{
							get
							{
								_mock._readTimeout0GetInvocation ??= new Invocation("Stream.ReadTimeout.get");
								_mock._readTimeout0GetInvocation.Register(_mock._invocationIndex);
								return _mock._readTimeout0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
							}
							set
							{
								_mock._readTimeout0SetInvocation ??= new Invocation<int>("Stream.ReadTimeout.set = {0}");
								_mock._readTimeout0SetInvocation.Register(_mock._invocationIndex, value);
								_mock._readTimeout0Set?.Invoke(value);
							}
						}

						public override int WriteTimeout
						{
							get
							{
								_mock._writeTimeout0GetInvocation ??= new Invocation("Stream.WriteTimeout.get");
								_mock._writeTimeout0GetInvocation.Register(_mock._invocationIndex);
								return _mock._writeTimeout0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
							}
							set
							{
								_mock._writeTimeout0SetInvocation ??= new Invocation<int>("Stream.WriteTimeout.set = {0}");
								_mock._writeTimeout0SetInvocation.Register(_mock._invocationIndex, value);
								_mock._writeTimeout0Set?.Invoke(value);
							}
						}

						public override object InitializeLifetimeService()
						{
							_mock._initializeLifetimeService0Invocation ??= new Invocation("Stream.InitializeLifetimeService()");
							_mock._initializeLifetimeService0Invocation.Register(_mock._invocationIndex);
							return _mock._initializeLifetimeService0?.Execute(out var returnValue) == true ? returnValue! : default!;
						}
					}

					public sealed class InvocationContainer
					{
						private readonly StreamMock _mock;

						public InvocationContainer(StreamMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(byte[] buffer, int offset, int count, System.AsyncCallback? callback, object? state)>> BeginRead => _mock._beginRead0Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<(byte[] buffer, int offset, int count, System.AsyncCallback? callback, object? state)>> BeginWrite => _mock._beginWrite0Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> Close => _mock._close0Invocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<(System.IO.Stream destination, int bufferSize)>> CopyTo => _mock._copyTo0Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<(System.IO.Stream destination, int bufferSize, System.Threading.CancellationToken cancellationToken)>> CopyToAsync => _mock._copyToAsync0Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> DisposeAsync => _mock._disposeAsync0Invocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<System.IAsyncResult>> EndRead => _mock._endRead0Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<System.IAsyncResult>> EndWrite => _mock._endWrite0Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> Flush => _mock._flush0Invocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<System.Threading.CancellationToken>> FlushAsync => _mock._flushAsync0Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<(byte[] buffer, int offset, int count)>> Read => _mock._read0Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<(byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken)>> ReadAsync => _mock._readAsync0Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<(System.Memory<byte> buffer, System.Threading.CancellationToken cancellationToken)>> ReadAsync2 => _mock._readAsync1Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> ReadByte => _mock._readByte0Invocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<(long offset, System.IO.SeekOrigin origin)>> Seek => _mock._seek0Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<long>> SetLength => _mock._setLength0Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<(byte[] buffer, int offset, int count)>> Write => _mock._write0Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<(byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken)>> WriteAsync => _mock._writeAsync0Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<(System.ReadOnlyMemory<byte> buffer, System.Threading.CancellationToken cancellationToken)>> WriteAsync2 => _mock._writeAsync1Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<byte>> WriteByte => _mock._writeByte0Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> CanReadGet => _mock._canRead0GetInvocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> CanSeekGet => _mock._canSeek0GetInvocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> CanTimeoutGet => _mock._canTimeout0GetInvocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> CanWriteGet => _mock._canWrite0GetInvocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> LengthGet => _mock._length0GetInvocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> PositionGet => _mock._position0GetInvocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<long>> PositionSet => _mock._position0SetInvocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> ReadTimeoutGet => _mock._readTimeout0GetInvocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<int>> ReadTimeoutSet => _mock._readTimeout0SetInvocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> WriteTimeoutGet => _mock._writeTimeout0GetInvocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<int>> WriteTimeoutSet => _mock._writeTimeout0SetInvocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> InitializeLifetimeService => _mock._initializeLifetimeService0Invocation?.GetInvocations() ?? [];
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<System.IO.Stream> @this)
					{
						public StreamMock.InvocationContainer Invocations => ((StreamMock)@this).Invocations;

						public void VerifyNoOtherCalls() =>
							((StreamMock)@this).VerifyNoOtherCalls();

						// BeginRead
						public ISetup<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<System.IAsyncResult>.CallbackDelegate, System.IAsyncResult, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<System.IAsyncResult>.ReturnsCallbackDelegate> SetupBeginRead(in It<byte[]> buffer = default, in It<int> offset = default, in It<int> count = default, in It<System.AsyncCallback?> callback = default, in It<object?> state = default) =>
							((StreamMock)@this).SetupBeginRead(buffer, offset, count, callback, state);

						public void VerifyBeginRead(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.AsyncCallback?> callback, in It<object?> state, in Times times) =>
							((StreamMock)@this).VerifyBeginRead(buffer, offset, count, callback, state, times);

						public void VerifyBeginRead(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.AsyncCallback?> callback, in It<object?> state, System.Func<Times> times) =>
							((StreamMock)@this).VerifyBeginRead(buffer, offset, count, callback, state, times());

						// BeginWrite
						public ISetup<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<System.IAsyncResult>.CallbackDelegate, System.IAsyncResult, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<System.IAsyncResult>.ReturnsCallbackDelegate> SetupBeginWrite(in It<byte[]> buffer = default, in It<int> offset = default, in It<int> count = default, in It<System.AsyncCallback?> callback = default, in It<object?> state = default) =>
							((StreamMock)@this).SetupBeginWrite(buffer, offset, count, callback, state);

						public void VerifyBeginWrite(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.AsyncCallback?> callback, in It<object?> state, in Times times) =>
							((StreamMock)@this).VerifyBeginWrite(buffer, offset, count, callback, state, times);

						public void VerifyBeginWrite(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.AsyncCallback?> callback, in It<object?> state, System.Func<Times> times) =>
							((StreamMock)@this).VerifyBeginWrite(buffer, offset, count, callback, state, times());

						// Close
						public ISetup<System.Action> SetupClose() =>
							((StreamMock)@this).SetupClose();

						public void VerifyClose(in Times times) =>
							((StreamMock)@this).VerifyClose(times);

						public void VerifyClose(System.Func<Times> times) =>
							((StreamMock)@this).VerifyClose(times());

						// CopyTo
						public ISetup<SetupStreamInt32.CallbackDelegate> SetupCopyTo(in It<System.IO.Stream> destination = default, in It<int> bufferSize = default) =>
							((StreamMock)@this).SetupCopyTo(destination, bufferSize);

						public void VerifyCopyTo(in It<System.IO.Stream> destination, in It<int> bufferSize, in Times times) =>
							((StreamMock)@this).VerifyCopyTo(destination, bufferSize, times);

						public void VerifyCopyTo(in It<System.IO.Stream> destination, in It<int> bufferSize, System.Func<Times> times) =>
							((StreamMock)@this).VerifyCopyTo(destination, bufferSize, times());

						// CopyToAsync
						public ISetup<SetupStreamInt32CancellationToken.CallbackDelegate> SetupCopyToAsync(in It<System.IO.Stream> destination = default, in It<int> bufferSize = default, in It<System.Threading.CancellationToken> cancellationToken = default) =>
							((StreamMock)@this).SetupCopyToAsync(destination, bufferSize, cancellationToken);

						public void VerifyCopyToAsync(in It<System.IO.Stream> destination, in It<int> bufferSize, in It<System.Threading.CancellationToken> cancellationToken, in Times times) =>
							((StreamMock)@this).VerifyCopyToAsync(destination, bufferSize, cancellationToken, times);

						public void VerifyCopyToAsync(in It<System.IO.Stream> destination, in It<int> bufferSize, in It<System.Threading.CancellationToken> cancellationToken, System.Func<Times> times) =>
							((StreamMock)@this).VerifyCopyToAsync(destination, bufferSize, cancellationToken, times());

						// DisposeAsync
						public ISetup<System.Action> SetupDisposeAsync() =>
							((StreamMock)@this).SetupDisposeAsync();

						public void VerifyDisposeAsync(in Times times) =>
							((StreamMock)@this).VerifyDisposeAsync(times);

						public void VerifyDisposeAsync(System.Func<Times> times) =>
							((StreamMock)@this).VerifyDisposeAsync(times());

						// EndRead
						public ISetup<System.Action<System.IAsyncResult>, int, System.Func<System.IAsyncResult, int>> SetupEndRead(in It<System.IAsyncResult> asyncResult = default) =>
							((StreamMock)@this).SetupEndRead(asyncResult);

						public void VerifyEndRead(in It<System.IAsyncResult> asyncResult, in Times times) =>
							((StreamMock)@this).VerifyEndRead(asyncResult, times);

						public void VerifyEndRead(in It<System.IAsyncResult> asyncResult, System.Func<Times> times) =>
							((StreamMock)@this).VerifyEndRead(asyncResult, times());

						// EndWrite
						public ISetup<System.Action<System.IAsyncResult>> SetupEndWrite(in It<System.IAsyncResult> asyncResult = default) =>
							((StreamMock)@this).SetupEndWrite(asyncResult);

						public void VerifyEndWrite(in It<System.IAsyncResult> asyncResult, in Times times) =>
							((StreamMock)@this).VerifyEndWrite(asyncResult, times);

						public void VerifyEndWrite(in It<System.IAsyncResult> asyncResult, System.Func<Times> times) =>
							((StreamMock)@this).VerifyEndWrite(asyncResult, times());

						// Flush
						public ISetup<System.Action> SetupFlush() =>
							((StreamMock)@this).SetupFlush();

						public void VerifyFlush(in Times times) =>
							((StreamMock)@this).VerifyFlush(times);

						public void VerifyFlush(System.Func<Times> times) =>
							((StreamMock)@this).VerifyFlush(times());

						// FlushAsync
						public ISetup<System.Action<System.Threading.CancellationToken>> SetupFlushAsync(in It<System.Threading.CancellationToken> cancellationToken = default) =>
							((StreamMock)@this).SetupFlushAsync(cancellationToken);

						public void VerifyFlushAsync(in It<System.Threading.CancellationToken> cancellationToken, in Times times) =>
							((StreamMock)@this).VerifyFlushAsync(cancellationToken, times);

						public void VerifyFlushAsync(in It<System.Threading.CancellationToken> cancellationToken, System.Func<Times> times) =>
							((StreamMock)@this).VerifyFlushAsync(cancellationToken, times());

						// Read
						public ISetup<SetupArray1ByteInt32Int32<int>.CallbackDelegate, int, SetupArray1ByteInt32Int32<int>.ReturnsCallbackDelegate> SetupRead(in It<byte[]> buffer = default, in It<int> offset = default, in It<int> count = default) =>
							((StreamMock)@this).SetupRead(buffer, offset, count);

						public void VerifyRead(in It<byte[]> buffer, in It<int> offset, in It<int> count, in Times times) =>
							((StreamMock)@this).VerifyRead(buffer, offset, count, times);

						public void VerifyRead(in It<byte[]> buffer, in It<int> offset, in It<int> count, System.Func<Times> times) =>
							((StreamMock)@this).VerifyRead(buffer, offset, count, times());

						// ReadAsync
						public ISetup<SetupArray1ByteInt32Int32CancellationToken<int>.CallbackDelegate, int, SetupArray1ByteInt32Int32CancellationToken<int>.ReturnsCallbackDelegate> SetupReadAsync(in It<byte[]> buffer = default, in It<int> offset = default, in It<int> count = default, in It<System.Threading.CancellationToken> cancellationToken = default) =>
							((StreamMock)@this).SetupReadAsync(buffer, offset, count, cancellationToken);

						public void VerifyReadAsync(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.Threading.CancellationToken> cancellationToken, in Times times) =>
							((StreamMock)@this).VerifyReadAsync(buffer, offset, count, cancellationToken, times);

						public void VerifyReadAsync(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.Threading.CancellationToken> cancellationToken, System.Func<Times> times) =>
							((StreamMock)@this).VerifyReadAsync(buffer, offset, count, cancellationToken, times());

						// ReadAsync
						public ISetup<SetupMemoryByteCancellationToken<int>.CallbackDelegate, int, SetupMemoryByteCancellationToken<int>.ReturnsCallbackDelegate> SetupReadAsync(in It<System.Memory<byte>> buffer = default, in It<System.Threading.CancellationToken> cancellationToken = default) =>
							((StreamMock)@this).SetupReadAsync(buffer, cancellationToken);

						public void VerifyReadAsync(in It<System.Memory<byte>> buffer, in It<System.Threading.CancellationToken> cancellationToken, in Times times) =>
							((StreamMock)@this).VerifyReadAsync(buffer, cancellationToken, times);

						public void VerifyReadAsync(in It<System.Memory<byte>> buffer, in It<System.Threading.CancellationToken> cancellationToken, System.Func<Times> times) =>
							((StreamMock)@this).VerifyReadAsync(buffer, cancellationToken, times());

						// ReadByte
						public ISetup<System.Action, int, System.Func<int>> SetupReadByte() =>
							((StreamMock)@this).SetupReadByte();

						public void VerifyReadByte(in Times times) =>
							((StreamMock)@this).VerifyReadByte(times);

						public void VerifyReadByte(System.Func<Times> times) =>
							((StreamMock)@this).VerifyReadByte(times());

						// Seek
						public ISetup<SetupInt64SeekOrigin<long>.CallbackDelegate, long, SetupInt64SeekOrigin<long>.ReturnsCallbackDelegate> SetupSeek(in It<long> offset = default, in It<System.IO.SeekOrigin> origin = default) =>
							((StreamMock)@this).SetupSeek(offset, origin);

						public void VerifySeek(in It<long> offset, in It<System.IO.SeekOrigin> origin, in Times times) =>
							((StreamMock)@this).VerifySeek(offset, origin, times);

						public void VerifySeek(in It<long> offset, in It<System.IO.SeekOrigin> origin, System.Func<Times> times) =>
							((StreamMock)@this).VerifySeek(offset, origin, times());

						// SetLength
						public ISetup<System.Action<long>> SetupSetLength(in It<long> value = default) =>
							((StreamMock)@this).SetupSetLength(value);

						public void VerifySetLength(in It<long> value, in Times times) =>
							((StreamMock)@this).VerifySetLength(value, times);

						public void VerifySetLength(in It<long> value, System.Func<Times> times) =>
							((StreamMock)@this).VerifySetLength(value, times());

						// Write
						public ISetup<SetupArray1ByteInt32Int32.CallbackDelegate> SetupWrite(in It<byte[]> buffer = default, in It<int> offset = default, in It<int> count = default) =>
							((StreamMock)@this).SetupWrite(buffer, offset, count);

						public void VerifyWrite(in It<byte[]> buffer, in It<int> offset, in It<int> count, in Times times) =>
							((StreamMock)@this).VerifyWrite(buffer, offset, count, times);

						public void VerifyWrite(in It<byte[]> buffer, in It<int> offset, in It<int> count, System.Func<Times> times) =>
							((StreamMock)@this).VerifyWrite(buffer, offset, count, times());

						// WriteAsync
						public ISetup<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate> SetupWriteAsync(in It<byte[]> buffer = default, in It<int> offset = default, in It<int> count = default, in It<System.Threading.CancellationToken> cancellationToken = default) =>
							((StreamMock)@this).SetupWriteAsync(buffer, offset, count, cancellationToken);

						public void VerifyWriteAsync(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.Threading.CancellationToken> cancellationToken, in Times times) =>
							((StreamMock)@this).VerifyWriteAsync(buffer, offset, count, cancellationToken, times);

						public void VerifyWriteAsync(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.Threading.CancellationToken> cancellationToken, System.Func<Times> times) =>
							((StreamMock)@this).VerifyWriteAsync(buffer, offset, count, cancellationToken, times());

						// WriteAsync
						public ISetup<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate> SetupWriteAsync(in It<System.ReadOnlyMemory<byte>> buffer = default, in It<System.Threading.CancellationToken> cancellationToken = default) =>
							((StreamMock)@this).SetupWriteAsync(buffer, cancellationToken);

						public void VerifyWriteAsync(in It<System.ReadOnlyMemory<byte>> buffer, in It<System.Threading.CancellationToken> cancellationToken, in Times times) =>
							((StreamMock)@this).VerifyWriteAsync(buffer, cancellationToken, times);

						public void VerifyWriteAsync(in It<System.ReadOnlyMemory<byte>> buffer, in It<System.Threading.CancellationToken> cancellationToken, System.Func<Times> times) =>
							((StreamMock)@this).VerifyWriteAsync(buffer, cancellationToken, times());

						// WriteByte
						public ISetup<System.Action<byte>> SetupWriteByte(in It<byte> value = default) =>
							((StreamMock)@this).SetupWriteByte(value);

						public void VerifyWriteByte(in It<byte> value, in Times times) =>
							((StreamMock)@this).VerifyWriteByte(value, times);

						public void VerifyWriteByte(in It<byte> value, System.Func<Times> times) =>
							((StreamMock)@this).VerifyWriteByte(value, times());

						// CanRead
						public ISetup<System.Action, bool, System.Func<bool>> SetupGetCanRead() =>
							((StreamMock)@this).SetupGetCanRead();

						public void VerifyGetCanRead(in Times times) =>
							((StreamMock)@this).VerifyGetCanRead(times);

						public void VerifyGetCanRead(System.Func<Times> times) =>
							((StreamMock)@this).VerifyGetCanRead(times());

						// CanSeek
						public ISetup<System.Action, bool, System.Func<bool>> SetupGetCanSeek() =>
							((StreamMock)@this).SetupGetCanSeek();

						public void VerifyGetCanSeek(in Times times) =>
							((StreamMock)@this).VerifyGetCanSeek(times);

						public void VerifyGetCanSeek(System.Func<Times> times) =>
							((StreamMock)@this).VerifyGetCanSeek(times());

						// CanTimeout
						public ISetup<System.Action, bool, System.Func<bool>> SetupGetCanTimeout() =>
							((StreamMock)@this).SetupGetCanTimeout();

						public void VerifyGetCanTimeout(in Times times) =>
							((StreamMock)@this).VerifyGetCanTimeout(times);

						public void VerifyGetCanTimeout(System.Func<Times> times) =>
							((StreamMock)@this).VerifyGetCanTimeout(times());

						// CanWrite
						public ISetup<System.Action, bool, System.Func<bool>> SetupGetCanWrite() =>
							((StreamMock)@this).SetupGetCanWrite();

						public void VerifyGetCanWrite(in Times times) =>
							((StreamMock)@this).VerifyGetCanWrite(times);

						public void VerifyGetCanWrite(System.Func<Times> times) =>
							((StreamMock)@this).VerifyGetCanWrite(times());

						// Length
						public ISetup<System.Action, long, System.Func<long>> SetupGetLength() =>
							((StreamMock)@this).SetupGetLength();

						public void VerifyGetLength(in Times times) =>
							((StreamMock)@this).VerifyGetLength(times);

						public void VerifyGetLength(System.Func<Times> times) =>
							((StreamMock)@this).VerifyGetLength(times());

						// Position
						public ISetup<System.Action, long, System.Func<long>> SetupGetPosition() =>
							((StreamMock)@this).SetupGetPosition();

						public void VerifyGetPosition(in Times times) =>
							((StreamMock)@this).VerifyGetPosition(times);

						public void VerifyGetPosition(System.Func<Times> times) =>
							((StreamMock)@this).VerifyGetPosition(times());

						public ISetup<System.Action<long>> SetupSetPosition(in It<long> value = default) =>
							((StreamMock)@this).SetupSetPosition(value);

						public void VerifySetPosition(in It<long> value, in Times times) =>
							((StreamMock)@this).VerifySetPosition(value, times);

						public void VerifySetPosition(in It<long> value, System.Func<Times> times) =>
							((StreamMock)@this).VerifySetPosition(value, times());

						// ReadTimeout
						public ISetup<System.Action, int, System.Func<int>> SetupGetReadTimeout() =>
							((StreamMock)@this).SetupGetReadTimeout();

						public void VerifyGetReadTimeout(in Times times) =>
							((StreamMock)@this).VerifyGetReadTimeout(times);

						public void VerifyGetReadTimeout(System.Func<Times> times) =>
							((StreamMock)@this).VerifyGetReadTimeout(times());

						public ISetup<System.Action<int>> SetupSetReadTimeout(in It<int> value = default) =>
							((StreamMock)@this).SetupSetReadTimeout(value);

						public void VerifySetReadTimeout(in It<int> value, in Times times) =>
							((StreamMock)@this).VerifySetReadTimeout(value, times);

						public void VerifySetReadTimeout(in It<int> value, System.Func<Times> times) =>
							((StreamMock)@this).VerifySetReadTimeout(value, times());

						// WriteTimeout
						public ISetup<System.Action, int, System.Func<int>> SetupGetWriteTimeout() =>
							((StreamMock)@this).SetupGetWriteTimeout();

						public void VerifyGetWriteTimeout(in Times times) =>
							((StreamMock)@this).VerifyGetWriteTimeout(times);

						public void VerifyGetWriteTimeout(System.Func<Times> times) =>
							((StreamMock)@this).VerifyGetWriteTimeout(times());

						public ISetup<System.Action<int>> SetupSetWriteTimeout(in It<int> value = default) =>
							((StreamMock)@this).SetupSetWriteTimeout(value);

						public void VerifySetWriteTimeout(in It<int> value, in Times times) =>
							((StreamMock)@this).VerifySetWriteTimeout(value, times);

						public void VerifySetWriteTimeout(in It<int> value, System.Func<Times> times) =>
							((StreamMock)@this).VerifySetWriteTimeout(value, times());

						// InitializeLifetimeService
						public ISetup<System.Action, object, System.Func<object>> SetupInitializeLifetimeService() =>
							((StreamMock)@this).SetupInitializeLifetimeService();

						public void VerifyInitializeLifetimeService(in Times times) =>
							((StreamMock)@this).VerifyInitializeLifetimeService(times);

						public void VerifyInitializeLifetimeService(System.Func<Times> times) =>
							((StreamMock)@this).VerifyInitializeLifetimeService(times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<System.IO.Stream> @this)
					{
						// BeginRead
						public void BeginRead(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.AsyncCallback?> callback, in It<object?> state)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyBeginRead(buffer, offset, count, callback, state, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// BeginWrite
						public void BeginWrite(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.AsyncCallback?> callback, in It<object?> state)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyBeginWrite(buffer, offset, count, callback, state, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// Close
						public void Close()
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyClose(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// CopyTo
						public void CopyTo(in It<System.IO.Stream> destination, in It<int> bufferSize)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyCopyTo(destination, bufferSize, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// CopyToAsync
						public void CopyToAsync(in It<System.IO.Stream> destination, in It<int> bufferSize, in It<System.Threading.CancellationToken> cancellationToken)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyCopyToAsync(destination, bufferSize, cancellationToken, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// DisposeAsync
						public void DisposeAsync()
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyDisposeAsync(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// EndRead
						public void EndRead(in It<System.IAsyncResult> asyncResult)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyEndRead(asyncResult, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// EndWrite
						public void EndWrite(in It<System.IAsyncResult> asyncResult)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyEndWrite(asyncResult, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// Flush
						public void Flush()
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyFlush(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// FlushAsync
						public void FlushAsync(in It<System.Threading.CancellationToken> cancellationToken)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyFlushAsync(cancellationToken, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// Read
						public void Read(in It<byte[]> buffer, in It<int> offset, in It<int> count)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyRead(buffer, offset, count, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// ReadAsync
						public void ReadAsync(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.Threading.CancellationToken> cancellationToken)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyReadAsync(buffer, offset, count, cancellationToken, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// ReadAsync
						public void ReadAsync(in It<System.Memory<byte>> buffer, in It<System.Threading.CancellationToken> cancellationToken)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyReadAsync(buffer, cancellationToken, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// ReadByte
						public void ReadByte()
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyReadByte(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// Seek
						public void Seek(in It<long> offset, in It<System.IO.SeekOrigin> origin)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifySeek(offset, origin, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// SetLength
						public void SetLength(in It<long> value)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifySetLength(value, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// Write
						public void Write(in It<byte[]> buffer, in It<int> offset, in It<int> count)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyWrite(buffer, offset, count, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// WriteAsync
						public void WriteAsync(in It<byte[]> buffer, in It<int> offset, in It<int> count, in It<System.Threading.CancellationToken> cancellationToken)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyWriteAsync(buffer, offset, count, cancellationToken, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// WriteAsync
						public void WriteAsync(in It<System.ReadOnlyMemory<byte>> buffer, in It<System.Threading.CancellationToken> cancellationToken)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyWriteAsync(buffer, cancellationToken, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// WriteByte
						public void WriteByte(in It<byte> value)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyWriteByte(value, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// CanRead
						public void GetCanRead()
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyGetCanRead(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// CanSeek
						public void GetCanSeek()
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyGetCanSeek(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// CanTimeout
						public void GetCanTimeout()
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyGetCanTimeout(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// CanWrite
						public void GetCanWrite()
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyGetCanWrite(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// Length
						public void GetLength()
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyGetLength(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// Position
						public void GetPosition()
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyGetPosition(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						public void SetPosition(in It<long> value)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifySetPosition(value, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// ReadTimeout
						public void GetReadTimeout()
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyGetReadTimeout(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						public void SetReadTimeout(in It<int> value)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifySetReadTimeout(value, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// WriteTimeout
						public void GetWriteTimeout()
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyGetWriteTimeout(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						public void SetWriteTimeout(in It<int> value)
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifySetWriteTimeout(value, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// InitializeLifetimeService
						public void InitializeLifetimeService()
						{
							var nextIndex = ((StreamMock)@this.Mock).VerifyInitializeLifetimeService(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			(
				"InvocationArray1ByteInt32Int32.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InvocationArray1ByteInt32Int32 : IInvocationVerify
				{
					private readonly string _name;
					private readonly string? _parameter1Prefix, _parameter2Prefix, _parameter3Prefix;
					private readonly InvocationContainer<Item> _invocations = [];

					public InvocationArray1ByteInt32Int32(string name, string? parameter1Prefix = null, string? parameter2Prefix = null, string? parameter3Prefix = null)
					{
						_name = name;
						_parameter1Prefix = parameter1Prefix;
						_parameter2Prefix = parameter2Prefix;
						_parameter3Prefix = parameter3Prefix;
					}

					public void Register(in InvocationIndex.Counter index, byte[] parameter1, int parameter2, int parameter3)
					{
						var invokedIndex = index.Increment();
						_invocations.Add(new Item(invokedIndex, parameter1, parameter2, parameter3, invocation: this));
					}

					public void Verify(in ItSetup<byte[]> parameter1, in ItSetup<int> parameter2, in ItSetup<int> parameter3, in Times times, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpan();

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						var count = 0;
						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							var verifyParameter3 = span[i].GetParameter3(parameter3.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}
							if (!parameter3.Check(verifyParameter3, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter3", result)]
									: [("parameter3", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							count++;
						}

						if (times.Predicate(count))
							return;

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix), parameter3.ToString(_parameter3Prefix));
						throw new MockVerifyCountException(verifyName, times, count, invocations);
					}

					public long Verify(in ItSetup<byte[]> parameter1, in ItSetup<int> parameter2, in ItSetup<int> parameter3, long index, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpanFrom(index);

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							var verifyParameter3 = span[i].GetParameter3(parameter3.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}
							if (!parameter3.Check(verifyParameter3, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter3", result)]
									: [("parameter3", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							return span[i].Index + 1;
						}

						if (invocationProviders is null)
						{
							span = _invocations.GetItemsSpanBefore(index);
							for (var i = 0; i < span.Length; i++)
								verifyOutput.Insert(i, (span[i], null));
						}

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix), parameter3.ToString(_parameter3Prefix));
						throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
					}

					public void VerifyNoOtherCalls(System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
						if (unverifiedItems is null)
							return;

						var typeNameParameter1 = !string.IsNullOrEmpty(_parameter1Prefix) ? $"{_parameter1Prefix} byte[]" : "byte[]";
						var typeNameParameter2 = !string.IsNullOrEmpty(_parameter2Prefix) ? $"{_parameter2Prefix} int" : "int";
						var typeNameParameter3 = !string.IsNullOrEmpty(_parameter3Prefix) ? $"{_parameter3Prefix} int" : "int";
						var verifyName = string.Format(_name, typeNameParameter1, typeNameParameter2, typeNameParameter3);
						throw new MockUnverifiedException(verifyName, unverifiedItems);
					}

					public System.Collections.Generic.IEnumerable<IInvocation> GetInvocations()
					{
						return _invocations;
					}

					public System.Collections.Generic.IEnumerable<IInvocation<(byte[] parameter1, int parameter2, int parameter3)>> GetInvocationsWithArguments()
					{
						return _invocations;
					}

					private sealed class Item : IInvocation<(byte[] parameter1, int parameter2, int parameter3)>
					{
						private readonly (byte[] parameter1, int parameter2, int parameter3) _argument;
						private readonly string? _jsonSnapshotParameter1, _jsonSnapshotParameter2, _jsonSnapshotParameter3;
						private readonly InvocationArray1ByteInt32Int32 _invocation;

						public Item(long index, byte[] parameter1, int parameter2, int parameter3, InvocationArray1ByteInt32Int32 invocation)
						{
							_argument = (parameter1, parameter2, parameter3);
							_invocation = invocation;
							Index = index;

							try
							{
								_jsonSnapshotParameter1 = parameter1.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter2 = parameter2.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter3 = parameter3.SerializeToJson();
							}
							catch
							{
								// Swallow
							}
						}

						public long Index { get; }

						public bool IsVerified { get; set; }

						public (byte[] parameter1, int parameter2, int parameter3) Arguments => _argument;

						public byte[] GetParameter1(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter1)
								? _jsonSnapshotParameter1.DeserializeFromJson(_argument.parameter1)
								: _argument.parameter1;
						}

						public int GetParameter2(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter2)
								? _jsonSnapshotParameter2.DeserializeFromJson(_argument.parameter2)
								: _argument.parameter2;
						}

						public int GetParameter3(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter3)
								? _jsonSnapshotParameter3.DeserializeFromJson(_argument.parameter3)
								: _argument.parameter3;
						}

						public override string ToString()
						{
							var stringBuilder = new System.Text.StringBuilder();

							// parameter1
							if (!string.IsNullOrEmpty(_invocation._parameter1Prefix))
								stringBuilder.Append($"{_invocation._parameter1Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter1))
								stringBuilder.Append(_jsonSnapshotParameter1);
							else
								stringBuilder.Append(_argument.parameter1);
							var parameter1 = stringBuilder.ToString();

							// parameter2
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter2Prefix))
								stringBuilder.Append($"{_invocation._parameter2Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter2))
								stringBuilder.Append(_jsonSnapshotParameter2);
							else
								stringBuilder.Append(_argument.parameter2);
							var parameter2 = stringBuilder.ToString();

							// parameter3
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter3Prefix))
								stringBuilder.Append($"{_invocation._parameter3Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter3))
								stringBuilder.Append(_jsonSnapshotParameter3);
							else
								stringBuilder.Append(_argument.parameter3);
							var parameter3 = stringBuilder.ToString();

							var stringValue = string.Format(_invocation._name, parameter1, parameter2, parameter3);
							return $"{Index}: {stringValue}";
						}
					}
				}
				"""
			),
			(
				"InvocationArray1ByteInt32Int32AsyncCallbackNullableObjectNullable.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InvocationArray1ByteInt32Int32AsyncCallbackNullableObjectNullable : IInvocationVerify
				{
					private readonly string _name;
					private readonly string? _parameter1Prefix, _parameter2Prefix, _parameter3Prefix, _parameter4Prefix, _parameter5Prefix;
					private readonly InvocationContainer<Item> _invocations = [];

					public InvocationArray1ByteInt32Int32AsyncCallbackNullableObjectNullable(string name, string? parameter1Prefix = null, string? parameter2Prefix = null, string? parameter3Prefix = null, string? parameter4Prefix = null, string? parameter5Prefix = null)
					{
						_name = name;
						_parameter1Prefix = parameter1Prefix;
						_parameter2Prefix = parameter2Prefix;
						_parameter3Prefix = parameter3Prefix;
						_parameter4Prefix = parameter4Prefix;
						_parameter5Prefix = parameter5Prefix;
					}

					public void Register(in InvocationIndex.Counter index, byte[] parameter1, int parameter2, int parameter3, System.AsyncCallback? parameter4, object? parameter5)
					{
						var invokedIndex = index.Increment();
						_invocations.Add(new Item(invokedIndex, parameter1, parameter2, parameter3, parameter4, parameter5, invocation: this));
					}

					public void Verify(in ItSetup<byte[]> parameter1, in ItSetup<int> parameter2, in ItSetup<int> parameter3, in ItSetup<System.AsyncCallback?> parameter4, in ItSetup<object?> parameter5, in Times times, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpan();

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						var count = 0;
						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							var verifyParameter3 = span[i].GetParameter3(parameter3.Type);
							var verifyParameter4 = span[i].GetParameter4(parameter4.Type);
							var verifyParameter5 = span[i].GetParameter5(parameter5.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}
							if (!parameter3.Check(verifyParameter3, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter3", result)]
									: [("parameter3", result)];
							}
							if (!parameter4.Check(verifyParameter4, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter4", result)]
									: [("parameter4", result)];
							}
							if (!parameter5.Check(verifyParameter5, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter5", result)]
									: [("parameter5", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							count++;
						}

						if (times.Predicate(count))
							return;

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix), parameter3.ToString(_parameter3Prefix), parameter4.ToString(_parameter4Prefix), parameter5.ToString(_parameter5Prefix));
						throw new MockVerifyCountException(verifyName, times, count, invocations);
					}

					public long Verify(in ItSetup<byte[]> parameter1, in ItSetup<int> parameter2, in ItSetup<int> parameter3, in ItSetup<System.AsyncCallback?> parameter4, in ItSetup<object?> parameter5, long index, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpanFrom(index);

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							var verifyParameter3 = span[i].GetParameter3(parameter3.Type);
							var verifyParameter4 = span[i].GetParameter4(parameter4.Type);
							var verifyParameter5 = span[i].GetParameter5(parameter5.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}
							if (!parameter3.Check(verifyParameter3, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter3", result)]
									: [("parameter3", result)];
							}
							if (!parameter4.Check(verifyParameter4, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter4", result)]
									: [("parameter4", result)];
							}
							if (!parameter5.Check(verifyParameter5, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter5", result)]
									: [("parameter5", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							return span[i].Index + 1;
						}

						if (invocationProviders is null)
						{
							span = _invocations.GetItemsSpanBefore(index);
							for (var i = 0; i < span.Length; i++)
								verifyOutput.Insert(i, (span[i], null));
						}

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix), parameter3.ToString(_parameter3Prefix), parameter4.ToString(_parameter4Prefix), parameter5.ToString(_parameter5Prefix));
						throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
					}

					public void VerifyNoOtherCalls(System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
						if (unverifiedItems is null)
							return;

						var typeNameParameter1 = !string.IsNullOrEmpty(_parameter1Prefix) ? $"{_parameter1Prefix} byte[]" : "byte[]";
						var typeNameParameter2 = !string.IsNullOrEmpty(_parameter2Prefix) ? $"{_parameter2Prefix} int" : "int";
						var typeNameParameter3 = !string.IsNullOrEmpty(_parameter3Prefix) ? $"{_parameter3Prefix} int" : "int";
						var typeNameParameter4 = !string.IsNullOrEmpty(_parameter4Prefix) ? $"{_parameter4Prefix} System.AsyncCallback?" : "System.AsyncCallback?";
						var typeNameParameter5 = !string.IsNullOrEmpty(_parameter5Prefix) ? $"{_parameter5Prefix} object?" : "object?";
						var verifyName = string.Format(_name, typeNameParameter1, typeNameParameter2, typeNameParameter3, typeNameParameter4, typeNameParameter5);
						throw new MockUnverifiedException(verifyName, unverifiedItems);
					}

					public System.Collections.Generic.IEnumerable<IInvocation> GetInvocations()
					{
						return _invocations;
					}

					public System.Collections.Generic.IEnumerable<IInvocation<(byte[] parameter1, int parameter2, int parameter3, System.AsyncCallback? parameter4, object? parameter5)>> GetInvocationsWithArguments()
					{
						return _invocations;
					}

					private sealed class Item : IInvocation<(byte[] parameter1, int parameter2, int parameter3, System.AsyncCallback? parameter4, object? parameter5)>
					{
						private readonly (byte[] parameter1, int parameter2, int parameter3, System.AsyncCallback? parameter4, object? parameter5) _argument;
						private readonly string? _jsonSnapshotParameter1, _jsonSnapshotParameter2, _jsonSnapshotParameter3, _jsonSnapshotParameter4, _jsonSnapshotParameter5;
						private readonly InvocationArray1ByteInt32Int32AsyncCallbackNullableObjectNullable _invocation;

						public Item(long index, byte[] parameter1, int parameter2, int parameter3, System.AsyncCallback? parameter4, object? parameter5, InvocationArray1ByteInt32Int32AsyncCallbackNullableObjectNullable invocation)
						{
							_argument = (parameter1, parameter2, parameter3, parameter4, parameter5);
							_invocation = invocation;
							Index = index;

							try
							{
								_jsonSnapshotParameter1 = parameter1.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter2 = parameter2.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter3 = parameter3.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter4 = parameter4.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter5 = parameter5.SerializeToJson();
							}
							catch
							{
								// Swallow
							}
						}

						public long Index { get; }

						public bool IsVerified { get; set; }

						public (byte[] parameter1, int parameter2, int parameter3, System.AsyncCallback? parameter4, object? parameter5) Arguments => _argument;

						public byte[] GetParameter1(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter1)
								? _jsonSnapshotParameter1.DeserializeFromJson(_argument.parameter1)
								: _argument.parameter1;
						}

						public int GetParameter2(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter2)
								? _jsonSnapshotParameter2.DeserializeFromJson(_argument.parameter2)
								: _argument.parameter2;
						}

						public int GetParameter3(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter3)
								? _jsonSnapshotParameter3.DeserializeFromJson(_argument.parameter3)
								: _argument.parameter3;
						}

						public System.AsyncCallback? GetParameter4(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter4)
								? _jsonSnapshotParameter4.DeserializeFromJson(_argument.parameter4)
								: _argument.parameter4;
						}

						public object? GetParameter5(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter5)
								? _jsonSnapshotParameter5.DeserializeFromJson(_argument.parameter5)
								: _argument.parameter5;
						}

						public override string ToString()
						{
							var stringBuilder = new System.Text.StringBuilder();

							// parameter1
							if (!string.IsNullOrEmpty(_invocation._parameter1Prefix))
								stringBuilder.Append($"{_invocation._parameter1Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter1))
								stringBuilder.Append(_jsonSnapshotParameter1);
							else
								stringBuilder.Append(_argument.parameter1);
							var parameter1 = stringBuilder.ToString();

							// parameter2
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter2Prefix))
								stringBuilder.Append($"{_invocation._parameter2Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter2))
								stringBuilder.Append(_jsonSnapshotParameter2);
							else
								stringBuilder.Append(_argument.parameter2);
							var parameter2 = stringBuilder.ToString();

							// parameter3
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter3Prefix))
								stringBuilder.Append($"{_invocation._parameter3Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter3))
								stringBuilder.Append(_jsonSnapshotParameter3);
							else
								stringBuilder.Append(_argument.parameter3);
							var parameter3 = stringBuilder.ToString();

							// parameter4
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter4Prefix))
								stringBuilder.Append($"{_invocation._parameter4Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter4))
								stringBuilder.Append(_jsonSnapshotParameter4);
							else
								stringBuilder.Append(_argument.parameter4);
							var parameter4 = stringBuilder.ToString();

							// parameter5
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter5Prefix))
								stringBuilder.Append($"{_invocation._parameter5Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter5))
								stringBuilder.Append(_jsonSnapshotParameter5);
							else
								stringBuilder.Append(_argument.parameter5);
							var parameter5 = stringBuilder.ToString();

							var stringValue = string.Format(_invocation._name, parameter1, parameter2, parameter3, parameter4, parameter5);
							return $"{Index}: {stringValue}";
						}
					}
				}
				"""
			),
			(
				"InvocationArray1ByteInt32Int32CancellationToken.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InvocationArray1ByteInt32Int32CancellationToken : IInvocationVerify
				{
					private readonly string _name;
					private readonly string? _parameter1Prefix, _parameter2Prefix, _parameter3Prefix, _parameter4Prefix;
					private readonly InvocationContainer<Item> _invocations = [];

					public InvocationArray1ByteInt32Int32CancellationToken(string name, string? parameter1Prefix = null, string? parameter2Prefix = null, string? parameter3Prefix = null, string? parameter4Prefix = null)
					{
						_name = name;
						_parameter1Prefix = parameter1Prefix;
						_parameter2Prefix = parameter2Prefix;
						_parameter3Prefix = parameter3Prefix;
						_parameter4Prefix = parameter4Prefix;
					}

					public void Register(in InvocationIndex.Counter index, byte[] parameter1, int parameter2, int parameter3, System.Threading.CancellationToken parameter4)
					{
						var invokedIndex = index.Increment();
						_invocations.Add(new Item(invokedIndex, parameter1, parameter2, parameter3, parameter4, invocation: this));
					}

					public void Verify(in ItSetup<byte[]> parameter1, in ItSetup<int> parameter2, in ItSetup<int> parameter3, in ItSetup<System.Threading.CancellationToken> parameter4, in Times times, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpan();

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						var count = 0;
						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							var verifyParameter3 = span[i].GetParameter3(parameter3.Type);
							var verifyParameter4 = span[i].GetParameter4(parameter4.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}
							if (!parameter3.Check(verifyParameter3, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter3", result)]
									: [("parameter3", result)];
							}
							if (!parameter4.Check(verifyParameter4, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter4", result)]
									: [("parameter4", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							count++;
						}

						if (times.Predicate(count))
							return;

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix), parameter3.ToString(_parameter3Prefix), parameter4.ToString(_parameter4Prefix));
						throw new MockVerifyCountException(verifyName, times, count, invocations);
					}

					public long Verify(in ItSetup<byte[]> parameter1, in ItSetup<int> parameter2, in ItSetup<int> parameter3, in ItSetup<System.Threading.CancellationToken> parameter4, long index, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpanFrom(index);

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							var verifyParameter3 = span[i].GetParameter3(parameter3.Type);
							var verifyParameter4 = span[i].GetParameter4(parameter4.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}
							if (!parameter3.Check(verifyParameter3, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter3", result)]
									: [("parameter3", result)];
							}
							if (!parameter4.Check(verifyParameter4, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter4", result)]
									: [("parameter4", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							return span[i].Index + 1;
						}

						if (invocationProviders is null)
						{
							span = _invocations.GetItemsSpanBefore(index);
							for (var i = 0; i < span.Length; i++)
								verifyOutput.Insert(i, (span[i], null));
						}

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix), parameter3.ToString(_parameter3Prefix), parameter4.ToString(_parameter4Prefix));
						throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
					}

					public void VerifyNoOtherCalls(System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
						if (unverifiedItems is null)
							return;

						var typeNameParameter1 = !string.IsNullOrEmpty(_parameter1Prefix) ? $"{_parameter1Prefix} byte[]" : "byte[]";
						var typeNameParameter2 = !string.IsNullOrEmpty(_parameter2Prefix) ? $"{_parameter2Prefix} int" : "int";
						var typeNameParameter3 = !string.IsNullOrEmpty(_parameter3Prefix) ? $"{_parameter3Prefix} int" : "int";
						var typeNameParameter4 = !string.IsNullOrEmpty(_parameter4Prefix) ? $"{_parameter4Prefix} System.Threading.CancellationToken" : "System.Threading.CancellationToken";
						var verifyName = string.Format(_name, typeNameParameter1, typeNameParameter2, typeNameParameter3, typeNameParameter4);
						throw new MockUnverifiedException(verifyName, unverifiedItems);
					}

					public System.Collections.Generic.IEnumerable<IInvocation> GetInvocations()
					{
						return _invocations;
					}

					public System.Collections.Generic.IEnumerable<IInvocation<(byte[] parameter1, int parameter2, int parameter3, System.Threading.CancellationToken parameter4)>> GetInvocationsWithArguments()
					{
						return _invocations;
					}

					private sealed class Item : IInvocation<(byte[] parameter1, int parameter2, int parameter3, System.Threading.CancellationToken parameter4)>
					{
						private readonly (byte[] parameter1, int parameter2, int parameter3, System.Threading.CancellationToken parameter4) _argument;
						private readonly string? _jsonSnapshotParameter1, _jsonSnapshotParameter2, _jsonSnapshotParameter3, _jsonSnapshotParameter4;
						private readonly InvocationArray1ByteInt32Int32CancellationToken _invocation;

						public Item(long index, byte[] parameter1, int parameter2, int parameter3, System.Threading.CancellationToken parameter4, InvocationArray1ByteInt32Int32CancellationToken invocation)
						{
							_argument = (parameter1, parameter2, parameter3, parameter4);
							_invocation = invocation;
							Index = index;

							try
							{
								_jsonSnapshotParameter1 = parameter1.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter2 = parameter2.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter3 = parameter3.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter4 = parameter4.SerializeToJson();
							}
							catch
							{
								// Swallow
							}
						}

						public long Index { get; }

						public bool IsVerified { get; set; }

						public (byte[] parameter1, int parameter2, int parameter3, System.Threading.CancellationToken parameter4) Arguments => _argument;

						public byte[] GetParameter1(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter1)
								? _jsonSnapshotParameter1.DeserializeFromJson(_argument.parameter1)
								: _argument.parameter1;
						}

						public int GetParameter2(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter2)
								? _jsonSnapshotParameter2.DeserializeFromJson(_argument.parameter2)
								: _argument.parameter2;
						}

						public int GetParameter3(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter3)
								? _jsonSnapshotParameter3.DeserializeFromJson(_argument.parameter3)
								: _argument.parameter3;
						}

						public System.Threading.CancellationToken GetParameter4(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter4)
								? _jsonSnapshotParameter4.DeserializeFromJson(_argument.parameter4)
								: _argument.parameter4;
						}

						public override string ToString()
						{
							var stringBuilder = new System.Text.StringBuilder();

							// parameter1
							if (!string.IsNullOrEmpty(_invocation._parameter1Prefix))
								stringBuilder.Append($"{_invocation._parameter1Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter1))
								stringBuilder.Append(_jsonSnapshotParameter1);
							else
								stringBuilder.Append(_argument.parameter1);
							var parameter1 = stringBuilder.ToString();

							// parameter2
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter2Prefix))
								stringBuilder.Append($"{_invocation._parameter2Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter2))
								stringBuilder.Append(_jsonSnapshotParameter2);
							else
								stringBuilder.Append(_argument.parameter2);
							var parameter2 = stringBuilder.ToString();

							// parameter3
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter3Prefix))
								stringBuilder.Append($"{_invocation._parameter3Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter3))
								stringBuilder.Append(_jsonSnapshotParameter3);
							else
								stringBuilder.Append(_argument.parameter3);
							var parameter3 = stringBuilder.ToString();

							// parameter4
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter4Prefix))
								stringBuilder.Append($"{_invocation._parameter4Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter4))
								stringBuilder.Append(_jsonSnapshotParameter4);
							else
								stringBuilder.Append(_argument.parameter4);
							var parameter4 = stringBuilder.ToString();

							var stringValue = string.Format(_invocation._name, parameter1, parameter2, parameter3, parameter4);
							return $"{Index}: {stringValue}";
						}
					}
				}
				"""
			),
			(
				"InvocationInt64SeekOrigin.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InvocationInt64SeekOrigin : IInvocationVerify
				{
					private readonly string _name;
					private readonly string? _parameter1Prefix, _parameter2Prefix;
					private readonly InvocationContainer<Item> _invocations = [];

					public InvocationInt64SeekOrigin(string name, string? parameter1Prefix = null, string? parameter2Prefix = null)
					{
						_name = name;
						_parameter1Prefix = parameter1Prefix;
						_parameter2Prefix = parameter2Prefix;
					}

					public void Register(in InvocationIndex.Counter index, long parameter1, System.IO.SeekOrigin parameter2)
					{
						var invokedIndex = index.Increment();
						_invocations.Add(new Item(invokedIndex, parameter1, parameter2, invocation: this));
					}

					public void Verify(in ItSetup<long> parameter1, in ItSetup<System.IO.SeekOrigin> parameter2, in Times times, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpan();

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						var count = 0;
						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							count++;
						}

						if (times.Predicate(count))
							return;

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifyCountException(verifyName, times, count, invocations);
					}

					public long Verify(in ItSetup<long> parameter1, in ItSetup<System.IO.SeekOrigin> parameter2, long index, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpanFrom(index);

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							return span[i].Index + 1;
						}

						if (invocationProviders is null)
						{
							span = _invocations.GetItemsSpanBefore(index);
							for (var i = 0; i < span.Length; i++)
								verifyOutput.Insert(i, (span[i], null));
						}

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
					}

					public void VerifyNoOtherCalls(System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
						if (unverifiedItems is null)
							return;

						var typeNameParameter1 = !string.IsNullOrEmpty(_parameter1Prefix) ? $"{_parameter1Prefix} long" : "long";
						var typeNameParameter2 = !string.IsNullOrEmpty(_parameter2Prefix) ? $"{_parameter2Prefix} System.IO.SeekOrigin" : "System.IO.SeekOrigin";
						var verifyName = string.Format(_name, typeNameParameter1, typeNameParameter2);
						throw new MockUnverifiedException(verifyName, unverifiedItems);
					}

					public System.Collections.Generic.IEnumerable<IInvocation> GetInvocations()
					{
						return _invocations;
					}

					public System.Collections.Generic.IEnumerable<IInvocation<(long parameter1, System.IO.SeekOrigin parameter2)>> GetInvocationsWithArguments()
					{
						return _invocations;
					}

					private sealed class Item : IInvocation<(long parameter1, System.IO.SeekOrigin parameter2)>
					{
						private readonly (long parameter1, System.IO.SeekOrigin parameter2) _argument;
						private readonly string? _jsonSnapshotParameter1, _jsonSnapshotParameter2;
						private readonly InvocationInt64SeekOrigin _invocation;

						public Item(long index, long parameter1, System.IO.SeekOrigin parameter2, InvocationInt64SeekOrigin invocation)
						{
							_argument = (parameter1, parameter2);
							_invocation = invocation;
							Index = index;

							try
							{
								_jsonSnapshotParameter1 = parameter1.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter2 = parameter2.SerializeToJson();
							}
							catch
							{
								// Swallow
							}
						}

						public long Index { get; }

						public bool IsVerified { get; set; }

						public (long parameter1, System.IO.SeekOrigin parameter2) Arguments => _argument;

						public long GetParameter1(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter1)
								? _jsonSnapshotParameter1.DeserializeFromJson(_argument.parameter1)
								: _argument.parameter1;
						}

						public System.IO.SeekOrigin GetParameter2(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter2)
								? _jsonSnapshotParameter2.DeserializeFromJson(_argument.parameter2)
								: _argument.parameter2;
						}

						public override string ToString()
						{
							var stringBuilder = new System.Text.StringBuilder();

							// parameter1
							if (!string.IsNullOrEmpty(_invocation._parameter1Prefix))
								stringBuilder.Append($"{_invocation._parameter1Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter1))
								stringBuilder.Append(_jsonSnapshotParameter1);
							else
								stringBuilder.Append(_argument.parameter1);
							var parameter1 = stringBuilder.ToString();

							// parameter2
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter2Prefix))
								stringBuilder.Append($"{_invocation._parameter2Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter2))
								stringBuilder.Append(_jsonSnapshotParameter2);
							else
								stringBuilder.Append(_argument.parameter2);
							var parameter2 = stringBuilder.ToString();

							var stringValue = string.Format(_invocation._name, parameter1, parameter2);
							return $"{Index}: {stringValue}";
						}
					}
				}
				"""
			),
			(
				"InvocationMemoryByteCancellationToken.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InvocationMemoryByteCancellationToken : IInvocationVerify
				{
					private readonly string _name;
					private readonly string? _parameter1Prefix, _parameter2Prefix;
					private readonly InvocationContainer<Item> _invocations = [];

					public InvocationMemoryByteCancellationToken(string name, string? parameter1Prefix = null, string? parameter2Prefix = null)
					{
						_name = name;
						_parameter1Prefix = parameter1Prefix;
						_parameter2Prefix = parameter2Prefix;
					}

					public void Register(in InvocationIndex.Counter index, System.Memory<byte> parameter1, System.Threading.CancellationToken parameter2)
					{
						var invokedIndex = index.Increment();
						_invocations.Add(new Item(invokedIndex, parameter1, parameter2, invocation: this));
					}

					public void Verify(in ItSetup<System.Memory<byte>> parameter1, in ItSetup<System.Threading.CancellationToken> parameter2, in Times times, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpan();

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						var count = 0;
						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							count++;
						}

						if (times.Predicate(count))
							return;

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifyCountException(verifyName, times, count, invocations);
					}

					public long Verify(in ItSetup<System.Memory<byte>> parameter1, in ItSetup<System.Threading.CancellationToken> parameter2, long index, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpanFrom(index);

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							return span[i].Index + 1;
						}

						if (invocationProviders is null)
						{
							span = _invocations.GetItemsSpanBefore(index);
							for (var i = 0; i < span.Length; i++)
								verifyOutput.Insert(i, (span[i], null));
						}

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
					}

					public void VerifyNoOtherCalls(System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
						if (unverifiedItems is null)
							return;

						var typeNameParameter1 = !string.IsNullOrEmpty(_parameter1Prefix) ? $"{_parameter1Prefix} System.Memory<byte>" : "System.Memory<byte>";
						var typeNameParameter2 = !string.IsNullOrEmpty(_parameter2Prefix) ? $"{_parameter2Prefix} System.Threading.CancellationToken" : "System.Threading.CancellationToken";
						var verifyName = string.Format(_name, typeNameParameter1, typeNameParameter2);
						throw new MockUnverifiedException(verifyName, unverifiedItems);
					}

					public System.Collections.Generic.IEnumerable<IInvocation> GetInvocations()
					{
						return _invocations;
					}

					public System.Collections.Generic.IEnumerable<IInvocation<(System.Memory<byte> parameter1, System.Threading.CancellationToken parameter2)>> GetInvocationsWithArguments()
					{
						return _invocations;
					}

					private sealed class Item : IInvocation<(System.Memory<byte> parameter1, System.Threading.CancellationToken parameter2)>
					{
						private readonly (System.Memory<byte> parameter1, System.Threading.CancellationToken parameter2) _argument;
						private readonly string? _jsonSnapshotParameter1, _jsonSnapshotParameter2;
						private readonly InvocationMemoryByteCancellationToken _invocation;

						public Item(long index, System.Memory<byte> parameter1, System.Threading.CancellationToken parameter2, InvocationMemoryByteCancellationToken invocation)
						{
							_argument = (parameter1, parameter2);
							_invocation = invocation;
							Index = index;

							try
							{
								_jsonSnapshotParameter1 = parameter1.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter2 = parameter2.SerializeToJson();
							}
							catch
							{
								// Swallow
							}
						}

						public long Index { get; }

						public bool IsVerified { get; set; }

						public (System.Memory<byte> parameter1, System.Threading.CancellationToken parameter2) Arguments => _argument;

						public System.Memory<byte> GetParameter1(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter1)
								? _jsonSnapshotParameter1.DeserializeFromJson(_argument.parameter1)
								: _argument.parameter1;
						}

						public System.Threading.CancellationToken GetParameter2(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter2)
								? _jsonSnapshotParameter2.DeserializeFromJson(_argument.parameter2)
								: _argument.parameter2;
						}

						public override string ToString()
						{
							var stringBuilder = new System.Text.StringBuilder();

							// parameter1
							if (!string.IsNullOrEmpty(_invocation._parameter1Prefix))
								stringBuilder.Append($"{_invocation._parameter1Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter1))
								stringBuilder.Append(_jsonSnapshotParameter1);
							else
								stringBuilder.Append(_argument.parameter1);
							var parameter1 = stringBuilder.ToString();

							// parameter2
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter2Prefix))
								stringBuilder.Append($"{_invocation._parameter2Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter2))
								stringBuilder.Append(_jsonSnapshotParameter2);
							else
								stringBuilder.Append(_argument.parameter2);
							var parameter2 = stringBuilder.ToString();

							var stringValue = string.Format(_invocation._name, parameter1, parameter2);
							return $"{Index}: {stringValue}";
						}
					}
				}
				"""
			),
			(
				"InvocationReadOnlyMemoryByteCancellationToken.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InvocationReadOnlyMemoryByteCancellationToken : IInvocationVerify
				{
					private readonly string _name;
					private readonly string? _parameter1Prefix, _parameter2Prefix;
					private readonly InvocationContainer<Item> _invocations = [];

					public InvocationReadOnlyMemoryByteCancellationToken(string name, string? parameter1Prefix = null, string? parameter2Prefix = null)
					{
						_name = name;
						_parameter1Prefix = parameter1Prefix;
						_parameter2Prefix = parameter2Prefix;
					}

					public void Register(in InvocationIndex.Counter index, System.ReadOnlyMemory<byte> parameter1, System.Threading.CancellationToken parameter2)
					{
						var invokedIndex = index.Increment();
						_invocations.Add(new Item(invokedIndex, parameter1, parameter2, invocation: this));
					}

					public void Verify(in ItSetup<System.ReadOnlyMemory<byte>> parameter1, in ItSetup<System.Threading.CancellationToken> parameter2, in Times times, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpan();

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						var count = 0;
						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							count++;
						}

						if (times.Predicate(count))
							return;

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifyCountException(verifyName, times, count, invocations);
					}

					public long Verify(in ItSetup<System.ReadOnlyMemory<byte>> parameter1, in ItSetup<System.Threading.CancellationToken> parameter2, long index, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpanFrom(index);

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							return span[i].Index + 1;
						}

						if (invocationProviders is null)
						{
							span = _invocations.GetItemsSpanBefore(index);
							for (var i = 0; i < span.Length; i++)
								verifyOutput.Insert(i, (span[i], null));
						}

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
					}

					public void VerifyNoOtherCalls(System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
						if (unverifiedItems is null)
							return;

						var typeNameParameter1 = !string.IsNullOrEmpty(_parameter1Prefix) ? $"{_parameter1Prefix} System.ReadOnlyMemory<byte>" : "System.ReadOnlyMemory<byte>";
						var typeNameParameter2 = !string.IsNullOrEmpty(_parameter2Prefix) ? $"{_parameter2Prefix} System.Threading.CancellationToken" : "System.Threading.CancellationToken";
						var verifyName = string.Format(_name, typeNameParameter1, typeNameParameter2);
						throw new MockUnverifiedException(verifyName, unverifiedItems);
					}

					public System.Collections.Generic.IEnumerable<IInvocation> GetInvocations()
					{
						return _invocations;
					}

					public System.Collections.Generic.IEnumerable<IInvocation<(System.ReadOnlyMemory<byte> parameter1, System.Threading.CancellationToken parameter2)>> GetInvocationsWithArguments()
					{
						return _invocations;
					}

					private sealed class Item : IInvocation<(System.ReadOnlyMemory<byte> parameter1, System.Threading.CancellationToken parameter2)>
					{
						private readonly (System.ReadOnlyMemory<byte> parameter1, System.Threading.CancellationToken parameter2) _argument;
						private readonly string? _jsonSnapshotParameter1, _jsonSnapshotParameter2;
						private readonly InvocationReadOnlyMemoryByteCancellationToken _invocation;

						public Item(long index, System.ReadOnlyMemory<byte> parameter1, System.Threading.CancellationToken parameter2, InvocationReadOnlyMemoryByteCancellationToken invocation)
						{
							_argument = (parameter1, parameter2);
							_invocation = invocation;
							Index = index;

							try
							{
								_jsonSnapshotParameter1 = parameter1.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter2 = parameter2.SerializeToJson();
							}
							catch
							{
								// Swallow
							}
						}

						public long Index { get; }

						public bool IsVerified { get; set; }

						public (System.ReadOnlyMemory<byte> parameter1, System.Threading.CancellationToken parameter2) Arguments => _argument;

						public System.ReadOnlyMemory<byte> GetParameter1(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter1)
								? _jsonSnapshotParameter1.DeserializeFromJson(_argument.parameter1)
								: _argument.parameter1;
						}

						public System.Threading.CancellationToken GetParameter2(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter2)
								? _jsonSnapshotParameter2.DeserializeFromJson(_argument.parameter2)
								: _argument.parameter2;
						}

						public override string ToString()
						{
							var stringBuilder = new System.Text.StringBuilder();

							// parameter1
							if (!string.IsNullOrEmpty(_invocation._parameter1Prefix))
								stringBuilder.Append($"{_invocation._parameter1Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter1))
								stringBuilder.Append(_jsonSnapshotParameter1);
							else
								stringBuilder.Append(_argument.parameter1);
							var parameter1 = stringBuilder.ToString();

							// parameter2
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter2Prefix))
								stringBuilder.Append($"{_invocation._parameter2Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter2))
								stringBuilder.Append(_jsonSnapshotParameter2);
							else
								stringBuilder.Append(_argument.parameter2);
							var parameter2 = stringBuilder.ToString();

							var stringValue = string.Format(_invocation._name, parameter1, parameter2);
							return $"{Index}: {stringValue}";
						}
					}
				}
				"""
			),
			(
				"InvocationStreamInt32.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InvocationStreamInt32 : IInvocationVerify
				{
					private readonly string _name;
					private readonly string? _parameter1Prefix, _parameter2Prefix;
					private readonly InvocationContainer<Item> _invocations = [];

					public InvocationStreamInt32(string name, string? parameter1Prefix = null, string? parameter2Prefix = null)
					{
						_name = name;
						_parameter1Prefix = parameter1Prefix;
						_parameter2Prefix = parameter2Prefix;
					}

					public void Register(in InvocationIndex.Counter index, System.IO.Stream parameter1, int parameter2)
					{
						var invokedIndex = index.Increment();
						_invocations.Add(new Item(invokedIndex, parameter1, parameter2, invocation: this));
					}

					public void Verify(in ItSetup<System.IO.Stream> parameter1, in ItSetup<int> parameter2, in Times times, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpan();

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						var count = 0;
						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							count++;
						}

						if (times.Predicate(count))
							return;

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifyCountException(verifyName, times, count, invocations);
					}

					public long Verify(in ItSetup<System.IO.Stream> parameter1, in ItSetup<int> parameter2, long index, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpanFrom(index);

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							return span[i].Index + 1;
						}

						if (invocationProviders is null)
						{
							span = _invocations.GetItemsSpanBefore(index);
							for (var i = 0; i < span.Length; i++)
								verifyOutput.Insert(i, (span[i], null));
						}

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
					}

					public void VerifyNoOtherCalls(System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
						if (unverifiedItems is null)
							return;

						var typeNameParameter1 = !string.IsNullOrEmpty(_parameter1Prefix) ? $"{_parameter1Prefix} System.IO.Stream" : "System.IO.Stream";
						var typeNameParameter2 = !string.IsNullOrEmpty(_parameter2Prefix) ? $"{_parameter2Prefix} int" : "int";
						var verifyName = string.Format(_name, typeNameParameter1, typeNameParameter2);
						throw new MockUnverifiedException(verifyName, unverifiedItems);
					}

					public System.Collections.Generic.IEnumerable<IInvocation> GetInvocations()
					{
						return _invocations;
					}

					public System.Collections.Generic.IEnumerable<IInvocation<(System.IO.Stream parameter1, int parameter2)>> GetInvocationsWithArguments()
					{
						return _invocations;
					}

					private sealed class Item : IInvocation<(System.IO.Stream parameter1, int parameter2)>
					{
						private readonly (System.IO.Stream parameter1, int parameter2) _argument;
						private readonly string? _jsonSnapshotParameter1, _jsonSnapshotParameter2;
						private readonly InvocationStreamInt32 _invocation;

						public Item(long index, System.IO.Stream parameter1, int parameter2, InvocationStreamInt32 invocation)
						{
							_argument = (parameter1, parameter2);
							_invocation = invocation;
							Index = index;

							try
							{
								_jsonSnapshotParameter1 = parameter1.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter2 = parameter2.SerializeToJson();
							}
							catch
							{
								// Swallow
							}
						}

						public long Index { get; }

						public bool IsVerified { get; set; }

						public (System.IO.Stream parameter1, int parameter2) Arguments => _argument;

						public System.IO.Stream GetParameter1(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter1)
								? _jsonSnapshotParameter1.DeserializeFromJson(_argument.parameter1)
								: _argument.parameter1;
						}

						public int GetParameter2(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter2)
								? _jsonSnapshotParameter2.DeserializeFromJson(_argument.parameter2)
								: _argument.parameter2;
						}

						public override string ToString()
						{
							var stringBuilder = new System.Text.StringBuilder();

							// parameter1
							if (!string.IsNullOrEmpty(_invocation._parameter1Prefix))
								stringBuilder.Append($"{_invocation._parameter1Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter1))
								stringBuilder.Append(_jsonSnapshotParameter1);
							else
								stringBuilder.Append(_argument.parameter1);
							var parameter1 = stringBuilder.ToString();

							// parameter2
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter2Prefix))
								stringBuilder.Append($"{_invocation._parameter2Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter2))
								stringBuilder.Append(_jsonSnapshotParameter2);
							else
								stringBuilder.Append(_argument.parameter2);
							var parameter2 = stringBuilder.ToString();

							var stringValue = string.Format(_invocation._name, parameter1, parameter2);
							return $"{Index}: {stringValue}";
						}
					}
				}
				"""
			),
			(
				"InvocationStreamInt32CancellationToken.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InvocationStreamInt32CancellationToken : IInvocationVerify
				{
					private readonly string _name;
					private readonly string? _parameter1Prefix, _parameter2Prefix, _parameter3Prefix;
					private readonly InvocationContainer<Item> _invocations = [];

					public InvocationStreamInt32CancellationToken(string name, string? parameter1Prefix = null, string? parameter2Prefix = null, string? parameter3Prefix = null)
					{
						_name = name;
						_parameter1Prefix = parameter1Prefix;
						_parameter2Prefix = parameter2Prefix;
						_parameter3Prefix = parameter3Prefix;
					}

					public void Register(in InvocationIndex.Counter index, System.IO.Stream parameter1, int parameter2, System.Threading.CancellationToken parameter3)
					{
						var invokedIndex = index.Increment();
						_invocations.Add(new Item(invokedIndex, parameter1, parameter2, parameter3, invocation: this));
					}

					public void Verify(in ItSetup<System.IO.Stream> parameter1, in ItSetup<int> parameter2, in ItSetup<System.Threading.CancellationToken> parameter3, in Times times, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpan();

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						var count = 0;
						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							var verifyParameter3 = span[i].GetParameter3(parameter3.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}
							if (!parameter3.Check(verifyParameter3, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter3", result)]
									: [("parameter3", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							count++;
						}

						if (times.Predicate(count))
							return;

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix), parameter3.ToString(_parameter3Prefix));
						throw new MockVerifyCountException(verifyName, times, count, invocations);
					}

					public long Verify(in ItSetup<System.IO.Stream> parameter1, in ItSetup<int> parameter2, in ItSetup<System.Threading.CancellationToken> parameter3, long index, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpanFrom(index);

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							var verifyParameter3 = span[i].GetParameter3(parameter3.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}
							if (!parameter3.Check(verifyParameter3, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter3", result)]
									: [("parameter3", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							return span[i].Index + 1;
						}

						if (invocationProviders is null)
						{
							span = _invocations.GetItemsSpanBefore(index);
							for (var i = 0; i < span.Length; i++)
								verifyOutput.Insert(i, (span[i], null));
						}

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix), parameter3.ToString(_parameter3Prefix));
						throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
					}

					public void VerifyNoOtherCalls(System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
						if (unverifiedItems is null)
							return;

						var typeNameParameter1 = !string.IsNullOrEmpty(_parameter1Prefix) ? $"{_parameter1Prefix} System.IO.Stream" : "System.IO.Stream";
						var typeNameParameter2 = !string.IsNullOrEmpty(_parameter2Prefix) ? $"{_parameter2Prefix} int" : "int";
						var typeNameParameter3 = !string.IsNullOrEmpty(_parameter3Prefix) ? $"{_parameter3Prefix} System.Threading.CancellationToken" : "System.Threading.CancellationToken";
						var verifyName = string.Format(_name, typeNameParameter1, typeNameParameter2, typeNameParameter3);
						throw new MockUnverifiedException(verifyName, unverifiedItems);
					}

					public System.Collections.Generic.IEnumerable<IInvocation> GetInvocations()
					{
						return _invocations;
					}

					public System.Collections.Generic.IEnumerable<IInvocation<(System.IO.Stream parameter1, int parameter2, System.Threading.CancellationToken parameter3)>> GetInvocationsWithArguments()
					{
						return _invocations;
					}

					private sealed class Item : IInvocation<(System.IO.Stream parameter1, int parameter2, System.Threading.CancellationToken parameter3)>
					{
						private readonly (System.IO.Stream parameter1, int parameter2, System.Threading.CancellationToken parameter3) _argument;
						private readonly string? _jsonSnapshotParameter1, _jsonSnapshotParameter2, _jsonSnapshotParameter3;
						private readonly InvocationStreamInt32CancellationToken _invocation;

						public Item(long index, System.IO.Stream parameter1, int parameter2, System.Threading.CancellationToken parameter3, InvocationStreamInt32CancellationToken invocation)
						{
							_argument = (parameter1, parameter2, parameter3);
							_invocation = invocation;
							Index = index;

							try
							{
								_jsonSnapshotParameter1 = parameter1.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter2 = parameter2.SerializeToJson();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter3 = parameter3.SerializeToJson();
							}
							catch
							{
								// Swallow
							}
						}

						public long Index { get; }

						public bool IsVerified { get; set; }

						public (System.IO.Stream parameter1, int parameter2, System.Threading.CancellationToken parameter3) Arguments => _argument;

						public System.IO.Stream GetParameter1(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter1)
								? _jsonSnapshotParameter1.DeserializeFromJson(_argument.parameter1)
								: _argument.parameter1;
						}

						public int GetParameter2(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter2)
								? _jsonSnapshotParameter2.DeserializeFromJson(_argument.parameter2)
								: _argument.parameter2;
						}

						public System.Threading.CancellationToken GetParameter3(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter3)
								? _jsonSnapshotParameter3.DeserializeFromJson(_argument.parameter3)
								: _argument.parameter3;
						}

						public override string ToString()
						{
							var stringBuilder = new System.Text.StringBuilder();

							// parameter1
							if (!string.IsNullOrEmpty(_invocation._parameter1Prefix))
								stringBuilder.Append($"{_invocation._parameter1Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter1))
								stringBuilder.Append(_jsonSnapshotParameter1);
							else
								stringBuilder.Append(_argument.parameter1);
							var parameter1 = stringBuilder.ToString();

							// parameter2
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter2Prefix))
								stringBuilder.Append($"{_invocation._parameter2Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter2))
								stringBuilder.Append(_jsonSnapshotParameter2);
							else
								stringBuilder.Append(_argument.parameter2);
							var parameter2 = stringBuilder.ToString();

							// parameter3
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter3Prefix))
								stringBuilder.Append($"{_invocation._parameter3Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter3))
								stringBuilder.Append(_jsonSnapshotParameter3);
							else
								stringBuilder.Append(_argument.parameter3);
							var parameter3 = stringBuilder.ToString();

							var stringValue = string.Format(_invocation._name, parameter1, parameter2, parameter3);
							return $"{Index}: {stringValue}";
						}
					}
				}
				"""
			),
			(
				"SetupArray1ByteInt32Int32.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class SetupArray1ByteInt32Int32 : ISetupCallbackJoin<SetupArray1ByteInt32Int32.CallbackDelegate>, ISetupCallbackReset<SetupArray1ByteInt32Int32.CallbackDelegate>, ISetupThrowsJoin<SetupArray1ByteInt32Int32.CallbackDelegate>, ISetupThrowsReset<SetupArray1ByteInt32Int32.CallbackDelegate>
				{
					private static readonly Comparer SortComparer = new();
					private static readonly EqualityComparer SetupEqualityComparer = new();
					private SetupContainer<Item>? _setups;
					private Item? _currentSetup;

					public delegate void CallbackDelegate(byte[] parameter1, int parameter2, int parameter3);

					public void Invoke(byte[] parameter1, int parameter2, int parameter3)
					{
						if (_setups is null)
							return;

						foreach (var setup in _setups)
						{
							if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
								continue;
							if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
								continue;
							if (setup.Parameter3.HasValue && !setup.Parameter3.Value.Check(parameter3))
								continue;

							var x = setup.GetSetup();
							x.Callback?.Invoke(parameter1, parameter2, parameter3);

							if (x.Exception is not null)
								throw x.Exception;

							return;
						}
					}

					public void SetupParameters(in ItSetup<byte[]> parameter1, in ItSetup<int> parameter2, in ItSetup<int> parameter3)
					{
						_currentSetup = new Item(parameter1, parameter2, parameter3);

						_setups ??= new SetupContainer<Item>(SortComparer, SetupEqualityComparer);
						_setups.Add(_currentSetup);
					}

					public void Callback(in CallbackDelegate callback)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(callback);
					}

					public void Throws(in System.Exception exception)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(exception);
					}

					ISetupCallbackJoin<SetupArray1ByteInt32Int32.CallbackDelegate> ISetupCallbackStart<SetupArray1ByteInt32Int32.CallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32.CallbackDelegate> ISetupCallbackReset<SetupArray1ByteInt32Int32.CallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetupThrowsJoin<SetupArray1ByteInt32Int32.CallbackDelegate> ISetupThrowsStart<SetupArray1ByteInt32Int32.CallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32.CallbackDelegate> ISetupThrowsReset<SetupArray1ByteInt32Int32.CallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetupCallbackReset<SetupArray1ByteInt32Int32.CallbackDelegate> ISetupThrowsJoin<SetupArray1ByteInt32Int32.CallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					ISetupThrowsReset<SetupArray1ByteInt32Int32.CallbackDelegate> ISetupCallbackJoin<SetupArray1ByteInt32Int32.CallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					private sealed class Item
					{
						private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
						private ItemSetup? _currentSetup;
						public bool AndContinue;

						public readonly ItSetup<byte[]>? Parameter1;
						public readonly ItSetup<int>? Parameter2;
						public readonly ItSetup<int>? Parameter3;

						public Item(in ItSetup<byte[]>? parameter1, in ItSetup<int>? parameter2, in ItSetup<int>? parameter3)
						{
							Parameter1 = parameter1;
							Parameter2 = parameter2;
							Parameter3 = parameter3;
						}

						public void Add(in CallbackDelegate callback)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Callback = callback;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(callback: callback);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in System.Exception exception)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Exception = exception;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(exception: exception);
								_queue.Enqueue(_currentSetup);
							}
						}

						public ItemSetup GetSetup()
						{
							return _queue.Count switch
							{
								0 => ItemSetup.Default,
								1 => _queue.Peek(),
								_ => _queue.Dequeue(),
							};
						}
					}

					private sealed class ItemSetup
					{
						public static readonly ItemSetup Default = new();

						public CallbackDelegate? Callback;
						public System.Exception? Exception;

						public ItemSetup(in CallbackDelegate? callback = null, in System.Exception? exception = null)
						{
							Callback = callback;
							Exception = exception;
						}
					}

					private sealed class Comparer: System.Collections.Generic.IComparer<Item>
					{
						public int Compare(Item? x, Item? y)
						{
							var xSort = 0;
							var ySort = 0;

							if (x is not null)
							{
								if (x.Parameter1.HasValue)
									xSort += x.Parameter1.Value.Sort;
								if (x.Parameter2.HasValue)
									xSort += x.Parameter2.Value.Sort;
								if (x.Parameter3.HasValue)
									xSort += x.Parameter3.Value.Sort;
							}

							if (y is not null)
							{
								if (y.Parameter1.HasValue)
									ySort += y.Parameter1.Value.Sort;
								if (y.Parameter2.HasValue)
									ySort += y.Parameter2.Value.Sort;
								if (y.Parameter3.HasValue)
									ySort += y.Parameter3.Value.Sort;
							}

							return xSort.CompareTo(ySort);
						}
					}

					private sealed class EqualityComparer: System.Collections.Generic.IEqualityComparer<Item>
					{
						public bool Equals(Item? x, Item? y) =>
							x?.Parameter1 == y?.Parameter1 &&
							x?.Parameter2 == y?.Parameter2 &&
							x?.Parameter3 == y?.Parameter3;

						public int GetHashCode(Item obj)
						{
							var hash = new System.HashCode();
							if (obj.Parameter1.HasValue)
								hash.Add(obj.Parameter1.Value.GetHashCode());
							if (obj.Parameter2.HasValue)
								hash.Add(obj.Parameter2.Value.GetHashCode());
							if (obj.Parameter3.HasValue)
								hash.Add(obj.Parameter3.Value.GetHashCode());
							return hash.ToHashCode();
						}
					}
				}
				"""
			),
			(
				"SetupArray1ByteInt32Int32_TReturns_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class SetupArray1ByteInt32Int32<TReturns> : ISetupCallbackJoin<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate>, ISetupCallbackReset<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate>
				{
					private static readonly Comparer SortComparer = new();
					private static readonly EqualityComparer SetupEqualityComparer = new();
					private SetupContainer<Item>? _setups;
					private Item? _currentSetup;

					public delegate void CallbackDelegate(byte[] parameter1, int parameter2, int parameter3);
					public delegate TReturns? ReturnsCallbackDelegate(byte[] parameter1, int parameter2, int parameter3);

					public bool Execute(byte[] parameter1, int parameter2, int parameter3, out TReturns? returnValue)
					{
						if (_setups is null)
							goto Default;

						foreach (var setup in _setups)
						{
							if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
								continue;
							if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
								continue;
							if (setup.Parameter3.HasValue && !setup.Parameter3.Value.Check(parameter3))
								continue;

							var x = setup.GetSetup();
							x.Callback?.Invoke(parameter1, parameter2, parameter3);

							if (x.Exception is not null)
								throw x.Exception;

							if (x.Returns is not null)
							{
								returnValue = x.Returns(parameter1, parameter2, parameter3);
								return true;
							}

							goto Default;
						}

						Default:
						returnValue = default!;
						return false;
					}

					public void SetupParameters(in ItSetup<byte[]> parameter1, in ItSetup<int> parameter2, in ItSetup<int> parameter3)
					{
						_currentSetup = new Item(parameter1, parameter2, parameter3);

						_setups ??= new SetupContainer<Item>(SortComparer, SetupEqualityComparer);
						_setups.Add(_currentSetup);
					}

					public void Returns(TReturns? returns)
					{
						Returns((_, _, _) => returns);
					}

					public void Returns(in ReturnsCallbackDelegate returns)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(returns);
					}

					public void Callback(in CallbackDelegate callback)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(callback);
					}

					public void Throws(in System.Exception exception)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(exception);
					}

					ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetupCallbackJoin<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate> ISetupCallbackStart<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate> ISetupCallbackReset<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetupCallbackReset<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate> ISetupCallbackJoin<SetupArray1ByteInt32Int32<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32<TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					private sealed class Item
					{
						private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
						private ItemSetup? _currentSetup;
						public bool AndContinue;

						public readonly ItSetup<byte[]>? Parameter1;
						public readonly ItSetup<int>? Parameter2;
						public readonly ItSetup<int>? Parameter3;

						public Item(in ItSetup<byte[]>? parameter1, in ItSetup<int>? parameter2, in ItSetup<int>? parameter3)
						{
							Parameter1 = parameter1;
							Parameter2 = parameter2;
							Parameter3 = parameter3;
						}

						public void Add(in CallbackDelegate callback)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Callback = callback;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(callback: callback);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in ReturnsCallbackDelegate returns)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Returns = returns;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(returns: returns);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in System.Exception exception)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Exception = exception;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(exception: exception);
								_queue.Enqueue(_currentSetup);
							}
						}

						public ItemSetup GetSetup()
						{
							return _queue.Count switch
							{
								0 => ItemSetup.Default,
								1 => _queue.Peek(),
								_ => _queue.Dequeue(),
							};
						}
					}

					private sealed class ItemSetup
					{
						public static readonly ItemSetup Default = new();

						public CallbackDelegate? Callback;
						public System.Exception? Exception;
						public ReturnsCallbackDelegate? Returns;

						public ItemSetup(in ReturnsCallbackDelegate? returns = null, in CallbackDelegate? callback = null, in System.Exception? exception = null)
						{
							Returns = returns;
							Callback = callback;
							Exception = exception;
						}
					}

					private sealed class Comparer: System.Collections.Generic.IComparer<Item>
					{
						public int Compare(Item? x, Item? y)
						{
							var xSort = 0;
							var ySort = 0;

							if (x is not null)
							{
								if (x.Parameter1.HasValue)
									xSort += x.Parameter1.Value.Sort;
								if (x.Parameter2.HasValue)
									xSort += x.Parameter2.Value.Sort;
								if (x.Parameter3.HasValue)
									xSort += x.Parameter3.Value.Sort;
							}

							if (y is not null)
							{
								if (y.Parameter1.HasValue)
									ySort += y.Parameter1.Value.Sort;
								if (y.Parameter2.HasValue)
									ySort += y.Parameter2.Value.Sort;
								if (y.Parameter3.HasValue)
									ySort += y.Parameter3.Value.Sort;
							}

							return xSort.CompareTo(ySort);
						}
					}

					private sealed class EqualityComparer: System.Collections.Generic.IEqualityComparer<Item>
					{
						public bool Equals(Item? x, Item? y) =>
							x?.Parameter1 == y?.Parameter1 &&
							x?.Parameter2 == y?.Parameter2 &&
							x?.Parameter3 == y?.Parameter3;

						public int GetHashCode(Item obj)
						{
							var hash = new System.HashCode();
							if (obj.Parameter1.HasValue)
								hash.Add(obj.Parameter1.Value.GetHashCode());
							if (obj.Parameter2.HasValue)
								hash.Add(obj.Parameter2.Value.GetHashCode());
							if (obj.Parameter3.HasValue)
								hash.Add(obj.Parameter3.Value.GetHashCode());
							return hash.ToHashCode();
						}
					}
				}
				"""
			),
			(
				"SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable_TReturns_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns> : ISetupCallbackJoin<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate>, ISetupCallbackReset<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate>
				{
					private static readonly Comparer SortComparer = new();
					private static readonly EqualityComparer SetupEqualityComparer = new();
					private SetupContainer<Item>? _setups;
					private Item? _currentSetup;

					public delegate void CallbackDelegate(byte[] parameter1, int parameter2, int parameter3, System.AsyncCallback? parameter4, object? parameter5);
					public delegate TReturns? ReturnsCallbackDelegate(byte[] parameter1, int parameter2, int parameter3, System.AsyncCallback? parameter4, object? parameter5);

					public bool Execute(byte[] parameter1, int parameter2, int parameter3, System.AsyncCallback? parameter4, object? parameter5, out TReturns? returnValue)
					{
						if (_setups is null)
							goto Default;

						foreach (var setup in _setups)
						{
							if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
								continue;
							if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
								continue;
							if (setup.Parameter3.HasValue && !setup.Parameter3.Value.Check(parameter3))
								continue;
							if (setup.Parameter4.HasValue && !setup.Parameter4.Value.Check(parameter4))
								continue;
							if (setup.Parameter5.HasValue && !setup.Parameter5.Value.Check(parameter5))
								continue;

							var x = setup.GetSetup();
							x.Callback?.Invoke(parameter1, parameter2, parameter3, parameter4, parameter5);

							if (x.Exception is not null)
								throw x.Exception;

							if (x.Returns is not null)
							{
								returnValue = x.Returns(parameter1, parameter2, parameter3, parameter4, parameter5);
								return true;
							}

							goto Default;
						}

						Default:
						returnValue = default!;
						return false;
					}

					public void SetupParameters(in ItSetup<byte[]> parameter1, in ItSetup<int> parameter2, in ItSetup<int> parameter3, in ItSetup<System.AsyncCallback?> parameter4, in ItSetup<object?> parameter5)
					{
						_currentSetup = new Item(parameter1, parameter2, parameter3, parameter4, parameter5);

						_setups ??= new SetupContainer<Item>(SortComparer, SetupEqualityComparer);
						_setups.Add(_currentSetup);
					}

					public void Returns(TReturns? returns)
					{
						Returns((_, _, _, _, _) => returns);
					}

					public void Returns(in ReturnsCallbackDelegate returns)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(returns);
					}

					public void Callback(in CallbackDelegate callback)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(callback);
					}

					public void Throws(in System.Exception exception)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(exception);
					}

					ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetupCallbackJoin<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate> ISetupCallbackStart<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate> ISetupCallbackReset<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetupCallbackReset<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate> ISetupCallbackJoin<SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32AsyncCallbackNullableObjectNullable<TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					private sealed class Item
					{
						private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
						private ItemSetup? _currentSetup;
						public bool AndContinue;

						public readonly ItSetup<byte[]>? Parameter1;
						public readonly ItSetup<int>? Parameter2;
						public readonly ItSetup<int>? Parameter3;
						public readonly ItSetup<System.AsyncCallback?>? Parameter4;
						public readonly ItSetup<object?>? Parameter5;

						public Item(in ItSetup<byte[]>? parameter1, in ItSetup<int>? parameter2, in ItSetup<int>? parameter3, in ItSetup<System.AsyncCallback?>? parameter4, in ItSetup<object?>? parameter5)
						{
							Parameter1 = parameter1;
							Parameter2 = parameter2;
							Parameter3 = parameter3;
							Parameter4 = parameter4;
							Parameter5 = parameter5;
						}

						public void Add(in CallbackDelegate callback)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Callback = callback;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(callback: callback);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in ReturnsCallbackDelegate returns)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Returns = returns;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(returns: returns);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in System.Exception exception)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Exception = exception;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(exception: exception);
								_queue.Enqueue(_currentSetup);
							}
						}

						public ItemSetup GetSetup()
						{
							return _queue.Count switch
							{
								0 => ItemSetup.Default,
								1 => _queue.Peek(),
								_ => _queue.Dequeue(),
							};
						}
					}

					private sealed class ItemSetup
					{
						public static readonly ItemSetup Default = new();

						public CallbackDelegate? Callback;
						public System.Exception? Exception;
						public ReturnsCallbackDelegate? Returns;

						public ItemSetup(in ReturnsCallbackDelegate? returns = null, in CallbackDelegate? callback = null, in System.Exception? exception = null)
						{
							Returns = returns;
							Callback = callback;
							Exception = exception;
						}
					}

					private sealed class Comparer: System.Collections.Generic.IComparer<Item>
					{
						public int Compare(Item? x, Item? y)
						{
							var xSort = 0;
							var ySort = 0;

							if (x is not null)
							{
								if (x.Parameter1.HasValue)
									xSort += x.Parameter1.Value.Sort;
								if (x.Parameter2.HasValue)
									xSort += x.Parameter2.Value.Sort;
								if (x.Parameter3.HasValue)
									xSort += x.Parameter3.Value.Sort;
								if (x.Parameter4.HasValue)
									xSort += x.Parameter4.Value.Sort;
								if (x.Parameter5.HasValue)
									xSort += x.Parameter5.Value.Sort;
							}

							if (y is not null)
							{
								if (y.Parameter1.HasValue)
									ySort += y.Parameter1.Value.Sort;
								if (y.Parameter2.HasValue)
									ySort += y.Parameter2.Value.Sort;
								if (y.Parameter3.HasValue)
									ySort += y.Parameter3.Value.Sort;
								if (y.Parameter4.HasValue)
									ySort += y.Parameter4.Value.Sort;
								if (y.Parameter5.HasValue)
									ySort += y.Parameter5.Value.Sort;
							}

							return xSort.CompareTo(ySort);
						}
					}

					private sealed class EqualityComparer: System.Collections.Generic.IEqualityComparer<Item>
					{
						public bool Equals(Item? x, Item? y) =>
							x?.Parameter1 == y?.Parameter1 &&
							x?.Parameter2 == y?.Parameter2 &&
							x?.Parameter3 == y?.Parameter3 &&
							x?.Parameter4 == y?.Parameter4 &&
							x?.Parameter5 == y?.Parameter5;

						public int GetHashCode(Item obj)
						{
							var hash = new System.HashCode();
							if (obj.Parameter1.HasValue)
								hash.Add(obj.Parameter1.Value.GetHashCode());
							if (obj.Parameter2.HasValue)
								hash.Add(obj.Parameter2.Value.GetHashCode());
							if (obj.Parameter3.HasValue)
								hash.Add(obj.Parameter3.Value.GetHashCode());
							if (obj.Parameter4.HasValue)
								hash.Add(obj.Parameter4.Value.GetHashCode());
							if (obj.Parameter5.HasValue)
								hash.Add(obj.Parameter5.Value.GetHashCode());
							return hash.ToHashCode();
						}
					}
				}
				"""
			),
			(
				"SetupArray1ByteInt32Int32CancellationToken.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class SetupArray1ByteInt32Int32CancellationToken : ISetupCallbackJoin<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate>, ISetupCallbackReset<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate>, ISetupThrowsJoin<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate>, ISetupThrowsReset<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate>
				{
					private static readonly Comparer SortComparer = new();
					private static readonly EqualityComparer SetupEqualityComparer = new();
					private SetupContainer<Item>? _setups;
					private Item? _currentSetup;

					public delegate void CallbackDelegate(byte[] parameter1, int parameter2, int parameter3, System.Threading.CancellationToken parameter4);

					public void Invoke(byte[] parameter1, int parameter2, int parameter3, System.Threading.CancellationToken parameter4)
					{
						if (_setups is null)
							return;

						foreach (var setup in _setups)
						{
							if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
								continue;
							if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
								continue;
							if (setup.Parameter3.HasValue && !setup.Parameter3.Value.Check(parameter3))
								continue;
							if (setup.Parameter4.HasValue && !setup.Parameter4.Value.Check(parameter4))
								continue;

							var x = setup.GetSetup();
							x.Callback?.Invoke(parameter1, parameter2, parameter3, parameter4);

							if (x.Exception is not null)
								throw x.Exception;

							return;
						}
					}

					public void SetupParameters(in ItSetup<byte[]> parameter1, in ItSetup<int> parameter2, in ItSetup<int> parameter3, in ItSetup<System.Threading.CancellationToken> parameter4)
					{
						_currentSetup = new Item(parameter1, parameter2, parameter3, parameter4);

						_setups ??= new SetupContainer<Item>(SortComparer, SetupEqualityComparer);
						_setups.Add(_currentSetup);
					}

					public void Callback(in CallbackDelegate callback)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(callback);
					}

					public void Throws(in System.Exception exception)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(exception);
					}

					ISetupCallbackJoin<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate> ISetupCallbackStart<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate> ISetupCallbackReset<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetupThrowsJoin<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate> ISetupThrowsStart<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate> ISetupThrowsReset<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetupCallbackReset<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate> ISetupThrowsJoin<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					ISetupThrowsReset<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate> ISetupCallbackJoin<SetupArray1ByteInt32Int32CancellationToken.CallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					private sealed class Item
					{
						private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
						private ItemSetup? _currentSetup;
						public bool AndContinue;

						public readonly ItSetup<byte[]>? Parameter1;
						public readonly ItSetup<int>? Parameter2;
						public readonly ItSetup<int>? Parameter3;
						public readonly ItSetup<System.Threading.CancellationToken>? Parameter4;

						public Item(in ItSetup<byte[]>? parameter1, in ItSetup<int>? parameter2, in ItSetup<int>? parameter3, in ItSetup<System.Threading.CancellationToken>? parameter4)
						{
							Parameter1 = parameter1;
							Parameter2 = parameter2;
							Parameter3 = parameter3;
							Parameter4 = parameter4;
						}

						public void Add(in CallbackDelegate callback)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Callback = callback;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(callback: callback);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in System.Exception exception)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Exception = exception;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(exception: exception);
								_queue.Enqueue(_currentSetup);
							}
						}

						public ItemSetup GetSetup()
						{
							return _queue.Count switch
							{
								0 => ItemSetup.Default,
								1 => _queue.Peek(),
								_ => _queue.Dequeue(),
							};
						}
					}

					private sealed class ItemSetup
					{
						public static readonly ItemSetup Default = new();

						public CallbackDelegate? Callback;
						public System.Exception? Exception;

						public ItemSetup(in CallbackDelegate? callback = null, in System.Exception? exception = null)
						{
							Callback = callback;
							Exception = exception;
						}
					}

					private sealed class Comparer: System.Collections.Generic.IComparer<Item>
					{
						public int Compare(Item? x, Item? y)
						{
							var xSort = 0;
							var ySort = 0;

							if (x is not null)
							{
								if (x.Parameter1.HasValue)
									xSort += x.Parameter1.Value.Sort;
								if (x.Parameter2.HasValue)
									xSort += x.Parameter2.Value.Sort;
								if (x.Parameter3.HasValue)
									xSort += x.Parameter3.Value.Sort;
								if (x.Parameter4.HasValue)
									xSort += x.Parameter4.Value.Sort;
							}

							if (y is not null)
							{
								if (y.Parameter1.HasValue)
									ySort += y.Parameter1.Value.Sort;
								if (y.Parameter2.HasValue)
									ySort += y.Parameter2.Value.Sort;
								if (y.Parameter3.HasValue)
									ySort += y.Parameter3.Value.Sort;
								if (y.Parameter4.HasValue)
									ySort += y.Parameter4.Value.Sort;
							}

							return xSort.CompareTo(ySort);
						}
					}

					private sealed class EqualityComparer: System.Collections.Generic.IEqualityComparer<Item>
					{
						public bool Equals(Item? x, Item? y) =>
							x?.Parameter1 == y?.Parameter1 &&
							x?.Parameter2 == y?.Parameter2 &&
							x?.Parameter3 == y?.Parameter3 &&
							x?.Parameter4 == y?.Parameter4;

						public int GetHashCode(Item obj)
						{
							var hash = new System.HashCode();
							if (obj.Parameter1.HasValue)
								hash.Add(obj.Parameter1.Value.GetHashCode());
							if (obj.Parameter2.HasValue)
								hash.Add(obj.Parameter2.Value.GetHashCode());
							if (obj.Parameter3.HasValue)
								hash.Add(obj.Parameter3.Value.GetHashCode());
							if (obj.Parameter4.HasValue)
								hash.Add(obj.Parameter4.Value.GetHashCode());
							return hash.ToHashCode();
						}
					}
				}
				"""
			),
			(
				"SetupArray1ByteInt32Int32CancellationToken_TReturns_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class SetupArray1ByteInt32Int32CancellationToken<TReturns> : ISetupCallbackJoin<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate>, ISetupCallbackReset<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate>
				{
					private static readonly Comparer SortComparer = new();
					private static readonly EqualityComparer SetupEqualityComparer = new();
					private SetupContainer<Item>? _setups;
					private Item? _currentSetup;

					public delegate void CallbackDelegate(byte[] parameter1, int parameter2, int parameter3, System.Threading.CancellationToken parameter4);
					public delegate TReturns? ReturnsCallbackDelegate(byte[] parameter1, int parameter2, int parameter3, System.Threading.CancellationToken parameter4);

					public bool Execute(byte[] parameter1, int parameter2, int parameter3, System.Threading.CancellationToken parameter4, out TReturns? returnValue)
					{
						if (_setups is null)
							goto Default;

						foreach (var setup in _setups)
						{
							if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
								continue;
							if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
								continue;
							if (setup.Parameter3.HasValue && !setup.Parameter3.Value.Check(parameter3))
								continue;
							if (setup.Parameter4.HasValue && !setup.Parameter4.Value.Check(parameter4))
								continue;

							var x = setup.GetSetup();
							x.Callback?.Invoke(parameter1, parameter2, parameter3, parameter4);

							if (x.Exception is not null)
								throw x.Exception;

							if (x.Returns is not null)
							{
								returnValue = x.Returns(parameter1, parameter2, parameter3, parameter4);
								return true;
							}

							goto Default;
						}

						Default:
						returnValue = default!;
						return false;
					}

					public void SetupParameters(in ItSetup<byte[]> parameter1, in ItSetup<int> parameter2, in ItSetup<int> parameter3, in ItSetup<System.Threading.CancellationToken> parameter4)
					{
						_currentSetup = new Item(parameter1, parameter2, parameter3, parameter4);

						_setups ??= new SetupContainer<Item>(SortComparer, SetupEqualityComparer);
						_setups.Add(_currentSetup);
					}

					public void Returns(TReturns? returns)
					{
						Returns((_, _, _, _) => returns);
					}

					public void Returns(in ReturnsCallbackDelegate returns)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(returns);
					}

					public void Callback(in CallbackDelegate callback)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(callback);
					}

					public void Throws(in System.Exception exception)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(exception);
					}

					ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetupCallbackJoin<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupCallbackStart<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupCallbackReset<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetup<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetupCallbackReset<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsJoin<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					ISetupReturnsThrowsReset<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupCallbackJoin<SetupArray1ByteInt32Int32CancellationToken<TReturns>.CallbackDelegate, TReturns, SetupArray1ByteInt32Int32CancellationToken<TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					private sealed class Item
					{
						private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
						private ItemSetup? _currentSetup;
						public bool AndContinue;

						public readonly ItSetup<byte[]>? Parameter1;
						public readonly ItSetup<int>? Parameter2;
						public readonly ItSetup<int>? Parameter3;
						public readonly ItSetup<System.Threading.CancellationToken>? Parameter4;

						public Item(in ItSetup<byte[]>? parameter1, in ItSetup<int>? parameter2, in ItSetup<int>? parameter3, in ItSetup<System.Threading.CancellationToken>? parameter4)
						{
							Parameter1 = parameter1;
							Parameter2 = parameter2;
							Parameter3 = parameter3;
							Parameter4 = parameter4;
						}

						public void Add(in CallbackDelegate callback)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Callback = callback;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(callback: callback);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in ReturnsCallbackDelegate returns)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Returns = returns;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(returns: returns);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in System.Exception exception)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Exception = exception;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(exception: exception);
								_queue.Enqueue(_currentSetup);
							}
						}

						public ItemSetup GetSetup()
						{
							return _queue.Count switch
							{
								0 => ItemSetup.Default,
								1 => _queue.Peek(),
								_ => _queue.Dequeue(),
							};
						}
					}

					private sealed class ItemSetup
					{
						public static readonly ItemSetup Default = new();

						public CallbackDelegate? Callback;
						public System.Exception? Exception;
						public ReturnsCallbackDelegate? Returns;

						public ItemSetup(in ReturnsCallbackDelegate? returns = null, in CallbackDelegate? callback = null, in System.Exception? exception = null)
						{
							Returns = returns;
							Callback = callback;
							Exception = exception;
						}
					}

					private sealed class Comparer: System.Collections.Generic.IComparer<Item>
					{
						public int Compare(Item? x, Item? y)
						{
							var xSort = 0;
							var ySort = 0;

							if (x is not null)
							{
								if (x.Parameter1.HasValue)
									xSort += x.Parameter1.Value.Sort;
								if (x.Parameter2.HasValue)
									xSort += x.Parameter2.Value.Sort;
								if (x.Parameter3.HasValue)
									xSort += x.Parameter3.Value.Sort;
								if (x.Parameter4.HasValue)
									xSort += x.Parameter4.Value.Sort;
							}

							if (y is not null)
							{
								if (y.Parameter1.HasValue)
									ySort += y.Parameter1.Value.Sort;
								if (y.Parameter2.HasValue)
									ySort += y.Parameter2.Value.Sort;
								if (y.Parameter3.HasValue)
									ySort += y.Parameter3.Value.Sort;
								if (y.Parameter4.HasValue)
									ySort += y.Parameter4.Value.Sort;
							}

							return xSort.CompareTo(ySort);
						}
					}

					private sealed class EqualityComparer: System.Collections.Generic.IEqualityComparer<Item>
					{
						public bool Equals(Item? x, Item? y) =>
							x?.Parameter1 == y?.Parameter1 &&
							x?.Parameter2 == y?.Parameter2 &&
							x?.Parameter3 == y?.Parameter3 &&
							x?.Parameter4 == y?.Parameter4;

						public int GetHashCode(Item obj)
						{
							var hash = new System.HashCode();
							if (obj.Parameter1.HasValue)
								hash.Add(obj.Parameter1.Value.GetHashCode());
							if (obj.Parameter2.HasValue)
								hash.Add(obj.Parameter2.Value.GetHashCode());
							if (obj.Parameter3.HasValue)
								hash.Add(obj.Parameter3.Value.GetHashCode());
							if (obj.Parameter4.HasValue)
								hash.Add(obj.Parameter4.Value.GetHashCode());
							return hash.ToHashCode();
						}
					}
				}
				"""
			),
			(
				"SetupInt64SeekOrigin_TReturns_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class SetupInt64SeekOrigin<TReturns> : ISetupCallbackJoin<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate>, ISetupCallbackReset<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsJoin<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsReset<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate>
				{
					private static readonly Comparer SortComparer = new();
					private static readonly EqualityComparer SetupEqualityComparer = new();
					private SetupContainer<Item>? _setups;
					private Item? _currentSetup;

					public delegate void CallbackDelegate(long parameter1, System.IO.SeekOrigin parameter2);
					public delegate TReturns? ReturnsCallbackDelegate(long parameter1, System.IO.SeekOrigin parameter2);

					public bool Execute(long parameter1, System.IO.SeekOrigin parameter2, out TReturns? returnValue)
					{
						if (_setups is null)
							goto Default;

						foreach (var setup in _setups)
						{
							if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
								continue;
							if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
								continue;

							var x = setup.GetSetup();
							x.Callback?.Invoke(parameter1, parameter2);

							if (x.Exception is not null)
								throw x.Exception;

							if (x.Returns is not null)
							{
								returnValue = x.Returns(parameter1, parameter2);
								return true;
							}

							goto Default;
						}

						Default:
						returnValue = default!;
						return false;
					}

					public void SetupParameters(in ItSetup<long> parameter1, in ItSetup<System.IO.SeekOrigin> parameter2)
					{
						_currentSetup = new Item(parameter1, parameter2);

						_setups ??= new SetupContainer<Item>(SortComparer, SetupEqualityComparer);
						_setups.Add(_currentSetup);
					}

					public void Returns(TReturns? returns)
					{
						Returns((_, _) => returns);
					}

					public void Returns(in ReturnsCallbackDelegate returns)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(returns);
					}

					public void Callback(in CallbackDelegate callback)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(callback);
					}

					public void Throws(in System.Exception exception)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(exception);
					}

					ISetupReturnsThrowsJoin<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetupCallbackJoin<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate> ISetupCallbackStart<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetup<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate> ISetupCallbackReset<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetup<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetupCallbackReset<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsJoin<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					ISetupReturnsThrowsReset<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate> ISetupCallbackJoin<SetupInt64SeekOrigin<TReturns>.CallbackDelegate, TReturns, SetupInt64SeekOrigin<TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					private sealed class Item
					{
						private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
						private ItemSetup? _currentSetup;
						public bool AndContinue;

						public readonly ItSetup<long>? Parameter1;
						public readonly ItSetup<System.IO.SeekOrigin>? Parameter2;

						public Item(in ItSetup<long>? parameter1, in ItSetup<System.IO.SeekOrigin>? parameter2)
						{
							Parameter1 = parameter1;
							Parameter2 = parameter2;
						}

						public void Add(in CallbackDelegate callback)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Callback = callback;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(callback: callback);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in ReturnsCallbackDelegate returns)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Returns = returns;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(returns: returns);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in System.Exception exception)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Exception = exception;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(exception: exception);
								_queue.Enqueue(_currentSetup);
							}
						}

						public ItemSetup GetSetup()
						{
							return _queue.Count switch
							{
								0 => ItemSetup.Default,
								1 => _queue.Peek(),
								_ => _queue.Dequeue(),
							};
						}
					}

					private sealed class ItemSetup
					{
						public static readonly ItemSetup Default = new();

						public CallbackDelegate? Callback;
						public System.Exception? Exception;
						public ReturnsCallbackDelegate? Returns;

						public ItemSetup(in ReturnsCallbackDelegate? returns = null, in CallbackDelegate? callback = null, in System.Exception? exception = null)
						{
							Returns = returns;
							Callback = callback;
							Exception = exception;
						}
					}

					private sealed class Comparer: System.Collections.Generic.IComparer<Item>
					{
						public int Compare(Item? x, Item? y)
						{
							var xSort = 0;
							var ySort = 0;

							if (x is not null)
							{
								if (x.Parameter1.HasValue)
									xSort += x.Parameter1.Value.Sort;
								if (x.Parameter2.HasValue)
									xSort += x.Parameter2.Value.Sort;
							}

							if (y is not null)
							{
								if (y.Parameter1.HasValue)
									ySort += y.Parameter1.Value.Sort;
								if (y.Parameter2.HasValue)
									ySort += y.Parameter2.Value.Sort;
							}

							return xSort.CompareTo(ySort);
						}
					}

					private sealed class EqualityComparer: System.Collections.Generic.IEqualityComparer<Item>
					{
						public bool Equals(Item? x, Item? y) =>
							x?.Parameter1 == y?.Parameter1 &&
							x?.Parameter2 == y?.Parameter2;

						public int GetHashCode(Item obj)
						{
							var hash = new System.HashCode();
							if (obj.Parameter1.HasValue)
								hash.Add(obj.Parameter1.Value.GetHashCode());
							if (obj.Parameter2.HasValue)
								hash.Add(obj.Parameter2.Value.GetHashCode());
							return hash.ToHashCode();
						}
					}
				}
				"""
			),
			(
				"SetupMemoryByteCancellationToken_TReturns_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class SetupMemoryByteCancellationToken<TReturns> : ISetupCallbackJoin<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate>, ISetupCallbackReset<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsJoin<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsReset<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate>
				{
					private static readonly Comparer SortComparer = new();
					private static readonly EqualityComparer SetupEqualityComparer = new();
					private SetupContainer<Item>? _setups;
					private Item? _currentSetup;

					public delegate void CallbackDelegate(System.Memory<byte> parameter1, System.Threading.CancellationToken parameter2);
					public delegate TReturns? ReturnsCallbackDelegate(System.Memory<byte> parameter1, System.Threading.CancellationToken parameter2);

					public bool Execute(System.Memory<byte> parameter1, System.Threading.CancellationToken parameter2, out TReturns? returnValue)
					{
						if (_setups is null)
							goto Default;

						foreach (var setup in _setups)
						{
							if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
								continue;
							if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
								continue;

							var x = setup.GetSetup();
							x.Callback?.Invoke(parameter1, parameter2);

							if (x.Exception is not null)
								throw x.Exception;

							if (x.Returns is not null)
							{
								returnValue = x.Returns(parameter1, parameter2);
								return true;
							}

							goto Default;
						}

						Default:
						returnValue = default!;
						return false;
					}

					public void SetupParameters(in ItSetup<System.Memory<byte>> parameter1, in ItSetup<System.Threading.CancellationToken> parameter2)
					{
						_currentSetup = new Item(parameter1, parameter2);

						_setups ??= new SetupContainer<Item>(SortComparer, SetupEqualityComparer);
						_setups.Add(_currentSetup);
					}

					public void Returns(TReturns? returns)
					{
						Returns((_, _) => returns);
					}

					public void Returns(in ReturnsCallbackDelegate returns)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(returns);
					}

					public void Callback(in CallbackDelegate callback)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(callback);
					}

					public void Throws(in System.Exception exception)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(exception);
					}

					ISetupReturnsThrowsJoin<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetupCallbackJoin<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupCallbackStart<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetup<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupCallbackReset<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetup<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetupCallbackReset<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsJoin<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					ISetupReturnsThrowsReset<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate> ISetupCallbackJoin<SetupMemoryByteCancellationToken<TReturns>.CallbackDelegate, TReturns, SetupMemoryByteCancellationToken<TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					private sealed class Item
					{
						private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
						private ItemSetup? _currentSetup;
						public bool AndContinue;

						public readonly ItSetup<System.Memory<byte>>? Parameter1;
						public readonly ItSetup<System.Threading.CancellationToken>? Parameter2;

						public Item(in ItSetup<System.Memory<byte>>? parameter1, in ItSetup<System.Threading.CancellationToken>? parameter2)
						{
							Parameter1 = parameter1;
							Parameter2 = parameter2;
						}

						public void Add(in CallbackDelegate callback)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Callback = callback;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(callback: callback);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in ReturnsCallbackDelegate returns)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Returns = returns;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(returns: returns);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in System.Exception exception)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Exception = exception;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(exception: exception);
								_queue.Enqueue(_currentSetup);
							}
						}

						public ItemSetup GetSetup()
						{
							return _queue.Count switch
							{
								0 => ItemSetup.Default,
								1 => _queue.Peek(),
								_ => _queue.Dequeue(),
							};
						}
					}

					private sealed class ItemSetup
					{
						public static readonly ItemSetup Default = new();

						public CallbackDelegate? Callback;
						public System.Exception? Exception;
						public ReturnsCallbackDelegate? Returns;

						public ItemSetup(in ReturnsCallbackDelegate? returns = null, in CallbackDelegate? callback = null, in System.Exception? exception = null)
						{
							Returns = returns;
							Callback = callback;
							Exception = exception;
						}
					}

					private sealed class Comparer: System.Collections.Generic.IComparer<Item>
					{
						public int Compare(Item? x, Item? y)
						{
							var xSort = 0;
							var ySort = 0;

							if (x is not null)
							{
								if (x.Parameter1.HasValue)
									xSort += x.Parameter1.Value.Sort;
								if (x.Parameter2.HasValue)
									xSort += x.Parameter2.Value.Sort;
							}

							if (y is not null)
							{
								if (y.Parameter1.HasValue)
									ySort += y.Parameter1.Value.Sort;
								if (y.Parameter2.HasValue)
									ySort += y.Parameter2.Value.Sort;
							}

							return xSort.CompareTo(ySort);
						}
					}

					private sealed class EqualityComparer: System.Collections.Generic.IEqualityComparer<Item>
					{
						public bool Equals(Item? x, Item? y) =>
							x?.Parameter1 == y?.Parameter1 &&
							x?.Parameter2 == y?.Parameter2;

						public int GetHashCode(Item obj)
						{
							var hash = new System.HashCode();
							if (obj.Parameter1.HasValue)
								hash.Add(obj.Parameter1.Value.GetHashCode());
							if (obj.Parameter2.HasValue)
								hash.Add(obj.Parameter2.Value.GetHashCode());
							return hash.ToHashCode();
						}
					}
				}
				"""
			),
			(
				"SetupReadOnlyMemoryByteCancellationToken.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class SetupReadOnlyMemoryByteCancellationToken : ISetupCallbackJoin<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate>, ISetupCallbackReset<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate>, ISetupThrowsJoin<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate>, ISetupThrowsReset<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate>
				{
					private static readonly Comparer SortComparer = new();
					private static readonly EqualityComparer SetupEqualityComparer = new();
					private SetupContainer<Item>? _setups;
					private Item? _currentSetup;

					public delegate void CallbackDelegate(System.ReadOnlyMemory<byte> parameter1, System.Threading.CancellationToken parameter2);

					public void Invoke(System.ReadOnlyMemory<byte> parameter1, System.Threading.CancellationToken parameter2)
					{
						if (_setups is null)
							return;

						foreach (var setup in _setups)
						{
							if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
								continue;
							if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
								continue;

							var x = setup.GetSetup();
							x.Callback?.Invoke(parameter1, parameter2);

							if (x.Exception is not null)
								throw x.Exception;

							return;
						}
					}

					public void SetupParameters(in ItSetup<System.ReadOnlyMemory<byte>> parameter1, in ItSetup<System.Threading.CancellationToken> parameter2)
					{
						_currentSetup = new Item(parameter1, parameter2);

						_setups ??= new SetupContainer<Item>(SortComparer, SetupEqualityComparer);
						_setups.Add(_currentSetup);
					}

					public void Callback(in CallbackDelegate callback)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(callback);
					}

					public void Throws(in System.Exception exception)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(exception);
					}

					ISetupCallbackJoin<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate> ISetupCallbackStart<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetup<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate> ISetupCallbackReset<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetupThrowsJoin<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate> ISetupThrowsStart<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetup<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate> ISetupThrowsReset<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetupCallbackReset<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate> ISetupThrowsJoin<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					ISetupThrowsReset<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate> ISetupCallbackJoin<SetupReadOnlyMemoryByteCancellationToken.CallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					private sealed class Item
					{
						private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
						private ItemSetup? _currentSetup;
						public bool AndContinue;

						public readonly ItSetup<System.ReadOnlyMemory<byte>>? Parameter1;
						public readonly ItSetup<System.Threading.CancellationToken>? Parameter2;

						public Item(in ItSetup<System.ReadOnlyMemory<byte>>? parameter1, in ItSetup<System.Threading.CancellationToken>? parameter2)
						{
							Parameter1 = parameter1;
							Parameter2 = parameter2;
						}

						public void Add(in CallbackDelegate callback)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Callback = callback;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(callback: callback);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in System.Exception exception)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Exception = exception;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(exception: exception);
								_queue.Enqueue(_currentSetup);
							}
						}

						public ItemSetup GetSetup()
						{
							return _queue.Count switch
							{
								0 => ItemSetup.Default,
								1 => _queue.Peek(),
								_ => _queue.Dequeue(),
							};
						}
					}

					private sealed class ItemSetup
					{
						public static readonly ItemSetup Default = new();

						public CallbackDelegate? Callback;
						public System.Exception? Exception;

						public ItemSetup(in CallbackDelegate? callback = null, in System.Exception? exception = null)
						{
							Callback = callback;
							Exception = exception;
						}
					}

					private sealed class Comparer: System.Collections.Generic.IComparer<Item>
					{
						public int Compare(Item? x, Item? y)
						{
							var xSort = 0;
							var ySort = 0;

							if (x is not null)
							{
								if (x.Parameter1.HasValue)
									xSort += x.Parameter1.Value.Sort;
								if (x.Parameter2.HasValue)
									xSort += x.Parameter2.Value.Sort;
							}

							if (y is not null)
							{
								if (y.Parameter1.HasValue)
									ySort += y.Parameter1.Value.Sort;
								if (y.Parameter2.HasValue)
									ySort += y.Parameter2.Value.Sort;
							}

							return xSort.CompareTo(ySort);
						}
					}

					private sealed class EqualityComparer: System.Collections.Generic.IEqualityComparer<Item>
					{
						public bool Equals(Item? x, Item? y) =>
							x?.Parameter1 == y?.Parameter1 &&
							x?.Parameter2 == y?.Parameter2;

						public int GetHashCode(Item obj)
						{
							var hash = new System.HashCode();
							if (obj.Parameter1.HasValue)
								hash.Add(obj.Parameter1.Value.GetHashCode());
							if (obj.Parameter2.HasValue)
								hash.Add(obj.Parameter2.Value.GetHashCode());
							return hash.ToHashCode();
						}
					}
				}
				"""
			),
			(
				"SetupStreamInt32.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class SetupStreamInt32 : ISetupCallbackJoin<SetupStreamInt32.CallbackDelegate>, ISetupCallbackReset<SetupStreamInt32.CallbackDelegate>, ISetupThrowsJoin<SetupStreamInt32.CallbackDelegate>, ISetupThrowsReset<SetupStreamInt32.CallbackDelegate>
				{
					private static readonly Comparer SortComparer = new();
					private static readonly EqualityComparer SetupEqualityComparer = new();
					private SetupContainer<Item>? _setups;
					private Item? _currentSetup;

					public delegate void CallbackDelegate(System.IO.Stream parameter1, int parameter2);

					public void Invoke(System.IO.Stream parameter1, int parameter2)
					{
						if (_setups is null)
							return;

						foreach (var setup in _setups)
						{
							if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
								continue;
							if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
								continue;

							var x = setup.GetSetup();
							x.Callback?.Invoke(parameter1, parameter2);

							if (x.Exception is not null)
								throw x.Exception;

							return;
						}
					}

					public void SetupParameters(in ItSetup<System.IO.Stream> parameter1, in ItSetup<int> parameter2)
					{
						_currentSetup = new Item(parameter1, parameter2);

						_setups ??= new SetupContainer<Item>(SortComparer, SetupEqualityComparer);
						_setups.Add(_currentSetup);
					}

					public void Callback(in CallbackDelegate callback)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(callback);
					}

					public void Throws(in System.Exception exception)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(exception);
					}

					ISetupCallbackJoin<SetupStreamInt32.CallbackDelegate> ISetupCallbackStart<SetupStreamInt32.CallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetup<SetupStreamInt32.CallbackDelegate> ISetupCallbackReset<SetupStreamInt32.CallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetupThrowsJoin<SetupStreamInt32.CallbackDelegate> ISetupThrowsStart<SetupStreamInt32.CallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetup<SetupStreamInt32.CallbackDelegate> ISetupThrowsReset<SetupStreamInt32.CallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetupCallbackReset<SetupStreamInt32.CallbackDelegate> ISetupThrowsJoin<SetupStreamInt32.CallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					ISetupThrowsReset<SetupStreamInt32.CallbackDelegate> ISetupCallbackJoin<SetupStreamInt32.CallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					private sealed class Item
					{
						private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
						private ItemSetup? _currentSetup;
						public bool AndContinue;

						public readonly ItSetup<System.IO.Stream>? Parameter1;
						public readonly ItSetup<int>? Parameter2;

						public Item(in ItSetup<System.IO.Stream>? parameter1, in ItSetup<int>? parameter2)
						{
							Parameter1 = parameter1;
							Parameter2 = parameter2;
						}

						public void Add(in CallbackDelegate callback)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Callback = callback;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(callback: callback);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in System.Exception exception)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Exception = exception;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(exception: exception);
								_queue.Enqueue(_currentSetup);
							}
						}

						public ItemSetup GetSetup()
						{
							return _queue.Count switch
							{
								0 => ItemSetup.Default,
								1 => _queue.Peek(),
								_ => _queue.Dequeue(),
							};
						}
					}

					private sealed class ItemSetup
					{
						public static readonly ItemSetup Default = new();

						public CallbackDelegate? Callback;
						public System.Exception? Exception;

						public ItemSetup(in CallbackDelegate? callback = null, in System.Exception? exception = null)
						{
							Callback = callback;
							Exception = exception;
						}
					}

					private sealed class Comparer: System.Collections.Generic.IComparer<Item>
					{
						public int Compare(Item? x, Item? y)
						{
							var xSort = 0;
							var ySort = 0;

							if (x is not null)
							{
								if (x.Parameter1.HasValue)
									xSort += x.Parameter1.Value.Sort;
								if (x.Parameter2.HasValue)
									xSort += x.Parameter2.Value.Sort;
							}

							if (y is not null)
							{
								if (y.Parameter1.HasValue)
									ySort += y.Parameter1.Value.Sort;
								if (y.Parameter2.HasValue)
									ySort += y.Parameter2.Value.Sort;
							}

							return xSort.CompareTo(ySort);
						}
					}

					private sealed class EqualityComparer: System.Collections.Generic.IEqualityComparer<Item>
					{
						public bool Equals(Item? x, Item? y) =>
							x?.Parameter1 == y?.Parameter1 &&
							x?.Parameter2 == y?.Parameter2;

						public int GetHashCode(Item obj)
						{
							var hash = new System.HashCode();
							if (obj.Parameter1.HasValue)
								hash.Add(obj.Parameter1.Value.GetHashCode());
							if (obj.Parameter2.HasValue)
								hash.Add(obj.Parameter2.Value.GetHashCode());
							return hash.ToHashCode();
						}
					}
				}
				"""
			),
			(
				"SetupStreamInt32CancellationToken.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class SetupStreamInt32CancellationToken : ISetupCallbackJoin<SetupStreamInt32CancellationToken.CallbackDelegate>, ISetupCallbackReset<SetupStreamInt32CancellationToken.CallbackDelegate>, ISetupThrowsJoin<SetupStreamInt32CancellationToken.CallbackDelegate>, ISetupThrowsReset<SetupStreamInt32CancellationToken.CallbackDelegate>
				{
					private static readonly Comparer SortComparer = new();
					private static readonly EqualityComparer SetupEqualityComparer = new();
					private SetupContainer<Item>? _setups;
					private Item? _currentSetup;

					public delegate void CallbackDelegate(System.IO.Stream parameter1, int parameter2, System.Threading.CancellationToken parameter3);

					public void Invoke(System.IO.Stream parameter1, int parameter2, System.Threading.CancellationToken parameter3)
					{
						if (_setups is null)
							return;

						foreach (var setup in _setups)
						{
							if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
								continue;
							if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
								continue;
							if (setup.Parameter3.HasValue && !setup.Parameter3.Value.Check(parameter3))
								continue;

							var x = setup.GetSetup();
							x.Callback?.Invoke(parameter1, parameter2, parameter3);

							if (x.Exception is not null)
								throw x.Exception;

							return;
						}
					}

					public void SetupParameters(in ItSetup<System.IO.Stream> parameter1, in ItSetup<int> parameter2, in ItSetup<System.Threading.CancellationToken> parameter3)
					{
						_currentSetup = new Item(parameter1, parameter2, parameter3);

						_setups ??= new SetupContainer<Item>(SortComparer, SetupEqualityComparer);
						_setups.Add(_currentSetup);
					}

					public void Callback(in CallbackDelegate callback)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(callback);
					}

					public void Throws(in System.Exception exception)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(exception);
					}

					ISetupCallbackJoin<SetupStreamInt32CancellationToken.CallbackDelegate> ISetupCallbackStart<SetupStreamInt32CancellationToken.CallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetup<SetupStreamInt32CancellationToken.CallbackDelegate> ISetupCallbackReset<SetupStreamInt32CancellationToken.CallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetupThrowsJoin<SetupStreamInt32CancellationToken.CallbackDelegate> ISetupThrowsStart<SetupStreamInt32CancellationToken.CallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetup<SetupStreamInt32CancellationToken.CallbackDelegate> ISetupThrowsReset<SetupStreamInt32CancellationToken.CallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetupCallbackReset<SetupStreamInt32CancellationToken.CallbackDelegate> ISetupThrowsJoin<SetupStreamInt32CancellationToken.CallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					ISetupThrowsReset<SetupStreamInt32CancellationToken.CallbackDelegate> ISetupCallbackJoin<SetupStreamInt32CancellationToken.CallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					private sealed class Item
					{
						private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
						private ItemSetup? _currentSetup;
						public bool AndContinue;

						public readonly ItSetup<System.IO.Stream>? Parameter1;
						public readonly ItSetup<int>? Parameter2;
						public readonly ItSetup<System.Threading.CancellationToken>? Parameter3;

						public Item(in ItSetup<System.IO.Stream>? parameter1, in ItSetup<int>? parameter2, in ItSetup<System.Threading.CancellationToken>? parameter3)
						{
							Parameter1 = parameter1;
							Parameter2 = parameter2;
							Parameter3 = parameter3;
						}

						public void Add(in CallbackDelegate callback)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Callback = callback;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(callback: callback);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in System.Exception exception)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Exception = exception;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(exception: exception);
								_queue.Enqueue(_currentSetup);
							}
						}

						public ItemSetup GetSetup()
						{
							return _queue.Count switch
							{
								0 => ItemSetup.Default,
								1 => _queue.Peek(),
								_ => _queue.Dequeue(),
							};
						}
					}

					private sealed class ItemSetup
					{
						public static readonly ItemSetup Default = new();

						public CallbackDelegate? Callback;
						public System.Exception? Exception;

						public ItemSetup(in CallbackDelegate? callback = null, in System.Exception? exception = null)
						{
							Callback = callback;
							Exception = exception;
						}
					}

					private sealed class Comparer: System.Collections.Generic.IComparer<Item>
					{
						public int Compare(Item? x, Item? y)
						{
							var xSort = 0;
							var ySort = 0;

							if (x is not null)
							{
								if (x.Parameter1.HasValue)
									xSort += x.Parameter1.Value.Sort;
								if (x.Parameter2.HasValue)
									xSort += x.Parameter2.Value.Sort;
								if (x.Parameter3.HasValue)
									xSort += x.Parameter3.Value.Sort;
							}

							if (y is not null)
							{
								if (y.Parameter1.HasValue)
									ySort += y.Parameter1.Value.Sort;
								if (y.Parameter2.HasValue)
									ySort += y.Parameter2.Value.Sort;
								if (y.Parameter3.HasValue)
									ySort += y.Parameter3.Value.Sort;
							}

							return xSort.CompareTo(ySort);
						}
					}

					private sealed class EqualityComparer: System.Collections.Generic.IEqualityComparer<Item>
					{
						public bool Equals(Item? x, Item? y) =>
							x?.Parameter1 == y?.Parameter1 &&
							x?.Parameter2 == y?.Parameter2 &&
							x?.Parameter3 == y?.Parameter3;

						public int GetHashCode(Item obj)
						{
							var hash = new System.HashCode();
							if (obj.Parameter1.HasValue)
								hash.Add(obj.Parameter1.Value.GetHashCode());
							if (obj.Parameter2.HasValue)
								hash.Add(obj.Parameter2.Value.GetHashCode());
							if (obj.Parameter3.HasValue)
								hash.Add(obj.Parameter3.Value.GetHashCode());
							return hash.ToHashCode();
						}
					}
				}
				"""
			),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}
}
