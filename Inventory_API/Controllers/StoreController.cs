using Inventory_API.Models.db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : Controller
    {
        private readonly InventoryContext _db;

        public StoreController(InventoryContext db)
        {
                _db = db;
        }
        [HttpGet("GetStore")]
        public async Task<IActionResult> get()
        {
            var data = await _db.Stores.ToListAsync();
            return Json(new {data = data});
        }
    }
}
