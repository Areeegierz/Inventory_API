﻿using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class Store
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }
}
