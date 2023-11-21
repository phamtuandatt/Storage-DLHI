using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class Warehouse
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public long? TotalItem { get; set; }

    public virtual ICollection<Po> Pos { get; set; } = new List<Po>();

    public virtual ICollection<WarehouseDetail> WarehouseDetails { get; set; } = new List<WarehouseDetail>();
}
