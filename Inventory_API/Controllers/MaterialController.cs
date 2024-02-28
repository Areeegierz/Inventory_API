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
using System.Reflection.Emit;

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
        public async Task<IActionResult> Get()
        {
            var data = await _db.ViewMaterials.OrderByDescending(i => i.Id).ToListAsync();
            return Ok(new { data = data, store = "" });
        }

        [HttpGet("GetMaterialPagination")]
        public async Task<IActionResult> GetMaterialPagination(int current,int pagesize,string searchtxt = null)
            
        {
            var data = new List<ViewMaterial>();
            var totalcount = 0;
            if (searchtxt != null)
            {
                data = await _db.ViewMaterials.Where(i => i.Code.Contains(searchtxt) || i.Name.Contains(searchtxt) || i.Detail.Contains(searchtxt) || i.Parts.Contains(searchtxt) || i.Unit.Contains(searchtxt) || i.UpdateBy.Contains(searchtxt)).Skip(pagesize * current - pagesize).Take(pagesize).OrderByDescending(i => i.Id).ToListAsync();
                totalcount = _db.ViewMaterials.Where(i => i.Code.Contains(searchtxt) || i.Name.Contains(searchtxt) || i.Detail.Contains(searchtxt) || i.Parts.Contains(searchtxt) || i.Unit.Contains(searchtxt) || i.UpdateBy.Contains(searchtxt)).Count();
            }
            else
            {
                data = await _db.ViewMaterials.Skip(pagesize * current - pagesize).Take(pagesize).OrderByDescending(i => i.Id).ToListAsync();
                totalcount = _db.ViewMaterials.Count();
            }
            
            return Ok(new { data = data, count = totalcount,rowcount = data.Count(),start = pagesize * current - pagesize,end = pagesize * current });
        }

        [HttpGet("GetMaterialbyName/{search}")]
        public async Task<IActionResult> GetbyName(string search)
        {
            var data = await _db.ViewMaterials.Where(i => i.Name.Contains(search) || i.Code.Contains(search)).ToListAsync();

            return Ok(new { data = data, store = "" });
        }

        [HttpGet("GetSingleMaterial/{id}")]
        public async Task<IActionResult> GetSingle(int id) {
            var data = await _db.Materials.Where(i=>i.Id == id).ToListAsync();
            return Ok(new { data = data});
        }
        

        [HttpGet("GetSingleMaterialbyCode/{code}")]
        public async Task<IActionResult> GetSinglebyCode(string code)
        {

            var data = await _db.Materials.Where(i => i.Code == code).FirstOrDefaultAsync();
            return Ok(new { data = data });
        }


        [HttpGet("Remove/{id}")]
        public async Task<IActionResult> Remove(int id) {
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
            model.Name = data.Name;
            model.Detail = data.Detail;
            model.Parts = data.Parts;
            model.Unit = data.Unit;
            model.StoreId = data.StoreId;
            model.Status = data.Status;
            model.Type = data.Type;
            model.File = data.File;
            model.AccountNo = data.AccountNo;
            model.CreateBy = data.CreateBy;
            model.CreateDate = DateTime.Now;
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
            model.Name = data.Name;
            model.Detail = data.Detail;
            model.Parts = data.Parts;
            model.Unit = data.Unit;
            model.StoreId = data.StoreId;
            model.Status = data.Status;
            model.Type = data.Type;
            model.File = data.File;
            model.AccountNo = data.AccountNo;
            model.CreateBy = data.CreateBy;
            model.CreateDate = data.CreateDate;
            model.UpdateBy = data.UpdateBy;
            model.UpdateDate = data.UpdateDate;

            _db.Materials.Update(model);
            await _db.SaveChangesAsync();

            return Ok(new { data = "Update Success" });
        }
    }
}
