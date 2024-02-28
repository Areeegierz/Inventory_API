using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class ViewOverView
{
    public string? Name { get; set; }

    public string? StoreId { get; set; }

    public double Count { get; set; }

    public string? DivisionCode { get; set; }
}
