using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Infrastructure;
using Simple.Application.Dto;
using Simple.Domain.Data;
using Simple.Domain.Model;

namespace Simple.Application.Services
{
    public class PersonService : CrudService<Person, PersonDto>, IPersonService
    {
        #region Fields
        private readonly IRepository<Country> _countryRepository;
        #endregion

        public PersonService(IApplicationMapper mapper
            , IUnitOfWork unitOfWork
            , IRepository<Person> repository
            , IRepository<Country> countryRepository)
            : base(mapper, unitOfWork, repository)
        {
            _countryRepository = countryRepository;
        }

        protected override IEnumerable<Expression<Func<Person, object>>> GetAllIncludes
        {
            get
            {
                return base
                    .GetAllIncludes
                    .Union(new Expression<Func<Person, object>>[]
                    {
                        x => x.Country
                    });
            }
        }

        protected override Guid CreateAbstract(PersonDto dto)
        {
            var country = _countryRepository.Find(dto.Country.Id);
            var person = new Person(dto.Name, country);
            
            _repository.Create(person);
            return person.Id;
        }

        protected override void UpdateAbstract(Guid id, PersonDto dto)
        {
            var person = _mapper.Map<PersonDto, Person>(dto);
            _repository.Update(person);
        }
    }
}