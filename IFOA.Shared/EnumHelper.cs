namespace IFOA.Shared;

public static class EnumHelper
{
    // static metod to obtain list of enum values
    public static IEnumerable<T> GetValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }
}
