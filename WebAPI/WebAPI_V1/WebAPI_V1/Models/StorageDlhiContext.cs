using System;
using Microsoft.EntityFrameworkCore;
using WebAPI_V1.Models.ResponseDto.ItemResponse;
using WebAPI_V1.Models.ResponseDto.ItemResponse.ItemResponse;
using WebAPI_V1.Models.ResponseDto.ItemResponse.ItemResponseDto;
using WebAPI_V1.Models.ResponseDto.UnitResponse;

namespace WebAPI_V1.Models;

public partial class StorageDlhiContext : DbContext
{
    public StorageDlhiContext()
    {
    }

    public StorageDlhiContext(DbContextOptions<StorageDlhiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ItemResponse> ItemResponses { get; set; }
    public virtual DbSet<ItemByWarehouseResponseDto> ItemByWarehouseResponses{ get; set; }


    //public List<ItemWare> ExcuteProc(Guid id)
    //{
    //    return this.Set<ItemWare>().FromSqlRaw($"EXEC GET_ITEMS_EXPORT_V2 '{id}'").ToList();
    //}

    //public class ItemWare
    //{
    //    public List<ItemByWarehouseResponseDto> Items { get; set; }
    //}

    //------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------
    public virtual DbSet<ExportItem> ExportItems { get; set; }

    public virtual DbSet<ExportItemDetail> ExportItemDetails { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<ImportItem> ImportItems { get; set; }

    public virtual DbSet<ImportItemDetail> ImportItemDetails { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<LocationWarehouse> LocationWarehouses { get; set; }

    public virtual DbSet<Mpr> Mprs { get; set; }

    public virtual DbSet<MprExport> MprExports { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Po> Pos { get; set; }

    public virtual DbSet<PoDetail> PoDetails { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierType> SupplierTypes { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<WarehouseDetail> WarehouseDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemByWarehouseResponseDto>(k => k.HasNoKey());
        // modelBuilder.Ignore<ItemByWarehouseResponseDto>();
        //------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------
        modelBuilder.Entity<ExportItem>(entity =>
        {
            entity.ToTable("EXPORT_ITEM");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BillNo).HasColumnName("BILL_NO");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("CREATED");
            entity.Property(e => e.SumQuantity).HasColumnName("SUM_QUANTITY");
        });

        modelBuilder.Entity<ExportItemDetail>(entity =>
        {
            entity.HasKey(e => new { e.ExportItemId, e.ItemId });

            entity.ToTable("EXPORT_ITEM_DETAIL");

            entity.Property(e => e.ExportItemId).HasColumnName("EXPORT_ITEM_ID");
            entity.Property(e => e.ItemId).HasColumnName("ITEM_ID");
            entity.Property(e => e.Note).HasColumnName("NOTE");
            entity.Property(e => e.Qty).HasColumnName("QTY");

            entity.HasOne(d => d.ExportItem).WithMany(p => p.ExportItemDetails)
                .HasForeignKey(d => d.ExportItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXPORT_ITEM_DETAIL_EXPORT_ITEM");

            entity.HasOne(d => d.Item).WithMany(p => p.ExportItemDetails)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXPORT_ITEM_DETAIL_ITEM");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GROUP_CONSUMABLE");

            entity.ToTable("GROUPS");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<ImportItem>(entity =>
        {
            entity.ToTable("IMPORT_ITEM");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BillNo).HasColumnName("BILL_NO");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("CREATED");
            entity.Property(e => e.SumPayment).HasColumnName("SUM_PAYMENT");
            entity.Property(e => e.SumPrice).HasColumnName("SUM_PRICE");
            entity.Property(e => e.SumQuantity).HasColumnName("SUM_QUANTITY");
        });

        modelBuilder.Entity<ImportItemDetail>(entity =>
        {
            entity.HasKey(e => new { e.ImportItemId, e.ItemId });

            entity.ToTable("IMPORT_ITEM_DETAIL");

            entity.Property(e => e.ImportItemId).HasColumnName("IMPORT_ITEM_ID");
            entity.Property(e => e.ItemId).HasColumnName("ITEM_ID");
            entity.Property(e => e.Note).HasColumnName("NOTE");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.Qty).HasColumnName("QTY");
            entity.Property(e => e.Total).HasColumnName("TOTAL");

            entity.HasOne(d => d.ImportItem).WithMany(p => p.ImportItemDetails)
                .HasForeignKey(d => d.ImportItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMPORT_ITEM_DETAIL_IMPORT_ITEM");

            entity.HasOne(d => d.Item).WithMany(p => p.ImportItemDetails)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMPORT_ITEM_DETAIL_ITEM");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("INVENTORY");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnName("AMOUNT");
            entity.Property(e => e.InventoryMonth).HasColumnName("INVENTORY_MONTH");
            entity.Property(e => e.ItemId).HasColumnName("ITEM_ID");

            entity.HasOne(d => d.Item).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_INVENTORY_ITEM");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CONSUMABLE");

            entity.ToTable("ITEM");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CODE");
            entity.Property(e => e.EngName).HasColumnName("ENG_NAME");
            entity.Property(e => e.GroupId).HasColumnName("GROUP_ID");
            entity.Property(e => e.LocationWarehouseId).HasColumnName("LOCATION_WAREHOUSE_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");
            entity.Property(e => e.Note).HasColumnName("NOTE");
            entity.Property(e => e.Picture).HasColumnName("PICTURE");
            entity.Property(e => e.PictureLink)
                .HasMaxLength(100)
                .HasColumnName("PICTURE_LINK");
            entity.Property(e => e.SupplierId).HasColumnName("SUPPLIER_ID");
            entity.Property(e => e.TypeId).HasColumnName("TYPE_ID");
            entity.Property(e => e.UnitId).HasColumnName("UNIT_ID");

            entity.HasOne(d => d.Group).WithMany(p => p.Items)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_COMSUMABLE_GROUP_CONSUMABLE");

            entity.HasOne(d => d.LocationWarehouse).WithMany(p => p.Items)
                .HasForeignKey(d => d.LocationWarehouseId)
                .HasConstraintName("FK_ITEM_LOCATIONWAREHOUSE");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Items)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_ITEM_SUPPLIER");

            entity.HasOne(d => d.Type).WithMany(p => p.Items)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_COMSUMABLE_TYPE_CONSUMABLE");

            entity.HasOne(d => d.Unit).WithMany(p => p.Items)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_COMSUMABLE_UNIT");
        });

        modelBuilder.Entity<LocationWarehouse>(entity =>
        {
            entity.ToTable("LOCATION_WAREHOUSE");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasColumnName("NAME");
        });

        modelBuilder.Entity<Mpr>(entity =>
        {
            entity.ToTable("MPR");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("CREATED");
            entity.Property(e => e.ExpectedDelivery)
                .HasColumnType("datetime")
                .HasColumnName("EXPECTED_DELIVERY");
            entity.Property(e => e.ItemId).HasColumnName("ITEM_ID");
            entity.Property(e => e.MprNo)
                .HasMaxLength(100)
                .HasColumnName("MPR_NO");
            entity.Property(e => e.Note).HasColumnName("NOTE");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
            entity.Property(e => e.Usage).HasColumnName("USAGE");

            entity.HasOne(d => d.Item).WithMany(p => p.Mprs)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MPR_ITEM");
        });

        modelBuilder.Entity<MprExport>(entity =>
        {
            entity.ToTable("MPR_EXPORT");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("CREATED");
            entity.Property(e => e.ItemCount).HasColumnName("ITEM_COUNT");
            entity.Property(e => e.Status).HasColumnName("STATUS");

            entity.HasMany(d => d.Mprs).WithMany(p => p.MprExports)
                .UsingEntity<Dictionary<string, object>>(
                    "MprExportDetail",
                    r => r.HasOne<Mpr>().WithMany()
                        .HasForeignKey("MprId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MPR_EXPORT_DETAIL_MPR"),
                    l => l.HasOne<MprExport>().WithMany()
                        .HasForeignKey("MprExportId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MPR_EXPORT_DETAIL_MPR_EXPORT"),
                    j =>
                    {
                        j.HasKey("MprExportId", "MprId");
                        j.ToTable("MPR_EXPORT_DETAIL");
                        j.IndexerProperty<Guid>("MprExportId").HasColumnName("MPR_EXPORT_ID");
                        j.IndexerProperty<Guid>("MprId").HasColumnName("MPR_ID");
                    });
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.ToTable("PAYMENT_METHOD");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasColumnName("NAME");
        });

        modelBuilder.Entity<Po>(entity =>
        {
            entity.ToTable("PO");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("CREATED");
            entity.Property(e => e.ExpectedDelivery)
                .HasColumnType("datetime")
                .HasColumnName("EXPECTED_DELIVERY");
            entity.Property(e => e.LocationWarehouseId).HasColumnName("LOCATION_WAREHOUSE_ID");
            entity.Property(e => e.PaymentMethodId).HasColumnName("PAYMENT_METHOD_ID");
            entity.Property(e => e.Total).HasColumnName("TOTAL");

            entity.HasOne(d => d.LocationWarehouse).WithMany(p => p.Pos)
                .HasForeignKey(d => d.LocationWarehouseId)
                .HasConstraintName("FK_PO_LOCATION_WAREHOUSE");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Pos)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK_PO_PAYMENT_METHOD");
        });

        modelBuilder.Entity<PoDetail>(entity =>
        {
            entity.HasKey(e => new { e.PoId, e.ItemId });

            entity.ToTable("PO_DETAIL");

            entity.Property(e => e.PoId).HasColumnName("PO_ID");
            entity.Property(e => e.ItemId).HasColumnName("ITEM_ID");
            entity.Property(e => e.MprNo).HasColumnName("MPR_NO");
            entity.Property(e => e.PoNo).HasColumnName("PO_NO");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

            entity.HasOne(d => d.Item).WithMany(p => p.PoDetails)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PO_DETAIL_ITEM");

            entity.HasOne(d => d.Po).WithMany(p => p.PoDetails)
                .HasForeignKey(d => d.PoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PO_DETIAL_PO");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("SUPPLIER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Address).HasColumnName("ADDRESS");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CODE");
            entity.Property(e => e.Email).HasColumnName("EMAIL");
            entity.Property(e => e.NameCompanySupplier).HasColumnName("NAME_COMPANY_SUPPLIER");
            entity.Property(e => e.NameSuppier)
                .HasMaxLength(100)
                .HasColumnName("NAME_SUPPIER");
            entity.Property(e => e.Note).HasColumnName("NOTE");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PHONE");
        });

        modelBuilder.Entity<SupplierType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SUPPLIER_TYPES");

            entity.ToTable("SUPPLIER_TYPE");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TYPE_CONSUMABLE");

            entity.ToTable("TYPES");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.ToTable("UNIT");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.ToTable("WAREHOUSE");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Address).HasColumnName("ADDRESS");
            entity.Property(e => e.Name).HasColumnName("NAME");
            entity.Property(e => e.TotalItem).HasColumnName("TOTAL_ITEM");
        });

        modelBuilder.Entity<WarehouseDetail>(entity =>
        {
            entity.HasKey(e => new { e.WarehouseId, e.ItemId, e.Month, e.Year });

            entity.ToTable("WAREHOUSE_DETAIL");

            entity.Property(e => e.WarehouseId).HasColumnName("WAREHOUSE_ID");
            entity.Property(e => e.ItemId).HasColumnName("ITEM_ID");
            entity.Property(e => e.Month).HasColumnName("MONTH");
            entity.Property(e => e.Year).HasColumnName("YEAR");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

            entity.HasOne(d => d.Item).WithMany(p => p.WarehouseDetails)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WAREHOUSE_DETAIL_ITEM");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.WarehouseDetails)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WAREHOUSE_DETAIL_WAREHOUSE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
