using Inventory_API.Models.db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly InventoryContext _db;

        public CategoryController(InventoryContext db)
        {
            _db = db;
        }
        [HttpGet("GetCategory")]
        public async Task<ActionResult> GetCategory()
        {
            var data = await _db.Categories.OrderBy(i=>i.Name).ToListAsync();
            return Ok(new {data = data});
        }

        [HttpGet("GetCategorybyId/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            var data = await _db.Categories.Where(i=>i.Id == id).ToListAsync();
            return Ok(new {data = data});
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] Category fdata)
        {
            var model = new Category();
            model.Name = fdata.Name;
            model.CreateBy = fdata.CreateBy;
            model.CreateDate = DateTime.Now;
            _db.Categories.Add(model);
            await _db.SaveChangesAsync();

            var data = await _db.Categories.OrderByDescending(i=>i.Id).ToListAsync();
            return Ok(new {data = data});
        }


        [HttpPost("Update")]
        public async Task<ActionResult> Edit([FromBody] Category fdata)
        {
            var model = await _db.Categories.Where(i=>i.Id == fdata.Id).FirstOrDefaultAsync();
            if (model != null)
            {
                model.Name = fdata.Name;
                _db.Categories.Update(model);
                await _db.SaveChangesAsync();
            }
            // new data
            var data = await _db.Categories.Where(i => i.Id == fdata.Id).FirstOrDefaultAsync();
            return Ok(new {data = data});
        }

        [HttpPost("Remove/{id}")]
        public async Task<ActionResult> Remove(int id)
        {

            var model = await _db.Categories.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (model != null)
            {
                _db.Categories.Remove(model);
                await _db.SaveChangesAsync();
            }
            return Ok(new { data = "Remove Success" });
        }

    }
}
