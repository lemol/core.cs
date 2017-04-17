using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lemolsoft.Framework.Domain;
using Simple.Domain.Data;
using Microsoft.AspNetCore.Mvc;
using Simple.Application.WebService.Dto;

namespace Simple.Application.WebService.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly IPersonRepository _repo;
        private readonly IUnitOfWork _uow;
        // GET api/values
        [HttpGet]
        public IEnumerable<PersonDto> Get()
        {
            return _repo.Query().Select(x => new PersonDto
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
