using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public enum ReservationStatus
    {
        Available,
        Reserved,
        Checkedin,
        CheckedOut
    }

    public class CalendarEvent : BaseEntity<int>
    {
        public string EventTitle { get; set; }

        [ForeignKey("RoomId")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool AllDay { get; set; }
        public bool IsDeleted { get; set; }
    }
}