using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public abstract class DbEntity { }

    public abstract class BaseEntity<T1> : DbEntity
    {
        [Key]
        public T1 Id { get; set; }
    }
}
