namespace Order.Domain;

public static class SystemTime
{
    private static Func<DateTime> _func = () => DateTime.UtcNow;

    public static DateTime Now => _func();

    public static void Initialisera(Func<DateTime> func)
    {
        _func = func;
    }

    public static void Reset()
    {
        _func = () => DateTime.UtcNow;
    }
}
