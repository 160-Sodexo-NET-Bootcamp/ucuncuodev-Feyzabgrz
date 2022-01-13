using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataModel
{
    public interface IBaseEntity
    {
        long Id { get; set; }
        //bool IsDeleted { get; set; }
    }
}
