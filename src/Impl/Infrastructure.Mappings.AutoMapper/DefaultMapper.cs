using System;
using System.Linq;
using System.Collections.Generic;
using AM = AutoMapper;

namespace Core.Infrastructure.Mappings.AutoMapper
{
  public static class MappingsExtensions
  {
    public static AM.IMappingExpression<TSource, TDestination> Flatilize<TSource, TDestination>(this AM.IMappingExpression<TSource, TDestination> mapping)
    {
      return mapping.AfterMap((source, destination) =>
      {
        if (destination == null)
          return;

        typeof(TDestination)
                  .GetProperties()
                  .Where(x => x.PropertyType.GetProperty("Id") != null)
                  .ToList()
                  .ForEach(x =>
                  {
                x
                          .SetValue(destination, null, null);
              });
      });
    }
  }
  public class DefaultMapper : IMapper
  {
    #region Campos
    private readonly AM.IMapper _mapper;
    #endregion

    #region Constructores
    public DefaultMapper(Type typeInAssembly)
    {
      var configuration = new AM.MapperConfiguration(cfg =>
      {
        cfg.AddMaps(typeInAssembly);
      });

      _mapper = configuration.CreateMapper();
    }
    #endregion

    #region IMapper
    public TDestination Map<TSource, TDestination>(TSource source)
    {
      return _mapper.Map<TSource, TDestination>(source);
    }

    public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
    {
      return _mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
    }
    #endregion
  }
}