using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class Inventory
{
    public Guid Id { get; set; }

    public Guid? ItemId { get; set; }

    public int? InventoryMonth { get; set; }

    public long? Amount { get; set; }

    public virtual Item? Item { get; set; }
}
