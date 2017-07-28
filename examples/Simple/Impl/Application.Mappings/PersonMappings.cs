using System;
using Simple.Application.Dto;
using Simple.Domain.Model;

namespace Simple.Application.Mappings
{
    public class PersonMappings : AutoMapper.Profile
    {
        public PersonMappings()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>()
                .ForMember(
                    y => y.CountryId,
                    o => o.ResolveUsing(x => x.Country.Id)
                );
        }
    }
}