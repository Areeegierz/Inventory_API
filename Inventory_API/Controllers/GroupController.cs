using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Inventory_API.Models.db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inventory_API.Controllers
{
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly InventoryContext _db;

        public GroupController(InventoryContext db)
        {
            _db = db;
        }
        // GET: api/values
        [HttpGet("GetGroup/{MachineId}")]
        public async Task<IActionResult> Get(int machineId)
        {
            var groups = await _db.Groups.Where(i=>i.MachineId == machineId).ToListAsync();
            return Json(new {status="200", groups = groups });
        }

        [HttpGet("GetSingleGroup/{id}")]
        public async Task<IActionResult> Single(int id)
        {
            var groups = await _db.Groups.Where(i => i.Id == id).FirstOrDefaultAsync();
            return Json(new { status = "200", groups = groups });
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Models.db.Group data)
        {
            var model = new Models.db.Group();
            model.Name = data.Name;
            model.MachineId = data.MachineId;
            model.CreateBy = data.CreateBy;
            model.CreateDate = DateTime.Now;
            _db.Groups.Add(model);
            await _db.SaveChangesAsync();

            var groups = await _db.Machines.OrderByDescending(i => i.Id).FirstOrDefaultAsync();
            return Json(new { status = "200", groups = groups });
        }


        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Models.db.Group data)
        {
            var model = await _db.Groups.Where(i => i.Id == data.Id).FirstOrDefaultAsync();
            if (model != null)
            {

                model.Name = data.Name;
                model.CreateBy = data.CreateBy;
                model.MachineId = data.MachineId;
                model.CreateDate = DateTime.Now;
                _db.Groups.Add(model);
                await _db.SaveChangesAsync();

                var groups = await _db.Groups.OrderByDescending(i => i.Id).FirstOrDefaultAsync();
                return Ok(new { status = "200", groups = groups });
            }
            return NotFound();
        }

        [HttpGet("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var model = await _db.Groups.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (model != null)
            {
                _db.Groups.Remove(model);
                await _db.SaveChangesAsync();

                return Ok(new { status = "200"});
            }
            return NotFound();
        }

    }
}

