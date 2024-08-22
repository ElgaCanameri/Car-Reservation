using Hotel.DAL.Entities;
using Hotel.BLL.DTO.Responses;

namespace Hotel.BLL.DTO.Responses
{
    public class RoomAndCaledarIndexModel
    {
        public IEnumerable<CalendarEvent> Events { get; set; }
        public IEnumerable<RoomIndexModel> Room { get; set; }
    }
}
