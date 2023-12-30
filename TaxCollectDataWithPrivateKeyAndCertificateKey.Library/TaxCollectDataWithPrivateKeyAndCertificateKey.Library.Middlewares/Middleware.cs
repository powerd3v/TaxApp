namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Middlewares;

public abstract class Middleware
{
	private Middleware _next;

	public static Middleware Link(Middleware first, params Middleware[] chain)
	{
		Middleware middleware = first;
		for (int i = 0; i < chain.Length; i++)
		{
			middleware = (middleware._next = chain[i]);
		}
		return first;
	}

	public abstract string Handle(string text);

	protected string HandleNext(string text)
	{
		return (_next == null) ? text : _next.Handle(text);
	}
}
