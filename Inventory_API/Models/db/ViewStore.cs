﻿using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class ViewStore
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? DivisionCode { get; set; }

    public string? DivisionName { get; set; }
}
