namespace MyNihongo.Mock;

public delegate void ActionOut<T>(out T obj)
	where T : allows ref struct;
