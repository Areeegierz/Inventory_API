using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class ViewPieChart
{
    public string? CompCode { get; set; }

    public string? CompName { get; set; }

    public int? Count { get; set; }
}
