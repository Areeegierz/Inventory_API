using Inventory_API.Models.db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoryController : Controller
    {
        private readonly InventoryContext _db;

        public SubCategoryController(InventoryContext db)
        {
            _db = db;
        }
        [HttpGet("GetSubCategory")]
        public async Task<ActionResult> GetSubCategory()
        {
            var data = await _db.ViewSubCategories.ToListAsync();
            return Ok(new {data = data});
        }

        [HttpGet("GetSubCategorybyId/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            var data = await _db.ViewSubCategories.Where(i=>i.Id == id).ToListAsync();
            return Ok(new {data = data});
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] SubCategory fdata)
        {
            var model = new SubCategory();
            model.Name = fdata.Name;
            model.CategoryId = fdata.CategoryId;
            model.CreateBy = fdata.CreateBy;
            model.CreateDate = DateTime.Now;
            _db.SubCategories.Add(model);
            await _db.SaveChangesAsync();

            var data = await _db.ViewSubCategories.OrderByDescending(i=>i.Id).ToListAsync();
            return Ok(new {data = data});
        }


        [HttpPost("Update")]
        public async Task<ActionResult> Edit([FromBody] SubCategory fdata)
        {
            var model = await _db.Categories.Where(i=>i.Id == fdata.Id).FirstOrDefaultAsync();
            if (model != null)
            {
                model.Name = fdata.Name;
                _db.Categories.Update(model);
                await _db.SaveChangesAsync();
            }
            // new data
            var data = await _db.ViewSubCategories.Where(i => i.Id == fdata.Id).FirstOrDefaultAsync();
            return Ok(new {data = data});
        }

        [HttpPost("Remove/{id}")]
        public async Task<ActionResult> Remove(int id)
        {

            var model = await _db.SubCategories.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (model != null)
            {
                _db.SubCategories.Remove(model);
                await _db.SaveChangesAsync();
            }
            return Ok(new { data = "Remove Success" });
        }

    }
}
