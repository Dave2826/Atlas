# Base de Datos

## Motor

- **Sistema**: PostgreSQL
- **Proveedor .NET**: Npgsql.EntityFrameworkCore.PostgreSQL 10.0.2
- **Cadena de conexión**: Definida en `appsettings.json` y `AtlasDesignTimeDbContextFactory.cs`

---

## ORM

Se utiliza **Entity Framework Core 10.0.9** con las siguientes características:

- **Code-First**: Las entidades definen el esquema de base de datos
- **Migraciones**: Evolución del esquema mediante migraciones incrementales
- **Fluent API**: Configuración de entidades mediante `IEntityTypeConfiguration<T>`
- **Auto-descubrimiento**: Las configuraciones se registran automáticamente con `ApplyConfigurationsFromAssembly`

---

## Migraciones

| Migración | Fecha | Cambios |
|---|---|---|
| `InitialCreate` | 2026-06-12 | Creación de todas las tablas iniciales |
| `AddAuthSeedData` | 2026-06-15 | Seed de Roles (Administrator, Employee) y usuario admin |
| `AddProductSkuAndColor` | 2026-06-17 | Columnas SKU (único) y Color en Products |

---

## Convenciones de Base de Datos

- **Nombres de tablas**: Plural en inglés (Products, Departments, Users, Roles)
- **Nombres de columnas**: PascalCase coincidiendo con las propiedades de la entidad
- **Llaves primarias**: `{EntityName}Id` (int, IdentityByDefaultColumn)
- **Llaves foráneas**: `{RelatedEntityName}Id`
- **DeleteBehavior predeterminado**: `Restrict`
- **Valores por defecto**: Configurados vía Fluent API (`HasDefaultValue`)

---

## Configuraciones por Entidad

Actualmente existen 9 archivos de configuración en `Data/Configurations/`:

| Configuración | Entidad | Aspectos principales |
|---|---|---|
| `DepartmentConfiguration` | Department | PK, Name(100), Description(500), IsActive default true, Restrict con Product |
| `ProductTypeConfiguration` | ProductType | PK, Name(100), Description(500), IsActive default true, Restrict con Product |
| `ProductConfiguration` | Product | PK, Name(200), BrandName(200), SKU único(50), Description(1000) |
| `UserConfiguration` | User | PK, Username(100), PasswordHash(500), IsActive default true, Restrict con Role |
| `RoleConfiguration` | Role | PK, Name(100), seed data |
| `CustomerConfiguration` | Customer | PK, FirstName(150), LastName(150), Phone(50) |
| `VoucherConfiguration` | Voucher | PK, Restrict con Customer |
| `LayawayConfiguration` | Layaway | PK, Restrict con Customer |
| `ProductSizeStockConfiguration` | ProductSizeStock | PK, Restrict con Product |

---

## Relaciones Principales

```
Department 1 ──< N Product
ProductType 1 ──< N Product
Product 1 ──< N ProductSizeStock
Customer 1 ──< N Layaway
Customer 1 ──< N Voucher
Customer 1 ──< N Sale
Customer 1 ──< N SpecialOrder
Role 1 ──< N User
User 1 ──< N CashSession
CashSession 1 ──< N Expense
Layaway 1 ──< N LayawayItem
Layaway 1 ──< N LayawayPayment
LayawayItem 1 ──< N LayawayPayment
LayawayItem 1 ──< N SpecialOrder
Voucher 1 ──< N VoucherTransaction
Sale 1 ──< N SaleDetail
Sale 1 ──< N SalePayment
```

Todas las relaciones `1 ──< N` utilizan `DeleteBehavior.Restrict` excepto donde se indica explícitamente lo contrario en la migración.
