using Hotel.BLL.Services;
using Hotel.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;


namespace Hotel.Controllers
{
    public class ExcelController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly ICalendarService _calendarService;

        public ExcelController(IRoomService roomService, ICalendarService calendarService)
        {
            _roomService = roomService;
            _calendarService = calendarService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public FileResult ExportRoomEvents()
        {
          DateTime currentDate = DateTime.Now;
            int currentYear = currentDate.Year;
            int currentMonth = currentDate.Month;
           
            var events = _calendarService.GetAllEvents(); // Replace with your actual method to fetch events
            var rooms = _roomService.GetAllRooms().Select(r => new Room
            {
                Id = r.Id,
                Capacity = r.Capacity,
                Description = r.Description,
                Price = r.Price,
                Title = r.Title,
            }).ToList();
            var roomEvents = GetRoomEventsForMonth(events, rooms, currentYear, currentMonth);         
           
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Room Events");

                worksheet.Cells[1, 1].Value = "Room Title";
                worksheet.Cells[1, 2].Value = "Price";
                worksheet.Cells[1, 3].Value = "Event Title";
                worksheet.Cells[1, 4].Value = "Start Date";
                worksheet.Cells[1, 5].Value = "End Date";
                worksheet.Cells[1, 6].Value = "Total Price";

                for (int i = 0; i < roomEvents.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = roomEvents[i].Room.Title;
                    worksheet.Cells[i + 2, 2].Value = roomEvents[i].PricePerNight; // Price
                    worksheet.Cells[i + 2, 3].Value = roomEvents[i].EventTitle;
                    worksheet.Cells[i + 2, 4].Value = roomEvents[i].Start.ToString("yyyy-MM-dd HH:mm");
                    worksheet.Cells[i + 2, 5].Value = roomEvents[i].End.ToString("yyyy-MM-dd HH:mm");
                    worksheet.Cells[i + 2, 6].Value = CalculateTotalPrice(roomEvents[i]);
                }

                worksheet.Cells[1, 1, roomEvents.Count + 1, 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells[1, 1, roomEvents.Count + 1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 1, roomEvents.Count + 1, 6].AutoFitColumns();

                var fileContents = package.GetAsByteArray();

                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RoomEvents.xlsx");
            }
        }

        private List<RoomEventViewModel> GetRoomEventsForMonth(List<CalendarEvent> events, List<Room> rooms, int year, int month)
        {
            return events
                .Where(e => !e.IsDeleted && e.Start.HasValue && e.End.HasValue &&
                            e.Start.Value.Year == year && e.Start.Value.Month == month)
                .Select(e => new RoomEventViewModel
                {
                    Room = rooms.FirstOrDefault(r => r.Id == e.RoomId),
                    EventTitle = e.EventTitle,
                    Start = e.Start.Value,
                    End = e.End.Value,
                    PricePerNight = rooms.FirstOrDefault(r => r.Id == e.RoomId)?.Price ?? 0
                })
                .ToList();
        }

        private decimal CalculateTotalPrice(RoomEventViewModel roomEvent)
        {
            int totalNights = (int)(roomEvent.End.Date - roomEvent.Start.Date).TotalDays;
            if (totalNights < 0) totalNights = 0;
            decimal totalPrice = roomEvent.PricePerNight * totalNights;
            return totalPrice;
        }

        public class RoomEventViewModel
        {
            public Room Room { get; set; }
            public decimal PricePerNight { get; set; }
            public string EventTitle { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }
    }
}