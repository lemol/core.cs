namespace Simple.Domain.Model
{
    public class Person : EntityBase
    {
        #region Fields
        public string Name { get; private set; }
        #endregion

        #region Constructores
        public Person(string name)
        {
            NewEntity();
            Name = name;
        }
        #endregion
    }
}