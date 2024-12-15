using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Database
{
    public partial class Client
    {
        public Client(ClientShort clientShort)
        {
            FirstName = clientShort.FirstName;
            LastName = clientShort.LastName;
            Address = clientShort.Address;
            Email = clientShort.Email;
            Phone = clientShort.Phone;
            Region = clientShort.Region;
            CountryId = clientShort.CountryId;
            PostalZip = clientShort.PostalZip;
            Login = clientShort.Login;
            Password = clientShort.Password;
        }
        public void AddEditPassportData(PassportData passport)
        {
            Address = passport.Address;
            Serial = passport.Serial;
            Number = passport.Number;
            IssuedBy = passport.IssuedBy;
            DateOfIssue = passport.DateOfIssue;
            RegistrationAddress = passport.RegistrationAddress;

        }
    }
}
