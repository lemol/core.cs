using System;
using Core.Domain.Model;
using Core.Domain.Data;
using Core.Infrastructure;

namespace Core.Application.Services
{
    public class SimpleCrudService<TEntity, TIdentity, TEditDto> : CrudService<TEntity, TIdentity, TEditDto>
        where TEntity : IEntity<TIdentity>
    {
        public SimpleCrudService(IMapper mapper, IUnitOfWork unitOfWork, IRepository<TEntity, TIdentity> repository)
            : base(mapper, unitOfWork, repository)
        {
        }

        protected override TIdentity CreateAbstract(TEditDto dto)
        {
            var entity = _mapper.Map<TEditDto, TEntity>(dto);
            _repository.Create(entity);
            _unitOfWork.Commit();
            return entity.Id;
        }

        protected override void UpdateAbstract(TIdentity id, TEditDto dto)
        {
            var entity = _mapper.Map<TEditDto, TEntity>(dto);
            _repository.Update(entity);
            _unitOfWork.Commit();
        }
    }
}