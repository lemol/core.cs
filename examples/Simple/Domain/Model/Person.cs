using Core.Domain;

namespace Simple.Domain.Model
{
    public class Person : EntityBase
    {
        #region Fields
        public string Name { get; protected set; }
        #endregion

        #region Constructores
        public Person(string name)
        {
            Name = name;
        }
        #endregion
    }
}