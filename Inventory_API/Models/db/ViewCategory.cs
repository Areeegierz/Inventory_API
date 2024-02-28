using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class ViewCategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public double Count { get; set; }
}
