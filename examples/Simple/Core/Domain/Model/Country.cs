namespace Simple.Domain.Model
{
    public class Country : EntityBase
    {
        #region Properties
        public string Name { get; private set; }
        #endregion

        #region Constructors
        private Country()
        {
        }

        public Country(string name)
        {
            NewEntity();
            Name = name;
        }
        #endregion
    }
}