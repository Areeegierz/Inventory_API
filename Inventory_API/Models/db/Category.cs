using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }
}
