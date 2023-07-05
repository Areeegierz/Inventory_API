using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class SubCategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? CategoryId { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }
}
