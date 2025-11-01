using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Models;

namespace QuanLyKho.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PurchaseOrder
            modelBuilder.Entity<QuanLyKho.Models.Invent.PurchaseOrder>()
                .Property(e => e.totalDiscountAmount).HasPrecision(18, 4);
            modelBuilder.Entity<QuanLyKho.Models.Invent.PurchaseOrder>()
                .Property(e => e.totalOrderAmount).HasPrecision(18, 4);

            // PurchaseOrderLine
            modelBuilder.Entity<QuanLyKho.Models.Invent.PurchaseOrderLine>()
                .Property(e => e.discountAmount).HasPrecision(18, 4);
            modelBuilder.Entity<QuanLyKho.Models.Invent.PurchaseOrderLine>()
                .Property(e => e.price).HasPrecision(18, 4);
            modelBuilder.Entity<QuanLyKho.Models.Invent.PurchaseOrderLine>()
                .Property(e => e.totalAmount).HasPrecision(18, 4);

            // SalesOrder
            modelBuilder.Entity<QuanLyKho.Models.Invent.SalesOrder>()
                .Property(e => e.totalDiscountAmount).HasPrecision(18, 4);
            modelBuilder.Entity<QuanLyKho.Models.Invent.SalesOrder>()
                .Property(e => e.totalOrderAmount).HasPrecision(18, 4);

            // SalesOrderLine
            modelBuilder.Entity<QuanLyKho.Models.Invent.SalesOrderLine>()
                .Property(e => e.discountAmount).HasPrecision(18, 4);
            modelBuilder.Entity<QuanLyKho.Models.Invent.SalesOrderLine>()
                .Property(e => e.price).HasPrecision(18, 4);
            modelBuilder.Entity<QuanLyKho.Models.Invent.SalesOrderLine>()
                .Property(e => e.totalAmount).HasPrecision(18, 4);

            // Thêm các property khác nếu có kiểu decimal tùy chỉnh


            // Sửa Multiple Cascade Paths cho TransferOrder → Branch
            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferOrder>()
                .HasOne<QuanLyKho.Models.Invent.Branch>(x => x.branchFrom)
                .WithMany()
                .HasForeignKey(x => x.branchIdFrom)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferOrder>()
                .HasOne<QuanLyKho.Models.Invent.Branch>(x => x.branchTo)
                .WithMany()
                .HasForeignKey(x => x.branchIdTo)
                .OnDelete(DeleteBehavior.Restrict);

            // Tương tự cho warehouseFromwarehouseId, warehouseTowarehouseId cũng vậy (nếu cần)
            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferOrder>()
                .HasOne<QuanLyKho.Models.Invent.Warehouse>(x => x.warehouseFrom)
                .WithMany()
                .HasForeignKey(x => x.warehouseIdFrom)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferOrder>()
                .HasOne<QuanLyKho.Models.Invent.Warehouse>(x => x.warehouseTo)
                .WithMany()
                .HasForeignKey(x => x.warehouseIdTo)
                .OnDelete(DeleteBehavior.Restrict);

            // Tránh lỗi multiple cascade paths cho TransferOrder
            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferOrder>()
                .HasOne(x => x.branchFrom)
                .WithMany()
                .HasForeignKey(x => x.branchIdFrom)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferOrder>()
                .HasOne(x => x.branchTo)
                .WithMany()
                .HasForeignKey(x => x.branchIdTo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferOrder>()
                .HasOne(x => x.warehouseFrom)
                .WithMany()
                .HasForeignKey(x => x.warehouseIdFrom)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferOrder>()
                .HasOne(x => x.warehouseTo)
                .WithMany()
                .HasForeignKey(x => x.warehouseIdTo)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure TransferOut relationships to avoid shadow FKs like branchFrombranchId, etc.
            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferOut>()
                .HasOne(x => x.transferOrder)
                .WithMany()
                .HasForeignKey(x => x.transferOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferOut>()
                .HasOne(x => x.branchFrom)
                .WithMany()
                .HasForeignKey(x => x.branchIdFrom)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferOut>()
                .HasOne(x => x.branchTo)
                .WithMany()
                .HasForeignKey(x => x.branchIdTo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferOut>()
                .HasOne(x => x.warehouseFrom)
                .WithMany()
                .HasForeignKey(x => x.warehouseIdFrom)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferOut>()
                .HasOne(x => x.warehouseTo)
                .WithMany()
                .HasForeignKey(x => x.warehouseIdTo)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure TransferIn relationships to avoid shadow FKs like branchFrombranchId, etc.
            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferIn>()
                .HasOne(x => x.transferOrder)
                .WithMany()
                .HasForeignKey(x => x.transferOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferIn>()
                .HasOne(x => x.branchFrom)
                .WithMany()
                .HasForeignKey(x => x.branchIdFrom)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferIn>()
                .HasOne(x => x.branchTo)
                .WithMany()
                .HasForeignKey(x => x.branchIdTo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferIn>()
                .HasOne(x => x.warehouseFrom)
                .WithMany()
                .HasForeignKey(x => x.warehouseIdFrom)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.TransferIn>()
                .HasOne(x => x.warehouseTo)
                .WithMany()
                .HasForeignKey(x => x.warehouseIdTo)
                .OnDelete(DeleteBehavior.Restrict);

            // Tránh lỗi multiple cascade paths cho Shipment
            modelBuilder.Entity<QuanLyKho.Models.Invent.Shipment>()
                .HasOne(x => x.branch)
                .WithMany()
                .HasForeignKey(x => x.branchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.Shipment>()
                .HasOne(x => x.customer)
                .WithMany()
                .HasForeignKey(x => x.customerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.Shipment>()
                .HasOne(x => x.salesOrder)
                .WithMany()
                .HasForeignKey(x => x.salesOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.Shipment>()
                .HasOne(x => x.warehouse)
                .WithMany()
                .HasForeignKey(x => x.warehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure ShipmentLine relationships to avoid multiple cascade paths
            modelBuilder.Entity<QuanLyKho.Models.Invent.ShipmentLine>()
                .HasOne(x => x.shipment)
                .WithMany()
                .HasForeignKey(x => x.shipmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuanLyKho.Models.Invent.ShipmentLine>()
                .HasOne(x => x.branch)
                .WithMany()
                .HasForeignKey(x => x.branchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.ShipmentLine>()
                .HasOne(x => x.warehouse)
                .WithMany()
                .HasForeignKey(x => x.warehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.ShipmentLine>()
                .HasOne(x => x.product)
                .WithMany()
                .HasForeignKey(x => x.productId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tránh lỗi multiple cascade paths cho Receiving
            modelBuilder.Entity<QuanLyKho.Models.Invent.Receiving>()
                .HasOne(x => x.branch)
                .WithMany()
                .HasForeignKey(x => x.branchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.Receiving>()
                .HasOne(x => x.vendor)
                .WithMany()
                .HasForeignKey(x => x.vendorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.Receiving>()
                .HasOne(x => x.purchaseOrder)
                .WithMany()
                .HasForeignKey(x => x.purchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.Receiving>()
                .HasOne(x => x.warehouse)
                .WithMany()
                .HasForeignKey(x => x.warehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure ReceivingLine relationships to avoid multiple cascade paths
            modelBuilder.Entity<QuanLyKho.Models.Invent.ReceivingLine>()
                .HasOne(x => x.receiving)
                .WithMany()
                .HasForeignKey(x => x.receivingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuanLyKho.Models.Invent.ReceivingLine>()
                .HasOne(x => x.branch)
                .WithMany()
                .HasForeignKey(x => x.branchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuanLyKho.Models.Invent.ReceivingLine>()
                .HasOne(x => x.warehouse)
                .WithMany()
                .HasForeignKey(x => x.warehouseId)
                .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<QuanLyKho.Models.Invent.ReceivingLine>()
                .HasOne(x => x.product)
                .WithMany()
                .HasForeignKey(x => x.productId)
                .OnDelete(DeleteBehavior.Restrict);

			// Seed basic fake data to enable initial queries
			modelBuilder.Entity<QuanLyKho.Models.Invent.Branch>().HasData(
				new QuanLyKho.Models.Invent.Branch
				{
					branchId = "00000000-0000-0000-0000-000000000001",
					branchName = "Main Branch",
					description = "Default branch",
					isDefaultBranch = true,
					street1 = "123 Main St",
					street2 = "",
					city = "Hanoi",
					province = "HN",
					country = "VN",
					createdAt = System.DateTime.UtcNow
				}
			);

			modelBuilder.Entity<QuanLyKho.Models.Invent.Warehouse>().HasData(
				new QuanLyKho.Models.Invent.Warehouse
				{
					warehouseId = "00000000-0000-0000-0000-000000000001",
					warehouseName = "Central WH",
					description = "Main warehouse",
					branchId = "00000000-0000-0000-0000-000000000001",
					street1 = "Zone A",
					street2 = "",
					city = "Hanoi",
					province = "HN",
					country = "VN",
					createdAt = System.DateTime.UtcNow
				}
			);

			modelBuilder.Entity<QuanLyKho.Models.Invent.Product>().HasData(
				new QuanLyKho.Models.Invent.Product
				{
					productId = "00000000-0000-0000-0000-000000000001",
					productCode = "P-001",
					productName = "Sample Product 1",
					description = "Demo item",
					barcode = "893000000001",
					serialNumber = "SN-001",
					productType = QuanLyKho.Models.Invent.ProductType.FMCG,
					uom = QuanLyKho.Models.Invent.UOM.Pcs,
					createdAt = System.DateTime.UtcNow
				},
				new QuanLyKho.Models.Invent.Product
				{
					productId = "00000000-0000-0000-0000-000000000002",
					productCode = "P-002",
					productName = "Sample Product 2",
					description = "Demo item",
					barcode = "893000000002",
					serialNumber = "SN-002",
					productType = QuanLyKho.Models.Invent.ProductType.Electronic,
					uom = QuanLyKho.Models.Invent.UOM.Unit,
					createdAt = System.DateTime.UtcNow
				}
			);

			modelBuilder.Entity<QuanLyKho.Models.Invent.Vendor>().HasData(
				new QuanLyKho.Models.Invent.Vendor
				{
					vendorId = "00000000-0000-0000-0000-000000000001",
					vendorName = "Acme Supplier",
					description = "Default vendor",
					size = QuanLyKho.Models.Invent.BusinessSize.Small,
					HasChild = "No",
					street1 = "12 Supplier Rd",
					street2 = "",
					city = "Hanoi",
					province = "HN",
					country = "VN",
					createdAt = System.DateTime.UtcNow
				}
			);

			modelBuilder.Entity<QuanLyKho.Models.Invent.Customer>().HasData(
				new QuanLyKho.Models.Invent.Customer
				{
					customerId = "00000000-0000-0000-0000-000000000001",
					customerName = "John Customer",
					description = "Default customer",
					size = QuanLyKho.Models.Invent.BusinessSize.Small,
					HasChild = "No",
					street1 = "34 Customer St",
					street2 = "",
					city = "Hanoi",
					province = "HN",
					country = "VN",
					createdAt = System.DateTime.UtcNow
				}
			);
        }

        public DbSet<QuanLyKho.Models.ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<QuanLyKho.Models.Invent.Branch> Branches { get; set; }
        public DbSet<QuanLyKho.Models.Invent.Warehouse> Warehouses { get; set; }
        public DbSet<QuanLyKho.Models.Invent.Product> Products { get; set; }
        public DbSet<QuanLyKho.Models.Invent.Vendor> Vendors { get; set; }
        public DbSet<QuanLyKho.Models.Invent.VendorLine> VendorLines { get; set; }
        public DbSet<QuanLyKho.Models.Invent.PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<QuanLyKho.Models.Invent.PurchaseOrderLine> PurchaseOrderLines { get; set; }
        public DbSet<QuanLyKho.Models.Invent.Customer> Customers { get; set; }
        public DbSet<QuanLyKho.Models.Invent.CustomerLine> CustomerLines { get; set; }
        public DbSet<QuanLyKho.Models.Invent.SalesOrder> SalesOrders { get; set; }
        public DbSet<QuanLyKho.Models.Invent.SalesOrderLine> SalesOrderLines { get; set; }
        public DbSet<QuanLyKho.Models.Invent.Shipment> Shipments { get; set; }
        public DbSet<QuanLyKho.Models.Invent.ShipmentLine> ShipmentLines { get; set; }
        public DbSet<QuanLyKho.Models.Invent.Receiving> Receivings { get; set; }
        public DbSet<QuanLyKho.Models.Invent.ReceivingLine> ReceivingLines { get; set; }
        public DbSet<QuanLyKho.Models.Invent.TransferOrder> TransferOrders { get; set; }
        public DbSet<QuanLyKho.Models.Invent.TransferOrderLine> TransferOrderLines { get; set; }
        public DbSet<QuanLyKho.Models.Invent.TransferOut> TransferOuts { get; set; }
        public DbSet<QuanLyKho.Models.Invent.TransferOutLine> TransferOutLines { get; set; }
        public DbSet<QuanLyKho.Models.Invent.TransferIn> TransferIns { get; set; }
        public DbSet<QuanLyKho.Models.Invent.TransferInLine> TransferInLines { get; set; }
    }
}
