using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class PoDetail
{
    public Guid PoId { get; set; }

    public Guid ItemId { get; set; }

    public string? MprNo { get; set; }

    public string? PoNo { get; set; }

    public long? Price { get; set; }

    public long? Quantity { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Po Po { get; set; } = null!;
}
