namespace MyNihongo.Mock;

public readonly ref struct Times
{
	public readonly int Count;

	private Times(int count)
	{
		Count = count;
	}

	public static Times Exactly(in int count)
	{
		return new Times(count);
	}

	public static Times Once()
	{
		return new Times(1);
	}

	public static Times Never()
	{
		return new Times();
	}
}
