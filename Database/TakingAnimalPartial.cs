using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Database
{
    public partial class TakingAnimal
    {
        public TakingAnimal(TakingAnimalShort @short)
        {
            ClientId = @short.ClientId;
            AnimalId = @short.AnimalId;
            DateOfTaking = @short.DateOfTaking;
        }
        public TakingAnimal()
        {

        }
    }
}
