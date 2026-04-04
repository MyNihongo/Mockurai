using System.Collections;
using System.Reflection;

namespace MyNihongo.Mockurai.Sample._Generated;

public abstract class SetupTestsBase
{
	protected static int GetSetupCount<T>(in T @this)
		where T : notnull
	{
		var type = @this.GetType();
		var setupField = type.GetField("_setups", BindingFlags.Instance | BindingFlags.NonPublic);
		dynamic value = setupField?.GetValue(@this) ?? throw new NullReferenceException("Field not found, name=`_setups`");

		var setupContainerType = value.GetType();
		setupField = setupContainerType.GetField("_setups", BindingFlags.Instance | BindingFlags.NonPublic);
		var list = (IList?)setupField?.GetValue(value) ?? throw new NullReferenceException("Field not found, name=`_setups`");

		return list.Count;
	}
}
