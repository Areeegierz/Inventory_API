﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Models.db;

public partial class InventoryContext : DbContext
{
    public InventoryContext()
    {
    }

    public InventoryContext(DbContextOptions<InventoryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupMaterial> GroupMaterials { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Machine> Machines { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Materialsss> Materialssses { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Plant> Plants { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Structure> Structures { get; set; }

    public virtual DbSet<Structuresss> Structuressses { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<Ucomp> Ucomps { get; set; }

    public virtual DbSet<Udepartment> Udepartments { get; set; }

    public virtual DbSet<Udivision> Udivisions { get; set; }

    public virtual DbSet<Uplant> Uplants { get; set; }

    public virtual DbSet<Usection> Usections { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<ViewBarChart> ViewBarCharts { get; set; }

    public virtual DbSet<ViewCategory> ViewCategories { get; set; }

    public virtual DbSet<ViewGroupMaterial> ViewGroupMaterials { get; set; }

    public virtual DbSet<ViewLog> ViewLogs { get; set; }

    public virtual DbSet<ViewMaterial> ViewMaterials { get; set; }

    public virtual DbSet<ViewOrder> ViewOrders { get; set; }

    public virtual DbSet<ViewOverView> ViewOverViews { get; set; }

    public virtual DbSet<ViewPieChart> ViewPieCharts { get; set; }

    public virtual DbSet<ViewStock> ViewStocks { get; set; }

    public virtual DbSet<ViewStore> ViewStores { get; set; }

    public virtual DbSet<ViewSubCategory> ViewSubCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.1.151\\SQL2012;Database=Inventory;User Id=sa;Password=Abc@1234;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Thai_CI_AS");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC0799B60670");

            entity.ToTable("Category");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompCode__3214EC0724566323");

            entity.ToTable("Company");

            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Group");

            entity.Property(e => e.CreateBy).IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.UpdateBy).IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<GroupMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GroupStock");

            entity.ToTable("GroupMaterial");

            entity.Property(e => e.CreateBy).IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MatCode).IsUnicode(false);
            entity.Property(e => e.UpdateBy).IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Inventoryss");

            entity.ToTable("Inventory");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CompCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Detail).IsUnicode(false);
            entity.Property(e => e.File)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Parts)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StoreArea)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StoreCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserAree)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("Log");

            entity.Property(e => e.CreateBy).IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Detail).IsUnicode(false);
            entity.Property(e => e.Refcode).IsUnicode(false);
            entity.Property(e => e.Status).IsUnicode(false);
        });

        modelBuilder.Entity<Machine>(entity =>
        {
            entity.ToTable("Machine");

            entity.Property(e => e.CreateBy).IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.UpdateBy).IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Mat1");

            entity.ToTable("Material");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CompCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Detail).IsUnicode(false);
            entity.Property(e => e.File)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Parts)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StoreId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Materialsss>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Material");

            entity.ToTable("Materialsss");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CompCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Detail).IsUnicode(false);
            entity.Property(e => e.File)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Parts)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StoreId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ComeOut");

            entity.ToTable("Order");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CompCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Detail).IsUnicode(false);
            entity.Property(e => e.File)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Parts)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RefCode).IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StoreId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Use)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Plant__3214EC07806D3D2F");

            entity.ToTable("Plant");

            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.ToTable("Stock");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CompCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Detail).IsUnicode(false);
            entity.Property(e => e.File)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Parts)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RefCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StoreId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Store__3214EC07251FD5B8");

            entity.ToTable("Store");

            entity.Property(e => e.DivisionCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Structure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ST");

            entity.ToTable("Structure");

            entity.Property(e => e.CompCode).HasMaxLength(255);
            entity.Property(e => e.CompName).HasMaxLength(255);
            entity.Property(e => e.DepartmentCode).HasMaxLength(255);
            entity.Property(e => e.DepartmentName).HasMaxLength(255);
            entity.Property(e => e.DivisionCode).HasMaxLength(255);
            entity.Property(e => e.DivisionName).HasMaxLength(255);
            entity.Property(e => e.PlantCode).HasMaxLength(255);
            entity.Property(e => e.PlantName).HasMaxLength(255);
            entity.Property(e => e.PlantType).HasMaxLength(255);
            entity.Property(e => e.Province).HasMaxLength(255);
            entity.Property(e => e.SectionCode).HasMaxLength(255);
            entity.Property(e => e.SectionName).HasMaxLength(255);
        });

        modelBuilder.Entity<Structuresss>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Structure");

            entity.ToTable("Structuresss");

            entity.Property(e => e.CompCode).HasMaxLength(255);
            entity.Property(e => e.CompName).HasMaxLength(255);
            entity.Property(e => e.DepartmentCode).HasMaxLength(255);
            entity.Property(e => e.DepartmentName).HasMaxLength(255);
            entity.Property(e => e.DivisionCode).HasMaxLength(255);
            entity.Property(e => e.DivisionName).HasMaxLength(255);
            entity.Property(e => e.PlantCode).HasMaxLength(255);
            entity.Property(e => e.PlantName).HasMaxLength(255);
            entity.Property(e => e.PlantType).HasMaxLength(255);
            entity.Property(e => e.Province).HasMaxLength(255);
            entity.Property(e => e.SectionCode).HasMaxLength(255);
            entity.Property(e => e.SectionName).HasMaxLength(255);
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.ToTable("SubCategory");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<Ucomp>(entity =>
        {
            entity.ToTable("UComp");

            entity.Property(e => e.CompCode)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Udepartment>(entity =>
        {
            entity.ToTable("UDepartments");

            entity.Property(e => e.CompCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Udivision>(entity =>
        {
            entity.ToTable("UDivision");

            entity.Property(e => e.CompCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Uplant>(entity =>
        {
            entity.ToTable("UPlant");

            entity.Property(e => e.CompCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlantCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SectionCode)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usection>(entity =>
        {
            entity.ToTable("USection");

            entity.Property(e => e.CompCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SectionCode)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Fname).HasColumnName("FName");
            entity.Property(e => e.Lname).HasColumnName("LName");
        });

        modelBuilder.Entity<ViewBarChart>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_BarChart");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PlantName).HasMaxLength(255);
            entity.Property(e => e.Use)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_Category");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<ViewGroupMaterial>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_GroupMaterial");

            entity.Property(e => e.MatCode).IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<ViewLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_Log");

            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateBy).IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Detail).IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Refcode).IsUnicode(false);
            entity.Property(e => e.StoreId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StoreName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewMaterial>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_Material");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CompCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Detail).IsUnicode(false);
            entity.Property(e => e.File)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Parts)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ViewOrder>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_Order");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CategoryName).IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CompCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DepartmentCode).HasMaxLength(255);
            entity.Property(e => e.DepartmentName).HasMaxLength(255);
            entity.Property(e => e.Detail).IsUnicode(false);
            entity.Property(e => e.DivisionCode).HasMaxLength(255);
            entity.Property(e => e.DivisionName).HasMaxLength(255);
            entity.Property(e => e.File)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Parts)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlantName).HasMaxLength(255);
            entity.Property(e => e.RefCode).IsUnicode(false);
            entity.Property(e => e.SectionCode).HasMaxLength(255);
            entity.Property(e => e.SectionName).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StoreId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StoreName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SubCategoryName).IsUnicode(false);
            entity.Property(e => e.ThisDivisionCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ThisDIvisionCode");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Use)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewOverView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_OverView");

            entity.Property(e => e.DivisionCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.StoreId)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewPieChart>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_PieChart");

            entity.Property(e => e.CompCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CompName).HasMaxLength(255);
        });

        modelBuilder.Entity<ViewStock>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_Stock");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CategoryName).IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CompCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Detail).IsUnicode(false);
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.File)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Parts)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StoreId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StoreName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SubCategoryName).IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ViewStore>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_Store");

            entity.Property(e => e.DivisionCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DivisionName).HasMaxLength(255);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewSubCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_SubCategory");

            entity.Property(e => e.CategoryName).IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
