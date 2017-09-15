using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Application.Dto.Auditoria;
using Domain.Data;
using Domain.Data.Repositories.Auditoria;
using Domain.Model.Auditoria;

namespace Application.Services.Auditoria
{
    public class UtilizadorService : SimpleCrudService<Utilizador, UtilizadorDto, UtilizadorQuery>, IUtilizadorService
    {
        public UtilizadorService(
            IApplicationMapper mapper
            , IUnitOfWork unitOfWork
            , IUtilizadorRepository repository)
            : base(mapper, unitOfWork, repository, Includes)
        {
        }
        
        public override IEnumerable<TDto> GetQuery<TDto>(UtilizadorQuery query)
        {
            var q = _repository
                .Query();

            return _mapper.Map<Utilizador, TDto>(q);
        }

        public override Guid Create(UtilizadorDto dto)
        {
            var utilizador = new Utilizador(dto.Nome, dto.Email);
            _repository.Create(utilizador);
            _unitOfWork.Commit();

            return utilizador.Id;
        }

        public override void Update(Guid id, UtilizadorDto dto)
        {
            var utilizador = _repository.Find(id);
            utilizador.Alterar(dto.Nome, dto.Email);

            _unitOfWork.Commit();
        }

        public override TDto Find<TDto>(Guid id)
        {
            var item = _repository
                .Query()
                .FirstOrDefault(x => id.Equals(x.Id));

            var itemDto = _mapper.Map<Utilizador, TDto>(item);
            return itemDto;
        }

        private static IEnumerable<Expression<Func<Utilizador, object>>> Includes
        {
            get
            {
                return Enumerable.Empty<Expression<Func<Utilizador, object>>>();
            }
        }
    }
}