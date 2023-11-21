using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class WarehouseDetail
{
    public Guid WarehouseId { get; set; }

    public Guid ItemId { get; set; }

    public long? Quantity { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
