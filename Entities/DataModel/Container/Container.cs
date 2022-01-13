using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataModel
{
    public class Container : BaseEntity
    {
      
        public string ContainerName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        [ForeignKey("Vehicle")]
        public long VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

    }
}
