using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class Structure
{
    public int Id { get; set; }

    public string? CompCode { get; set; }

    public string? CompName { get; set; }

    public string? DivisionCode { get; set; }

    public string? DivisionName { get; set; }

    public string? DepartmentCode { get; set; }

    public string? DepartmentName { get; set; }

    public string? SectionCode { get; set; }

    public string? SectionName { get; set; }

    public string? PlantCode { get; set; }

    public string? PlantName { get; set; }

    public string? PlantType { get; set; }

    public string? Province { get; set; }
}
