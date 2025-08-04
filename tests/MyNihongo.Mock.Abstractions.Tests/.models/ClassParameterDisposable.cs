namespace MyNihongo.Mock.Abstractions.Tests.models;

public sealed class ClassParameterDisposable(string name) : IDisposable
{
	private bool _isDisposed;

	public string Name
	{
		get
		{
			ObjectDisposedException.ThrowIf(_isDisposed, this);
			return name;
		}
	}

	public override bool Equals(object? obj)
	{
		ObjectDisposedException.ThrowIf(_isDisposed, this);

		if (obj is not ClassParameterDisposable other)
			return false;

		ObjectDisposedException.ThrowIf(other._isDisposed, other);
		return other.Name == Name;
	}

	public override int GetHashCode()
	{
		return Name.GetHashCode();
	}

	public void Dispose()
	{
		_isDisposed = true;
	}
}
