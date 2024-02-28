using Inventory_API.Models.db;
using Inventory_API.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly InventoryContext _db;

        public UserController(InventoryContext db)
        {
            _db = db;
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var user = await _db.Users.ToListAsync();
            return Ok(new { user = user });
        }


        [HttpGet("GetbyUserId/{uid}")]
        public async Task<IActionResult> Get(int uid)
        {
            var user = await _db.Users.Where(i=>i.Id == uid).FirstOrDefaultAsync();
            return Ok(new { user = user });
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UserUpdateViewModel data)
        {
            var user = await _db.Users.Where(i => i.Id == data.Uid).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Status = data.Status;
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
            }
            return Ok(new { message = "update user success" });
        }

    }
}
