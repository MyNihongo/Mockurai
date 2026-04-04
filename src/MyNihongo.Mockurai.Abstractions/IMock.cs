namespace MyNihongo.Mockurai;

public interface IMock<out T>
{
	T Object { get; }
}
