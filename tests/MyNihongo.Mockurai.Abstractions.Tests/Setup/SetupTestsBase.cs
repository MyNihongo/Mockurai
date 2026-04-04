using System.Reflection;

namespace MyNihongo.Mockurai.Abstractions.Tests.Setup;

public abstract class SetupTestsBase
{
	protected static int GetSetupCount<T>(in T @this)
		where T : notnull
	{
		var type = @this.GetType().BaseType;
		var setupField = type?.GetField("Setups", BindingFlags.Instance | BindingFlags.NonPublic);
		dynamic value = setupField?.GetValue(@this) ?? throw new NullReferenceException("Field not found, name=`Setups`");

		var setupContainerType = value.GetType();
		setupField = setupContainerType?.GetField("_setups", BindingFlags.Instance | BindingFlags.NonPublic);
		var list = (IList?)setupField?.GetValue(value) ?? throw new NullReferenceException("Field not found, name=`_setups`");

		return list.Count;
	}
}
