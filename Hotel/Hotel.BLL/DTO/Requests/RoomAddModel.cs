using Hotel.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Hotel.BLL.DTO.Requests
{
    public class RoomAddModel
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }

        [Range(0, 200)]
        public int Capacity { get; set; }

        [Precision(18, 2)]
        [Range(0.0, 100, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }
    }
}
