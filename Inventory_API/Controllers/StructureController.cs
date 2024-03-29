﻿using Inventory_API.Models.db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StructureController : Controller
    {
        private readonly InventoryContext _db;

        public StructureController(InventoryContext db)
        {
                _db = db;
        }
        [HttpGet("GetPlant")]
        public async Task<IActionResult> getPlant()
        {
            var data = await _db.Structures.Where(i=>i.PlantType == "CPAC" || i.PlantType =="N").Select(i=> new {Code = i.PlantCode , Name = i.PlantName}).OrderBy(i=>i.Name).Distinct().ToListAsync();
            return Json(new {data = data});
        }
    }
}
