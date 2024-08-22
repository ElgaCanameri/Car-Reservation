using Hotel.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace Hotel.DAL.Repositories
{
    public interface IRoomRepository : IBaseRepository<Room, int>
    {
        public List<Room> GetRoomByTitle(string title);
    }
    public class RoomRepository : BaseRepository<Room, int>, IRoomRepository
    {
        public RoomRepository(AppDbContext dbContext) : base(dbContext) { }
        public List<Room> GetRoomByTitle(string title)
        {
            return _set.Where(x => x.Title == title).ToList();
        }
    }
}
