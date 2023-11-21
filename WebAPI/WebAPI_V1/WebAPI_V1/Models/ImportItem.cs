using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class ImportItem
{
    public Guid Id { get; set; }

    public DateTime? Created { get; set; }

    public string? BillNo { get; set; }

    public long? SumQuantity { get; set; }

    public long? SumPrice { get; set; }

    public long? SumPayment { get; set; }

    public virtual ICollection<ImportItemDetail> ImportItemDetails { get; set; } = new List<ImportItemDetail>();
}
