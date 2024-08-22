using Hotel.DAL.Entities;
using Hotel.BLL.DTO.Responses;

namespace Hotel.BLL.DTO.Responses
{
    public class RoomIndexModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }

    }
}
