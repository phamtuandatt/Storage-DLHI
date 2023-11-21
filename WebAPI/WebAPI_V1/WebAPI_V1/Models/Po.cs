using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class Po
{
    public Guid Id { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? ExpectedDelivery { get; set; }

    public long? Total { get; set; }

    public Guid? LocationWarehouseId { get; set; }

    public Guid? PaymentMethodId { get; set; }

    public virtual Warehouse? LocationWarehouse { get; set; }

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual ICollection<PoDetail> PoDetails { get; set; } = new List<PoDetail>();
}
