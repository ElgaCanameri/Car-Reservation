using Hotel.DAL.Entities;
using Hotel.BLL.DTO.Responses;
using Microsoft.AspNetCore.Http;

namespace Hotel.BLL.DTO.Responses
{
    public class RoomDetailsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? Image { get; set; }
    }
}
