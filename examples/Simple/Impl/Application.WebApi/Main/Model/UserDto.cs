using System;

namespace Application.WebApi.Main.Model
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public Guid? UtilizadorId { get; set; }
    }
}