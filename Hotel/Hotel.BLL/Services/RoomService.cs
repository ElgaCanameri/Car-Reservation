using Hotel.BLL.DTO;
using Hotel.BLL.DTO.Requests;
using Hotel.BLL.DTO.Responses;
using Hotel.DAL.Entities;


namespace Hotel.BLL.Services
{
    public interface IRoomService
    {
        StandardViewResponse<bool> AddRoom(RoomAddModel model);
        public RoomDetailsModel GetById(int id);
        public IEnumerable<RoomDetailsModel> GetRoomsByTitle(string title);

        public IEnumerable<RoomIndexModel> GetAllRooms();
    }
    public class RoomService : BaseService, IRoomService
    {
        public RoomService(IServiceProvider unitOfWork) : base(unitOfWork) { }
        public StandardViewResponse<bool> AddRoom(RoomAddModel model)
        {
            var addedRoom = new Room();
            try
            {
                addedRoom = new Room
                {
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price,
                    Capacity = model.Capacity,
                    ImagePath = model.ImagePath
                };
                _unitOfWork.RoomRepository.Add(addedRoom);
                _unitOfWork.Commit();
                if (addedRoom.Id > 0)
                {
                    return new StandardViewResponse<bool>(true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new StandardViewResponse<bool>(false, "Nuk u shtua dot dhoma.");

        }

        public RoomDetailsModel GetById(int id)
        {
            var room = new Room();
            try
            {
                room = _unitOfWork.RoomRepository.GetById(id);
               
                var roomDetails = new RoomDetailsModel
                {
                    Id = room.Id,
                    Title = room.Title,
                    Price = room.Price,
                    Capacity = room.Capacity,
                    Description = room.Description,
                    ImagePath = room.ImagePath
                   
                };

                return roomDetails;
            }
            catch (Exception ex){}
            return null;
        }
        public IEnumerable<RoomIndexModel> GetAllRooms()
        {
            var rooms = new List<Room>();
            try
            {
                rooms = _unitOfWork.RoomRepository.GetAll().Where(x => x.IsDeleted == false).ToList();
                var list = rooms.Select(x => new RoomIndexModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    Capacity = x.Capacity,
                    Description = x.Description,
                    ImagePath = x.ImagePath
                }).ToList();

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public IEnumerable<RoomDetailsModel> GetRoomsByTitle(string title)
        {
            try
            {
                var rooms = _unitOfWork.RoomRepository.GetRoomByTitle(title); 
                var roomDetailsList = rooms.Select(x => new RoomDetailsModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Capacity = x.Capacity,
                    Price = x.Price,
                    Description = x.Description,
                    ImagePath= x.ImagePath
                    
                }).ToList();

                return roomDetailsList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching rooms by title", ex);
            }
        }

    }

}

