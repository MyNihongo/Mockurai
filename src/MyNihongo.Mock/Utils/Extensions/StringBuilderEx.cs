using System.Text;

namespace MyNihongo.Mock.Utils;

internal static class StringBuilderEx
{
	extension(StringBuilder @this)
	{
		public StringBuilder Indent(int times)
		{
			for (var i = 0; i < times; ++i)
				@this.Append('\t');

			return @this;
		}

		public StringBuilder AppendFieldName(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
				return @this;

			return @this
				.Append('_')
				.Append(char.ToLower(propertyName[0]))
				.Append(propertyName.Substring(1));
		}

		public StringBuilder AppendParameterName(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
				return @this;

			return @this
				.Append(char.ToLower(propertyName[0]))
				.Append(propertyName.Substring(1));
		}
	}
}
