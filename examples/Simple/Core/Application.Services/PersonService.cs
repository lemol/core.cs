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
    }
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}