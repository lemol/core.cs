using System;
using Microsoft.AspNetCore.Identity;
using Domain.Model.Auditoria;

namespace Application.WebApi.Main.Model
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public Guid? UtilizadorId { get; set; }
    }
}