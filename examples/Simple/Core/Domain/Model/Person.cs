using System;

namespace Simple.Domain.Model
{
    public class Person : EntityBase
    {
        #region Fields
        public string Name { get; private set; }
        public Guid CountryId { get; private set; }
        public Country Country { get; private set; }  
        #endregion

        #region Constructores
        private Person()
        {
        }

        public Person(string name, Country country)
        {
            NewEntity();
            Name = name;
            Country = Country;
            CountryId = Country?.Id ?? default(Guid);
        }
        #endregion
    }
}