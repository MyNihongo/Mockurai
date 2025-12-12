namespace MyNihongo.Mock;

#if NET8_0
public delegate void ActionRef<T>(ref T obj);
#else
public delegate void ActionRef<T>(ref T obj)
	where T : allows ref struct;
#endif
