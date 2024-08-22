using Hotel.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Repositories
{
    public interface ICalendarRepository : IBaseRepository<CalendarEvent, int>
    {
        public IEnumerable<CalendarEvent> GetCalendarEventsByRoomId(int id);
    }
    public class CalendarRepository : BaseRepository<CalendarEvent, int>, ICalendarRepository
    {
        public CalendarRepository(AppDbContext dbContext) : base(dbContext) { }
        public IEnumerable<CalendarEvent> GetCalendarEventsByRoomId(int id)
        {
            return _set.Where(e => e.RoomId == id).ToList();
        }
    }

}
