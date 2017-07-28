using Simple.Application.Dto;
using Simple.Domain.Model;

namespace Simple.Application.Mappings
{
    public class CountryMappings : AutoMapper.Profile
    {
        public CountryMappings()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
        }
    }
}