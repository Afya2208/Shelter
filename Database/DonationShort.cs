using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Database
{
    public class DonationShort
    {
        public System.DateTime DateOfDonation { get; set; }
        public int ClientId { get; set; }
        public int FoodId { get; set; }
        public int Amount { get; set; }
    }
}
