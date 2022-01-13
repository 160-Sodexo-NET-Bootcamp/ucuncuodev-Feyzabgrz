using Data.Dtos;
using Data.Generic;
using Entities.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ContainerRepo
{
    public interface IContainerRepository : IGenericRepository<Container>
    {
    }
}