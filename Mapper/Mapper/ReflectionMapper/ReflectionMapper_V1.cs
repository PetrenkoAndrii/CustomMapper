using System.Reflection;

namespace Mapper.ReflectionMapper;

/// <summary>
/// A simple reflection-based mapper that maps properties from a source object to a destination object of type TDest.
/// Not supported mapping for custom class type property
/// </summary>
internal class ReflectionMapper_V1 : IMapper
{
    public TDest Map<TSource, TDest>(TSource source) where TDest : new() =>
        source == null
            ? throw new ArgumentNullException("source is NULL")
            : Map<TDest>(source);

    private protected virtual TDest Map<TDest>(object source) where TDest : new()
    {
        var sourceProps = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        var destinationProps = typeof(TDest).GetProperties(BindingFlags.Instance | BindingFlags.Public);

        var result = new TDest();
        foreach (var destProp in destinationProps)
        {
            var sourceProp = sourceProps.FirstOrDefault(p => p.Name == destProp.Name);
            if (sourceProp != null && sourceProp.CanRead && destProp.CanWrite)
            {
                try
                {
                    destProp.SetValue(result, sourceProp.GetValue(source));
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Not supported mapping property '{sourceProp.Name}': {ex.Message}");
                    Console.ResetColor();
                    continue;
                }
            }
        }
        return result;
    }
}
