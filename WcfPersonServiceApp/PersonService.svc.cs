using System;
using System.Collections.Generic;

namespace WcfPersonServiceApp
{
    public class PersonService : IPersonService
    {
        private static readonly Dictionary<Guid, Person> _db = new Dictionary<Guid, Person>();

        static PersonService()
        {
            var id = Guid.Parse("11111111-1111-1111-1111-111111111111");
            _db[id] = new Person { Id = id, Firstname = "John", Lastname = "Doe" };
        }

        public Person GetPersonById(Guid id)
        {
            return _db.TryGetValue(id, out var p)
                ? p
                : new Person { Id = id, Firstname = "", Lastname = "" };
        }
    }
}
