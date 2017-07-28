using Microsoft.AspNetCore.Mvc;
using Simple.Application;
using Simple.Application.Dto;
using Simple.Domain.Data;
using Simple.Domain.Model;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CountryController : SimpleCrudController<Country, CountryDto>
    {
        public CountryController(IApplicationMapper mapper, IUnitOfWork unitOfWork, IRepository<Country> repository)
            : base(mapper, unitOfWork, repository)
        {
        }
    }
}
