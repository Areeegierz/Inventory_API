using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class ViewBarChart
{
    public string? Use { get; set; }

    public string? PlantName { get; set; }

    public int? Count { get; set; }

    public DateTime? CreateDate { get; set; }
}
