namespace MyNihongo.Mock;

public delegate void ActionRef<T>(ref T obj)
	where T : allows ref struct;
