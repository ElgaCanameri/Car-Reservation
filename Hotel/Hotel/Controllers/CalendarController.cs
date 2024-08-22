using Hotel.BLL.DTO;
using Hotel.BLL.DTO.Responses;
using Hotel.BLL.Services;
using Hotel.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ICalendarService _calendarService;
        private readonly IRoomService _roomService;
        public CalendarController(ICalendarService calendarService, IRoomService roomService)
        {
            _calendarService = calendarService;
            _roomService = roomService;
        }
        // GET: Calendar/Index
        public ActionResult Index()
        {
            var events = _calendarService.GetAllEvents();
            var rooms = _roomService.GetAllRooms();

            var mainModel = new RoomAndCaledarIndexModel
            {
                Events = events,
                Room = rooms
            };
            return View(mainModel);
        }



        // POST: Calendar/CreateEvent
        [HttpPost]
        public JsonResult CreateEvent(string userInput, DateTime start, DateTime end)
        {
            try
            {
                int CHECKIN_HOUR = 14; // 2 PM
                int CHECKOUT_HOUR = 10; // 10 AM
                start = start.Date.AddHours(CHECKIN_HOUR);
                end = end.Date.AddDays(-1).AddHours(CHECKOUT_HOUR);

                var existingRooms = _roomService.GetRoomsByTitle(userInput);
                var existingRoom = existingRooms.FirstOrDefault();

                if (existingRoom != null)
                {

                    var existingEvents = _calendarService.GetEventsByRoomId(existingRoom.Id);
                    bool isRoomAvailable = true;

                    foreach (var ev in existingEvents)
                    {
                        if (ev.Start.HasValue && ev.End.HasValue)
                        {
                            // Check for overlap
                            if (start < ev.End.Value && end > ev.Start.Value)
                            {
                                isRoomAvailable = false;
                                break;
                            }
                        }
                    }

                    if (isRoomAvailable)
                    {
                       
                        int eventId = _calendarService.CreateEvent($"Booked: {existingRoom.Title}", start, end, existingRoom.Id); // Pass roomId
                        return Json(new { success = true, eventId = eventId });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Room is not available for the selected dates." });
                    }
                }
                else
                {

                    int newEventId = _calendarService.CreateEvent(userInput, start, end, 0);
                    return Json(new { success = true, eventId = newEventId });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }





        [HttpPost]
        public JsonResult DeleteEvent(int eventId)
        {
            try
            {
                _calendarService.DeleteEvent(eventId);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateEventDate(int eventId, DateTime start, DateTime? end)
        {
            try
            {
                var response = _calendarService.UpdateEventDate(eventId, start, end);
                return Json(new { success = true, eventId = eventId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}