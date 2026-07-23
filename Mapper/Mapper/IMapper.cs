namespace Mapper;

internal interface IMapper
{
    TDest Map<TSource, TDest>(TSource source) where TDest : new();
}
