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
        [HttpGet("Get/{uid}")]
        public async Task<IActionResult> get(int uid)
        {
            var user = await _db.Users.Where(i => i.Id == uid).FirstOrDefaultAsync();
            if (user.Status != "Admin")
            {

                var div = await _db.Udivisions.Where(i => i.UserId == uid).Select(i => i.DivisionCode).ToListAsync();
                if (div != null)
                {
                    var data = await _db.ViewOrders.Where(i => div.Contains(i.DivisionCode)).OrderByDescending(i=>i.Id).ToListAsync();
                    var store = data.Select(i => new { text = i.StoreName, value = i.StoreName }).Distinct().ToList();
                    return Json(new { data = data, store = store });
                }
            }
            else
            {
                var data = await _db.ViewOrders.OrderByDescending(i => i.Id).ToListAsync();
                var store = data.Select(i => new { text = i.StoreName, value = i.StoreName }).Distinct().ToList();
                return Json(new { data = data, store = store });
            }
            return NotFound();
        }
        [HttpGet("GetOrderPagination")]
        public async Task<IActionResult> GetOrderPagination(int uid, int current, int pagesize, string searchtxt = null)

        {
            var data = new List<ViewOrder>();
            var user = await _db.Users.Where(i => i.Id == uid).FirstOrDefaultAsync();
            var totalcount = 0;
            if (user != null)
            {
                if (user.Status != "Admin")
                {

                    var div = await _db.Udivisions.Where(i => i.UserId == uid).Select(i => i.DivisionCode).ToListAsync();
                    if (div != null)
                    {

                        if (searchtxt != null)
                        {
                            data = await _db.ViewOrders.Where(i => i.DivisionCode.Contains(i.DivisionCode) && (i.Code.Contains(searchtxt) || i.Name.Contains(searchtxt) || i.Detail.Contains(searchtxt) || i.Parts.Contains(searchtxt) || i.Unit.Contains(searchtxt) || i.UpdateBy.Contains(searchtxt) || i.StoreName.Contains(searchtxt))).Skip(pagesize * current - pagesize).Take(pagesize).OrderByDescending(i => i.Id).ToListAsync();
                            totalcount = _db.ViewOrders.Where(i => i.DivisionCode.Contains(i.DivisionCode) && (i.Code.Contains(searchtxt) || i.Name.Contains(searchtxt) || i.Detail.Contains(searchtxt) || i.Parts.Contains(searchtxt) || i.Unit.Contains(searchtxt) || i.UpdateBy.Contains(searchtxt) || i.StoreName.Contains(searchtxt))).Count();
                        }
                        else
                        {
                            data = await _db.ViewOrders.Where(i => i.DivisionCode.Contains(i.DivisionCode)).Skip(pagesize * current - pagesize).Take(pagesize).OrderByDescending(i => i.Id).ToListAsync();
                            totalcount = _db.ViewOrders.Where(i => i.DivisionCode.Contains(i.DivisionCode)).Count();
                        }
                    }
                }
                else
                {
                    if (searchtxt != null)
                    {
                        data = await _db.ViewOrders.Where(i => i.Code.Contains(searchtxt) || i.Name.Contains(searchtxt) || i.Detail.Contains(searchtxt) || i.Parts.Contains(searchtxt) || i.Unit.Contains(searchtxt) || i.UpdateBy.Contains(searchtxt) || i.StoreName.Contains(searchtxt)).Skip(pagesize * current - pagesize).Take(pagesize).OrderByDescending(i => i.Id).ToListAsync();
                        totalcount = _db.ViewOrders.Where(i => i.Code.Contains(searchtxt) || i.Name.Contains(searchtxt) || i.Detail.Contains(searchtxt) || i.Parts.Contains(searchtxt) || i.Unit.Contains(searchtxt) || i.UpdateBy.Contains(searchtxt) || i.StoreName.Contains(searchtxt)).Count();
                    }
                    else
                    {
                        data = await _db.ViewOrders.Skip(pagesize * current - pagesize).Take(pagesize).OrderByDescending(i => i.Id).ToListAsync();
                        totalcount = _db.ViewOrders.Count();
                    }
                }
            }




            return Ok(new { data = data, count = totalcount, rowcount = data.Count(), start = pagesize * current - pagesize, end = pagesize * current });
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
            var thisMat = await _db.Stocks.Where(i => i.Code == data.Code && i.StoreId == data.StoreId).FirstOrDefaultAsync();
            if (thisMat != null)
            {
                if ( thisMat.Count - data.Count >= 0)
                {
                    thisMat.Count = thisMat.Count - data.Count;
                    _db.Stocks.Update(thisMat);

                    var model = new Order();
                    model.Code = thisMat.Code;
                    model.Name = thisMat.Name;
                    model.Detail = thisMat.Detail;
                    model.Parts = thisMat.Parts;
                    model.Count = data.Count;
                    model.Unit = thisMat.Unit;
                    model.StoreId = thisMat.StoreId;
                    model.RefCode = thisMat.RefCode;
                    model.Status = thisMat.Status;
                    model.CategoryId = thisMat.CategoryId;
                    model.SubCategoryId = thisMat.SubCategoryId;
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
