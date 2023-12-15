using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class Mpr
{
    public Guid Id { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? ExpectedDelivery { get; set; }

    public string? Note { get; set; }

    public Guid ItemId { get; set; }

    public string? MprNo { get; set; }

    public string? Usage { get; set; }

    public long? Quantity { get; set; }

    public virtual Item? Item { get; set; }

    public virtual ICollection<MprExport> MprExports { get; set; } = new List<MprExport>();
}
