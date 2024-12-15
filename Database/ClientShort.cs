using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Database
{
    public class ClientShort
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostalZip { get; set; }
        public string Region { get; set; }
        public int CountryId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        
    }
}
