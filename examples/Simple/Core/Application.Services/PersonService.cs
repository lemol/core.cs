using System;
using Core.Infrastructure;
using Simple.Domain;
using Simple.Domain.Model;

namespace Simple.Application.Services
{
    public class PersonService : CrudService<Person, PersonDto>
    {
        public PersonService(IMapper mapper, IRepository<Person> repository)
            : base(mapper, repository)
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
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}