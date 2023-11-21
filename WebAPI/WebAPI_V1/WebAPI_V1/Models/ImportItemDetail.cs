using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class ImportItemDetail
{
    public Guid ImportItemId { get; set; }

    public Guid ItemId { get; set; }

    public long? Qty { get; set; }

    public long? Price { get; set; }

    public long? Total { get; set; }

    public string? Note { get; set; }

    public virtual ImportItem ImportItem { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
