using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Database
{
    public partial class Donation
    {
        public decimal Sum
        {
            get
            {
                return Food.Price * Amount;
            }
        }
        public Donation()
        {

        }
        public Donation(DonationShort @short)
        {
            DateOfDonation = @short.DateOfDonation;
            ClientId = @short.ClientId;
            Amount = @short.Amount;
            FoodId = @short.FoodId;
        }
    }
}
