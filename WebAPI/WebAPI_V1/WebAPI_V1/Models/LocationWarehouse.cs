using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class LocationWarehouse
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
