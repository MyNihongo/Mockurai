using MockClassName = (string Constructor, string Type, string GenericTypes);

namespace MyNihongo.Mockurai.Utils;

internal static class MockImplementationGenerator
{
	public static MockImplementationResult GenerateMockImplementation(this ITypeSymbol typeSymbol, CompilationCombinedResult result)
	{
		var stringBuilder = new StringBuilder();
		var typeString = typeSymbol.ToString();

		var accessibility = GetAccessibility(typeSymbol);
		var accessibilityString = accessibility.GetString();

		var (constructor, mockClassName, genericTypes) = CreateMockClassName(stringBuilder, typeSymbol);
		var mockedTypeSymbol = new MockedTypeSymbol(typeSymbol);
		var mockableMembers = GetMockableMembers(typeSymbol);

		var source = stringBuilder
			.AppendLine("#nullable enable")
			.Append("namespace ").Append(result.Options.RootNamespace).AppendLine(";")
			.AppendLine()
			.Append(accessibilityString).Append(" sealed class ").Append(mockClassName).Append(" : IMock<").Append(typeString).AppendLine(">")
			.AppendLine("{")
			.Indent(1).AppendLine("private readonly InvocationIndex.Counter _invocationIndex;")
			.Indent(1).AppendLine("private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;")
			.Indent(1).AppendLine("private Proxy? _proxy;")
			.AppendLine()
			.Indent(1).Append("public ").Append(constructor).AppendLine("(InvocationIndex.Counter invocationIndex)")
			.Indent(1).AppendLine("{")
			.Indent(2).AppendLine("_invocationIndex = invocationIndex;")
			.Indent(2).AppendLine("_invocationProviders = GetInvocations;")
			.Indent(1).AppendLine("}")
			.AppendLine()
			.Indent(1).Append("public ").Append(typeString).AppendLine(" Object => _proxy ??= new Proxy(this);")
			.AppendLine()
			.Indent(1).AppendLine("public InvocationContainer Invocations => field ??= new InvocationContainer(this);")
			.AppendLine()
			.CreateMockMethods(mockedTypeSymbol, mockableMembers, indent: 1, out var methodSymbols)
			.Indent(1).AppendLine("public void VerifyNoOtherCalls()")
			.Indent(1).AppendLine("{")
			.CreateVerifyNoOtherCalls(mockableMembers, indent: 2)
			.Indent(1).AppendLine("}")
			.AppendLine()
			.Indent(1).AppendLine("private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()")
			.Indent(1).AppendLine("{")
			.CreateGetInvocations(mockableMembers, indent: 2).AppendLine()
			.Indent(1).AppendLine("}")
			.AppendLine()
			.Indent(1).Append("private sealed class Proxy : ").AppendLine(typeString)
			.Indent(1).AppendLine("{")
			.Indent(2).Append("private readonly ").Append(mockClassName).AppendLine(" _mock;")
			.AppendLine()
			.Indent(2).Append("public Proxy(").Append(mockClassName).AppendLine(" mock)")
			.Indent(2).AppendLine("{")
			.Indent(3).AppendLine("_mock = mock;")
			.Indent(2).AppendLine("}")
			.CreateProxyMethods(mockedTypeSymbol, mockableMembers, indent: 2)
			.Indent(1).AppendLine("}")
			.AppendLine()
			.Indent(1).AppendLine("public sealed class InvocationContainer")
			.Indent(1).AppendLine("{")
			.Indent(2).Append("private readonly ").Append(mockClassName).AppendLine(" _mock;")
			.AppendLine()
			.Indent(2).Append("public InvocationContainer(").Append(mockClassName).AppendLine(" mock)")
			.Indent(2).AppendLine("{")
			.Indent(3).AppendLine("_mock = mock;")
			.Indent(2).AppendLine("}")
			.CreateInvocationContainerProperties(mockedTypeSymbol, mockableMembers, indent: 2)
			.Indent(1).AppendLine("}")
			.AppendLine("}")
			.AppendLine()
			.AppendLine("public static partial class MockExtensions")
			.AppendLine("{")
			.Indent(1).Append("extension").Append(genericTypes).Append("(IMock<").Append(typeString).AppendLine("> @this)")
			.Indent(1).AppendLine("{")
			.Indent(2).Append(accessibilityString).Append(' ').Append(mockClassName).Append(".InvocationContainer Invocations => ((").Append(mockClassName).AppendLine(")@this).Invocations;")
			.AppendLine()
			.Indent(2).Append(accessibilityString).AppendLine(" void VerifyNoOtherCalls() =>")
			.Indent(3).Append("((").Append(mockClassName).AppendLine(")@this).VerifyNoOtherCalls();")
			.CreateMockExtensions(mockedTypeSymbol, mockableMembers, mockClassName, accessibility, indent: 2)
			.Indent(1).AppendLine("}")
			.AppendLine("}")
			.AppendLine()
			.AppendLine("public static partial class MockSequenceExtensions")
			.AppendLine("{")
			.Indent(1).Append("extension").Append(genericTypes).Append("(IMockSequence<").Append(typeString).AppendLine("> @this)")
			.Indent(1).AppendLine("{")
			.CreateMockSequenceExtensions(mockedTypeSymbol, mockableMembers, mockClassName, accessibility, indent: 2)
			.Indent(1).AppendLine("}")
			.Append('}')
			.ToString();

		return new MockImplementationResult(
			name: mockClassName,
			source: source,
			methodSymbols: methodSymbols
		);
	}

	private static Accessibility GetAccessibility(ITypeSymbol typeSymbol)
	{
		return typeSymbol.DeclaredAccessibility.IsInternal
			? Accessibility.Internal
			: Accessibility.Public;
	}

	private static MockClassName CreateMockClassName(StringBuilder stringBuilder, ITypeSymbol typeSymbol)
	{
		stringBuilder.Clear();
		stringBuilder.AppendMockClassName(typeSymbol, appendGenericTypes: false);
		var constructorName = stringBuilder.ToString();

		stringBuilder.Clear();
		stringBuilder.AppendGenericTypes(typeSymbol);

		if (stringBuilder.Length == 0)
			return (constructorName, constructorName, string.Empty);

		var genericTypes = stringBuilder.ToString();
		stringBuilder.Clear();

		return (constructorName, constructorName + genericTypes, genericTypes);
	}

	private static IReadOnlyList<MockedMemberSymbol> GetMockableMembers(ITypeSymbol typeSymbol)
	{
		var symbols = typeSymbol.TypeKind switch
		{
			TypeKind.Class => typeSymbol.GetOverridableMembers(),
			TypeKind.Interface => typeSymbol.GetInterfaceMembers(),
			_ => [],
		};

		return symbols
			.FilterMockableSymbols()
			.ToLookup(static x => x.GetSymbolName())
			.SelectMany(static x => x.Select((y, i) => new MockedMemberSymbol($"{x.Key}{i}", i, y)))
			.ToArray();
	}

	extension(StringBuilder stringBuilder)
	{
		private StringBuilder CreateMockMethods(MockedTypeSymbol typeSymbol,
			IReadOnlyList<MockedMemberSymbol> members,
			int indent,
			out IReadOnlyList<IMethodSymbol>? methodSymbols)
		{
			List<IMethodSymbol>? methodSymbolList = null;

			foreach (var member in members)
			{
				Func<StringBuilder, MockedTypeSymbol, MockedMemberSymbol, int, ImmutableArray<IMethodSymbol>>? handler = member.Symbol.Kind switch
				{
					SymbolKind.Event => MockImplementationEventGenerator.AppendEventMockMethod,
					SymbolKind.Property => MockImplementationPropertyGenerator.AppendPropertyMockMethod,
					SymbolKind.Method => MockImplementationMethodGenerator.AppendMethodMockMethod,
					_ => null,
				};

				if (handler is null)
					continue;

				var methodSymbolResults = handler(stringBuilder, typeSymbol, member, indent);
				if (!methodSymbolResults.IsDefaultOrEmpty)
				{
					var methodSymbolsToAdd = methodSymbolResults
						.Where(static x => x.Parameters.Length >= 2);

					methodSymbolList ??= [];
					methodSymbolList.AddRange(methodSymbolsToAdd);
				}

				stringBuilder
					.AppendLine()
					.AppendLine();
			}

			methodSymbols = methodSymbolList;
			return stringBuilder;
		}

		private StringBuilder CreateVerifyNoOtherCalls(IReadOnlyList<MockedMemberSymbol> members, int indent)
		{
			foreach (var member in members)
			{
				Func<MockedMemberSymbol, IEnumerable<IMethodSymbol>>? methods = member.Symbol.Kind switch
				{
					SymbolKind.Event => MockImplementationEventGenerator.GetEventMethods,
					SymbolKind.Property => MockImplementationPropertyGenerator.GetPropertyMethods,
					SymbolKind.Method => MockImplementationMethodGenerator.GetMethodMethods,
					_ => null,
				};

				if (methods is null)
					continue;

				foreach (var method in methods(member))
				{
					stringBuilder
						.Indent(indent)
						.AppendInvocationFieldName(member.MemberName, method.MethodKind)
						.AppendVerifyNoOtherCallsInvocation()
						.AppendLine(";");
				}
			}

			return stringBuilder;
		}

		private StringBuilder CreateGetInvocations(IReadOnlyList<MockedMemberSymbol> members, int indent)
		{
			if (members.Count == 0)
			{
				stringBuilder
					.Indent(indent)
					.Append("yield break;");

				return stringBuilder;
			}

			var methodIndex = 0;
			foreach (var member in members)
			{
				Func<MockedMemberSymbol, IEnumerable<IMethodSymbol>>? methods = member.Symbol.Kind switch
				{
					SymbolKind.Event => MockImplementationEventGenerator.GetEventMethods,
					SymbolKind.Property => MockImplementationPropertyGenerator.GetPropertyMethods,
					SymbolKind.Method => MockImplementationMethodGenerator.GetMethodMethods,
					_ => null,
				};

				if (methods is null)
					continue;

				foreach (var method in methods(member))
				{
					if (methodIndex > 0)
						stringBuilder.AppendLine();

					stringBuilder
						.Indent(indent)
						.Append("yield return ")
						.AppendInvocationFieldName(member.MemberName, method.MethodKind)
						.Append(';');

					methodIndex++;
				}
			}

			return stringBuilder;
		}

		private StringBuilder CreateProxyMethods(MockedTypeSymbol typeSymbol, IReadOnlyList<MockedMemberSymbol> members, int indent)
		{
			var generatedCount = 0;
			foreach (var member in members)
			{
				Action<StringBuilder, MockedTypeSymbol, MockedMemberSymbol, int>? handler = member.Symbol.Kind switch
				{
					SymbolKind.Event => MockImplementationEventGenerator.AppendProxyEventImplementation,
					SymbolKind.Property => MockImplementationPropertyGenerator.AppendProxyPropertyImplementation,
					SymbolKind.Method => MockImplementationMethodGenerator.AppendProxyMethodImplementation,
					_ => null,
				};

				if (handler is null)
					continue;

				// Since StringBuilder.Insert(startIndex, Environment.NewLine) does not work in source generators,
				// forced to do this workaround to prepend a new line
				switch (generatedCount)
				{
					case 0:
						stringBuilder.AppendLine();
						break;
					case > 0:
						stringBuilder.AppendLine().AppendLine();
						break;
				}

				handler(stringBuilder, typeSymbol, member, indent);
				generatedCount++;
			}

			foreach (var member in typeSymbol.TypeSymbol.GetIrrelevantOverridableMembers().FilterMockableSymbols())
			{
				Action<StringBuilder, ISymbol, int>? handler = member.Kind switch
				{
					SymbolKind.Event => MockImplementationEventGenerator.AppendProxyEventDummyImplementation,
					SymbolKind.Property => MockImplementationPropertyGenerator.AppendProxyPropertyDummyImplementation,
					SymbolKind.Method => MockImplementationMethodGenerator.AppendProxyMethodDummyImplementation,
					_ => null,
				};

				if (handler is null)
					continue;

				// Since StringBuilder.Insert(startIndex, Environment.NewLine) does not work in source generators,
				// forced to do this workaround to prepend a new line
				switch (generatedCount)
				{
					case 0:
						stringBuilder.AppendLine();
						break;
					case > 0:
						stringBuilder.AppendLine().AppendLine();
						break;
				}

				handler(stringBuilder, member, indent);
				generatedCount++;
			}

			return generatedCount > 0
				? stringBuilder.AppendLine()
				: stringBuilder;
		}

		private StringBuilder CreateInvocationContainerProperties(
			MockedTypeSymbol typeSymbol,
			IReadOnlyList<MockedMemberSymbol> members,
			int indent)
		{
			if (members.Count > 0)
				stringBuilder.AppendLine();

			for (int i = 0, generatedCount = 0; i < members.Count; i++)
			{
				var memberSymbol = members[i];
				Func<MockedMemberSymbol, IEnumerable<IMethodSymbol>>? handler = memberSymbol.Symbol.Kind switch
				{
					SymbolKind.Event => MockImplementationEventGenerator.GetEventMethods,
					SymbolKind.Property => MockImplementationPropertyGenerator.GetPropertyMethods,
					SymbolKind.Method => MockImplementationMethodGenerator.GetMethodMethods,
					_ => null,
				};

				if (handler is null)
					continue;

				foreach (var method in handler(memberSymbol))
				{
					const string fieldPrefix = MockGeneratorConst.Suffixes.MockVariableCall;
					var symbolName = memberSymbol.Symbol.GetSymbolName();

					if (generatedCount > 0)
						stringBuilder.AppendLine().AppendLine();

					stringBuilder
						.Indent(indent)
						.Append("public System.Collections.Generic.IEnumerable<")
						.AppendInvocationInterface(method)
						.Append("> ")
						.AppendPropertyName(symbolName, method.MethodKind);

					// For not this is meant to solve method overload naming collisions.
					// maybe in the future we can come up with something better
					if (memberSymbol.Index > 0)
						stringBuilder.Append(memberSymbol.Index + 1);

					if (method.TypeArguments.IsDefaultOrEmpty)
					{
						stringBuilder
							.Append($" => {fieldPrefix}")
							.AppendInvocationFieldName(memberSymbol.MemberName, method.MethodKind)
							.Append("?.")
							.AppendInvocationGetMethodName(method)
							.Append("() ?? [];");
					}
					else
					{
						stringBuilder
							.AppendGenericTypes(method.TypeArguments).AppendLine("()")
							.Indent(indent++).AppendLine("{")
							.Indent(indent).AppendInvocationDeclaration(method, typeSymbol, memberSymbol, fieldPrefix, indent, out var genericTypeNames).AppendLine();

						stringBuilder
							.Indent(indent)
							.Append("return ");

						if (genericTypeNames.IsDefaultOrEmpty)
						{
							stringBuilder
								.Append(fieldPrefix)
								.AppendFieldName(memberSymbol.MemberName, method.MethodKind, suffix: MockGeneratorConst.Suffixes.Invocation);
						}
						else
						{
							stringBuilder.AppendParameterName(memberSymbol.MemberName, method.MethodKind, suffix: MockGeneratorConst.Suffixes.Invocation);
						}

						stringBuilder
							.Append('.')
							.AppendInvocationGetMethodName(method)
							.AppendLine("() ?? [];");

						stringBuilder
							.Indent(--indent)
							.Append('}');
					}

					generatedCount++;
				}
			}

			return members.Count > 0
				? stringBuilder.AppendLine()
				: stringBuilder;
		}

		private StringBuilder CreateMockExtensions(
			MockedTypeSymbol typeSymbol,
			IReadOnlyList<MockedMemberSymbol> members,
			string mockClassName,
			Accessibility accessibility,
			int indent)
		{
			if (members.Count > 0)
				stringBuilder.AppendLine();

			var generatedCount = 0;
			foreach (var member in members)
			{
				Action<StringBuilder, MockedTypeSymbol, MockedMemberSymbol, string, Accessibility, int>? handler = member.Symbol.Kind switch
				{
					SymbolKind.Event => MockImplementationEventGenerator.AppendEventMockExtensions,
					SymbolKind.Property => MockImplementationPropertyGenerator.AppendPropertyMockExtensions,
					SymbolKind.Method => MockImplementationMethodGenerator.AppendMethodMockExtensions,
					_ => null,
				};

				if (handler is null)
					continue;

				if (generatedCount > 0)
				{
					stringBuilder
						.AppendLine()
						.AppendLine();
				}

				stringBuilder.AppendNameComment(member, indent);
				handler(stringBuilder, typeSymbol, member, mockClassName, accessibility, indent);
				generatedCount++;
			}

			return members.Count > 0
				? stringBuilder.AppendLine()
				: stringBuilder;
		}

		private StringBuilder CreateMockSequenceExtensions(
			MockedTypeSymbol mockedTypeSymbol,
			IReadOnlyList<MockedMemberSymbol> members,
			string mockClassName,
			Accessibility accessibility,
			int indent)
		{
			var generatedCount = 0;
			foreach (var member in members)
			{
				Action<StringBuilder, MockedTypeSymbol, MockedMemberSymbol, string, Accessibility, int>? handler = member.Symbol.Kind switch
				{
					SymbolKind.Event => MockImplementationEventGenerator.AppendEventMockSequenceExtensions,
					SymbolKind.Property => MockImplementationPropertyGenerator.AppendPropertyMockSequenceExtensions,
					SymbolKind.Method => MockImplementationMethodGenerator.AppendMethodMockSequenceExtensions,
					_ => null,
				};

				if (handler is null)
					continue;

				if (generatedCount > 0)
				{
					stringBuilder
						.AppendLine()
						.AppendLine();
				}

				stringBuilder.AppendNameComment(member, indent);
				handler(stringBuilder, mockedTypeSymbol, member, mockClassName, accessibility, indent);
				generatedCount++;
			}

			return members.Count > 0
				? stringBuilder.AppendLine()
				: stringBuilder;
		}
	}
}
