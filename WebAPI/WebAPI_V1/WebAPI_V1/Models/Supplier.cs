using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class Supplier
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string? NameSuppier { get; set; }

    public string? NameCompanySupplier { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
