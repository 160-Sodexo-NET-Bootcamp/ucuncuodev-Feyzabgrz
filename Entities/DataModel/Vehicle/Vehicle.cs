using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataModel
{
    public class Vehicle : BaseEntity
    {
        public string VehicleName { get; set; }
        public string VehiclePlate { get; set; }

    }
}
