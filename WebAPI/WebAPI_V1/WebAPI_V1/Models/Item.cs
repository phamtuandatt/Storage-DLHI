using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class Item
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? PictureLink { get; set; }

    public byte[]? Picture { get; set; }

    public string? Note { get; set; }

    public string? EngName { get; set; }

    public Guid? UnitId { get; set; }

    public Guid? GroupId { get; set; }

    public Guid? TypeId { get; set; }

    public Guid? SupplierId { get; set; }

    public Guid? LocationWarehouseId { get; set; }

    public virtual ICollection<ExportItemDetail> ExportItemDetails { get; set; } = new List<ExportItemDetail>();

    public virtual Group? Group { get; set; }

    public virtual ICollection<ImportItemDetail> ImportItemDetails { get; set; } = new List<ImportItemDetail>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual LocationWarehouse? LocationWarehouse { get; set; }

    public virtual ICollection<Mpr> Mprs { get; set; } = new List<Mpr>();

    public virtual ICollection<PoDetail> PoDetails { get; set; } = new List<PoDetail>();

    public virtual Supplier? Supplier { get; set; }

    public virtual Type? Type { get; set; }

    public virtual Unit? Unit { get; set; }

    public virtual ICollection<WarehouseDetail> WarehouseDetails { get; set; } = new List<WarehouseDetail>();
}
