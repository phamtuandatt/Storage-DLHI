using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class ExportItem
{
    public Guid Id { get; set; }

    public DateTime? Created { get; set; }

    public string? BillNo { get; set; }

    public long? SumQuantity { get; set; }

    public virtual ICollection<ExportItemDetail> ExportItemDetails { get; set; } = new List<ExportItemDetail>();
}
