using Inventory_API.Models.db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Inventory_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly InventoryContext _db;

        public HomeController(InventoryContext db)
        {
            _db = db;
        }
        [HttpGet("GetOverview")]
        public async Task<IActionResult> GetOverview()
        {
            var data1 = _db.Stocks.Count();
            var data2 = _db.Orders.Count();
            var data3 = data1+data2;
            var data4 = _db.Stocks.Count();
            return Ok(new { data1 = data1, data2 = data2, data3 = data3, data4 = data4 });
        }
        [HttpGet("GetOverview/{uid}/{start}/{end}")]
        public async Task<IActionResult> GetOverview(int uid,string start,string end)
        {

            var data1 = _db.Stocks.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date)).Count();
            var data2 = _db.Orders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date)).Count();
            var data3 = data1+data2;
            var data4 = _db.Stocks.Count();
            var data5 = _db.Stocks.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date)).Sum(i=>i.Count);
            var data6 = _db.Orders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date)).Sum(i => i.Count);
            var data7 = data5-data6;
            return Ok(new { data1 = data1, data2 = data2, data3 = data3, data4 = data4, count1 = data5, count2 = data6, count3 = data7 });
        }
        [HttpGet("GetPie")]
        public IActionResult Index()
        {
            var data1 = _db.ViewOrders.Where(i =>i.ThisDivisionCode == "90000140").ToList();
            var data2 = _db.ViewOrders.Where(i =>i.ThisDivisionCode == "90000141").ToList();
            var data3 = _db.ViewOrders.Where(i =>i.ThisDivisionCode == "90000142").ToList();
            var data4 = _db.ViewOrders.Where(i =>i.ThisDivisionCode == "90000143").ToList();
            var data5 = _db.ViewOrders.Where(i =>i.ThisDivisionCode == "90000144").ToList();
            var data6 = _db.ViewOrders.Where(i =>i.ThisDivisionCode == "90000145").ToList();
            return Ok(new { data1 = data1.Count(), data2 = data2.Count(), data3 = data3.Count(), data4 = data4.Count(), data5 = data5.Count(), data6 = data6.Count() });
        }
        [HttpGet("GetPie/{start}/{end}")]
        public IActionResult Index(string start,string end)
        {
            var data1 = _db.ViewOrders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.ThisDivisionCode == "90000140").ToList();
            var data2 = _db.ViewOrders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.ThisDivisionCode == "90000141").ToList();
            var data3 = _db.ViewOrders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.ThisDivisionCode == "90000142").ToList();
            var data4 = _db.ViewOrders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.ThisDivisionCode == "90000143").ToList();
            var data5 = _db.ViewOrders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.ThisDivisionCode == "90000144").ToList();
            var data6 = _db.ViewOrders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.ThisDivisionCode == "90000145").ToList();
            return Ok(new { data1 = data1.Count(), data2 = data2.Count(), data3 = data3.Count(), data4 = data4.Count(), data5 = data5.Count(), data6 = data6.Count(), unit1 = data1.Sum(i => i.Count), unit2 = data2.Sum(i => i.Count), unit3 = data3.Sum(i => i.Count), unit4 = data4.Sum(i => i.Count), unit5 = data5.Sum(i => i.Count), unit6 = data6.Sum(i => i.Count)});
        }
        [HttpGet("GetChart/{uid}")]
        public async Task<IActionResult> GetChart(int uid)
        {
            var user = await _db.Users.Where(i => i.Id == uid).FirstOrDefaultAsync();
            if (user != null)
            {
                if (user.Status == "Admin")
                {
                    var data = await _db.ViewOrders.OrderBy(i => i.DepartmentCode).Select(i => new { name = i.StoreName, count = _db.Orders.Where(j => j.StoreId == i.StoreId).Sum(k => k.Count) }).Distinct().ToListAsync();
                    return Ok(new { data = data });
                }
                else
                {

                    var div = await _db.Udivisions.Where(i => i.UserId == uid).Select(i => i.DivisionCode).ToListAsync();
                    if (div != null)
                    {
                        var data = await _db.ViewOrders.OrderBy(i => i.DepartmentCode).Where(i=> div.Contains(i.DivisionCode)).Select(i => new { name = i.StoreName, count = _db.Orders.Where(j => j.StoreId == i.StoreId).Sum(k => k.Count) }).Distinct().ToListAsync();
                        return Ok(new { data = data });
                    }
                }
            }
            return NotFound();
        }
        [HttpGet("GetChart/{uid}/{start}/{end}")]
        public async Task<IActionResult> GetChart(int uid,string start,string end)
        {


            var user = await _db.Users.Where(i => i.Id == uid).FirstOrDefaultAsync();
            if (user != null)
            {
                if (user.Status == "Admin")
                {
                    var data = await _db.ViewOrders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date)).OrderBy(i => i.DepartmentCode).Select(i => new { name = i.StoreName, count = _db.Orders.Where(j => j.StoreId == i.StoreId).Sum(k => k.Count) }).Distinct().ToListAsync();
                    return Ok(new { data = data });
                }
                else
                {

                    var div = await _db.Udivisions.Where(i => i.UserId == uid).Select(i => i.DivisionCode).ToListAsync();
                    if (div != null)
                    {
                        var data = await _db.ViewOrders.Where(i => div.Contains(i.DivisionCode) && (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date)).OrderBy(i => i.DepartmentCode).Select(i => new { name = i.StoreName, count = _db.Orders.Where(j => j.StoreId == i.StoreId).Sum(k => k.Count) }).Distinct().ToListAsync();
                        return Ok(new { data = data });
                    }
                }
            }
            return NotFound();
        }


        [HttpGet("GetPieStock")]
        public IActionResult GetPieStock()
        {
            var data1 = _db.ViewStocks.Where(i => i.DivisionCode == "90000140").ToList();
            var data2 = _db.ViewStocks.Where(i => i.DivisionCode == "90000141").ToList();
            var data3 = _db.ViewStocks.Where(i => i.DivisionCode == "90000142").ToList();
            var data4 = _db.ViewStocks.Where(i => i.DivisionCode == "90000143").ToList();
            var data5 = _db.ViewStocks.Where(i => i.DivisionCode == "90000144").ToList();
            var data6 = _db.ViewStocks.Where(i => i.DivisionCode == "90000145").ToList();
            return Ok(new { data1 = data1.Count(), data2 = data2.Count(), data3 = data3.Count(), data4 = data4.Count(), data5 = data5.Count(), data6 = data6.Count() });
        }


        [HttpGet("GetPieStock/{start}/{end}")]
        public IActionResult GetPieStockbyDate(string start, string end)
        {
            var data1 = _db.ViewStocks.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.DivisionCode == "90000140").ToList();
            var data2 = _db.ViewStocks.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.DivisionCode == "90000141").ToList();
            var data3 = _db.ViewStocks.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.DivisionCode == "90000142").ToList();
            var data4 = _db.ViewStocks.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.DivisionCode == "90000143").ToList();
            var data5 = _db.ViewStocks.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.DivisionCode == "90000144").ToList();
            var data6 = _db.ViewStocks.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.DivisionCode == "90000145").ToList();
            return Ok(new { data1 = data1.Count(), data2 = data2.Count(), data3 = data3.Count(), data4 = data4.Count(), data5 = data5.Count(), data6 = data6.Count(), unit1 = data1.Sum(i => i.Count), unit2 = data2.Sum(i => i.Count), unit3 = data3.Sum(i => i.Count), unit4 = data4.Sum(i => i.Count), unit5 = data5.Sum(i => i.Count), unit6 = data6.Sum(i => i.Count) });
        }




        [HttpGet("GetChartStock/{uid}")]
        public async Task<IActionResult> GetChartStock(int uid)
        {
            var user = await _db.Users.Where(i => i.Id == uid).FirstOrDefaultAsync();
            if (user!=null)
            {
                if (user.Status == "Admin")
                {
                    var data = await _db.ViewStocks.Select(i => new { name = i.StoreName, count = _db.Stocks.Where(j => j.StoreId == i.StoreId).Sum(k => k.Count) }).Distinct().ToListAsync();
                    return Ok(new { data = data });
                }
                else
                {

                    var div = await _db.Udivisions.Where(i => i.UserId == uid).Select(i=> i.DivisionCode).ToListAsync();
                    if (div != null)
                    {
                        var data = await _db.ViewStocks.Where(i=> div.Contains(i.DivisionCode)).Select(i => new {div = i.DivisionCode , name = i.StoreName, count = _db.Stocks.Where(j => j.StoreId == i.StoreId).Sum(k => k.Count) }).Distinct().ToListAsync();
                        //var data = await _db.ViewStocks.Where(i=>i.DivisionCode == div.DivisionCode).Select(i => new { name = i.StoreName, count = _db.Stocks.Where(j => j.StoreId == i.StoreId).Sum(k => k.Count) }).Distinct().ToListAsync();
                        return Ok(new { data = data});
                    }
                }
            }
            return NotFound();
        }
        [HttpGet("GetChartStock/{uid}/{start}/{end}")]
        public async Task<IActionResult> ViewStocksbyDate(int uid,string start, string end)
        {
            var user = await _db.Users.Where(i => i.Id == uid).FirstOrDefaultAsync();
            if (user != null)
            {
                if (user.Status == "Admin")
                {

                    var data = await _db.ViewStocks.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date)).Select(i => new { name = i.StoreName, count = _db.Stocks.Where(j => j.StoreId == i.StoreId).Sum(k => k.Count) }).Distinct().ToListAsync();
                    return Ok(new { data = data });
                }
                else
                {

                    var div = await _db.Udivisions.Where(i => i.UserId == uid).Select(i=> i.DivisionCode).ToListAsync();
                    if (div != null)
                    {
                        var data = await _db.ViewStocks.Where(i=> div.Contains(i.DivisionCode)).Select(i => new {div = i.DivisionCode , name = i.StoreName, count = _db.Stocks.Where(j => j.StoreId == i.StoreId).Sum(k => k.Count) }).Distinct().ToListAsync();
                        //var data = await _db.ViewStocks.Where(i => i.DivisionCode == div.DivisionCode && (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date)).Select(i => new { name = i.StoreName, count = _db.Stocks.Where(j => j.StoreId == i.StoreId).Sum(k => k.Count) }).Distinct().ToListAsync();
                        return Ok(new { data = data });
                    }
                }
            }
            return NotFound();
        }
    }
}
