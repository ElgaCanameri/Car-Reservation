using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class Room : BaseEntity<int>
    {
        public string Title { get; set; }
        public int Capacity { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<CalendarEvent> CalendarEvents { get; set; }
    }

}
