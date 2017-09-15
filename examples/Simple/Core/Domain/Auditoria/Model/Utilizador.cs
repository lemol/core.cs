using System;

namespace Domain.Model.Auditoria
{
    public class Utilizador : EntityBase
    {
        #region Propriedades
        public string Nome { get; private set; }
        public string Email { get; private set; }
        #endregion

        #region Constructores
        private Utilizador()
        {
        }

        public Utilizador(string nome, string email)
            : this()
        {
            NewEntity();
            Nome = nome;
            Email = email;
        }

        public void Alterar(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }
        #endregion
    }
}