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
    public class MaterialController : ControllerBase
    {
        private readonly InventoryContext _db;

        public MaterialController(InventoryContext db)
        {
            _db = db;
        }

        [HttpGet("GetMaterial")]
        public async Task<IActionResult> Get() {
            var data = await _db.Materials.ToListAsync();
            return Ok(new { data = data});
        }

        [HttpGet("GetSingleMaterial")]
        public async Task<IActionResult> GetSingle([FromBody] int id) {
            var data = await _db.Materials.Where(i=>i.Id == id).ToListAsync();
            return Ok(new { data = data});
        }


        [HttpGet("Remove")]
        public async Task<IActionResult> Remove([FromBody] int id) {
            var data = await _db.Materials.Where(i=>i.Id == id).FirstOrDefaultAsync();
            if (data!=null)
            {
                _db.Materials.Remove(data);
                await _db.SaveChangesAsync();
                return Ok(new { data = "Remove Success" });

            }
            return Ok(new { data = "Remove Error" });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Material data) {

            var model = new Material();
            model.CompCode = data.CompCode;
            model.Code = data.Code;
            model.Detail = data.Detail;
            model.Parts = data.Parts;
            model.Unit = data.Unit;
            model.StoreCode = data.StoreCode;
            model.Status = data.Status;
            model.Type = data.Type;
            model.File = data.File;
            model.AccountNo = data.AccountNo;
            model.CreateBy = data.CreateBy;
            model.CreateDate = data.CreateDate;
            model.UpdateBy = data.UpdateBy;
            model.UpdateDate = data.UpdateDate;
            _db.Materials.Add(model);
            await _db.SaveChangesAsync();
            var mydata = await _db.Materials.Where(i=>i.Code == data.Code).ToListAsync();
            return Ok(new { data = mydata });
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Material data) {

            var model = await _db.Materials.Where(i=>i.Id== data.Id).FirstOrDefaultAsync();
            model.CompCode = data.CompCode;
            model.Code = data.Code;
            model.Detail = data.Detail;
            model.Parts = data.Parts;
            model.Unit = data.Unit;
            model.StoreCode = data.StoreCode;
            model.Status = data.Status;
            model.Type = data.Type;
            model.File = data.File;
            model.AccountNo = data.AccountNo;
            model.CreateBy = data.CreateBy;
            model.CreateDate = data.CreateDate;
            model.UpdateBy = data.UpdateBy;
            model.UpdateDate = data.UpdateDate;

            _db.Materials.Add(model);
            await _db.SaveChangesAsync();

            return Ok(new { data = "Update Success" });
        }
    }
}
