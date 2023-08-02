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
    public class MachineController : Controller
    {
        private readonly InventoryContext _db;

        public MachineController(InventoryContext db)
        {
            _db = db;
        }
        // GET: api/values
        [HttpGet("GetMachine")]
        public async Task<IActionResult> Get()
        {
            var machine = await _db.Machines.ToListAsync();
            return Json(new {status="200", machine = machine});
        }

        [HttpGet("GetSingleMachine/{id}")]
        public async Task<IActionResult> Single(int id)
        {
            var machine = await _db.Machines.Where(i => i.Id == id).FirstOrDefaultAsync();
            return Json(new { status = "200", machine = machine });
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Models.db.Machine data)
        {
            var model = new Models.db.Machine();
            model.Name = data.Name;
            model.CreateBy = data.CreateBy;
            model.CreateDate = DateTime.Now;
            _db.Machines.Add(model);
            await _db.SaveChangesAsync();

            var machine = await _db.Machines.OrderByDescending(i => i.Id).FirstOrDefaultAsync();
            return Json(new { status = "200", machine = machine });
        }


        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Models.db.Machine data)
        {
            var model = await _db.Machines.Where(i => i.Id == data.Id).FirstOrDefaultAsync();
            if (model != null)
            {

                model.Name = data.Name;
                model.CreateBy = data.CreateBy;
                model.CreateDate = DateTime.Now;
                _db.Machines.Update(model);
                await _db.SaveChangesAsync();

                var machine = await _db.Machines.Where(i => i.Id == data.Id).OrderByDescending(i => i.Id).FirstOrDefaultAsync();
                return Ok(new { status = "200", machine = machine });
            }
            return NotFound();
        }

        [HttpPost("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var model = await _db.Machines.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (model != null)
            {
                _db.Machines.Remove(model);
                await _db.SaveChangesAsync();

                return Ok(new { status = "200"});
            }
            return NotFound();
        }

    }
}

