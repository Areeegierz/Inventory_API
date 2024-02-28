using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Inventory_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Inventory_API.Models.db;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using static System.Collections.Specialized.BitVector32;
using System.Net.Http.Headers;
using System.Numerics;
using Inventory_API.Models.AD;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OverviewController : ControllerBase
    {
        private readonly InventoryContext _db;

        public OverviewController(InventoryContext db)
        {
            _db = db;
        }

        [HttpGet("GetOverView/{id}")]
        public async Task<IActionResult> GetOverView(int id)
        {
            var data = await _db.ViewOverViews.Where(i => i.StoreId == id.ToString()).Select(i=> new {Name = i.Name , Count = i.Count}).ToListAsync();
            return Ok(new { data = data });
        }
    }
}
