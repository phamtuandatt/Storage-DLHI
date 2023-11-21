using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class MprExport
{
    public Guid Id { get; set; }

    public DateTime? Created { get; set; }

    public long? ItemCount { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Mpr> Mprs { get; set; } = new List<Mpr>();
}
