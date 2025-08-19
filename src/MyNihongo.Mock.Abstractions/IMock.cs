namespace MyNihongo.Mock;

public interface IMock<out T>
{
	T Object { get; }
}
