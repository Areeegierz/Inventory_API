using System;
using System.Collections.Generic;

namespace Inventory_API.Models.db;

public partial class Order
{
    public int Id { get; set; }

    public string? CompCode { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Detail { get; set; }

    public string? Parts { get; set; }

    public double? Count { get; set; }

    public string? Unit { get; set; }

    public string? StoreCode { get; set; }

    public string? RefCode { get; set; }

    public string? Status { get; set; }

    public string? Use { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Type { get; set; }

    public string? File { get; set; }

    public string? AccountNo { get; set; }

    public int? CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }
}
