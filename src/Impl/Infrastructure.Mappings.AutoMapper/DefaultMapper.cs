using System;
using System.Collections.Generic;
using AM = AutoMapper;

namespace Core.Infrastructure.Mappings.AutoMapper
{
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
                cfg.AddProfiles(typeInAssembly);
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