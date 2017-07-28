using Core.Application.Services;
using Core.Domain.Model;
using Core.Domain.Data;
using Core.Infrastructure;

namespace Core.Application.WebApi.Controllers
{
    public class SimpleCrudController<TEntity, TIdentity, TDto> : CrudController<TIdentity, TDto>
        where TEntity : IEntity<TIdentity>
    {
        public SimpleCrudController(IMapper mapper, IUnitOfWork unitOfWork, IRepository<TEntity, TIdentity> repository)
            : base(new SimpleCrudService<TEntity, TIdentity, TDto>(mapper, unitOfWork, repository))
        {
        }
    }
}