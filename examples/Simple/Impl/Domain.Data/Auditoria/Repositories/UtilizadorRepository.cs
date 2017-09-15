using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Data;
using Domain.Model.Auditoria;

namespace Domain.Data.Repositories.Auditoria
{
    public class UtilizadorRepository : RepositoryBase<Utilizador>, IUtilizadorRepository
    {
        public UtilizadorRepository(DefaultDbContext context)
            : base(context)
        {
        }

        public override IQueryable<Utilizador> Query()
        {
            return _dbSet;
        }
    }
}