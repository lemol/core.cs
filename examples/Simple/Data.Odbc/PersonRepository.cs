using System;
using System.Linq;
using Simple.Domain.Data;
using Simple.Domain.Model;
using System.

namespace Simple.Data.Odbc
{
    public class PersonRepository : IPersonRepository
    {
        OdbcConnection _connection;
        public void Create(Person item)
        {
            throw new NotImplementedException();
        }

        public Person Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Person> Query()
        {
            throw new NotImplementedException();
        }

        public void Update(Person item)
        {
            throw new NotImplementedException();
        }
    }
}