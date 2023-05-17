using Hola_Resort.Models;
using System.Linq;
using System.Web.Mvc;

public class RoomController : Controller
{
    private HolaDBDataContext data = new HolaDBDataContext();

    public ActionResult Room(string id)
    {
        var rooms = data.Rooms.Where(r => r.RoomTypeId == id).ToList();
        return View(rooms);
    }

    public ActionResult RoomDetails(string id)
    {
        var room = data.Rooms.FirstOrDefault(r => r.RoomId == id);

        if (room == null)
        {
            return HttpNotFound();
        }

        var roomType = data.RoomTypes.FirstOrDefault(rt => rt.RoomTypeId == room.RoomTypeId);

        if (roomType == null)
        {
            return HttpNotFound();
        }

        ViewBag.RoomTypeName = roomType.RoomTypeName;
        ViewBag.Capacity = roomType.Capacity;
        ViewBag.PriceDay = roomType.PriceDay;
        ViewBag.RoomNumber = room.RoomNumber;
        ViewBag.Description = room.Description;

        // Truyền giá trị RoomId vào BookingDetails action
        return RedirectToAction("BookingDetails", "Home", new { id = room.RoomId });
    }

}
