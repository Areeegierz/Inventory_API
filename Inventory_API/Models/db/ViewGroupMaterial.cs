using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class ViewGroupMaterial
{
    public int Id { get; set; }

    public string? MatCode { get; set; }

    public int? GroupId { get; set; }

    public string? Name { get; set; }

    public double Count { get; set; }
}
