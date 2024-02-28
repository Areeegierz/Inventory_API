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
            var data = await _db.ViewStores.ToListAsync();
            return Json(new { data = data });
        }
        [HttpGet("GetStorebyDivision/{code}")]
        public async Task<IActionResult> GetStorebyDivision(string code)
        {
            var data = await _db.ViewStores.Where(i=>i.DivisionCode == code).ToListAsync();
            return Json(new { data = data });
        }

        [HttpGet("GetDivision")]
        public async Task<IActionResult> GetDivision()
        {
            var data = await _db.Structures.Select(i=> new {Code = i.DivisionCode , Name = i.DivisionName}).Distinct().ToListAsync();
            return Json(new { data = data });
        }

        [HttpGet("GetStorebyId/{id}")]
        public async Task<IActionResult> get(int id)
        {
            var data = await _db.ViewStores.Where(i=>i.Id == id).FirstOrDefaultAsync();
            return Json(new { data = data });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Store data)
        {
            var model = new Store();
            model.Name = data.Name;
            model.DivisionCode = data.DivisionCode;
            _db.Stores.Add(model);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Create Item Success" });
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody]Store data)
        {
            var model = await _db.Stores.Where(i => i.Id == data.Id).FirstOrDefaultAsync();
            if (model!=null)
            {
                model.Name = data.Name;
                model.DivisionCode = data.DivisionCode;
                _db.Stores.Update(model);
                await _db.SaveChangesAsync();
            }
            return Ok(new { message = "Update Item Success" });
        }

        [HttpPost("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var data = await _db.Stores.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (data != null)
            {
                _db.Stores.Remove(data);
                await _db.SaveChangesAsync();
            }
            return Ok(new { message = "Remove Item Success" });
        }
        [HttpGet("GetStoreByUser/{uid}")]
        public async Task<IActionResult> GetStoreByUser(int uid)
        {
            var div = await _db.Udivisions.Where(i => i.UserId == uid).FirstOrDefaultAsync();
            if (div != null)
            {

                var data = await _db.Stores.Where(i=>i.DivisionCode == div.DivisionCode).ToListAsync();
                return Json(new { data = data });
            }
            return NotFound();
        }
    }
}
