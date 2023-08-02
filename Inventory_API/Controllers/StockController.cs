using Inventory_API.Models.db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StockController : Controller
    {
        private readonly InventoryContext _db;

        public StockController(InventoryContext db)
        {
                _db = db;
        }
        [HttpGet("GetStock/{uid}")]
        public async Task<IActionResult> get(int uid)
        {
            var div = await _db.Udivisions.Where(i => i.UserId == uid).FirstOrDefaultAsync();
            var data = await _db.ViewStocks.Where(i=>i.DivisionCode == div.DivisionCode).ToListAsync();
            var store = data.Select(i=>new {text = i.StoreName,value = i.StoreName}).Distinct().ToList();
            return Json(new {data = data ,store = store});
        }

        [HttpGet("GetSingle/{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var data = await _db.ViewStocks.Where(i => i.Id == id).ToListAsync();
            return Ok(new { data = data });
        }


        [HttpGet("GetSinglebyCode/{code}/{uid}")]
        public async Task<IActionResult> GetSinglebyCode(string code, int uid)
        {
            if (uid > 0)
            {
                var division = await _db.Udivisions.Where(i => i.UserId == uid).FirstOrDefaultAsync();
                if (division != null)
                {
                    var data = await _db.ViewStocks.Where(i => i.Code == code && i.DivisionCode == division.DivisionCode).FirstOrDefaultAsync();
                    return Ok(new { data = data });
                }
            }
            return Ok(new { data = "" });
        }


        [HttpGet("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var data = await _db.Stocks.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (data != null)
            {
                _db.Stocks.Remove(data);
                await _db.SaveChangesAsync();
                return Ok(new { data = "Remove Success" });

            }
            return Ok(new { data = "Remove Error" });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Stock data)
        {
            var mat = await _db.Stocks.Where(i => i.Code == data.Code && i.StoreId == data.StoreId).FirstOrDefaultAsync();
            if (mat!=null)
            {
                mat.Count = mat.Count + data.Count;
                _db.Stocks.Update(mat);
                await _db.SaveChangesAsync();
            }
            else
            {

                var model = new Stock();
                model.CompCode = data.CompCode;
                model.Code = data.Code;
                model.Name = data.Name;
                model.Detail = data.Detail;
                model.Parts = data.Parts;
                model.Count = data.Count;
                model.Unit = data.Unit;
                model.StoreId = data.StoreId;
                model.Status = data.Status;
                model.Type = data.Type;
                model.File = data.File;
                model.AccountNo = data.AccountNo;
                model.CategoryId = data.CategoryId;
                model.SubCategoryId = data.SubCategoryId;
                model.CreateBy = data.CreateBy;
                model.CreateDate = DateTime.Now;
                model.UpdateBy = data.UpdateBy;
                model.UpdateDate = data.UpdateDate;
                model.RefCode = data.RefCode;
                _db.Stocks.Add(model);
                await _db.SaveChangesAsync();
            }
            var mydata = await _db.Stocks.Where(i => i.Code == data.Code && i.StoreId == data.StoreId).OrderByDescending(i=>i.Id).FirstOrDefaultAsync();
            if (mydata != null)
            {
                var log = new Log();
                log.Refcode = data.RefCode;
                log.Refcode = "เพิ่มจำนวนอะไหล่จำนวน"+ data.Count +" "+ data.Unit;
                log.Status = "Add";
                log.StockId = mydata.Id;
                log.CreateBy = data.CreateBy;
                log.CreateDate = DateTime.Now;
            }
            return Ok(new { data = mydata });
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Stock data)
        {

            var model = await _db.Stocks.Where(i => i.Id == data.Id).FirstOrDefaultAsync();
            model.CompCode = data.CompCode;
            model.Code = data.Code;
            model.Name = data.Name;
            model.Detail = data.Detail;
            model.Parts = data.Parts;
            model.Count = data.Count;
            model.Unit = data.Unit;
            model.StoreId = data.StoreId;
            model.Status = data.Status;
            model.Type = data.Type;
            model.File = data.File;
            model.AccountNo = data.AccountNo;
            model.CategoryId = data.CategoryId;
            model.SubCategoryId = data.SubCategoryId;
            model.CreateBy = data.CreateBy;
            model.CreateDate = data.CreateDate;
            model.UpdateBy = data.UpdateBy;
            model.UpdateDate = data.UpdateDate;

            _db.Stocks.Update(model);
            await _db.SaveChangesAsync();

            return Ok(new { data = "Update Success" });
        }

        [HttpGet("GetLog/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var data = await _db.Logs.Where(i => i.StockId == id).ToListAsync();
            return Ok(new { data = data });
        }
    }
}
