using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Inventory_API.Models.db;
using Inventory_API.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inventory_API.Controllers
{
    [Route("api/[controller]")]
    public class GroupMaterialController : Controller
    {
        private readonly InventoryContext _db;

        public GroupMaterialController(InventoryContext db)
        {
            _db = db;
        }


        [HttpGet("GetMaterial")]
        public async Task<IActionResult> Get()
        {
            var data = await _db.ViewMaterials.Select(i => new { id = i.Id, key = i.Id, code = i.Code, name = i.Name }).Take(100).OrderByDescending(i => i.id).ToListAsync();
            return Ok(new { data = data });
        }


        [HttpGet("GetMaterialByGroupId/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _db.ViewGroupMaterials.Where(i=>i.GroupId == id).Take(100).ToListAsync();
            return Ok(new { data = data });
        }


        [HttpPost("Create/{id}")]
        public async Task<IActionResult> Post(int id, [FromBody] List<CatalogCreate> datalist)
        {
            foreach (var item in datalist)
            {
                var model = new GroupMaterial();
                model.GroupId = id;
                model.MatCode = item.Code;
                model.CreateDate = DateTime.Now;
                _db.GroupMaterials.Add(model);
            }

            await _db.SaveChangesAsync();
            return Json(new { status = 200, message = "create success" });
        }


        [HttpGet("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {

            var data = await _db.GroupMaterials.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (data != null)
            {
                _db.GroupMaterials.Remove(data);
                await _db.SaveChangesAsync();

            }

            return Json(new { status = 200, message = "remove success" });
        }


        [HttpGet("GetStock/{divisioncode}/{code}")]
        public async Task<IActionResult> GetStock(string divisioncode,string code)
        {

            var data = await _db.ViewStocks.Where(i => i.DivisionCode == divisioncode && i.Code == code).ToListAsync();
            return Json(new { status = 200, data = data });
        }

    }
}

