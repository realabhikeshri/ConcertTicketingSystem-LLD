using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Models
{
    public abstract class User
    {
        public string Id { get; }
        public string Name { get; }
        public string Email { get; }

        protected User(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
