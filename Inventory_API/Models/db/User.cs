using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string? Email { get; set; }

    public string? Status { get; set; }

    public string? Role { get; set; }
}
