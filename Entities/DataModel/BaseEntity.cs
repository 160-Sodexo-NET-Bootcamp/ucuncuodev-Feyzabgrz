using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataModel
{
    public class BaseEntity : IBaseEntity
    {

        //İlerleyen zamanlarda projenin karmaşıklaşması durumunda dblerin columnları artması durumunda generic ifadeleri bu yapı üzerinden gerçekleştireceğim .
        //Hard delete yapmak yerine IsDeleted alanının değeri değiştirilecek.Fakat bakamadım :(
         
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", Order = 0)]
        public long Id { get; set; }

        //[Required]
        //[Column("IsDeleted", Order = 15)]
        //public bool IsDeleted { get; set; }
    }
}
