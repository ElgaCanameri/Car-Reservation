using Hotel.BLL.DTO;
using Hotel.BLL.DTO.Requests;
using Hotel.BLL.DTO.Responses;
using Hotel.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Hotel.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RoomController(IRoomService roomService, IWebHostEnvironment hostEnvironment)
        {
            _roomService = roomService;
            webHostEnvironment = hostEnvironment;
        }

        // GET: RoomController/Index
        public ActionResult Index()
        {
            var rooms = _roomService.GetAllRooms();
            ViewBag.Rooms = rooms;
            return View(rooms);
        }

       
        // GET: RoomController/Create
        //[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomController/Create
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomAddModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImagePath = UploadedFile(model);
                var res = _roomService.AddRoom(model);
                if (res.Status == ViewResponseStatus.OK)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        private string UploadedFile(RoomAddModel model)
        {
            string uniqueFileName = null;
            try
            {
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception) { }

            return uniqueFileName;



        }
    }

}

