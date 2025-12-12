namespace MyNihongo.Mock;

#if NET8_0
public delegate void ActionOut<T>(out T obj);
#else
public delegate void ActionOut<T>(out T obj)
	where T : allows ref struct;
#endif
