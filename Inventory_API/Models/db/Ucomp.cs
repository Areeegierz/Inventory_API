using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class Ucomp
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? CompCode { get; set; }
}
