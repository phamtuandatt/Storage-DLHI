using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class PaymentMethod
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Po> Pos { get; set; } = new List<Po>();
}
