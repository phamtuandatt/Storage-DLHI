using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class ExportItemDetail
{
    public Guid ExportItemId { get; set; }

    public Guid ItemId { get; set; }

    public long? Qty { get; set; }

    public string? Note { get; set; }

    public virtual ExportItem ExportItem { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
