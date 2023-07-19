using Inventory_API.Models.db;
using Inventory_API.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Inventory_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly InventoryContext _db;

        public OrderController(InventoryContext db)
        {
                _db = db;
        }
        [HttpGet("Get/{compcode}")]
        public async Task<IActionResult> get(string compcode)
        {
            var data = await _db.ViewOrders.Where(i=>i.CompCode == compcode).ToListAsync();
            var store = data.Select(i => new { text = i.StoreName, value = i.StoreName }).Distinct().ToList();
            return Json(new { data = data, store = store });
        }

        [HttpGet("GetSingle/{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var data = await _db.ViewOrders.Where(i => i.Id == id).ToListAsync();
            return Ok(new { data = data });
        }


        [HttpGet("GetSinglebyCode/{code}/{uid}")]
        public async Task<IActionResult> GetSinglebyCode(string code, int uid)
        {
            if (uid > 0)
            {
                var compcode = await _db.Ucomps.Where(i => i.UserId == uid).FirstOrDefaultAsync();
                if (compcode != null)
                {
                    var data = await _db.ViewOrders.Where(i => i.Code == code && i.CompCode == compcode.CompCode).FirstOrDefaultAsync();
                    return Ok(new { data = data });
                }
            }
            return Ok(new { data = "" });
        }


        [HttpGet("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var data = await _db.Orders.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (data != null)
            {
                _db.Orders.Remove(data);
                await _db.SaveChangesAsync();
                return Ok(new { data = "Remove Success" });

            }
            return Ok(new { data = "Remove Error" });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] InvoiceViewModel data)
        {
            var thisMat = await _db.Stocks.Where(i => i.Code == data.Code && i.CompCode == data.CompCode).FirstOrDefaultAsync();
            if (thisMat != null)
            {
                if ( thisMat.Count - data.Count >= 0)
                {
                    thisMat.Count = thisMat.Count - data.Count;
                    _db.Stocks.Update(thisMat);

                    var model = new Order();
                    model.CompCode = thisMat.CompCode;
                    model.Code = thisMat.Code;
                    model.Name = thisMat.Name;
                    model.Detail = thisMat.Detail;
                    model.Parts = thisMat.Parts;
                    model.Count = data.Count;
                    model.Unit = thisMat.Unit;
                    model.StoreId = thisMat.StoreId;
                    model.RefCode = data.RefCode;
                    model.Status = thisMat.Status;
                    model.CategoryId = data.CategoryId;
                    model.SubCategoryId = data.SubCategoryId;
                    model.Type = thisMat.Type;
                    model.File = thisMat.File;
                    model.Use = data.Use;
                    model.AccountNo = thisMat.AccountNo;
                    model.CreateBy = data.CreateBy;
                    model.CreateDate = DateTime.Now;
                    _db.Orders.Add(model);
                    await _db.SaveChangesAsync();
                    var mydata = await _db.Materials.Where(i => i.Code == data.Code).ToListAsync();
                    return Ok(new { data = mydata });
                }

                return Ok(new {data = ""});
            }
            return BadRequest(new { data = "" });
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Order data)
        {

            var model = await _db.Orders.Where(i => i.Id == data.Id).FirstOrDefaultAsync();
            model.CompCode = data.CompCode;
            model.Code = data.Code;
            model.Name = data.Name;
            model.Detail = data.Detail;
            model.Parts = data.Parts;
            model.Count = data.Count;
            model.Unit = data.Unit;
            model.StoreId = data.StoreId;
            model.RefCode = data.RefCode;
            model.Status = data.Status;
            model.Type = data.Type;
            model.File = data.File;
            model.Use = data.Use;
            model.AccountNo = data.AccountNo;
            model.CategoryId = data.CategoryId;
            model.SubCategoryId = data.SubCategoryId;
            model.CreateBy = data.CreateBy;
            model.CreateDate = data.CreateDate;
            model.UpdateBy = data.UpdateBy;
            model.UpdateDate = data.UpdateDate;

            _db.Orders.Update(model);
            await _db.SaveChangesAsync();

            return Ok(new { data = "Update Success" });
        }
    }
}
