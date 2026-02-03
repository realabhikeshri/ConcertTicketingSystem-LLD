using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketingSystem_LLD.Models
{
    public class Customer : User
    {
        public Customer(string id, string name, string email)
            : base(id, name, email)
        {
        }
    }
}
