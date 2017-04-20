using System;

namespace Lemolsoft.Framework.Application.Services
{
    public interface IEditService<TEditDto>
    {
        Guid Create(TEditDto dto);
        void Update(Guid id, TEditDto dto);
        void Delete(Guid id);
    }
}