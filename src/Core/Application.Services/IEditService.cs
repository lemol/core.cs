using System;

namespace Lemolsoft.Framework.Application.Services
{
    public interface IEditService<TIdentity, TEditDto>
    {
        TIdentity Create(TEditDto dto);
        void Update(TIdentity id, TEditDto dto);
        void Delete(TIdentity id);
    }
}