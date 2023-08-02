using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class Machine
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
