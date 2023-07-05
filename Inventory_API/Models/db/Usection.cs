using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class Usection
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? CompCode { get; set; }

    public string? DivisionCode { get; set; }

    public string? DepartmentCode { get; set; }

    public string? SectionCode { get; set; }
}
