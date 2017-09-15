using System;
using Microsoft.AspNetCore.Identity;

namespace Application.WebApi.Main.Model
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}