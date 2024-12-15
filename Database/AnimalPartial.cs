using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Database
{
    public partial class Animal
    {
        public int Days
        {
            get
            {
                return (DateTime.Today - DateOfArrival).Days;
            }
        }
        
        public string Color
        {
            get
            {
                if (GenderCode == "F")
                {
                    return "Pink";
                } else
                {
                    return "LightBlue";
                }
            }
        }
    }
}
