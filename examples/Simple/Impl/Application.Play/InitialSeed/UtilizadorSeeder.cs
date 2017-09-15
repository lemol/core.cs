using System.Linq;
using Domain.Data;
using Domain.Model.Auditoria;

namespace Application.Play.InitialSeed
{
    public class UtilizadorSeeder : SimpleSeeder<Utilizador>
    {
        public UtilizadorSeeder(
            IRepository<Utilizador> repository
        )
            : base(repository)
        {
        }

        public override void Seed()
        {
            new []
            {
                new Utilizador("admin", "admin@admin.com")
            }
                .ToList()
                .ForEach(_repository.Create);
        }
    }
}