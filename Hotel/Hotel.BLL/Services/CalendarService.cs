using Hotel.BLL.DTO;
using Hotel.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Services
{
    public interface ICalendarService
    {
        int CreateEvent(string title, DateTime start, DateTime end, int roomId);
        public StandardViewResponse<bool> DeleteEvent(int id);
        public StandardViewResponse<bool> UpdateEventDate(int eventId, DateTime start, DateTime? end);
        List<CalendarEvent> GetAllEvents();
        public IEnumerable<CalendarEvent> GetEventsByRoomId(int roomId);

    }
    public class CalendarService : BaseService, ICalendarService
    {
        public CalendarService(IServiceProvider unitOfWork) : base(unitOfWork) { }
        public int CreateEvent(string title, DateTime start, DateTime end, int roomId)
        {
            try
            {
                var calendarEvent = new CalendarEvent
                {
                    EventTitle = title,
                    Start = start,
                    End = end,
                    AllDay = false, 
                    RoomId = roomId
                };

                _unitOfWork.CalendarRepository.Add(calendarEvent);
                _unitOfWork.Commit();

                return calendarEvent.Id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error creating event", ex);
            }
        }
        public StandardViewResponse<bool> DeleteEvent(int id)
        {
            var calendarEvent = new CalendarEvent();
            try
            {
                calendarEvent = _unitOfWork.CalendarRepository.GetById(id);
                if (calendarEvent != null)
                {
                    calendarEvent.IsDeleted = true;
                    _unitOfWork.CalendarRepository.Update(calendarEvent);
                    _unitOfWork.Commit();
                    return new StandardViewResponse<bool>(true);
                }
                else
                {
                    return new StandardViewResponse<bool>(false, "Event not found.");
                }
            }
            catch (Exception ex)
            {
                // Log exception if needed
                throw;
            }
        }
        public StandardViewResponse<bool> UpdateEventDate(int eventId, DateTime start, DateTime? end)
        {
            try
            {
                var calendarEvent = _unitOfWork.CalendarRepository.GetById(eventId);
                if (calendarEvent != null)
                {
                    calendarEvent.Start = start;
                    calendarEvent.End = end;
                    _unitOfWork.CalendarRepository.Update(calendarEvent);
                    _unitOfWork.Commit();
                    return new StandardViewResponse<bool>(true);
                }
                else
                {
                    return new StandardViewResponse<bool>(false, "Event not found.");
                }
            }
            catch (Exception ex)
            {
                // Log exception if needed
                throw new ApplicationException("Error updating event date", ex);
            }
        }
        public List<CalendarEvent> GetAllEvents()
        {
            return _unitOfWork.CalendarRepository.GetAll().ToList();
        }
        public IEnumerable<CalendarEvent> GetEventsByRoomId(int roomId)
        {
            return _unitOfWork.CalendarRepository.GetCalendarEventsByRoomId(roomId).ToList();
        }
    }
}
