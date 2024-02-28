using Inventory_API.Models.db;
using Inventory_API.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        private readonly InventoryContext _db;

        public LogController(InventoryContext db)
        {
            _db = db;
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var Log = await _db.ViewLogs.ToListAsync();
            return Ok(new { Log = Log });
        }
    }
}
