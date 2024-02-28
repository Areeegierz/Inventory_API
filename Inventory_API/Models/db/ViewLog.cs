using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class ViewLog
{
    public int Id { get; set; }

    public string? Refcode { get; set; }

    public string? StoreId { get; set; }

    public string? StoreName { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Detail { get; set; }
    public string? Status { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }
}
