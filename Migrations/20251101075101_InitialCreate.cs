using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetCore_Update.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HomeRole = table.Column<bool>(type: "bit", nullable: false),
                    BranchRole = table.Column<bool>(type: "bit", nullable: false),
                    CustomerRole = table.Column<bool>(type: "bit", nullable: false),
                    ProductRole = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseOrderRole = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseOrderLineRole = table.Column<bool>(type: "bit", nullable: false),
                    ReceivingRole = table.Column<bool>(type: "bit", nullable: false),
                    ReceivingLineRole = table.Column<bool>(type: "bit", nullable: false),
                    SalesOrderRole = table.Column<bool>(type: "bit", nullable: false),
                    SalesOrderLineRole = table.Column<bool>(type: "bit", nullable: false),
                    ShipmentRole = table.Column<bool>(type: "bit", nullable: false),
                    ShipmentLineRole = table.Column<bool>(type: "bit", nullable: false),
                    StockRole = table.Column<bool>(type: "bit", nullable: false),
                    TransferInRole = table.Column<bool>(type: "bit", nullable: false),
                    TransferInLineRole = table.Column<bool>(type: "bit", nullable: false),
                    TransferOrderRole = table.Column<bool>(type: "bit", nullable: false),
                    TransferOrderLineRole = table.Column<bool>(type: "bit", nullable: false),
                    TransferOutRole = table.Column<bool>(type: "bit", nullable: false),
                    TransferOutLineRole = table.Column<bool>(type: "bit", nullable: false),
                    VendorRole = table.Column<bool>(type: "bit", nullable: false),
                    VendorLineRole = table.Column<bool>(type: "bit", nullable: false),
                    WarehouseRole = table.Column<bool>(type: "bit", nullable: false),
                    profilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserRole = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    branchId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    branchName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    isDefaultBranch = table.Column<bool>(type: "bit", nullable: false),
                    street1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    street2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    city = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    province = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    country = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.branchId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customerId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    customerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    size = table.Column<int>(type: "int", nullable: false),
                    street1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    street2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    city = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    province = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    country = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasChild = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.customerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    productId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    productCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    productName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    serialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    productType = table.Column<int>(type: "int", nullable: false),
                    uom = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.productId);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    vendorId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    vendorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    size = table.Column<int>(type: "int", nullable: false),
                    street1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    street2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    city = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    province = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    country = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasChild = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.vendorId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    warehouseId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    warehouseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    branchId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    street1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    street2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    city = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    province = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    country = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.warehouseId);
                    table.ForeignKey(
                        name: "FK_Warehouses_Branches_branchId",
                        column: x => x.branchId,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLines",
                columns: table => new
                {
                    customerLineId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    jobTitle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    customerId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    middleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nickName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    salutation = table.Column<int>(type: "int", nullable: false),
                    mobilePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    officePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    fax = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    personalEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    workEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLines", x => x.customerLineId);
                    table.ForeignKey(
                        name: "FK_CustomerLines_Customers_customerId",
                        column: x => x.customerId,
                        principalTable: "Customers",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrders",
                columns: table => new
                {
                    salesOrderId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    salesOrderNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    top = table.Column<int>(type: "int", nullable: false),
                    soDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deliveryAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    referenceNumberInternal = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    referenceNumberExternal = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    branchId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    customerId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    picInternal = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    picCustomer = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    salesOrderStatus = table.Column<int>(type: "int", nullable: false),
                    totalDiscountAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    totalOrderAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    salesShipmentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasChild = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrders", x => x.salesOrderId);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Branches_branchId",
                        column: x => x.branchId,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Customers_customerId",
                        column: x => x.customerId,
                        principalTable: "Customers",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    purchaseOrderId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    purchaseOrderNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    top = table.Column<int>(type: "int", nullable: false),
                    poDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deliveryAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    referenceNumberInternal = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    referenceNumberExternal = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    branchId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    vendorId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    picInternal = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    picVendor = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    purchaseOrderStatus = table.Column<int>(type: "int", nullable: false),
                    totalDiscountAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    totalOrderAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    purchaseReceiveNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasChild = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.purchaseOrderId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Branches_branchId",
                        column: x => x.branchId,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Vendors_vendorId",
                        column: x => x.vendorId,
                        principalTable: "Vendors",
                        principalColumn: "vendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorLines",
                columns: table => new
                {
                    vendorLineId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    jobTitle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    vendorId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    middleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nickName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    salutation = table.Column<int>(type: "int", nullable: false),
                    mobilePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    officePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    fax = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    personalEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    workEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorLines", x => x.vendorLineId);
                    table.ForeignKey(
                        name: "FK_VendorLines_Vendors_vendorId",
                        column: x => x.vendorId,
                        principalTable: "Vendors",
                        principalColumn: "vendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferOrders",
                columns: table => new
                {
                    transferOrderId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    transferOrderNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    transferOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    picName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    branchIdFrom = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    warehouseIdFrom = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    branchIdTo = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    warehouseIdTo = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    transferOrderStatus = table.Column<int>(type: "int", nullable: false),
                    isIssued = table.Column<bool>(type: "bit", nullable: false),
                    isReceived = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasChild = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferOrders", x => x.transferOrderId);
                    table.ForeignKey(
                        name: "FK_TransferOrders_Branches_branchIdFrom",
                        column: x => x.branchIdFrom,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOrders_Branches_branchIdTo",
                        column: x => x.branchIdTo,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOrders_Warehouses_warehouseIdFrom",
                        column: x => x.warehouseIdFrom,
                        principalTable: "Warehouses",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOrders_Warehouses_warehouseIdTo",
                        column: x => x.warehouseIdTo,
                        principalTable: "Warehouses",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderLines",
                columns: table => new
                {
                    salesOrderLineId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    salesOrderId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    productId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    qty = table.Column<float>(type: "real", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    discountAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    totalAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderLines", x => x.salesOrderLineId);
                    table.ForeignKey(
                        name: "FK_SalesOrderLines_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrderLines_SalesOrders_salesOrderId",
                        column: x => x.salesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "salesOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    shipmentId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    salesOrderId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    shipmentNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    shipmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    customerId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    customerPO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    invoice = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    branchId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    warehouseId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    expeditionType = table.Column<int>(type: "int", nullable: false),
                    expeditionMode = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasChild = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.shipmentId);
                    table.ForeignKey(
                        name: "FK_Shipments_Branches_branchId",
                        column: x => x.branchId,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_Customers_customerId",
                        column: x => x.customerId,
                        principalTable: "Customers",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_SalesOrders_salesOrderId",
                        column: x => x.salesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "salesOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_Warehouses_warehouseId",
                        column: x => x.warehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderLines",
                columns: table => new
                {
                    purchaseOrderLineId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    purchaseOrderId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    productId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    qty = table.Column<float>(type: "real", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    discountAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    totalAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderLines", x => x.purchaseOrderLineId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLines_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLines_PurchaseOrders_purchaseOrderId",
                        column: x => x.purchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "purchaseOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receivings",
                columns: table => new
                {
                    receivingId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    purchaseOrderId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    receivingNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    receivingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    vendorId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    vendorDO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    vendorInvoice = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    branchId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    warehouseId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasChild = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivings", x => x.receivingId);
                    table.ForeignKey(
                        name: "FK_Receivings_Branches_branchId",
                        column: x => x.branchId,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receivings_PurchaseOrders_purchaseOrderId",
                        column: x => x.purchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "purchaseOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receivings_Vendors_vendorId",
                        column: x => x.vendorId,
                        principalTable: "Vendors",
                        principalColumn: "vendorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receivings_Warehouses_warehouseId",
                        column: x => x.warehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferIns",
                columns: table => new
                {
                    transferInId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    transferOrderId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    transferInNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    transferInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    branchIdFrom = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    warehouseIdFrom = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    branchIdTo = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    warehouseIdTo = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasChild = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferIns", x => x.transferInId);
                    table.ForeignKey(
                        name: "FK_TransferIns_Branches_branchIdFrom",
                        column: x => x.branchIdFrom,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferIns_Branches_branchIdTo",
                        column: x => x.branchIdTo,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferIns_TransferOrders_transferOrderId",
                        column: x => x.transferOrderId,
                        principalTable: "TransferOrders",
                        principalColumn: "transferOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferIns_Warehouses_warehouseIdFrom",
                        column: x => x.warehouseIdFrom,
                        principalTable: "Warehouses",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferIns_Warehouses_warehouseIdTo",
                        column: x => x.warehouseIdTo,
                        principalTable: "Warehouses",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferOrderLines",
                columns: table => new
                {
                    transferOrderLineId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    transferOrderId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    productId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    qty = table.Column<float>(type: "real", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferOrderLines", x => x.transferOrderLineId);
                    table.ForeignKey(
                        name: "FK_TransferOrderLines_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferOrderLines_TransferOrders_transferOrderId",
                        column: x => x.transferOrderId,
                        principalTable: "TransferOrders",
                        principalColumn: "transferOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferOuts",
                columns: table => new
                {
                    transferOutId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    transferOrderId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    transferOutNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    transferOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    branchIdFrom = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    warehouseIdFrom = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    branchIdTo = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    warehouseIdTo = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasChild = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferOuts", x => x.transferOutId);
                    table.ForeignKey(
                        name: "FK_TransferOuts_Branches_branchIdFrom",
                        column: x => x.branchIdFrom,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOuts_Branches_branchIdTo",
                        column: x => x.branchIdTo,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOuts_TransferOrders_transferOrderId",
                        column: x => x.transferOrderId,
                        principalTable: "TransferOrders",
                        principalColumn: "transferOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferOuts_Warehouses_warehouseIdFrom",
                        column: x => x.warehouseIdFrom,
                        principalTable: "Warehouses",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOuts_Warehouses_warehouseIdTo",
                        column: x => x.warehouseIdTo,
                        principalTable: "Warehouses",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentLines",
                columns: table => new
                {
                    shipmentLineId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    shipmentId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    branchId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    warehouseId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    productId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    qty = table.Column<float>(type: "real", nullable: false),
                    qtyShipment = table.Column<float>(type: "real", nullable: false),
                    qtyInventory = table.Column<float>(type: "real", nullable: false),
                    shipmentId1 = table.Column<string>(type: "nvarchar(38)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentLines", x => x.shipmentLineId);
                    table.ForeignKey(
                        name: "FK_ShipmentLines_Branches_branchId",
                        column: x => x.branchId,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShipmentLines_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShipmentLines_Shipments_shipmentId",
                        column: x => x.shipmentId,
                        principalTable: "Shipments",
                        principalColumn: "shipmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipmentLines_Shipments_shipmentId1",
                        column: x => x.shipmentId1,
                        principalTable: "Shipments",
                        principalColumn: "shipmentId");
                    table.ForeignKey(
                        name: "FK_ShipmentLines_Warehouses_warehouseId",
                        column: x => x.warehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceivingLines",
                columns: table => new
                {
                    receivingLineId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    receivingId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    branchId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    warehouseId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    productId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    qty = table.Column<float>(type: "real", nullable: false),
                    qtyReceive = table.Column<float>(type: "real", nullable: false),
                    qtyInventory = table.Column<float>(type: "real", nullable: false),
                    receivingId1 = table.Column<string>(type: "nvarchar(38)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivingLines", x => x.receivingLineId);
                    table.ForeignKey(
                        name: "FK_ReceivingLines_Branches_branchId",
                        column: x => x.branchId,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivingLines_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivingLines_Receivings_receivingId",
                        column: x => x.receivingId,
                        principalTable: "Receivings",
                        principalColumn: "receivingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceivingLines_Receivings_receivingId1",
                        column: x => x.receivingId1,
                        principalTable: "Receivings",
                        principalColumn: "receivingId");
                    table.ForeignKey(
                        name: "FK_ReceivingLines_Warehouses_warehouseId",
                        column: x => x.warehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferInLines",
                columns: table => new
                {
                    transferInLineId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    transferInId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    productId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    qty = table.Column<float>(type: "real", nullable: false),
                    qtyInventory = table.Column<float>(type: "real", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferInLines", x => x.transferInLineId);
                    table.ForeignKey(
                        name: "FK_TransferInLines_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferInLines_TransferIns_transferInId",
                        column: x => x.transferInId,
                        principalTable: "TransferIns",
                        principalColumn: "transferInId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferOutLines",
                columns: table => new
                {
                    transferOutLineId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    transferOutId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    productId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    qty = table.Column<float>(type: "real", nullable: false),
                    qtyInventory = table.Column<float>(type: "real", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferOutLines", x => x.transferOutLineId);
                    table.ForeignKey(
                        name: "FK_TransferOutLines_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferOutLines_TransferOuts_transferOutId",
                        column: x => x.transferOutId,
                        principalTable: "TransferOuts",
                        principalColumn: "transferOutId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "branchId", "branchName", "city", "country", "createdAt", "description", "isDefaultBranch", "province", "street1", "street2" },
                values: new object[] { "00000000-0000-0000-0000-000000000001", "Main Branch", "Hanoi", "VN", new DateTime(2025, 11, 1, 7, 51, 0, 853, DateTimeKind.Utc).AddTicks(8390), "Default branch", true, "HN", "123 Main St", "" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "customerId", "HasChild", "city", "country", "createdAt", "customerName", "description", "province", "size", "street1", "street2" },
                values: new object[] { "00000000-0000-0000-0000-000000000001", "No", "Hanoi", "VN", new DateTime(2025, 11, 1, 7, 51, 0, 853, DateTimeKind.Utc).AddTicks(8573), "John Customer", "Default customer", "HN", 1, "34 Customer St", "" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "productId", "barcode", "createdAt", "description", "productCode", "productName", "productType", "serialNumber", "uom" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000001", "893000000001", new DateTime(2025, 11, 1, 7, 51, 0, 853, DateTimeKind.Utc).AddTicks(8536), "Demo item", "P-001", "Sample Product 1", 3, "SN-001", 1 },
                    { "00000000-0000-0000-0000-000000000002", "893000000002", new DateTime(2025, 11, 1, 7, 51, 0, 853, DateTimeKind.Utc).AddTicks(8539), "Demo item", "P-002", "Sample Product 2", 2, "SN-002", 7 }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "vendorId", "HasChild", "city", "country", "createdAt", "description", "province", "size", "street1", "street2", "vendorName" },
                values: new object[] { "00000000-0000-0000-0000-000000000001", "No", "Hanoi", "VN", new DateTime(2025, 11, 1, 7, 51, 0, 853, DateTimeKind.Utc).AddTicks(8557), "Default vendor", "HN", 1, "12 Supplier Rd", "", "Acme Supplier" });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "warehouseId", "branchId", "city", "country", "createdAt", "description", "province", "street1", "street2", "warehouseName" },
                values: new object[] { "00000000-0000-0000-0000-000000000001", "00000000-0000-0000-0000-000000000001", "Hanoi", "VN", new DateTime(2025, 11, 1, 7, 51, 0, 853, DateTimeKind.Utc).AddTicks(8523), "Main warehouse", "HN", "Zone A", "", "Central WH" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLines_customerId",
                table: "CustomerLines",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLines_productId",
                table: "PurchaseOrderLines",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLines_purchaseOrderId",
                table: "PurchaseOrderLines",
                column: "purchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_branchId",
                table: "PurchaseOrders",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_vendorId",
                table: "PurchaseOrders",
                column: "vendorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingLines_branchId",
                table: "ReceivingLines",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingLines_productId",
                table: "ReceivingLines",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingLines_receivingId",
                table: "ReceivingLines",
                column: "receivingId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingLines_receivingId1",
                table: "ReceivingLines",
                column: "receivingId1");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingLines_warehouseId",
                table: "ReceivingLines",
                column: "warehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Receivings_branchId",
                table: "Receivings",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_Receivings_purchaseOrderId",
                table: "Receivings",
                column: "purchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Receivings_vendorId",
                table: "Receivings",
                column: "vendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Receivings_warehouseId",
                table: "Receivings",
                column: "warehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLines_productId",
                table: "SalesOrderLines",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLines_salesOrderId",
                table: "SalesOrderLines",
                column: "salesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_branchId",
                table: "SalesOrders",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_customerId",
                table: "SalesOrders",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentLines_branchId",
                table: "ShipmentLines",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentLines_productId",
                table: "ShipmentLines",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentLines_shipmentId",
                table: "ShipmentLines",
                column: "shipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentLines_shipmentId1",
                table: "ShipmentLines",
                column: "shipmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentLines_warehouseId",
                table: "ShipmentLines",
                column: "warehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_branchId",
                table: "Shipments",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_customerId",
                table: "Shipments",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_salesOrderId",
                table: "Shipments",
                column: "salesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_warehouseId",
                table: "Shipments",
                column: "warehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferInLines_productId",
                table: "TransferInLines",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferInLines_transferInId",
                table: "TransferInLines",
                column: "transferInId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferIns_branchIdFrom",
                table: "TransferIns",
                column: "branchIdFrom");

            migrationBuilder.CreateIndex(
                name: "IX_TransferIns_branchIdTo",
                table: "TransferIns",
                column: "branchIdTo");

            migrationBuilder.CreateIndex(
                name: "IX_TransferIns_transferOrderId",
                table: "TransferIns",
                column: "transferOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferIns_warehouseIdFrom",
                table: "TransferIns",
                column: "warehouseIdFrom");

            migrationBuilder.CreateIndex(
                name: "IX_TransferIns_warehouseIdTo",
                table: "TransferIns",
                column: "warehouseIdTo");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrderLines_productId",
                table: "TransferOrderLines",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrderLines_transferOrderId",
                table: "TransferOrderLines",
                column: "transferOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrders_branchIdFrom",
                table: "TransferOrders",
                column: "branchIdFrom");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrders_branchIdTo",
                table: "TransferOrders",
                column: "branchIdTo");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrders_warehouseIdFrom",
                table: "TransferOrders",
                column: "warehouseIdFrom");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrders_warehouseIdTo",
                table: "TransferOrders",
                column: "warehouseIdTo");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOutLines_productId",
                table: "TransferOutLines",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOutLines_transferOutId",
                table: "TransferOutLines",
                column: "transferOutId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOuts_branchIdFrom",
                table: "TransferOuts",
                column: "branchIdFrom");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOuts_branchIdTo",
                table: "TransferOuts",
                column: "branchIdTo");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOuts_transferOrderId",
                table: "TransferOuts",
                column: "transferOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOuts_warehouseIdFrom",
                table: "TransferOuts",
                column: "warehouseIdFrom");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOuts_warehouseIdTo",
                table: "TransferOuts",
                column: "warehouseIdTo");

            migrationBuilder.CreateIndex(
                name: "IX_VendorLines_vendorId",
                table: "VendorLines",
                column: "vendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_branchId",
                table: "Warehouses",
                column: "branchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CustomerLines");

            migrationBuilder.DropTable(
                name: "PurchaseOrderLines");

            migrationBuilder.DropTable(
                name: "ReceivingLines");

            migrationBuilder.DropTable(
                name: "SalesOrderLines");

            migrationBuilder.DropTable(
                name: "ShipmentLines");

            migrationBuilder.DropTable(
                name: "TransferInLines");

            migrationBuilder.DropTable(
                name: "TransferOrderLines");

            migrationBuilder.DropTable(
                name: "TransferOutLines");

            migrationBuilder.DropTable(
                name: "VendorLines");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Receivings");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "TransferIns");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "TransferOuts");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "SalesOrders");

            migrationBuilder.DropTable(
                name: "TransferOrders");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Branches");
        }
    }
}
