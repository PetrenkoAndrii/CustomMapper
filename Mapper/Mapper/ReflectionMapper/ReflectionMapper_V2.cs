using System.Reflection;

namespace Mapper.ReflectionMapper
{
    /// <summary>
    /// An extended reflection-based mapper that maps properties from a source object to a destination object of type TDest.
    /// Supported mapping for custom class type property
    /// </summary>
    internal class ReflectionMapper_V2 : ReflectionMapper_V1
    {
        private protected sealed override TDest Map<TDest>(object source) =>
            (TDest)Map(source, typeof(TDest));

        private object Map(object source, Type destinationType)
        {
            var sourceProps = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var destinationProps = destinationType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var result = Activator.CreateInstance(destinationType);
            foreach (var destProp in destinationProps)
            {
                var sourceProp = sourceProps.FirstOrDefault(p => p.Name == destProp.Name);
                if (sourceProp != null && sourceProp.CanRead && destProp.CanWrite)
                {
                    object? value = sourceProp.GetValue(source);
                    if (value == null || !value.GetType().IsAssignableTo(destProp.PropertyType))
                        value = Map(value!, destProp.PropertyType);

                    destProp.SetValue(result, value);
                }
            }
            return result;
        }
    }
}
