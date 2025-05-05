namespace WebService.Authorization.Shard.Extensions;

public static class IEnumerableExtension
{
    public static bool IsAny<T>(this IEnumerable<T>? source)
    {
        return source != null && source.Any();
    }
}