using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class Udivision
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? CompCode { get; set; }

    public string? DivisionCode { get; set; }
}
