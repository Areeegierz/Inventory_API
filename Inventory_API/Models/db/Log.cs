using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class Log
{
    public int Id { get; set; }

    public string? Refcode { get; set; }

    public string? Detail { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Status { get; set; }

    public int? StockId { get; set; }
}
