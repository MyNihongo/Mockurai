namespace MyNihongo.Mockurai.Utils;

internal static class MockClassGenerator
{
	public static string GenerateMockClass(this INamedTypeSymbol classSymbol, List<MockClassDeclaration> mocks)
	{
		var stringBuilder = new StringBuilder();
		const int indent = 1;

		return stringBuilder
			.AppendLine("#nullable enable")
			.Append("namespace ").Append(classSymbol.ContainingNamespace).AppendLine(";")
			.AppendLine()
			.Append(classSymbol.DeclaredAccessibility.GetString()).Append(" partial class ").AppendLine(classSymbol.Name)
			.AppendLine("{")
			.CreateProperties(mocks, indent)
			.CreateVerifyNoOtherCalls(classSymbol, mocks, indent)
			.AppendLine()
			.CreateVerifyInSequence(classSymbol, mocks, indent)
			.AppendLine()
			.CreateVerifySequenceContext(classSymbol, mocks, indent)
			.AppendLine()
			.Append('}')
			.ToString();
	}

	extension(StringBuilder stringBuilder)
	{
		private StringBuilder CreateProperties(List<MockClassDeclaration> mocks, int indent)
		{
			foreach (var mock in mocks)
			{
				if (mock.Property is null || !mock.Property.IsPartialDefinition)
					continue;

				var propertyName = mock.Property.Name;

				stringBuilder
					.Indent(indent)
					.Append("// ")
					.Append(propertyName)
					.AppendLine();

				stringBuilder
					.Indent(indent)
					.Append("private readonly ")
					.AppendMockClassName(mock.Type)
					.Append(' ')
					.AppendFieldName(propertyName)
					.AppendLine(" = new(InvocationIndex.CounterValue);");

				stringBuilder
					.Indent(indent)
					.Append(mock.Property.DeclaredAccessibility.GetString())
					.Append(" partial ")
					.AppendType(mock.Property.Type)
					.Append(' ')
					.Append(propertyName)
					.Append(" => ")
					.AppendFieldName(propertyName)
					.Append(';');

				stringBuilder
					.AppendLine()
					.AppendLine();
			}

			return stringBuilder;
		}

		private StringBuilder CreateVerifyNoOtherCalls(INamedTypeSymbol classSymbol, List<MockClassDeclaration> mocks, int indent)
		{
			var baseMethod = classSymbol.TryGetBaseClassMethod(methodName: "VerifyNoOtherCalls", canOverride: true);
			var beforeMethod = classSymbol.TryGetMemberByAttribute<IMethodSymbol>(
				attributePredicate: static x => x is MockGeneratorConst.BeforeVerifyNoOtherCallsAttribute or MockGeneratorConst.BeforeVerifyNoOtherCallsAttributeName
			);
			var combinedParameters = MethodSymbolUtils.CombineParameters(beforeMethod, baseMethod);

			if (!baseMethod.IsOverride(combinedParameters))
				stringBuilder.AppendOverloadResolutionPriority(baseMethod, indent);

			stringBuilder
				.Indent(indent)
				.Append("protected ")
				.TryAppendFunctionOverrideModifier(classSymbol, baseMethod, combinedParameters)
				.Append("void VerifyNoOtherCalls(")
				.AppendParameters(combinedParameters, appendDefaultValues: true)
				.AppendLine(")")
				.Indent(indent++).AppendLine("{");

			if (beforeMethod is not null)
			{
				stringBuilder
					.Indent(indent)
					.Append("this.")
					.Append(beforeMethod.Name)
					.Append('(')
					.AppendParameterNames(beforeMethod.Parameters)
					.AppendLine(");");
			}

			if (baseMethod is not null)
			{
				stringBuilder
					.Indent(indent)
					.Append("base.VerifyNoOtherCalls(")
					.AppendParameterNames(baseMethod.Parameters)
					.AppendLine(");");
			}

			foreach (var mock in mocks)
			{
				var symbol = mock.PropertyOrField;

				var skipVerifyNoOtherCalls = symbol.GetAttributeValue(
					static x => x is MockGeneratorConst.BehaviourAttribute or MockGeneratorConst.BehaviourAttributeName,
					MockGeneratorConst.SkipVerifyNoOtherCallsPropertyName,
					defaultValue: false
				);

				if (skipVerifyNoOtherCalls)
					continue;

				stringBuilder
					.Indent(indent)
					.AppendFieldOrPropertyName(symbol)
					.TryAppendNullableAnnotation(symbol)
					.AppendLine(".VerifyNoOtherCalls();");
			}

			return stringBuilder
				.Indent(--indent).AppendLine("}");
		}

		private StringBuilder CreateVerifyInSequence(INamedTypeSymbol classSymbol, List<MockClassDeclaration> mocks, int indent)
		{
			var baseMethod = classSymbol.TryGetBaseClassMethod(methodName: "VerifyInSequence", canOverride: false);

			stringBuilder
				.AppendOverloadResolutionPriority(baseMethod, indent)
				.Indent(indent).AppendLine("protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)")
				.Indent(indent++).AppendLine("{");

			string ctxVariableName;
			if (baseMethod is not null)
			{
				ctxVariableName = "thisCtx";

				stringBuilder
					.Indent(indent).AppendLine("base.VerifyInSequence(ctx =>")
					.Indent(indent++).AppendLine("{");
			}
			else
			{
				ctxVariableName = "ctx";
			}

			stringBuilder
				.Indent(indent++)
				.Append("var ")
				.Append(ctxVariableName)
				.AppendLine(" = new VerifySequenceContext(");

			if (baseMethod is not null)
			{
				stringBuilder
					.Indent(indent)
					.Append("ctx: ctx");

				if (mocks.Count > 0)
					stringBuilder.Append(',');

				stringBuilder.AppendLine();
			}

			for (int i = 0, lastIndex = mocks.Count - 1; i < mocks.Count; i++)
			{
				var symbol = mocks[i].PropertyOrField;
				var symbolName = symbol?.Name;

				stringBuilder
					.Indent(indent)
					.AppendParameterName(symbolName)
					.Append(": ")
					.AppendFieldOrPropertyName(symbol);

				if (i < lastIndex)
					stringBuilder.Append(',');

				stringBuilder.AppendLine();
			}

			stringBuilder
				.Indent(--indent).AppendLine(");").AppendLine()
				.Indent(indent).Append("verify(").Append(ctxVariableName).AppendLine(");");

			if (baseMethod is not null)
			{
				stringBuilder
					.Indent(--indent)
					.AppendLine("});");
			}

			return stringBuilder
				.Indent(--indent).AppendLine("}");
		}

		private StringBuilder AppendOverloadResolutionPriority(ISymbol? baseMethod, int indent)
		{
			const string overloadResolutionPriorityName = "OverloadResolutionPriority",
				overloadResolutionPriority = $"{overloadResolutionPriorityName}Attribute";

			var value = baseMethod.GetAttributeValue(
				static x => x == overloadResolutionPriority,
				index: 0,
				defaultValue: 0
			);

			return stringBuilder
				.Indent(indent)
				.Append($"[System.Runtime.CompilerServices.{overloadResolutionPriorityName}(")
				.Append(value + 1)
				.AppendLine(")]");
		}

		private StringBuilder CreateVerifySequenceContext(INamedTypeSymbol classSymbol, List<MockClassDeclaration> mocks, int indent)
		{
			var baseClass = classSymbol.TryGetBaseClassWithNestedClassName("VerifySequenceContext");

			stringBuilder
				.Indent(indent)
				.Append("protected ")
				.AppendIf(baseClass is not null, "new ")
				.Append("class VerifySequenceContext");

			if (baseClass is not null)
			{
				stringBuilder
					.Append(" : ")
					.AppendType(baseClass)
					.Append(".VerifySequenceContext");
			}

			stringBuilder
				.AppendLine()
				.Indent(indent++)
				.AppendLine("{");

			if (baseClass is null)
			{
				stringBuilder
					.Indent(indent)
					.AppendLine("protected readonly VerifyIndex VerifyIndex;");
			}

			// Generate properties
			var ctorAccessibility = Accessibility.Public;
			foreach (var mock in mocks)
			{
				var symbol = mock.PropertyOrField;
				var symbolName = symbol?.Name;
				var accessibility = symbol.GetVerifySequenceContextFieldAccessibility();

				if (accessibility.IsInternal)
					ctorAccessibility = accessibility;

				stringBuilder
					.Indent(indent)
					.AppendDeclaredAccessibility(accessibility)
					.Append(" readonly IMockSequence<")
					.Append(mock.Type)
					.Append('>')
					.TryAppendNullableAnnotation(symbol)
					.Append(' ')
					.AppendPropertyName(symbolName)
					.AppendLine(";");
			}

			// Generate constructor
			stringBuilder
				.AppendLine()
				.Indent(indent)
				.AppendDeclaredAccessibility(ctorAccessibility)
				.Append(" VerifySequenceContext(");

			// Constructor - parameters
			if (baseClass is not null)
			{
				stringBuilder
					.AppendType(baseClass)
					.Append(".VerifySequenceContext ctx");

				if (mocks.Count > 0)
					stringBuilder.Append(", ");
			}

			for (int i = 0, lastIndex = mocks.Count - 1; i < mocks.Count; i++)
			{
				var mock = mocks[i];

				if (mock.Property is not null)
				{
					stringBuilder
						.Append(mock.Property.Type)
						.Append(' ')
						.AppendParameterName(mock.Property.Name);
				}
				else if (mock.Field is not null)
				{
					stringBuilder
						.Append(mock.Field.Type)
						.Append(' ')
						.AppendParameterName(mock.Field.Name);
				}

				if (i < lastIndex)
					stringBuilder.Append(", ");
			}

			stringBuilder.AppendLine(")");

			if (baseClass is not null)
			{
				stringBuilder
					.Indent(indent + 1)
					.AppendLine(": base(ctx)");
			}

			stringBuilder
				.Indent(indent++)
				.AppendLine("{");

			// Constructor - initialize properties
			if (baseClass is null)
			{
				stringBuilder
					.Indent(indent)
					.AppendLine("VerifyIndex = new VerifyIndex();");
			}

			foreach (var mock in mocks)
			{
				var symbol = mock.PropertyOrField;
				var symbolName = symbol?.Name;
				var nullableAnnotation = symbol.GetNullableAnnotation();

				if (nullableAnnotation == NullableAnnotation.Annotated)
				{
					stringBuilder
						.Indent(indent)
						.Append("if (")
						.AppendParameterName(symbolName)
						.AppendLine(" is not null)");

					stringBuilder
						.Indent(indent)
						.AppendLine("{");

					indent++;
				}

				stringBuilder
					.Indent(indent)
					.AppendPropertyName(symbolName)
					.Append(" = new MockSequence<")
					.Append(mock.Type)
					.AppendLine(">");

				stringBuilder
					.Indent(indent++)
					.AppendLine("{");

				stringBuilder
					.Indent(indent)
					.AppendLine("VerifyIndex = VerifyIndex,")
					.Indent(indent)
					.Append("Mock = ").AppendParameterName(symbolName)
					.AppendLine(",");

				stringBuilder
					.Indent(--indent)
					.AppendLine("};");

				if (nullableAnnotation == NullableAnnotation.Annotated)
				{
					stringBuilder
						.Indent(--indent)
						.AppendLine("}");
				}
			}

			stringBuilder
				.Indent(--indent)
				.AppendLine("}")
				.AppendLine();

			// Copy constructor
			stringBuilder
				.Indent(indent)
				.AppendLine("protected VerifySequenceContext(VerifySequenceContext ctx)");

			if (baseClass is not null)
			{
				stringBuilder
					.Indent(indent + 1)
					.AppendLine(": base(ctx)");
			}

			stringBuilder
				.Indent(indent++)
				.AppendLine("{");

			if (baseClass is null)
			{
				stringBuilder
					.Indent(indent)
					.AppendLine("VerifyIndex = ctx.VerifyIndex;");
			}

			foreach (var mock in mocks)
			{
				var symbolName = mock.PropertyOrField?.Name;

				stringBuilder
					.Indent(indent)
					.AppendPropertyName(symbolName)
					.Append(" = ")
					.Append("ctx.")
					.AppendPropertyName(symbolName)
					.AppendLine(";");
			}

			return stringBuilder
				.Indent(--indent).AppendLine("}")
				.Indent(--indent).Append('}');
		}
	}

	private static Accessibility GetVerifySequenceContextFieldAccessibility(this ISymbol? symbol)
	{
		if (symbol is null)
			return Accessibility.Public;

		return symbol.DeclaredAccessibility.IsInternal
			? Accessibility.Internal
			: Accessibility.Public;
	}

	private static StringBuilder TryAppendFunctionOverrideModifier(this StringBuilder @this, INamedTypeSymbol classSymbol, IMethodSymbol? baseMethodSymbol, ImmutableArray<IParameterSymbol> combinedParameters)
	{
		if (baseMethodSymbol.IsOverride(combinedParameters))
			@this.Append("override ");
		else if (!classSymbol.IsSealed)
			@this.Append("virtual ");

		return @this;
	}

	private static bool IsOverride(this IMethodSymbol? @this, ImmutableArray<IParameterSymbol> combinedParameters) =>
		@this is not null && @this.Parameters.Length == combinedParameters.Length;
}
