using Microsoft.AspNetCore.Mvc;
using Simple.Application.Dto;
using Simple.Application.Services;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : CrudController<PersonDto>
    {
        public PersonController(IPersonService personService)
            : base(personService)
        {
        }
    }
}
