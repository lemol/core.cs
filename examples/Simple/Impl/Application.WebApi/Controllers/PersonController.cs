using Microsoft.AspNetCore.Mvc;
using Simple.Application;
using Simple.Application.Dto;
using Simple.Domain.Data;
using Simple.Domain.Model;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : SimpleCrudController<Person, PersonDto>
    {
        public PersonController(IApplicationMapper mapper, IUnitOfWork unitOfWork, IRepository<Person> repository)
            : base(mapper, unitOfWork, repository)
        {
        }
    }
}
