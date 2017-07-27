using System;
using Core.Infrastructure;
using Simple.Application.Dto;
using Simple.Domain.Data;
using Simple.Domain.Model;

namespace Simple.Application.Services
{
    public class PersonService : CrudService<Person, PersonDto>
    {
        public PersonService(IApplicationMapper mapper, IUnitOfWork unitOfWork, IRepository<Person> repository)
            : base(mapper, unitOfWork, repository)
        {
        }

        protected override Guid CreateAbstract(PersonDto dto)
        {
            var person = new Person(dto.Name);
            _repository.Create(person);
            return person.Id;
        }

        protected override void UpdateAbstract(Guid id, PersonDto dto)
        {
            var person = _repository.Find(id);
            _repository.Update(person);
        }
    }
}