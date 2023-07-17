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
        [HttpGet("GetOverview/{start}/{end}")]
        public IActionResult GetOverview(string start,string end)
        {

            var data1 = _db.Stocks.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date)).Count();
            var data2 = _db.Orders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date)).Count();
            var data3 = data1+data2;
            var data4 = _db.Stocks.Count();
            return Ok(new { data1 = data1, data2 = data2, data3 = data3, data4 = data4 });
        }
        [HttpGet("GetPie")]
        public IActionResult Index()
        {
            var data1 = _db.Orders.Where(i => i.CompCode == "0130").ToList();
            var data2 = _db.Orders.Where(i => i.CompCode == "0140").ToList();
            var data3 = _db.Orders.Where(i => i.CompCode == "0150").ToList();
            var data4 = _db.Orders.Where(i => i.CompCode == "0190").ToList();
            return Ok(new { data1 = data1.Count(), data2 = data2.Count(), data3 = data3.Count(), data4 = data4.Count() });
        }
        [HttpGet("GetPie/{start}/{end}")]
        public IActionResult Index(string start,string end)
        {
            var data1 = _db.Orders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.CompCode == "0130").ToList();
            var data2 = _db.Orders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.CompCode == "0140").ToList();
            var data3 = _db.Orders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.CompCode == "0150").ToList();
            var data4 = _db.Orders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date) && i.CompCode == "0190").ToList();
            return Ok(new { data1 = data1.Count(), data2 = data2.Count(), data3 = data3.Count(), data4 = data4.Count() });
        }
        [HttpGet("GetChart")]
        public async Task<IActionResult> GetChart()
        {
            var data = await _db.ViewOrders.Select(i => new { name = i.Use + " " + i.PlantName, count = _db.Orders.Where(j => j.Use == i.Use).Sum(k => k.Count) }).Distinct().ToListAsync();
            return Ok(new { data = data });
        }
        [HttpGet("GetChart/{start}/{end}")]
        public async Task<IActionResult> GetChart(string start,string end)
        {

            var data = await _db.ViewOrders.Where(i => (i.CreateDate.Value.Date > DateTime.Parse(start).Date && i.CreateDate.Value.Date < DateTime.Parse(end).Date)).Select(i => new { name = i.Use + " " + i.PlantName, count = _db.Orders.Where(j => j.Use == i.Use).Sum(k => k.Count) }).Distinct().ToListAsync();
            return Ok(new { data = data });
        }
    }
}
